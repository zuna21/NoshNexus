import 'package:customer_client/src/services/employee_service.dart';
import 'package:customer_client/src/views/screens/employee_screen/employee_screen_child.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class EmployeeScreen extends StatelessWidget {
  const EmployeeScreen({super.key, required this.employeeId});

  final EmployeeService employeeService = const EmployeeService();
  final int employeeId;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Employee Username"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(future: employeeService.getEmployee(employeeId), builder: (_, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const LoadingScreen();
        } else if (!snapshot.hasData) {
          return const EmptyScreen(message: "There is no employee");
        } else if (snapshot.hasError) {
          return ErrorScreen(errorMessage: "Error: ${snapshot.error!.toString()}");
        } else {
          return EmployeeScreenChild(
            employee: snapshot.data!,
          );
        }
      }),
    );
  }
}
