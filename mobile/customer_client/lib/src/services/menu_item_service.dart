import 'dart:convert';

import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:http/http.dart' as http;

class MenuItemService {
  final String baseUrl = 'http://192.168.0.107:3000/menu-items';

  const MenuItemService();

  Future<List<MenuItemCardModel>> getBestMenuItems(int restaurantId) async {
    final url = Uri.parse('$baseUrl/get-best-menu-items/$restaurantId');
    final response = await http.get(url);
    if (response.statusCode == 200) {
      final List<dynamic> decodedData = json.decode(response.body);
      final menuItemsList = decodedData.map((e) => MenuItemCardModel.fromJson(e)).toList();
      return menuItemsList;
    } else {
      throw Exception("Failed to load menu items.");
    }
  }
}