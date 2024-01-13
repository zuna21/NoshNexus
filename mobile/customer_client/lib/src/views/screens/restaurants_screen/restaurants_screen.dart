import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurants_screen/restaurants_screen_child.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantsScreen extends StatefulWidget {
  const RestaurantsScreen({super.key});

  @override
  State<RestaurantsScreen> createState() => _RestaurantsScreenState();
}

class _RestaurantsScreenState extends State<RestaurantsScreen> {
  final RestaurantService restaurantService = const RestaurantService();
  late Future<List<RestaurantCardModel>> futureRestaurants;

  @override
  void initState() {
    super.initState();
    futureRestaurants = restaurantService.getRestaurants();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Restaurants"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(
        future: futureRestaurants,
        builder: (ctx, snapshot) {
          if (snapshot.hasData && snapshot.data!.isNotEmpty) {
            return RestaurantsScreenChild(restaurants: snapshot.data!);
          } else if (snapshot.hasError) {
            return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
          } 

          return const LoadingScreen();
        },
      ),
    );
  }
}
