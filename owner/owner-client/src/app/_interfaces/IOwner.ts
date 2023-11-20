import { ICountry } from "./ICountry";

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