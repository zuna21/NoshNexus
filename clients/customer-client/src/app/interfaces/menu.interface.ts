export interface IMenuCard {
    id: number;
    name: string;
    description: string;
    menuItemNumber: number;
    restaurantName: string;
}

export interface IMenu {
    id: number;
    restaurant: IMenuRestaurant;
    totalMenuItems: number;
    description: string;
}

export interface IMenuRestaurant {
    id: number;
    name: string;
}