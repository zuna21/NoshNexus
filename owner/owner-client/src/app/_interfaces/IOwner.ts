import { ICountry } from "./ICountry";
import { IProfileHeader } from "./IProfileHeader";

export interface IGetOwner {
    id: number;
    profileHeader: IProfileHeader;
    username: string;
    firstName: string;
    lastName: string;
    description: string;
    email: string;
    country: string;
    city: string;
    address: string;
    phoneNumber: string;
    birth: Date;
    employeesNumber: number;
    restaurantsNumber: number;
    menusNumber: number;
    todayOrdersNumber: number;
}

export interface IGetOwnerEdit {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    birth: Date;
    countryId: number;
    city: string;
    address: string;
    description: string;
    allCountries: ICountry[];
}

export interface IEditOwner {
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    birth: Date;
    countryId: number;
    city: string;
    address: string;
    description: string;
}