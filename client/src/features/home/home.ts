import { Component, input, Input, signal } from '@angular/core';
import { Register } from "../account/register/register";
import { UserInfo } from '../../type/User';


/**
 * 首頁元件。
 */
@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {

  /**
   * 註冊模式訊號。
   */
  protected registerMode = signal(false);

  /**
   * 顯示註冊模式。
   */
  showRegister(value: boolean) {
    this.registerMode.set(value);
  }
}
