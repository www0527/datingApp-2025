// ...existing code...
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { LoginData } from '../../type/User';
import { firstValueFrom } from 'rxjs';


/**
 * 導覽元件 (Nav)
 * 處理使用者登入/登出流程，並維護本地 UI 狀態。
 */
@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {
  protected accountService = inject(AccountService);
  
  /**
   * 綁定登入表單資料 (雙向綁定於模板)。
   * 初始值：空的 email 與 password。
   */
  protected loginData = signal<LoginData>({ email: '', password: '' });

  /**
   * 發出登入請求並訂閱回應。
   *
   * 行為：
   * - 成功：設定 isLogin 為 true、清空 loginData
   * - 失敗：顯示 alert（可改為更友善的錯誤處理）
   *
   * @remarks
   * 這個方法直接在元件內訂閱 Observable；若欲測試或管理取消訂閱，可改為回傳 Observable 並在呼叫處訂閱。
   */
  async login() {
    this.accountService.login(this.loginData()).subscribe({
      next: _ => {
        this.loginData.set({ email: '', password: '' });
      },
      error: err => alert(`Login failed: ${err.message}`),
    });
  }

  /**
   * 登出：重設本地狀態並通知 AccountService。
   */
  logout() {
    this.accountService.logout();
  }
}