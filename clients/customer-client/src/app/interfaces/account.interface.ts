export interface IAccount {
    username: string;
    token: string;
}

export interface IActivateAccount {
    username: string;
    password: string;
    repeatPassword: string;
}

export interface ILogin {
    username: string;
    password: string;
}

export interface IGetAccountDetails {
    id: number;
    profileImage: string;
    username: string;
    firstName: string;
    lastName: string;
    description: string;
    country: string;
    isActivated: boolean;
    city: string;
    joined: Date;
}