class RestaurantCardModel {
  const RestaurantCardModel({
    required this.id,
    required this.address,
    required this.city,
    required this.country,
    required this.isOpen,
    required this.name,
    required this.profileImage,
  });

  final int id;
  final String profileImage;
  final String name;
  final bool isOpen;
  final String country;
  final String city;
  final String address;
}
