import 'package:customer_client/src/models/employee/employee_model.dart';
import 'package:customer_client/src/services/employee_service.dart';
import 'package:customer_client/src/views/screens/employee_screen/employee_screen_child.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class EmployeeScreen extends StatefulWidget {
  const EmployeeScreen({super.key, required this.employeeId});

  final int employeeId;

  @override
  State<EmployeeScreen> createState() => _EmployeeScreenState();
}

class _EmployeeScreenState extends State<EmployeeScreen> {
  final EmployeeService employeeService = const EmployeeService();
  late Future<EmployeeModel> futureEmployee;

  @override
  void initState() {
    super.initState();
    futureEmployee = employeeService.getEmployee(widget.employeeId);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Employee Username"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(future: futureEmployee, builder: (_, snapshot) {
        if (snapshot.hasData) {
          return EmployeeScreenChild(employee: snapshot.data!);
        } else if (snapshot.hasError) {
          return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
        }

        return const LoadingScreen();
      }),
    );
  }
}
