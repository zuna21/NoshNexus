export interface IOrderCard {
  id: string;
  user: IOrderCardUser;
  tableName: string;
  description: string;
  totalPrice: number;
  totalItems: number;
  createdAt: Date;
}

export interface IOrderCardUser {
  id: string;
  username: string;
  profileImage: string;
  firstName: string;
  lastName: string;
}
