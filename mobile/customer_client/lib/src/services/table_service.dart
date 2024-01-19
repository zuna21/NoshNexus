import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/table/table_model.dart';
import 'package:http/http.dart' as http;

class TableService {
  const TableService();

  Future<List<TableModel>> getTables(int restaurantId) async {
    final url = Uri.http(AppConfig.baseUrl, "/api/tables/get-tables/$restaurantId");
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => TableModel.fromJson(e)).toList();
    } else {
      throw Exception("Failed to load restaurant tables.");
    }
  }
}