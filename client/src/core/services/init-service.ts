import { inject, Injectable } from '@angular/core';
import { AccountService } from './account-service';
import { of } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class InitService {
    private accountService = inject(AccountService);
    
    /**
     * 從 localStorage 中取得使用者資料並設定至 AccountService 的 currentUser。
     */
    init() {
        const userString = localStorage.getItem('user');
        if (!userString) return of(null);
        const user = JSON.parse(userString);
        this.accountService.currentUser.set(user);

        return of(null);
    }
}
