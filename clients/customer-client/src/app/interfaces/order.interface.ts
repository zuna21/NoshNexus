import { IMenuItemCard } from "./menu-item.interface";

export interface ICreateOrder {
    tableId: number;
    note: string;
    menuItemIds: number[];
}

export interface IOrder {
    tableId: number;
    note: string;
    menuItems: IMenuItemCard[];
    totalMenuItems: number;
    totalPrice: number;
}