import { IProfileHeader } from "./IProfileHeader";

export interface IAccount {
    id: string;
    profileHeader: IProfileHeader;
    username: string;
    firstName: string;
    lastname: string;
    description: string;
    email: string;
    country: string;
    city: string;
    address: string;
    phone: string;
    birth: Date;
    employeesNumber: number;
    restaurantsNumber: number;
    menusNumber: number;
    todayOrdersNumber: number;
}

export interface IAccountLogin {
    username: string;
    password: string;
}

export interface IUser {
    username: string;
    token: string;
}