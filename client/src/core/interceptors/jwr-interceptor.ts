import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../services/account-service';

export const jwrInterceptor: HttpInterceptorFn = (req, next) => {
    const accountService = inject(AccountService);

    const user = accountService.currentUser();     // 取得目前使用者(已經失去響應性)

    if (user){
        req = req.clone({
            setHeaders:{
                Authorization: `Bearer ${user.token}`
            }
        });
    }

    return next(req);
};
