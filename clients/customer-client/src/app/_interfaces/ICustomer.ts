export interface ICustomer {
    username: string;
    token: string;
}

export interface IRegisterCustomer {
    username: string;
    password: string;
    repeatPassword: string;
}

export interface ILoginCustomer {
    username: string;
    password: string;
}