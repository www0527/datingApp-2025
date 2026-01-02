import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ToastService } from '../services/toast-service';
import { NavigationExtras, Router } from '@angular/router';

// 錯誤攔截器
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const toast = inject(ToastService);
    const router = inject(Router);


    return next(req).pipe(
        catchError((error) => {
            if (!error) throw error;

            switch (error.status) {
                case 400:
                    if (!error.error.errors) toast.error(error.error);

                    const modelStateErrors = [];

                    for (const key in error.error.errors) {
                        if (error.error.errors[key]) {
                            modelStateErrors.push(error.error.errors[key]);
                        }
                    }
                    throw modelStateErrors.flat()

                case 401:
                    toast.error('未授權，請重新登入');
                    break;
                case 404:
                    router.navigateByUrl('/not-found');
                    break;
                case 500:
                    const navigationExtras: NavigationExtras = {
                        state: { error: error.error }
                    };
                    router.navigateByUrl('/server-error', navigationExtras);
                    break;
                default:
                    toast.error('發生未知錯誤');
                    break;
            }
            throw error;
        })
    );
};
