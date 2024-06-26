export interface IAccountLogin {
    username: string;
    password: string;
}

export interface IUser {
    username: string;
    token: string;
    profileImage: string;
    refreshToken: string;
}

export interface IRefreshToken {
    token: string;
    refreshToken: string;
}