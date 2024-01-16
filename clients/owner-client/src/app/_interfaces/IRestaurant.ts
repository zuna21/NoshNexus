import { ICountry } from './ICountry';
import { ICurrency } from './ICurrency';
import { IImageCard } from './IImage';

export interface ICreateRestaurant {
  name: string;
  postalCode: number;
  phoneNumber: string;
  countryId: number;
  currencyId: number;
  city: string;
  address: string;
  description: string;
  facebookUrl: string;
  instagramUrl: string;
  websiteUrl: string;
  isActive: boolean;
  longitude: number;
  latitude: number;
}

export interface IGetRestaurantCreate {
  countries: ICountry[];
  currencies: ICurrency[];
}


export interface IGetEditRestaurant {
  id: number;
  name: string;
  countryId: number;
  currencyId: number;
  postalCode: number;
  phoneNumber: string;
  city: string;
  address: string;
  facebookUrl: string;
  instagramUrl: string;
  websiteUrl: string;
  description: string;
  latitude: number;
  longitude: number;
  isActive: boolean;
  profileImage: IImageCard;
  images: IImageCard[];

  allCountries: ICountry[];
  allCurrencies: ICurrency[];
}

export interface IEditRestaurant {
  address: string;
  city: string;
  countryId: number;
  currencyId: number;
  description: string;
  facebookUrl: string;
  instagramUrl: string;
  websiteUrl: string;
  isActive: boolean;
  latitude: number;
  longitude: number;
  name: string;
  phoneNumber: string;
  postalCode: number;
}

export interface IRestaurantCard {
  id: number;
  profileImage: string;
  name: string;
  isOpen: boolean;
  country: string;
  city: string;
  address: string;
}

export interface IRestaurantDetails {
  id: number;
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
  id: number;
  name: string;
}
