import 'dart:convert';

import 'package:customer_client/src/models/order/order_card_model.dart';
import 'package:http/http.dart' as http;

class OrderService {
  const OrderService();

  final String baseUrl = "http://192.168.0.107:3000/orders";

  Future<List<OrderCardModel>> getOrders() async {
    final url = Uri.parse("$baseUrl/get-orders");
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>)
          .map((e) => OrderCardModel.fromJson(e))
          .toList();
    } else {
      throw Exception("Failed to fetch orers");
    }
  }
}
