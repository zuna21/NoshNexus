import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:flutter/material.dart';

class RestaurantCard extends StatelessWidget {
  const RestaurantCard({super.key, required this.restaurantCard});

  final RestaurantCardModel restaurantCard;

  @override
  Widget build(BuildContext context) {
    return Card(
      margin: const EdgeInsets.symmetric(vertical: 5),
      child: Stack(
        children: [
          SizedBox(
            height: 200,
            width: double.infinity,
            child: Image(
              image: NetworkImage(restaurantCard.profileImage),
              fit: BoxFit.cover,
              loadingBuilder: (context, child, loadingProgress) {
                if (loadingProgress == null) return child;
                return const Center(
                  child: CircularProgressIndicator(),
                );
              },
            ),
          ),
          Positioned(
            bottom: 0,
            left: 0,
            right: 0,
            child: Container(
              padding: const EdgeInsets.symmetric(vertical: 10, horizontal: 10),
              decoration: BoxDecoration(
                color: Colors.black.withOpacity(0.8),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(
                        restaurantCard.name,
                        style: Theme.of(context).textTheme.titleLarge!.copyWith(
                            color: Theme.of(context).colorScheme.primary,
                            fontSize: 30),
                      ),
                      Icon(restaurantCard.isOpen ? Icons.lock_open : Icons.lock,
                        color: restaurantCard.isOpen ? Colors.green : Colors.red,
                      )
                    ],
                  ),
                  const SizedBox(
                    height: 5,
                  ),
                  Text(
                    "${restaurantCard.city}, ${restaurantCard.address}",
                    style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                        color: Theme.of(context).colorScheme.onBackground),
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      const Icon(
                        Icons.public,
                        size: 15,
                      ),
                      const SizedBox(
                        width: 5,
                      ),
                      Text(
                        restaurantCard.country,
                        style: Theme.of(context).textTheme.bodySmall!.copyWith(
                            color: Theme.of(context).colorScheme.onBackground),
                      )
                    ],
                  )
                ],
              ),
            ),
          )
        ],
      ),
    );
  }
}
