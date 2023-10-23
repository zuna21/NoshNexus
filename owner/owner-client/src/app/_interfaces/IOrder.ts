export interface IOrderCard {
  id: string;
  user: IOrderCardUser;
  tableName: string;
  description: string;
  totalPrice: number;
  totalItems: number;
  items: IOrderMenuItem[];
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
  description: string;
  profileImage: string;
  price: number;
  quantity: number;
}