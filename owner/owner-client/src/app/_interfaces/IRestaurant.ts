import { ICountry } from './ICountry';
import { ICurrency } from './ICurrency';
import { IImageCard } from './IImage';

export interface IRestaurantCreate {
  name: string;
  country: string;
  postalCode: number;
  phone: string;
  city: string;
  address: string;
  description: string;
  facebookUrl: string;
  instagramUrl: string;
  websiteUrl: string;
  isActive: boolean;
}

export interface IGetRestaurantCreate {
  countries: ICountry[];
  currencies: ICurrency[];
}


export interface IRestaurantEdit {
  id: string;
  name: string;
  country: string;
  postalCode: number;
  phoneNumber: string;
  city: string;
  address: string;
  facebookUrl: string;
  instagramUrl: string;
  websiteUrl: string;
  description: string;
  isActive: boolean;
  profileImage: IImageCard;
  images: IImageCard[];
}

export interface IRestaurantCard {
  id: string;
  profileImage: string;
  name: string;
  isOpen: boolean;
  country: string;
  city: string;
  address: string;
}

export interface IRestaurantDetails {
  id: string;
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
  isActive: boolean;
  restaurantImages: string[];
  employeesNumber: number;
  menusNumber: number;
  todayOrdersNumber: number;
}

export interface IRestaurantSelect {
  id: string;
  name: string;
}
