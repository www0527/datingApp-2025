import { ApplicationConfig, inject, provideAppInitializer, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
// import { InitService } from '../core/services/init-service';
import { lastValueFrom } from 'rxjs';
import { InitService } from '../core/services/init-service';
import { errorInterceptor } from '../core/interceptors/error-interceptor';
import { jwrInterceptor } from '../core/interceptors/jwr-interceptor';

/**
 * <summary>
 * 應用程式的全域設定檔，包含路由、HTTP 拦截器、錯誤監聽、初始化流程等。
 * </summary>
 * <remarks>
 * - 提供全域錯誤監聽與無 Zone 變更偵測。
 * - 設定路由與視圖轉場效果。
 * - 註冊 HTTP 拦截器（errorInterceptor, jwrInterceptor）。
 * - 使用 AppInitializer 於啟動時執行 InitService，並移除初始載入畫面。
 * </remarks>
 */
export const appConfig: ApplicationConfig = {
    providers: [
        /**
         * <summary>
         * 註冊全域瀏覽器錯誤監聽器，捕捉未處理的錯誤並統一處理。
         * </summary>
         */
        provideBrowserGlobalErrorListeners(),

        /**
         * <summary>
         * 啟用無 Zone 的變更偵測機制，提升效能並減少不必要的檢查。
         * </summary>
         */
        provideZonelessChangeDetection(),

        /**
         * <summary>
         * 設定應用程式路由，並啟用視圖轉場動畫效果。
         * </summary>
         */
        provideRouter(routes, withViewTransitions()),

        /**
         * <summary>
         * 註冊 HTTP 拦截器，統一處理 API 錯誤與 JWT 驗證。
         * </summary>
         * <param name="errorInterceptor">API 錯誤攔截器，處理 HTTP 錯誤。</param>
         * <param name="jwrInterceptor">JWT 權杖攔截器，於請求中加入驗證資訊。</param>
         */
        provideHttpClient(withInterceptors([errorInterceptor, jwrInterceptor])),

        /**
         * <summary>
         * 註冊應用程式初始化器，啟動時執行 InitService 初始化流程，並移除初始載入畫面。
         * </summary>
         */
        provideAppInitializer(async () => {
            const initService = inject(InitService);
            return new Promise<void>(async (resolve) => {
                setTimeout(async () => {
                    try {
                        await lastValueFrom(initService.init());
                    } finally {
                        const splash = document.getElementById('initial-splash');
                        if (splash) {
                            splash.remove();
                        }
                        resolve()
                    }
                }, 1000);
            });
        })
    ]
};
