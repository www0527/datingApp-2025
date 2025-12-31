import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterData, UserInfo } from '../../../type/User';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private accountService = inject(AccountService);

  cancelRegister = output<boolean>();
  protected registerData = {} as RegisterData;

  protected confirmPassword = '' as string;

  register() {
    this.accountService.register(this.registerData).subscribe({
      next: res => {
        console.log(res)
        this.cancel()
      },
      error: err => console.log(err)
    });
  }

  cancel(): void {
    this.cancelRegister.emit(false);
  }
}
