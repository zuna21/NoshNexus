import 'package:customer_client/src/models/employee/employee_card_model.dart';
import 'package:customer_client/src/services/employee_service.dart';
import 'package:customer_client/src/views/screens/employees_screen/employees_screen_child.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class EmployeesScreen extends StatefulWidget {
  const EmployeesScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  State<EmployeesScreen> createState() => _EmployeesScreenState();
}

class _EmployeesScreenState extends State<EmployeesScreen> {
  final EmployeeService _employeeService = const EmployeeService();
  late Future<List<EmployeeCardModel>> futureEmployees;

  @override
  void initState() {
    super.initState();
    futureEmployees = _employeeService.getEmployees(widget.restaurantId);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Ime restorana"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(
          future: futureEmployees,
          builder: (_, snapshot) {
            if (snapshot.hasData && snapshot.data!.isNotEmpty) {
              return EmployeesScreenChild(employees: snapshot.data!);
            } else if (snapshot.hasError) {
              return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
            }
            return const LoadingScreen();
          }),
    );
  }
}
