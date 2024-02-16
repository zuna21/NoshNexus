import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/order/create_order_model.dart';
import 'package:customer_client/src/models/order/order_card_model.dart';
import 'package:customer_client/src/models/query_params/orders_query_params.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class OrderService {
  const OrderService();

  Future<List<OrderCardModel>> getOrders(OrdersQueryPrams ordersQueryPrams) async {
    const storage = FlutterSecureStorage();
    final token = await storage.read(key: "token");
    final params = {
      "pageIndex": ordersQueryPrams.pageIndex.toString(),
      "status": ordersQueryPrams.status,
      "search": ordersQueryPrams.search
    };
    final url = AppConfig.isProduction ?
      Uri.https(AppConfig.baseUrl, "/api/orders/get-orders", params) :
      Uri.http(AppConfig.baseUrl, "/api/orders/get-orders", params);
    final response = await http.get(url, headers: {
      "Authorization": "Bearer $token"
    });

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
    final url = AppConfig.isProduction ?
      Uri.https(AppConfig.baseUrl, '/api/orders/create/$restaurantId') :
      Uri.http(AppConfig.baseUrl, '/api/orders/create/$restaurantId');
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
