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