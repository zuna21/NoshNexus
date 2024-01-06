import 'dart:convert';

import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:http/http.dart' as http;

class RestaurantService {
  const RestaurantService({required this.baseUrl});

  final String baseUrl;

  Future<List<RestaurantCardModel>> getRestaurants() async {
    final url = Uri.parse('$baseUrl/get-restaurants');
    final response = await http.get(url);
    if (response.statusCode == 200) {
      final List<dynamic> decodedData = json.decode(response.body);
      final List<RestaurantCardModel> restaurants = decodedData
          .map((jsonObject) => RestaurantCardModel(
              id: jsonObject['id'],
              address: jsonObject['address'],
              city: jsonObject['city'],
              country: jsonObject['country'],
              isOpen: jsonObject['isOpen'],
              name: jsonObject['name'],
              profileImage: jsonObject['profileImage']))
          .toList();

      return restaurants;
    } else {
      throw Exception('Failed to load Restaurants');
    }
  }
}
