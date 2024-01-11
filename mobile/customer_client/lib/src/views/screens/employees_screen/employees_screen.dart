import 'package:customer_client/src/services/employee_service.dart';
import 'package:customer_client/src/views/screens/employees_screen/employees_screen_child.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class EmployeesScreen extends StatelessWidget {
  const EmployeesScreen({super.key, required this.restaurantId});

  final int restaurantId;
  final EmployeeService _employeeService = const EmployeeService();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text("Ime restorana"),
        ),
        drawer: const MainDrawer(),
        body: FutureBuilder(
            future: _employeeService.getEmployees(restaurantId),
            builder: (_, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return const LoadingScreen();
              } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
                return const EmptyScreen(message: "There are no employees");
              } else if (snapshot.hasError) {
                return ErrorScreen(
                    errorMessage: "Error: ${snapshot.error.toString()}");
              } else {
                return EmployeesScreenChild(employees: snapshot.data!);
              }
            }));
  }
}
