import 'package:customer_client/src/services/restaurant_service.dart';
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
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (snapshot.hasError) {
            return Center(
              child: Text(
                "${snapshot.error}",
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onBackground),
              ),
            );
          }

          if (!snapshot.hasData) {
            return Center(
              child: Text(
                "There is no restaurant.",
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onBackground),
              ),
            );
          }

          return RestaurantScreenChild(restaurant: snapshot.data!);
        }),
      ),
    );
  }
}
