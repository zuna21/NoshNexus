import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/account/account_model.dart';
import 'package:customer_client/src/models/account/login_account_model.dart';
import 'package:http/http.dart' as http;

class AccountService {
  const AccountService();

  Future<AccountModel> login(LoginAccountModel loginAccount) async {
    final response = await http.post(
      Uri.http(AppConfig.baseUrl, "/api/account/login"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: json.encode(loginAccount.toJson()),
    );
    if (response.statusCode == 200) {
      return AccountModel.fromJson(
        json.decode(response.body),
      );
    } else if (response.statusCode == 401) {
      return AccountModel();
    } else {
      throw Exception("Failed to login user.");
    }
  }
}
