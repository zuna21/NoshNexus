export interface IEmployee {
    id: number;
    restaurantImage: string;
    profileImage: string;
    username: string;
    firstName: string;
    lastName: string;
    description: string;
    city: string;
    birth: Date;
    country: string;
}

export interface IEmployeeCard {
    id: number;
    firstName: string;
    lastName: string;
    username: string;
    description: string;
    profileImage: string;
    restaurant: IEmployeeRestaurant;
}

export interface IEmployeeRestaurant {
    id: number;
    name: string;
    profileImage: string;
}