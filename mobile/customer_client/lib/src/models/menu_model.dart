class MenuModel {
  int? id;
  String? name;
  String? description;
  int? menuItemNumber;
  String? restaurantName;

  MenuModel(
      {this.id,
      this.name,
      this.description,
      this.menuItemNumber,
      this.restaurantName});

  MenuModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    name = json['name'];
    description = json['description'];
    menuItemNumber = json['menuItemNumber'];
    restaurantName = json['restaurantName'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    data['description'] = description;
    data['menuItemNumber'] = menuItemNumber;
    data['restaurantName'] = restaurantName;
    return data;
  }
}