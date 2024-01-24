import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:customer_client/src/models/restaurant/restaurant_details_model.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class RestaurantService {
  const RestaurantService();

  Future<List<RestaurantCardModel>> getRestaurants({int pageIndex = 0}) async {
    final queryParams = {
      "pageIndex": pageIndex.toString()
    };
    final url = Uri.http(AppConfig.baseUrl, "/api/restaurants/get-restaurants", queryParams);
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => RestaurantCardModel.fromJson(e)).toList();
    } else {
      throw Exception('Failed to load Restaurants');
    }
  }

  Future<RestaurantDetailsModel> getRestaurant(int restaurantId) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = Uri.http(AppConfig.baseUrl, "/api/restaurants/get-restaurant/$restaurantId");
    final response = await http.get(url, headers: {
      'Authorization': 'Bearer $token'
    });
    if (response.statusCode == 200) {
      return RestaurantDetailsModel.fromJson(json.decode(response.body) as Map<String, dynamic>);
    } else {
      throw Exception("Failed to load Restaurant");
    }
  }

  Future<bool> addFavouriteRestaurant(int restaurantId) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = Uri.http(AppConfig.baseUrl, '/api/restaurants/add-favourite-restaurant/$restaurantId');
    final response = await http.get(url, headers: {
      'Authorization': 'Bearer $token'
    });

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception("Failed to add restaurant to favourite");
    }
  }

  Future<int> removeFavouriteRestaurant(int restaurantId) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = Uri.http(AppConfig.baseUrl, '/api/restaurants/remove-favourite-restaurant/$restaurantId');
    final response = await http.delete(url, headers: {
      'Authorization': 'Bearer $token'
    });

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception("Failed to remove restaurant from favourites.");
    }
  }


}
