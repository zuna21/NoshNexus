import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class EmployeeScreen extends StatelessWidget {
  const EmployeeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Employee Username"),
      ),
      drawer: const MainDrawer(),
      body: const Center(
        child: Text(
          "Ovo je employee Screen",
          style: TextStyle(color: Colors.white),
        ),
      ),
    );
  }
}
