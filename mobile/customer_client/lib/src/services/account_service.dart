import 'dart:convert';
import 'dart:io';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/account/account_details_model.dart';
import 'package:customer_client/src/models/account/account_model.dart';
import 'package:customer_client/src/models/account/activate_account_model.dart';
import 'package:customer_client/src/models/account/edit_account_model.dart';
import 'package:customer_client/src/models/account/fcm_token.model.dart';
import 'package:customer_client/src/models/account/get_account_edit_model.dart';
import 'package:customer_client/src/models/account/image_card_model.dart';
import 'package:customer_client/src/models/account/login_account_model.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class AccountService {
  const AccountService();

  Future<AccountModel> login(LoginAccountModel loginAccount) async {
    final response = await http.post(
      AppConfig.isProduction
          ? Uri.https(AppConfig.baseUrl, "/api/account/login")
          : Uri.http(AppConfig.baseUrl, "/api/account/login"),
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

  Future<AccountModel> loginAsGuest() async {
    final url = AppConfig.isProduction
        ? Uri.https(AppConfig.baseUrl, '/api/account/login-as-guest')
        : Uri.http(AppConfig.baseUrl, '/api/account/login-as-guest');

    final response = await http.get(url);
    if (response.statusCode == 200) {
      return AccountModel.fromJson(
        json.decode(response.body),
      );
    } else {
      throw Exception("Failed to create an account.");
    }
  }

  Future<AccountDetailsModel> getAccountDetails() async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = AppConfig.isProduction
        ? Uri.https(AppConfig.baseUrl, "/api/account/get-account-details")
        : Uri.http(AppConfig.baseUrl, "/api/account/get-account-details");
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

  Future<AccountModel> activateAccount(
      ActivateAccountModel activateAccountModel) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = AppConfig.isProduction
        ? Uri.https(AppConfig.baseUrl, "/api/account/activate-account")
        : Uri.http(AppConfig.baseUrl, "/api/account/activate-account");

    final response = await http.post(
      url,
      headers: {
        'Content-Type': 'application/json; charset=UTF-8',
        'Authorization': 'Bearer $token'
      },
      body: json.encode(activateAccountModel.toJson()),
    );

    if (response.statusCode == 200) {
      AccountModel account = AccountModel.fromJson(json.decode(response.body));
      await storage.write(key: "token", value: account.token);
      return account;
    } else {
      throw Exception("Failed to activate account.");
    }
  }

  Future<GetAccountEditModel> getAccountEdit() async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = AppConfig.isProduction
        ? Uri.https(AppConfig.baseUrl, "/api/account/get-account-edit")
        : Uri.http(AppConfig.baseUrl, "/api/account/get-account-edit");

    final response =
        await http.get(url, headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      return GetAccountEditModel.fromJson(json.decode(response.body));
    } else {
      throw Exception("Failed to get account details.");
    }
  }

  Future<AccountModel> editAccount(EditAccountModel editAccountModel) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = AppConfig.isProduction
        ? Uri.https(AppConfig.baseUrl, "/api/account/update-account")
        : Uri.http(AppConfig.baseUrl, "/api/account/update-account");

    final response = await http.put(url,
        headers: {
          'Content-Type': 'application/json; charset=UTF-8',
          'Authorization': 'Bearer $token'
        },
        body: json.encode(editAccountModel.toJson()));

    if (response.statusCode == 200) {
      AccountModel account = AccountModel.fromJson(
        json.decode(response.body),
      );
      await storage.write(key: "token", value: account.token);
      return account;
    } else {
      throw Exception("Failed to edit account");
    }
  }

  Future<ImageCardModel> uploadImage(File imageFile) async {
  // Create a multipart request
  const storage = FlutterSecureStorage();
  final token = await storage.read(key: "token");

  var request = http.MultipartRequest(
    'POST',
    AppConfig.isProduction
        ? Uri.https(AppConfig.baseUrl, "/api/account/upload-profile-image")
        : Uri.http(AppConfig.baseUrl, "/api/account/upload-profile-image"),
  );

  Map<String, String> headers = { "Authorization": "Bearer $token"};

  request.headers.addAll(headers);
  var image = await http.MultipartFile.fromPath('image', imageFile.path);
  request.files.add(image);

  // Send the request
  var streamedResponse = await request.send();

  // Check if the request was successful
  if (streamedResponse.statusCode == 200) {
    var response = await http.Response.fromStream(streamedResponse);
    return ImageCardModel.fromJson(json.decode(response.body));
  } else {
    throw Exception("Failed to upload profile image.");
  }
}

  Future<bool> updateFcmToken() async {
    FirebaseMessaging.instance.requestPermission(provisional: true);
    final generatedFcmToken = await FirebaseMessaging.instance.getToken();
    if (generatedFcmToken == null) return false;

    const storage = FlutterSecureStorage();
    final fcmToken = await storage.read(key: "fcmToken");
    if (fcmToken == generatedFcmToken) return true;

    FcmTokenModel fcmTokenModel = FcmTokenModel(token: generatedFcmToken);

    final token = await storage.read(key: "token");
    if (token == null) return false;
    
    final url = AppConfig.isProduction
        ? Uri.https(AppConfig.baseUrl, "/api/account/update-fcm-token")
        : Uri.http(AppConfig.baseUrl, "/api/account/update-fcm-token");

    final response = await http.post(
      url,
      headers: {
        'Content-Type': 'application/json; charset=UTF-8',
        'Authorization': 'Bearer $token'
      },
      body: json.encode(fcmTokenModel.toJson()),
    );

    if (response.statusCode == 200) {
      await storage.write(key: "fcmToken", value: generatedFcmToken);
      return true;
    } else {
      throw Exception("Failed to update fcm token");
    }
  }
}


