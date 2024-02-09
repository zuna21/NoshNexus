export interface IMenuItemCard {
    id: number;
    restaurantId: number;
    menu: IMenuItemMenu;
    name: string;
    description: string;
    price: number;
    hasSpecialOffer: boolean;
    isFavourite: boolean;
    specialOfferPrice: number;
    profileImage: string;
    images: string[];
}

export interface IMenuItemMenu {
    id: number;
    name: string;
}