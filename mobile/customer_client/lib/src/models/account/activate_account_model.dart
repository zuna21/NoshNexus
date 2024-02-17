class ActivateAccountModel {
  String? username;
  String? password;
  String? repeatPassword;

  ActivateAccountModel({this.username, this.password, this.repeatPassword});

  ActivateAccountModel.fromJson(Map<String, dynamic> json) {
    username = json['username'];
    password = json['password'];
    repeatPassword = json['repeatPassword'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['username'] = username;
    data['password'] = password;
    data['repeatPassword'] = repeatPassword;
    return data;
  }
}