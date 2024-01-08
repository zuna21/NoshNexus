import 'package:customer_client/src/services/restaurant_service.dart';
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
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (restaurantsSnapshot.hasError) {
            return Center(
              child: Text(
                "${restaurantsSnapshot.error}",
                style: Theme.of(context)
                    .textTheme
                    .bodyLarge!
                    .copyWith(color: Colors.white),
              ),
            );
          }

          return RestaurantsScreenChild(restaurants: restaurantsSnapshot.data!);
        },
      ),
    );
  }
}
