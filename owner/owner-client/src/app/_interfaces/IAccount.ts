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

export interface IAccountEdit {
    id: string;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    birth: Date;
    country: string;
    city: string;
    address: string;
    description: string;
    profileImage: {
        id: string;
        url: string;
        size: number;
    }
}

export interface IAccountLogin {
    username: string;
    password: string;
}