export interface IRestaurant {
    id: number;
    name: string;
    country: string;
    city: string;
    address: string;
    postalCode: number;
    phoneNumber: string;
    description: string;
    facebookUrl: string;
    instagramUrl: string;
    websiteUrl: string;
    isOpen: boolean;
    restaurantImages: string[];
    employeesNumber: number;
    menusNumber: number;
    isFavourite: boolean;
}