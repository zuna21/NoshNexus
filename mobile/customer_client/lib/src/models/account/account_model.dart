class AccountModel {
  String? username;
  String? token;
  String? profileImage;

  AccountModel({this.username, this.token, this.profileImage});

  AccountModel.fromJson(Map<String, dynamic> json) {
    username = json['username'];
    token = json['token'];
    profileImage = json['profileImage'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['username'] = username;
    data['token'] = token;
    data['profileImage'] = profileImage;
    return data;
  }
}
