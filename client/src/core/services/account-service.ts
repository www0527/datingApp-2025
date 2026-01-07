import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { LoginData, RegisterData, UserInfo } from '../../type/User';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';

/** AccountService 管理目前使用者狀態與登入登出。*/
@Injectable({
    providedIn: 'root',
})
export class AccountService {
    /** HttpClient 實例 */
    protected http = inject(HttpClient);

    /** API 根網址 */
    private baseUrl = environment.apiUrl;

    /** 目前使用者，若未登入為 null */
    currentUser = signal<UserInfo | null>(null);

    register(registerData: RegisterData) {
        return this.http.post<UserInfo>(`${this.baseUrl}/account/register`, registerData).pipe(
            tap(user => {
                if (user) {
                    this.setCurrentUser(user);
                }
            })
        );
    }

    /**
     * 登入並設定 localStorage 內的 user。
     * @param loginData 登入資料
     * @returns Observable<User>
     */
    login(loginData: LoginData) {
        return this.http.post<UserInfo>(`${this.baseUrl}/account/login`, loginData).pipe(
            tap(user => {
                if (user) {
                    this.setCurrentUser(user);
                }
            })
        );
    }

    setCurrentUser(user: UserInfo | null) {
        localStorage.setItem('user', JSON.stringify(user),)
        this.currentUser.set(user)
    }

    /** 登出並清除 localStorage 內的 user */
    logout() {
        localStorage.removeItem('user')
        this.currentUser.set(null);
    }
}
