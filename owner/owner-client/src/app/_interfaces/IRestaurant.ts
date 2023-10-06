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
  phone: string;
  description: string;
  facebookUrl: string;
  instagramUrl: string;
  websiteUrl: string;
}
