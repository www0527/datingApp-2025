import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { catchError, lastValueFrom, take } from 'rxjs';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";
import { UserInfo } from '../type/User';

@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  protected readonly title = signal('Dating APP');
  private http = inject(HttpClient);

  // 在組件生命週期的初始化階段發出 HTTP GET 請求
  async ngOnInit() {
    this.setCurrentUser();
  }

  /**
   * 從 localStorage 中取得使用者資料並設定至 AccountService 的 currentUser。
   */
  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;

    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  /** 
   * 發出 HTTP GET 請求以取得所有會員資料 
   */
  async getMembers() {
    try {
      return lastValueFrom(this.http.get<UserInfo[]>('https://localhost:7287/api/members'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}
