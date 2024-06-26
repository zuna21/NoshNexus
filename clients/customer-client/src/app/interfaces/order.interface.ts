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

export interface IOrderCard {
    id: number;
    user: IOrderCardUser;
    restaurant: IOrderRestaurant;
    tableName: string;
    note: string;
    totalPrice: number;
    totalItems: number;
    items: IOrderMenuItem[];
    status: string;
    declineReason: string;
    createdAt: Date;
  }
  
  export interface IOrderCardUser {
    id: number;
    username: string;
    profileImage: string;
    firstName: string;
    lastName: string;
  }
  
  export interface IOrderMenuItem {
    id: number;
    name: string;
    price: number;
  }
  
  export interface IOrderRestaurant {
    id: number;
    name: string;
  }