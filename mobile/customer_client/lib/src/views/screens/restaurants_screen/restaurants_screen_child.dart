import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen.dart';
import 'package:customer_client/src/views/widgets/cards/restaurant_card.dart';
import 'package:flutter/material.dart';

class RestaurantsScreenChild extends StatelessWidget {
  const RestaurantsScreenChild({super.key, required this.restaurants});

  final List<RestaurantCardModel> restaurants;

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: restaurants.length,
      itemBuilder: (ctx, index) => InkWell(
        onTap: () {
          Navigator.of(context).push(
            MaterialPageRoute(
              builder: (ctx) => RestaurantScreen(
                restaurantId: restaurants[index].id,
              ),
            ),
          );
        },
        child: RestaurantCard(
          restaurantCard: restaurants[index],
        ),
      ),
    );
  }
}
