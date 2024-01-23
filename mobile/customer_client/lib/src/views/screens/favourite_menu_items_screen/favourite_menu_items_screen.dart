import 'package:flutter/material.dart';

class FavouriteMenuItemsScreen extends StatelessWidget {
  const FavouriteMenuItemsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Favourite items"),
      ),
      body: const Center(
      child: Text(
        "Favourite menu items",
        style: TextStyle(color: Colors.white),
      ),
    ),
    );
  }
}
