import { IImageCard } from "./IImage";
import { IProfileHeader } from "./IProfileHeader";

export interface ICreateEmployee {
    username: string;
    firstName: string;
    lastName: string;
    password: string;
    restaurantId: number;   
    email: string;
    phoneNumber: string;
    city: string;
    address: string;
    description: string;
    birth: Date;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean;
}

export interface IEditEmployee {
    firstName: string;
    lastName: string;
    email: string;
    username: string;
    password: string | null;
    phoneNumber: string;
    city: string;
    address: string;
    restaurantId: number;
    birth: Date;
    description: string;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean;
}

export interface IEmployeeCard {
    id: string;
    firstName: string;
    lastName: string;
    username: string;
    description: string;
    profileImage: string;
    restaurant: {
        id: string;
        name: string;
        profileImage: string;
    }
}

export interface IEmployeeDetails {
    id: number;
    profileHeader: IProfileHeader;
    description: string;
    restaurant: string;
    email: string;
    phoneNumber: string;
    birth: Date;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean;
}

export interface IGetEditEmployee {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    username: string;
    phoneNumber: string;
    city: string;
    address: string;
    ownerRestaurants: {
        id: number;
        name: string;
    }[];
    restaurantId: number;
    profileImage: IImageCard
    birth: Date;
    description: string;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean;
}