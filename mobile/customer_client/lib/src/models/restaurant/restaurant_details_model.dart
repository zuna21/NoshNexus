class RestaurantDetailsModel {
  const RestaurantDetailsModel({
    required this.id,
    required this.name,
    required this.address,
    required this.city,
    required this.country,
    required this.description,
    required this.employeesNumber,
    required this.facebookUrl,
    required this.instagramUrl,
    required this.isOpen,
    required this.menusNumber,
    required this.phoneNumber,
    required this.postalCode, 
    required this.restaurantImages,
    required this.websiteUrl
  });

  final int id;
  final String name;
  final String country;
  final String city;
  final String address;
  final String postalCode;
  final String phoneNumber;
  final String description;
  final String facebookUrl;
  final String instagramUrl;
  final String websiteUrl;
  final bool isOpen;
  final List<String> restaurantImages;
  final int employeesNumber;
  final int menusNumber;
}