import { IMenuItemRow } from "./IMenuItem";

export interface IMenuCard {
    id: number;
    name: string;
    description: string;
    menuItemNumber: number;
    restaurantName: string;
}

export interface IMenuDetails {
    id: number;
    name: string;
    description: string;
    restaurantImage: string;
    menuItems: IMenuItemRow[];
}