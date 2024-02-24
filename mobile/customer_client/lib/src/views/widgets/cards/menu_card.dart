import 'package:customer_client/src/models/menu/menu_card_model.dart';
import 'package:flutter/material.dart';
import 'package:flutter_translate/flutter_translate.dart';

class MenuCard extends StatelessWidget {
  const MenuCard({super.key, required this.menu, this.onMenu});

  final MenuCardModel menu;
  final void Function(int menuId)? onMenu;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Container(
        padding: const EdgeInsets.all(10),
        height: 300,
        decoration: BoxDecoration(
          border: Border.all(color: Theme.of(context).colorScheme.primary),
          borderRadius: BorderRadius.circular(5),
        ),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Text(
              menu.name!,
              style: Theme.of(context)
                  .textTheme
                  .titleLarge!
                  .copyWith(color: Theme.of(context).colorScheme.primary),
            ),
            Text(
              menu.description!.length > 350 ? "${menu.description!.substring(0, 350)}..." : menu.description!,
              style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                  color: Theme.of(context).colorScheme.onPrimaryContainer),
            ),
            ElevatedButton(
              onPressed: () {
                onMenu!(menu.id!);
              },
              style: ElevatedButton.styleFrom(
                backgroundColor:
                    Theme.of(context).colorScheme.onSecondaryContainer,
                foregroundColor: Theme.of(context).colorScheme.secondaryContainer,
              ),
              child: Text(translate("View More")),
            ),
            Row(
              children: [
                Row(
                  children: [
                    Text(
                      menu.menuItemNumber!.toString(),
                      style: Theme.of(context).textTheme.titleMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onPrimaryContainer,
                          fontSize: 25),
                    ),
                    const SizedBox(width: 5,),
                    Icon(
                      Icons.restaurant,
                      color: Theme.of(context).colorScheme.onPrimaryContainer,
                    ),
                  ],
                ),
                const Spacer(),
                Text(
                  menu.restaurantName!,
                  style: Theme.of(context).textTheme.titleLarge!.copyWith(
                        color: Theme.of(context).colorScheme.onPrimaryContainer,
                      ),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }
}
