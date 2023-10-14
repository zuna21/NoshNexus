export interface IMenuCard {
    id: string;
    name: string;
    description: string;
    menuItemNumber: number;
    restaurantName: string;
}

export interface IMenuDetails {
    id: string;
    name: string;
    description: string;
    restaurantImage: string;
    menuItems: IMenuItemCard[];
}

export interface IMenuItemCard {
    id: string;
    name: string;
    description: string;
    price: number;
    image: string;
    active: boolean;
    specialOffer: boolean;
    specialOfferPrice: number;
}