import { ICountry } from './country.interface';
import { IImage } from './image.interface';

export interface IAccount {
  username: string;
  token: string;
  profileImage: string;
}

export interface IActivateAccount {
  username: string;
  password: string;
  repeatPassword: string;
}

export interface ILogin {
  username: string;
  password: string;
}

export interface IGetAccountDetails {
  id: number;
  profileImage: string;
  username: string;
  firstName: string;
  lastName: string;
  description: string;
  country: string;
  isActivated: boolean;
  city: string;
  joined: Date;
}

export interface IGetAccountEdit {
  id: number;
  profileImage: IImage;
  username: string;
  firstName: string;
  lastName: string;
  description: string;
  countryId: number;
  city: string;
  countries: ICountry[];
}

export interface IEditAccount {
  username: string;
  firstName: string;
  lastName: string;
  description: string;
  countryId: number;
  city: string;
}