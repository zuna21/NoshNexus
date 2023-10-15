export interface IMenuCard {
    id: string;
    name: string;
    description: string;
    menuItemNumber: number;
    restaurantName: string;
}

export interface IMenuEdit {
    id: string;
    name: string;
    description: string;
    restaurant: string;
    ownerRestaurants: {
        id: string;
        name: string;
    }[];
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

export interface IMenuItemDetails {
    id: string;
    name: string;
    image: string;
    price: number;
    active: boolean;
    specialOffer: boolean;
    specialOfferPrice: number;
    description: string;
    todayOrders: number;
}