/**
 * 使用者類型定義。
 */
type UserInfo = {
    id: string;
    displayName: string;
    email: string;
    imageUrl?: string;
    token: string;
}

/**
 * 登入資料類型定義。
 */
type LoginData = {
    email: string;
    password: string;
}

/**
 * 註冊資料類型定義。
 */
type RegisterData = {
    email: string;
    password: string;
    displayName: string;
}

export type { UserInfo, LoginData, RegisterData };
