export interface IMenuCard {
    id: string;
    name: string;
    isActive: boolean;
    description: string;
    menuItemNumber: number;
    restaurantName: string;
}

export interface IEditMenu {
    name: string;
    description: string;
    isActive: boolean;
    restaurantId: number;
}

export interface ICreateMenu {
    name: string;
    description: string;
    restaurantId: number;
    isActive: boolean;
}

export interface IGetMenuEdit {
    id: string;
    name: string;
    description: string;
    restaurantId: number;
    isActive: boolean;
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

export interface IMenuItemEdit {
    id: string;
    name: string;
    price: number;
    description: string;
    active: boolean;
    specialOffer: boolean;
    specialOfferPrice: number;
    image: {
        id: string;
        url: string;
        size: number;
    }
}