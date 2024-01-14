import 'package:customer_client/src/models/order/order_card_model.dart';
import 'package:flutter/material.dart';

class OrderCardMenuItems extends StatelessWidget {
  const OrderCardMenuItems({super.key, required this.menuItems});

  final List<Items> menuItems;

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
        padding: const EdgeInsets.symmetric(
          horizontal: 5,
          vertical: 10,
        ),
        itemCount: menuItems.length,
        itemBuilder: (_, index) {
          return Column(
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    menuItems[index].name!,
                    style: Theme.of(context)
                        .textTheme
                        .bodyLarge!
                        .copyWith(color: Theme.of(context).colorScheme.primary),
                  ),
                  Text(
                    "\$ ${menuItems[index].price!.toStringAsFixed(2)}",
                    style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                        color: Theme.of(context).colorScheme.onBackground),
                  ),
                ],
              ),
              if (index != menuItems.length - 1) const Divider(),
            ],
          );
        });
  }
}
