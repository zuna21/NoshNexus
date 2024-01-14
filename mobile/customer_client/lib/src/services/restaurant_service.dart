import 'dart:convert';

import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:customer_client/src/models/restaurant/restaurant_details_model.dart';
import 'package:http/http.dart' as http;

class RestaurantService {
  const RestaurantService();

  final String baseUrl = '192.168.0.107:3000';

  Future<List<RestaurantCardModel>> getRestaurants({int pageIndex = 0}) async {
    // final url = Uri.parse('$baseUrl/get-restaurants');
    final queryParams = {
      "pageIndex": pageIndex.toString()
    };
    final url = Uri.http(baseUrl, "/restaurants/get-restaurants", queryParams);
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => RestaurantCardModel.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load Restaurants');
    }
  }

  Future<RestaurantDetailsModel> getRestaurant(int restaurantId) async {
    final url = Uri.http(baseUrl, "/restaurants/get-restaurant/$restaurantId");
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return RestaurantDetailsModel.fromJson(json.decode(response.body) as Map<String, dynamic>);
    } else {
      throw Exception("Failed to load Restaurant");
    }
  }
}
