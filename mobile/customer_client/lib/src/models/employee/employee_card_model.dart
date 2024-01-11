class EmployeeCardModel {
  int? id;
  String? firstName;
  String? lastName;
  String? username;
  String? description;
  String? profileImage;
  Restaurant? restaurant;

  EmployeeCardModel(
      {this.id,
      this.firstName,
      this.lastName,
      this.username,
      this.description,
      this.profileImage,
      this.restaurant});

  EmployeeCardModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    firstName = json['firstName'];
    lastName = json['lastName'];
    username = json['username'];
    description = json['description'];
    profileImage = json['profileImage'];
    restaurant = json['restaurant'] != null
        ? Restaurant.fromJson(json['restaurant'])
        : null;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['firstName'] = firstName;
    data['lastName'] = lastName;
    data['username'] = username;
    data['description'] = description;
    data['profileImage'] = profileImage;
    if (restaurant != null) {
      data['restaurant'] = restaurant!.toJson();
    }
    return data;
  }
}

class Restaurant {
  int? id;
  String? name;
  String? profileImage;

  Restaurant({this.id, this.name, this.profileImage});

  Restaurant.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    name = json['name'];
    profileImage = json['profileImage'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    data['profileImage'] = profileImage;
    return data;
  }
}
