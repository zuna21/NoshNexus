import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class MenuItemService {
  const MenuItemService();

  Future<List<MenuItemCardModel>> getRestaurantMenuItems(
      {required int restaurantId, int pageIndex = 0}) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final queryParams = {"pageIndex": pageIndex.toString()};
    final url = AppConfig.isProduction ?
     Uri.https(AppConfig.baseUrl, "/api/menuItems/get-restaurant-menu-items/$restaurantId", queryParams) :
     Uri.http(AppConfig.baseUrl, "/api/menuItems/get-restaurant-menu-items/$restaurantId", queryParams);
    final response =
        await http.get(url, headers: {'Authorization': 'Bearer $token'});
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>)
          .map((e) => MenuItemCardModel.fromJson(e))
          .toList();
    } else {
      throw Exception("Failed to load menu items.");
    }
  }

  Future<List<MenuItemCardModel>> getMenuMenuItems(
      {required int menuId, int pageIndex = 0}) async {
    final queryParams = {"pageIndex": pageIndex.toString()};
    final url = AppConfig.isProduction ?
      Uri.https(AppConfig.baseUrl, "/api/menuItems/get-menu-menu-items/$menuId", queryParams) :
      Uri.http(AppConfig.baseUrl, "/api/menuItems/get-menu-menu-items/$menuId", queryParams);
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>)
          .map((e) => MenuItemCardModel.fromJson(e))
          .toList();
    } else {
      throw Exception("Failed to load menu items.");
    }
  }

  Future<bool> addFavouriteMenuItem(int menuItemId) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = AppConfig.isProduction ?
      Uri.https(AppConfig.baseUrl, "/api/menuItems/add-favourite-menu-item/$menuItemId") :
      Uri.http(AppConfig.baseUrl, "/api/menuItems/add-favourite-menu-item/$menuItemId");
    final response = await http.get(url, headers: {
      'Content-Type': 'application/json; charset=UTF-8',
      'Authorization': 'Bearer $token'
    });

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception("Failed to add favourite item.");
    }
  }

  Future<int> removeFavouriteMenuItem(int menuItemId) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = AppConfig.isProduction ?
      Uri.https(AppConfig.baseUrl, "/api/menuItems/remove-favourite-menu-item/$menuItemId") :
      Uri.http(AppConfig.baseUrl, "/api/menuItems/remove-favourite-menu-item/$menuItemId");
    final response =
        await http.delete(url, headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception("Failed to remove from favourite");
    }
  }

  Future<List<MenuItemCardModel>> getFavouriteMenuItems() async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = AppConfig.isProduction ?
        Uri.https(AppConfig.baseUrl, '/api/menuItems/get-favourite-menu-items') :
        Uri.http(AppConfig.baseUrl, '/api/menuItems/get-favourite-menu-items');
    final response =
        await http.get(url, headers: {'Authorization': 'Bearer $token'});

    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>)
          .map((e) => MenuItemCardModel.fromJson(e))
          .toList();
    } else {
      throw Exception("Failed to fetch favourite menu items.");
    }
  }
}
