import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/order/create_order_model.dart';
import 'package:customer_client/src/models/order/order_card_model.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class OrderService {
  const OrderService();

  Future<List<OrderCardModel>> getOrders({int pageIndex = 0}) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final params = {"pageIndex": pageIndex.toString()};
    final url = Uri.http(AppConfig.baseUrl, "/api/orders/get-orders", params);
    final response = await http.get(url, headers: {
      "Authorization": "Bearer $token"
    });

    print(response.statusCode);

    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>)
          .map((e) => OrderCardModel.fromJson(e))
          .toList();
    } else {
      throw Exception("Failed to fetch orders");
    }
  }

  Future<bool> createOrder(int restaurantId, CreateOrderModel order) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final url = Uri.http(AppConfig.baseUrl, '/api/orders/create/$restaurantId');
    final response = await http.post(
      url,
      headers: <String, String>{
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer $token'
      },
      body: json.encode(
        order.toJson(),
      ),
    );

    if (response.statusCode == 200) {
      return true;
    } else {
      throw Exception("Failed to create order");
    }
  }
}
