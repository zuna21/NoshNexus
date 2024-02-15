class AccountDetailsModel {
  int? id;
  String? profileImage;
  String? username;
  String? firstName;
  String? lastName;
  String? description;
  String? country;
  bool? isActivated;
  String? city;
  String? joined;

  AccountDetailsModel(
      {this.id,
      this.profileImage,
      this.username,
      this.firstName,
      this.lastName,
      this.description,
      this.country,
      this.isActivated,
      this.city,
      this.joined});

  AccountDetailsModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    profileImage = json['profileImage'];
    username = json['username'];
    firstName = json['firstName'];
    lastName = json['lastName'];
    description = json['description'];
    country = json['country'];
    isActivated = json['isActivated'];
    city = json['city'];
    joined = json['joined'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['profileImage'] = profileImage;
    data['username'] = username;
    data['firstName'] = firstName;
    data['lastName'] = lastName;
    data['description'] = description;
    data['country'] = country;
    data['isActivated'] = isActivated;
    data['city'] = city;
    data['joined'] = joined;
    return data;
  }
}
