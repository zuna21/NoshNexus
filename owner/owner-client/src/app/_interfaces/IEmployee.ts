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