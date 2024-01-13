import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class OrderHistoryScreen extends StatelessWidget {
  const OrderHistoryScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Order History"),
      ),
      drawer: const MainDrawer(),
      body: const Center(
        child: Text(
          "Ovo je order history",
          style: TextStyle(color: Colors.white),
        ),
      ),
    );
  }
}
