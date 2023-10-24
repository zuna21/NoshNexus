export interface IOrderCard {
  id: string;
  user: IOrderCardUser;
  restaurant: IOrderRestaurant;
  tableName: string;
  description: string;
  totalPrice: number;
  totalItems: number;
  items: IOrderMenuItem[];
  status: string;
  declineReason: string;
  createdAt: Date;
}

export interface IOrderCardUser {
  id: string;
  username: string;
  profileImage: string;
  firstName: string;
  lastName: string;
}

export interface IOrderMenuItem {
  id: string;
  name: string;
  price: number;
}

export interface IOrderRestaurant {
  id: string;
  name: string;
}