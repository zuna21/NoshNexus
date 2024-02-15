import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/account/account_details_model.dart';
import 'package:customer_client/src/models/account/account_model.dart';
import 'package:customer_client/src/models/account/login_account_model.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
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

  Future<AccountDetailsModel> getAccountDetails() async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = Uri.http(AppConfig.baseUrl, "/api/account/get-account-details");
    final response =
        await http.get(url, headers: {'Authorization': 'Bearer $token'});
    if (response.statusCode == 200) {
      return AccountDetailsModel.fromJson(
        json.decode(response.body),
      );
    } else {
      throw Exception("Failed to load account details.");
    }
  }
}
