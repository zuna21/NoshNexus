export interface IRestaurantCard {
    id: number;
    profileImage: string;
    name: string;
    isOpen: boolean;
    country: string;
    city: string;
    address: string;
  }

  export interface IRestaurantDetails {
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
    restaurantImages: any[];
    employeesNumber: number;
    menusNumber: number;
  }
  