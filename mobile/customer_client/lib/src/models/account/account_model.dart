class AccountModel {
  String? username;
  String? token;

  AccountModel({this.username, this.token});

  AccountModel.fromJson(Map<String, dynamic> json) {
    username = json['username'];
    token = json['token'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['username'] = username;
    data['token'] = token;
    return data;
  }
}
