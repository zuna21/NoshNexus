import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:http/http.dart' as http;

class MenuItemService {

  const MenuItemService();

  Future<List<MenuItemCardModel>> getRestaurantMenuItems({required int restaurantId, int pageIndex = 0}) async {
    final queryParams = {
      "pageIndex": pageIndex.toString()
    };
    final url = Uri.http(AppConfig.baseUrl, "/api/menuItems/get-restaurant-menu-items/$restaurantId", queryParams);
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => MenuItemCardModel.fromJson(e)).toList();
    } else {
      throw Exception("Failed to load menu items.");
    }
  }

  Future<List<MenuItemCardModel>> getMenuMenuItems({required int menuId, int pageIndex = 0}) async {
    final queryParams = {
      "pageIndex": pageIndex.toString()
    };
    final url = Uri.http(AppConfig.baseUrl, "/api/menuItems/get-menu-menu-items/$menuId", queryParams);
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => MenuItemCardModel.fromJson(e)).toList();
    } else {
      throw Exception("Failed to load menu items.");
    }
  }
}