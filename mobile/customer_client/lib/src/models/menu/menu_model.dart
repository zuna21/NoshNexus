class MenuModel {
  int? id;
  Restaurant? restaurant;
  int? totalMenuItems;
  String? description;

  MenuModel({this.id, this.restaurant, this.totalMenuItems, this.description});

  MenuModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    restaurant = json['restaurant'] != null
        ? Restaurant.fromJson(json['restaurant'])
        : null;
    totalMenuItems = json['totalMenuItems'];
    description = json['description'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    if (restaurant != null) {
      data['restaurant'] = restaurant!.toJson();
    }
    data['totalMenuItems'] = totalMenuItems;
    data['description'] = description;
    return data;
  }
}

class Restaurant {
  int? id;
  String? name;

  Restaurant({this.id, this.name});

  Restaurant.fromJson(Map<String, dynamic> json) {
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
