class RestaurantCardModel {
  int? id;
  String? profileImage;
  String? name;
  bool? isOpen;
  String? country;
  String? city;
  String? address;

  RestaurantCardModel(
      {this.id,
      this.profileImage,
      this.name,
      this.isOpen,
      this.country,
      this.city,
      this.address});

  RestaurantCardModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    profileImage = json['profileImage'];
    name = json['name'];
    isOpen = json['isOpen'];
    country = json['country'];
    city = json['city'];
    address = json['address'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['profileImage'] = profileImage;
    data['name'] = name;
    data['isOpen'] = isOpen;
    data['country'] = country;
    data['city'] = city;
    data['address'] = address;
    return data;
  }
}
