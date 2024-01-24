class RestaurantDetailsModel {
  int? id;
  String? name;
  String? country;
  String? city;
  String? address;
  int? postalCode;
  String? phoneNumber;
  String? description;
  String? facebookUrl;
  String? instagramUrl;
  String? websiteUrl;
  bool? isOpen;
  List<String>? restaurantImages;
  int? employeesNumber;
  int? menusNumber;
  bool? isFavourite;

  RestaurantDetailsModel(
      {this.id,
      this.name,
      this.country,
      this.city,
      this.address,
      this.postalCode,
      this.phoneNumber,
      this.description,
      this.facebookUrl,
      this.instagramUrl,
      this.websiteUrl,
      this.isOpen,
      this.restaurantImages,
      this.employeesNumber,
      this.menusNumber,
      this.isFavourite});

  RestaurantDetailsModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    name = json['name'];
    country = json['country'];
    city = json['city'];
    address = json['address'];
    postalCode = json['postalCode'];
    phoneNumber = json['phoneNumber'];
    description = json['description'];
    facebookUrl = json['facebookUrl'];
    instagramUrl = json['instagramUrl'];
    websiteUrl = json['websiteUrl'];
    isOpen = json['isOpen'];
    restaurantImages = json['restaurantImages'].cast<String>();
    employeesNumber = json['employeesNumber'];
    menusNumber = json['menusNumber'];
    isFavourite = json['isFavourite'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    data['country'] = country;
    data['city'] = city;
    data['address'] = address;
    data['postalCode'] = postalCode;
    data['phoneNumber'] = phoneNumber;
    data['description'] = description;
    data['facebookUrl'] = facebookUrl;
    data['instagramUrl'] = instagramUrl;
    data['websiteUrl'] = websiteUrl;
    data['isOpen'] = isOpen;
    data['restaurantImages'] = restaurantImages;
    data['employeesNumber'] = employeesNumber;
    data['menusNumber'] = menusNumber;
    data['isFavourite'] = isFavourite;
    return data;
  }
}
