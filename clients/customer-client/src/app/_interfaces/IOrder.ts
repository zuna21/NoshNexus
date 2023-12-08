import { IMenuItemRow } from "./IMenuItem";

export interface ICreateOrder {
    tableId: number;
    note?: string;
    menuItemIds: number[];
}

export interface IOrder {
    menuItems: IMenuItemRow[];
    totalItems: number;
    totalPrice: number;
}