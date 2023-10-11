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
    birth: string;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean
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