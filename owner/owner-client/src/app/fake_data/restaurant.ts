import { IRestaurantCard, IRestaurantDetails, IRestaurantEdit } from "../_interfaces/IRestaurant";

export const RESTAURANTS_FOR_CARD: IRestaurantCard[] = [
  {
    id: '1a34b5c6-d789-4e01-a234-567890123456',
    profileImage: 'assets/img/restaurants/rupa.jpg',
    name: 'Restaurant One',
    isOpen: true,
    country: 'United States',
    city: 'New York',
    address: '123 Main Street',
  },
  {
    id: '2b45c6d7-e890-4f12-b345-678901234567',
    profileImage: 'assets/img/restaurants/restauran1.jpg',
    name: 'Restaurant Two',
    isOpen: false,
    country: 'United Kingdom',
    city: 'London',
    address: '456 Elm Street',
  },
  {
    id: '3c56d7e8-f901-4g23-c456-789012345678',
    profileImage: 'assets/img/restaurants/restauran2.jpeg',
    name: 'Restaurant Three',
    isOpen: true,
    country: 'France',
    city: 'Paris',
    address: '789 Oak Avenue',
  },
  {
    id: '4d67e8f9-g012-4h34-d567-890123456789',
    profileImage: 'assets/img/restaurants/restauran3.jpg',
    name: 'Restaurant Four',
    isOpen: true,
    country: 'Germany',
    city: 'Berlin',
    address: '101 River Road',
  },
  {
    id: '5e89f90a-1234-5i67-e890-123456789012',
    profileImage: 'assets/img/restaurants/restauran4.jpeg',
    name: 'Restaurant Five',
    isOpen: false,
    country: 'Spain',
    city: 'Madrid',
    address: '222 Pine Lane',
  },
  {
    id: '6f90a12b-3456-7j89-f012-345678901234',
    profileImage: 'assets/img/restaurants/restauran5.jpg',
    name: 'Restaurant Six',
    isOpen: true,
    country: 'Italy',
    city: 'Rome',
    address: '333 Cedar Street',
  },
  {
    id: '7a12b34c-5678-9k01-2l34-567890123456',
    profileImage: 'assets/img/restaurants/restaurant6.jpg',
    name: 'Restaurant Seven',
    isOpen: false,
    country: 'Canada',
    city: 'Toronto',
    address: '444 Birch Avenue',
  },
  {
    id: '8b34c56d-7890-1m23-4n56-789012345678',
    profileImage: 'assets/img/restaurants/restaurant7.jpg',
    name: 'Restaurant Eight',
    isOpen: true,
    country: 'Australia',
    city: 'Sydney',
    address: '555 Willow Road',
  },
  {
    id: '9c56d78e-9012-3o45-6p78-901234567890',
    profileImage: 'assets/img/restaurants/rupa.jpg',
    name: 'Restaurant Nine',
    isOpen: true,
    country: 'Japan',
    city: 'Tokyo',
    address: '666 Oak Street',
  },
  {
    id: '0d78e90f-1234-5q67-8r90-123456789012',
    profileImage: 'assets/img/restaurants/restauran1.jpg',
    name: 'Restaurant Ten',
    isOpen: false,
    country: 'Brazil',
    city: 'Rio de Janeiro',
    address: '777 Maple Avenue',
  },
  {
    id: '1e90f12g-3456-7r89-0s12-345678901234',
    profileImage: 'assets/img/restaurants/restauran2.jpeg',
    name: 'Restaurant Eleven',
    isOpen: true,
    country: 'South Korea',
    city: 'Seoul',
    address: '888 Elm Street',
  },
  {
    id: '2f12g34h-5678-9s01-2t34-567890123456',
    profileImage: 'assets/img/restaurants/restauran3.jpg',
    name: 'Restaurant Twelve',
    isOpen: true,
    country: 'Mexico',
    city: 'Mexico City',
    address: '999 Cedar Avenue',
  },
  {
    id: '3g34h56i-7890-1t23-4u56-789012345678',
    profileImage: 'assets/img/restaurants/restauran4.jpeg',
    name: 'Restaurant Thirteen',
    isOpen: false,
    country: 'China',
    city: 'Beijing',
    address: '1010 Pine Lane',
  },
  {
    id: '4h56i78j-9012-3u45-6v78-901234567890',
    profileImage: 'assets/img/restaurants/restauran5.jpg',
    name: 'Restaurant Fourteen',
    isOpen: true,
    country: 'India',
    city: 'Mumbai',
    address: '1111 Oak Street',
  },
  {
    id: '5i78j90k-1234-5v67-8w90-123456789012',
    profileImage: 'assets/img/restaurants/restaurant6.jpg',
    name: 'Restaurant Fifteen',
    isOpen: true,
    country: 'Russia',
    city: 'Moscow',
    address: '1212 Willow Road',
  },
];

export const RESTAURANT_FOR_DETAILS: IRestaurantDetails = {
  "id": "1a34b5c6-d789-4e01-a234-567890123456",
  "name": "Restaurant One",
  "country": "United States",
  "city": "New York",
  "address": "123 Main Street",
  "postalCode": 10001,
  "phone": "+1 (123) 456-7890",
  "description": "A cozy restaurant in the heart of New York City, offering a diverse menu of international cuisine. With its warm ambiance and friendly staff, Restaurant One is the perfect place for a romantic dinner, a family gathering, or a casual lunch.",
  "facebookUrl": "https://www.facebook.com/restaurantone",
  "instagramUrl": "https://www.instagram.com/restaurantone",
  "websiteUrl": "https://www.restaurantone.com",
  "isActive": true,
  "restaurantImages": [
    "assets/img/restaurants/rupa.jpg",
    "assets/img/restaurants/restauran1.jpg",
    "assets/img/restaurants/restauran2.jpeg"
  ],
  "employeesNumber": 20,
  "menusNumber": 3,
  "todayOrdersNumber": 50
}

export const RESTAURANT_FOR_EDIT: IRestaurantEdit = {
  "id": "1a34b5c6-d789-4e01-a234-567890123456",
  "name": "Restaurant One",
  "country": "United States",
  "postalCode": 10001,
  "phone": "+1 (123) 456-7890",
  "city": "New York",
  "address": "123 Main Street",
  "facebookUrl": "https://www.facebook.com/restaurantone",
  "instagramUrl": "https://www.instagram.com/restaurantone",
  "websiteUrl": "https://www.restaurantone.com",
  "description": "A cozy restaurant in the heart of New York City, offering a diverse menu of international cuisine. With its warm ambiance and friendly staff, Restaurant One is the perfect place for a romantic dinner, a family gathering, or a casual lunch.",
  "isActive": true,
  "profileImage": {
    "id": "6e89f90a-1234-5i67-e890-123456789012",
    "url": "assets/img/restaurants/rupa.jpg",
    "size": 1024
  },
  "images": [
    {
      "id": "1a34b5c6-d789-4e01-a234-567890123456",
      "url": "assets/img/restaurants/restauran1.jpg",
      "size": 1024
    },
    {
      "id": "2b45c6d7-e890-4f12-b345-678901234567",
      "url": "assets/img/restaurants/restauran2.jpeg",
      "size": 2048
    },
    {
      "id": "3c56d7e8-f901-4g23-c456-789012345678",
      "url": "assets/img/restaurants/restauran3.jpg",
      "size": 512
    }
  ]
};