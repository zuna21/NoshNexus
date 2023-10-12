export interface IEmployeeCreate {
    username: string;
    firstName: string;
    lastName: string;
    password: string;
    restaurantId: string;   
    email: string;
    phone: string;
    city: string;
    address: string;
    description: string;
    birth: Date;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean;
}

export interface IEmployeeCard {
    id: string;
    firstName: string;
    lastName: string;
    username: string;
    description: string;
    profileImage: string;
    restaurant: {
        id: string;
        name: string;
        profileImage: string;
    }
}

export interface IEmployeeDetails {
    id: string;
    username: string;
    firstName: string;
    lastName: string;
    profileImage: string;
    description: string;
    restaurant: {
        id: string;
        name: string;
        profileImage: string;
    };
    email: string;
    phone: string;
    birth: Date;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean;
}