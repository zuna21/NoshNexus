import 'dart:convert';

import 'package:customer_client/config.dart';
import 'package:customer_client/src/models/employee/employee_card_model.dart';
import 'package:customer_client/src/models/employee/employee_model.dart';
import 'package:http/http.dart' as http;

class EmployeeService {
  const EmployeeService();

  Future<List<EmployeeCardModel>> getEmployees(int restaurantId) async {
    final url = AppConfig.isProduction ? 
      Uri.https(AppConfig.baseUrl, '/api/employees/get-employees/$restaurantId') :
      Uri.http(AppConfig.baseUrl, '/api/employees/get-employees/$restaurantId');
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return (json.decode(response.body) as List<dynamic>).map((e) => EmployeeCardModel.fromJson(e)).toList();
    } else {
      throw Exception("Failed to load employees");
    }
  }

  Future<EmployeeModel> getEmployee(int employeeId) async {
    final url = AppConfig.isProduction ? 
      Uri.https(AppConfig.baseUrl, "/api/employees/get-employee/$employeeId") :
      Uri.http(AppConfig.baseUrl, "/api/employees/get-employee/$employeeId");
    final response = await http.get(url);
    if (response.statusCode == 200) {
      return EmployeeModel.fromJson(json.decode(response.body) as Map<String, dynamic>);
    } else {
      throw Exception("Failed to load employee");
    }
  }
}