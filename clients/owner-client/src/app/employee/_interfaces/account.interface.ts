import { ICountry } from "src/app/_interfaces/ICountry";
import { IImageCard } from "src/app/_interfaces/IImage";
import { IProfileHeader } from "src/app/_interfaces/IProfileHeader";

export interface IGetAccountDetails {
    id: number;
    accountHeader: IProfileHeader;
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
    restaurant: string;
}

export interface IGetAccountEdit {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    city: string;
    countryId: number;
    address: string;
    description: string;
    birth: Date;
    phoneNumber: string;
    email: string;
    profileImage: IImageCard;
    allCountries: ICountry[];
}

export interface IEditAccount {
    username: string;
    firstName: string;
    lastName: string;
    city: string;
    countryId: number;
    address: string;
    description: string;
    birth: Date;
    phoneNumber: string;
    email: string;
}