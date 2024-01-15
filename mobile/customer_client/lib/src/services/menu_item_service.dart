import 'dart:convert';

import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:http/http.dart' as http;

class MenuItemService {
  final String baseUrl = '192.168.0.107:3000';

  const MenuItemService();

  Future<List<MenuItemCardModel>> getBestMenuItems({required int restaurantId, int pageIndex = 0}) async {
    final queryParams = {
      "pageIndex": pageIndex.toString()
    };
    final url = Uri.http(baseUrl, "/menu-items/get-best-menu-items/$restaurantId", queryParams);
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
    final url = Uri.http(baseUrl, "/menu-items/get-menu-menu-items/$menuId", queryParams);
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => MenuItemCardModel.fromJson(e)).toList();
    } else {
      throw Exception("Failed to load menu items.");
    }
  }
}