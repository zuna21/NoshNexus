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

export interface IEditMenuItem {
    name: string;
    price: number;
    description: string;
    isActive: boolean;
    hasSpecialOffer: boolean;
    specialOfferPrice: number;
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
    id: number;
    name: string;
    description: string;
    restaurantImage: string;
    menuItems: IMenuItemCard[];
}

export interface ICreateMenuItem {
    name: string;
    price: number;
    description: string;
    isActive: boolean;
    hasSpecialOffer: boolean;
    specialOfferPrice: number;
}

export interface IMenuItemCard {
    id: number;
    name: string;
    description: string;
    price: number;
    image: string;
    isActive: boolean;
    hasSpecialOffer: boolean;
    specialOfferPrice: number;
}

export interface IGetMenuItem {
    id: string;
    name: string;
    image: string;
    price: number;
    isActive: boolean;
    hasSpecialOffer: boolean;
    specialOfferPrice: number;
    description: string;
    todayOrders: number;
}

export interface IGetMenuItemEdit {
    id: string;
    name: string;
    price: number;
    description: string;
    isActive: boolean;
    hasSpecialOffer: boolean;
    specialOfferPrice: number;
    profileImage: {
        id: string;
        url: string;
        size: number;
    }
}
