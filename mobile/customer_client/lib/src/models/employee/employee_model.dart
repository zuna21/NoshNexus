class EmployeeModel {
  int? id;
  String? restaurantImage;
  String? profileImage;
  String? username;
  String? firstName;
  String? lastName;
  String? description;
  String? city;
  String? birth;
  String? country;

  EmployeeModel(
      {this.id,
      this.restaurantImage,
      this.profileImage,
      this.username,
      this.firstName,
      this.lastName,
      this.description,
      this.city,
      this.birth,
      this.country});

  EmployeeModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    restaurantImage = json['restaurantImage'];
    profileImage = json['profileImage'];
    username = json['username'];
    firstName = json['firstName'];
    lastName = json['lastName'];
    description = json['description'];
    city = json['city'];
    birth = json['birth'];
    country = json['country'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['restaurantImage'] = restaurantImage;
    data['profileImage'] = profileImage;
    data['username'] = username;
    data['firstName'] = firstName;
    data['lastName'] = lastName;
    data['description'] = description;
    data['city'] = city;
    data['birth'] = birth;
    data['country'] = country;
    return data;
  }
}
