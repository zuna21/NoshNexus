import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen_child.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantScreen extends StatelessWidget {
  const RestaurantScreen({super.key, required this.restaurantId});

  final int restaurantId;
  final RestaurantService _restaurantService = const RestaurantService();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Nosh Nexus"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(
        future: _restaurantService.getRestaurant(restaurantId),
        builder: ((context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const LoadingScreen();
          }

          if (snapshot.hasError) {
            return ErrorScreen(errorMessage: "Error: ${snapshot.error!.toString()}");
          }

          if (!snapshot.hasData || snapshot.data == null) {
            return const EmptyScreen(message: "There is no restaurant.");
          }

          return RestaurantScreenChild(restaurant: snapshot.data!);
        }),
      ),
    );
  }
}
