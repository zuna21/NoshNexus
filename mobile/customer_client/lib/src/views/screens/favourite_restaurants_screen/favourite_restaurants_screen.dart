import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen.dart';
import 'package:customer_client/src/views/widgets/cards/restaurant_card.dart';
import 'package:flutter/material.dart';

class FavouriteRestaurantsScreen extends StatefulWidget {
  const FavouriteRestaurantsScreen({super.key});

  @override
  State<FavouriteRestaurantsScreen> createState() =>
      _FavouriteRestaurantsScreenState();
}

class _FavouriteRestaurantsScreenState
    extends State<FavouriteRestaurantsScreen> {
  final RestaurantService _restaurantService = const RestaurantService();
  late Future<List<RestaurantCardModel>> futureRestaurants;

  @override
  void initState() {
    super.initState();
    futureRestaurants = _restaurantService.getFavouriteRestaurants();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text("Favourite restaurants"),
        ),
        body: FutureBuilder(
            future: futureRestaurants,
            builder: (_, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return const LoadingScreen();
              } else if (snapshot.hasError) {
                return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
              } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
                return const EmptyScreen(
                    message: "You haven't favourite restaurants");
              } else {
                return ListView.builder(
                    itemCount: snapshot.data!.length,
                    itemBuilder: (_, index) {
                      return InkWell(
                        onTap: () {
                          Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (_) => RestaurantScreen(
                                  restaurantId: snapshot.data![index].id!),
                            ),
                          );
                        },
                        child: RestaurantCard(
                          restaurantCard: snapshot.data![index],
                        ),
                      );
                    });
              }
            }));
  }
}
