import { Injectable } from '@angular/core';

/**
 * Toast 通知服務 (ToastService)
 * 提供全域訊息通知功能，支援 success、error、warning、info 四種類型。
 * 
 * @remarks
 * 此服務在 root 層級提供，初始化時會自動在 DOM 中建立 toast 容器。
 * Toast 訊息會在指定時間後自動移除，也可由使用者手動關閉。
 */
@Injectable({
    providedIn: 'root',
})
export class ToastService {

    constructor() {
        this.createToastContainer();
    }

    /**
     * 建立 Toast 容器元素並加入至 DOM。
     * 
     * @remarks
     * 若容器已存在則不會重複建立。
     * 容器使用 DaisyUI 的 toast 樣式，定位於畫面右下角。
     */
    private createToastContainer() {
        if (!document.getElementById('toast-container')) {
            const container = document.createElement('div');
            container.id = 'toast-container';
            container.className = 'toast toast-bottom toast-end';
            document.body.appendChild(container);
        }
    }

    /**
     * 建立單一 Toast 訊息元素並加入至容器。
     * 
     * @param message - 要顯示的訊息內容
     * @param alertClass - DaisyUI alert 樣式類別（如 alert-success、alert-error）
     * @param duration - 訊息顯示時間（毫秒），預設 5000ms
     * 
     * @remarks
     * 每個 toast 都包含關閉按鈕，使用者可手動關閉。
     * 若超過 duration 時間，toast 會自動移除。
     */
    private createToastElement(message: string, alertClass: string, duration: number = 5000) {
        const toastContainer = document.getElementById('toast-container');
        if (!toastContainer) return;

        const toast = document.createElement('div');
        toast.classList.add('alert', alertClass, 'shadow-lg', 'mb-4', 'fade-in');
        toast.innerHTML = `
      <span>${message}</span>
      <button class="btn btn-sm btn-ghost ml-4">✕</button>
    `;

        toast.querySelector('button')?.addEventListener('click', () => {
            toastContainer.removeChild(toast);
        });

        toastContainer.appendChild(toast);

        setTimeout(() => {
            if (!toastContainer.contains(toast)) return;
            toastContainer.removeChild(toast);
        }, duration);
    }

    /**
     * 顯示成功訊息 (綠色)。
     * 
     * @param message - 要顯示的訊息內容
     * @param duration - 訊息顯示時間（毫秒），預設 5000ms
     */
    success(message: string, duration: number = 5000) {
        this.createToastElement(message, 'alert-success', duration);
    }

    /**
     * 顯示錯誤訊息 (紅色)。
     * 
     * @param message - 要顯示的訊息內容
     * @param duration - 訊息顯示時間（毫秒），預設 5000ms
     */
    error(message: string, duration: number = 5000) {
        this.createToastElement(message, 'alert-error', duration);
    }

    /**
     * 顯示警告訊息 (黃色)。
     * 
     * @param message - 要顯示的訊息內容
     * @param duration - 訊息顯示時間（毫秒），預設 5000ms
     */
    warning(message: string, duration: number = 5000) {
        this.createToastElement(message, 'alert-warning', duration);
    }

    /**
     * 顯示資訊訊息 (藍色)。
     * 
     * @param message - 要顯示的訊息內容
     * @param duration - 訊息顯示時間（毫秒），預設 5000ms
     */
    info(message: string, duration: number = 5000) {
        this.createToastElement(message, 'alert-info', duration);
    }
}