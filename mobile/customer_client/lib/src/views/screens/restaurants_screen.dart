import 'package:customer_client/src/views/widgets/cards/restaurant_card.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantsScreen extends StatelessWidget {
  const RestaurantsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Restaurants"),
      ),
      drawer: const MainDrawer(),
      body: const RestaurantCard()
    );
  }
}
