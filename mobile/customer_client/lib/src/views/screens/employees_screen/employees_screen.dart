import 'package:customer_client/src/views/widgets/cards/employee_card.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class EmployeesScreen extends StatelessWidget {
  const EmployeesScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Ime restorana"),
      ),
      drawer: const MainDrawer(),
      body: ListView(
        children: const [
          EmployeeCard()
        ],
      ),
    );
  }
}
