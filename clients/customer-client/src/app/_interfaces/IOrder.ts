import { IMenuItemRow } from "./IMenuItem";

export interface ICreateOrder {
    restaurantId: number;
    tableId: number;
    note?: string;
    menuItemIds: number[];
}

export interface IOrder {
    menuItems: IMenuItemRow[];
    totalItems: number;
    totalPrice: number;
}