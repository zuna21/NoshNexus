export interface ICreateEmployee {
    username: string;
    firstName: string;
    lastName: string;
    password: string;
    restaurantId: number;   
    email: string;
    phoneNumber: string;
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

export interface IEmployeeEdit {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    username: string;
    phone: string;
    city: string;
    address: string;
    ownerRestaurants: {
        id: string;
        name: string;
    }[];
    employeeRestaurant: {
        id: string;
        name: string;
    };
    birth: Date;
    description: string;
    canEditMenus: boolean;
    canViewFolders: boolean;
    canEditFolders: boolean;
    profileImage: {
      id: string;
      url: string;
      size: number;  
    };
}