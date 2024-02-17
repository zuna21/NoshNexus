class EditAccountModel {
  String? username;
  String? firstName;
  String? lastName;
  String? description;
  int? countryId;
  String? city;

  EditAccountModel(
      {this.username,
      this.firstName,
      this.lastName,
      this.description,
      this.countryId,
      this.city});

  EditAccountModel.fromJson(Map<String, dynamic> json) {
    username = json['username'];
    firstName = json['firstName'];
    lastName = json['lastName'];
    description = json['description'];
    countryId = json['countryId'];
    city = json['city'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['username'] = username;
    data['firstName'] = firstName;
    data['lastName'] = lastName;
    data['description'] = description;
    data['countryId'] = countryId;
    data['city'] = city;
    return data;
  }
}
