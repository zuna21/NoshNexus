import 'package:customer_client/src/models/restaurant/restaurant_details_model.dart';
import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen_child.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantScreen extends StatefulWidget {
  const RestaurantScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  State<RestaurantScreen> createState() => _RestaurantScreenState();
}

class _RestaurantScreenState extends State<RestaurantScreen> {

  final RestaurantService _restaurantService = const RestaurantService();
  late Future<RestaurantDetailsModel> futureRestaurant;

  @override
  void initState() {
    super.initState();
    futureRestaurant = _restaurantService.getRestaurant(widget.restaurantId);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Nosh Nexus"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(
        future: _restaurantService.getRestaurant(widget.restaurantId),
        builder: ((context, snapshot) {
          if (snapshot.hasData) {
            return RestaurantScreenChild(restaurant: snapshot.data!);
          } else if (snapshot.hasError) {
            return ErrorScreen(errorMessage: "Error ${snapshot.error}");
          } 
          return const LoadingScreen();
        }),
      ),
    );
  }
}
