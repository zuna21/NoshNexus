import 'dart:convert';

import 'package:customer_client/src/models/employee/employee_card_model.dart';
import 'package:customer_client/src/models/employee/employee_model.dart';
import 'package:http/http.dart' as http;

class EmployeeService {
  const EmployeeService();

  final String baseUrl = 'http://192.168.0.107:3000/employees';

  Future<List<EmployeeCardModel>> getEmployees(int restaurantId) async {
    final url = Uri.parse('$baseUrl/get-employees/$restaurantId');
    final response = await http.get(url);
    if (response.statusCode == 200) {
      final List<dynamic> decodedData = json.decode(response.body);
      return decodedData.map((e) => EmployeeCardModel.fromJson(e)).toList();
    } else {
      throw Exception("Failed to load employees");
    }
  }

  Future<EmployeeModel> getEmployee(int employeeId) async {
    final url = Uri.parse("$baseUrl/get-employee/$employeeId");
    final response = await http.get(url);
    if (response.statusCode == 200) {
      final decodedData = json.decode(response.body);
      return EmployeeModel.fromJson(decodedData);
    } else {
      throw Exception("Failed to load employee");
    }
  }
}