class GetAccountEditModel {
  int? id;
  ProfileImage? profileImage;
  String? username;
  String? firstName;
  String? lastName;
  String? description;
  int? countryId;
  List<Countries>? countries;
  String? city;

  GetAccountEditModel(
      {this.id,
      this.profileImage,
      this.username,
      this.firstName,
      this.lastName,
      this.description,
      this.countryId,
      this.countries,
      this.city});

  GetAccountEditModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    profileImage = json['profileImage'] != null
        ? ProfileImage.fromJson(json['profileImage'])
        : null;
    username = json['username'];
    firstName = json['firstName'];
    lastName = json['lastName'];
    description = json['description'];
    countryId = json['countryId'];
    if (json['countries'] != null) {
      countries = <Countries>[];
      json['countries'].forEach((v) {
        countries!.add(Countries.fromJson(v));
      });
    }
    city = json['city'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    if (profileImage != null) {
      data['profileImage'] = profileImage!.toJson();
    }
    data['username'] = username;
    data['firstName'] = firstName;
    data['lastName'] = lastName;
    data['description'] = description;
    data['countryId'] = countryId;
    if (countries != null) {
      data['countries'] = countries!.map((v) => v.toJson()).toList();
    }
    data['city'] = city;
    return data;
  }
}

class ProfileImage {
  int? id;
  String? url;

  ProfileImage({this.id, this.url});

  ProfileImage.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    url = json['url'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['url'] = url;
    return data;
  }
}

class Countries {
  int? id;
  String? name;

  Countries({this.id, this.name});

  Countries.fromJson(Map<String, dynamic> json) {
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
