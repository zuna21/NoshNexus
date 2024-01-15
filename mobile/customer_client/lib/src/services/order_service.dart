import 'dart:convert';

import 'package:customer_client/src/models/order/order_card_model.dart';
import 'package:http/http.dart' as http;

class OrderService {
  const OrderService();

  final String baseUrl = "192.168.0.107:3000";

  Future<List<OrderCardModel>> getOrders({int pageIndex = 0}) async {
    final params = {
      "pageIndex": pageIndex.toString()
    };
    final url = Uri.http(baseUrl, "/orders/get-orders", params);
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
