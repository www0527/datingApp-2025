import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Nav],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly title = signal('Dating APP');
  private http = inject(HttpClient);
  protected members = signal<any>([]);

  // 在組件生命週期的初始化階段發出 HTTP GET 請求
  async ngOnInit(){
    // 新寫法
    this.members.set( await this.fetchMembers() );
    
    // 舊寫法
    // this.http.get('https://localhost:7287/api/members').subscribe({
    //   next: response => this.members.set(response),        // 處理成功的回應
    //   error: error => console.log(error),               // 處理錯誤情況
      // complete: () => console.log('Request completed')  // 請求完成後的操作
    }
  
    async fetchMembers() {
    try {
      return lastValueFrom( this.http.get('https://localhost:7287/api/members'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

}
