export interface IMenuItemRow {
    id: number;
    menu: {
        id: number;
        name: string;
    }
    name: string;
    description: string;
    price: number;
    hasSpecialOffer: boolean;
    specialOfferPrice: number;
    profileImage: string;
    images: string[];
}