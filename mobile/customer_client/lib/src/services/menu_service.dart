import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/menu/menu_card_model.dart';
import 'package:customer_client/src/models/menu/menu_model.dart';
import 'package:http/http.dart' as http;

class MenuService {
  const MenuService();

  Future<List<MenuCardModel>> getMenus({required int restaurantId, int pageIndex = 0}) async {
    final queryParams = {
      "pageIndex": pageIndex.toString()
    };
    final url = AppConfig.isProduction ?
      Uri.https(AppConfig.baseUrl, "/api/menus/get-restaurant-menus/$restaurantId", queryParams) :
      Uri.http(AppConfig.baseUrl, "/api/menus/get-restaurant-menus/$restaurantId", queryParams);
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => MenuCardModel.fromJson(e)).toList();
    } else {
      throw Exception("Failed to load menus.");
    }
  }

  Future<MenuModel> getMenu(int menuId) async {
    final url = AppConfig.isProduction ? 
      Uri.https(AppConfig.baseUrl, "/api/menus/get-menu/$menuId") :
      Uri.http(AppConfig.baseUrl, "/api/menus/get-menu/$menuId");
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return MenuModel.fromJson(json.decode(response.body) as Map<String, dynamic>);
    } else {
      throw Exception("Failed to load menu.");
    }
  }
}