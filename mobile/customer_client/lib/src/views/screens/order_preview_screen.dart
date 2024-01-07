import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class OrderPreviewScreen extends StatelessWidget {
  const OrderPreviewScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Order Preview"),
      ),
      drawer: const MainDrawer(),
      body: const Center(
        child: Text(
          "Ovo je order preview",
          style: TextStyle(color: Colors.white),
        ),
      ),
    );
  }
}
