import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/restaurant_screen.dart';
import 'package:customer_client/src/views/widgets/cards/restaurant_card.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantsScreen extends StatelessWidget {
  const RestaurantsScreen({super.key});

  final RestaurantService restaurantService =
      const RestaurantService(baseUrl: 'http://192.168.0.107:3000/restaurants');

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

          return ListView.builder(
            itemCount: restaurantsSnapshot.data!.length,
            itemBuilder: (ctx, index) => InkWell(
              onTap: () {
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (ctx) => RestaurantScreen(
                      restaurantId: restaurantsSnapshot.data![index].id,
                    ),
                  ),
                );
              },
              child: RestaurantCard(
                restaurantCard: restaurantsSnapshot.data![index],
              ),
            ),
          );
        },
      ),
    );
  }
}
