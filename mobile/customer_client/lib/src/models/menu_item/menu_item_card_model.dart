class MenuItemCardModel {
  int? id;
  int? restaurantId;
  Menu? menu;
  String? name;
  String? description;
  double? price;
  bool? hasSpecialOffer;
  double? specialOfferPrice;
  String? profileImage;
  List<String>? images;

  MenuItemCardModel(
      {this.id,
      this.restaurantId,
      this.menu,
      this.name,
      this.description,
      this.price,
      this.hasSpecialOffer,
      this.specialOfferPrice,
      this.profileImage,
      this.images});

  MenuItemCardModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    restaurantId = json['restaurantId'];
    menu = json['menu'] != null ? Menu.fromJson(json['menu']) : null;
    name = json['name'];
    description = json['description'];
    price = (json['price'] as num).toDouble();
    hasSpecialOffer = json['hasSpecialOffer'];
    specialOfferPrice = (json['specialOfferPrice'] as num).toDouble();
    profileImage = json['profileImage'];
    images = json['images'].cast<String>();
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['restaurantId'] = restaurantId;
    if (menu != null) {
      data['menu'] = menu!.toJson();
    }
    data['name'] = name;
    data['description'] = description;
    data['price'] = price;
    data['hasSpecialOffer'] = hasSpecialOffer;
    data['specialOfferPrice'] = specialOfferPrice;
    data['profileImage'] = profileImage;
    data['images'] = images;
    return data;
  }
}

class Menu {
  int? id;
  String? name;

  Menu({this.id, this.name});

  Menu.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    name = json['name'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    return data;
  }
}
