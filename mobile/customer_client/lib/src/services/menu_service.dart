import 'dart:convert';

import 'package:customer_client/src/models/menu_card_model.dart';
import 'package:http/http.dart' as http;

class MenuService {
  const MenuService();
  final String baseUrl = 'http://192.168.0.107:3000/menus';

  Future<List<MenuCardModel>> getMenus(int restaurantId) async {
    final url = Uri.parse("$baseUrl/get-menus/$restaurantId");
    final response = await http.get(url);
    if (response.statusCode == 200) {
      final List<dynamic> decodedData = json.decode(response.body);
      final menus = decodedData.map((e) => MenuCardModel.fromJson(e)).toList();
      return menus;
    } else {
      throw Exception("Failed to load menus.");
    }
  }
}