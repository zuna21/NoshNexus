import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurants_screen/restaurants_screen_child.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantsScreen extends StatelessWidget {
  const RestaurantsScreen({super.key});

  final RestaurantService restaurantService = const RestaurantService();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Restaurants"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(
        future: restaurantService.getRestaurants(),
        builder: (ctx, restaurantsSnapshot) {
          if (restaurantsSnapshot.connectionState == ConnectionState.waiting) {
            return const LoadingScreen();
          }

          if (!restaurantsSnapshot.hasData ||
              restaurantsSnapshot.data!.isEmpty) {
            return const EmptyScreen(message: "There are no restaurants.");
          }

          if (restaurantsSnapshot.hasError) {
            return ErrorScreen(
                errorMessage:
                    "Error: ${restaurantsSnapshot.error!.toString()}");
          }

          return RestaurantsScreenChild(restaurants: restaurantsSnapshot.data!);
        },
      ),
    );
  }
}
