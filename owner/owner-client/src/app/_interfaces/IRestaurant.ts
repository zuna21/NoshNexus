export interface IRestaurantCreate {
    name: string;
    country: string;
    postalCode: number;
    phoneNumber: string;
    city: string;
    address: string;
    description: string;
    facebookUrl: string;
    instagramUrl: string;
    websiteUrl: string;
}

export interface IRestaurantCard {
    id: string;
    profileImage: string;
    name: string;
    isOpen: boolean;
    country: string;
    city: string;
    address: string;
}