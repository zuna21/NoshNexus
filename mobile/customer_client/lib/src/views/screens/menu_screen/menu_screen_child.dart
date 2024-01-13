import 'package:customer_client/src/models/menu/menu_model.dart';
import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:flutter/material.dart';

class MenuScreenChild extends StatelessWidget {
  const MenuScreenChild({super.key, required this.menu, this.menuItems});

  final MenuModel menu;
  final List<MenuItemCardModel>? menuItems;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 10),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                menu.restaurant!.name!,
                style: Theme.of(context).textTheme.titleMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onBackground),
              ),
              SizedBox(
                child: Row(
                  children: [
                    Text(
                      menu.totalMenuItems!.toString(),
                      style: Theme.of(context).textTheme.titleMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    const SizedBox(
                      width: 10,
                    ),
                    const Icon(Icons.restaurant, size: 15),
                  ],
                ),
              )
            ],
          ),
        ),
        const SizedBox(
          height: 15,
        ),
        Container(
          padding: const EdgeInsets.all(10),
          width: double.infinity,
          decoration: BoxDecoration(
              border: Border.all(color: Theme.of(context).colorScheme.primary)),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                "Description",
                style: Theme.of(context)
                    .textTheme
                    .titleMedium!
                    .copyWith(color: Theme.of(context).colorScheme.primary),
              ),
              const SizedBox(
                height: 10,
              ),
              Text(
                menu.description!,
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onPrimaryContainer),
              ),
            ],
          ),
        ),
        if (menuItems == null || menuItems!.isEmpty)
          Padding(
            padding: const EdgeInsets.only(top: 50),
            child: Text(
              "This menu doesn't have menu items.",
              style: Theme.of(context).textTheme.titleLarge!.copyWith(
                    color: Theme.of(context).colorScheme.onBackground,
                  ),
            ),
          )
        else
          Expanded(
            child: Padding(
              padding: const EdgeInsets.only(top: 15),
              child: ListView.builder(
                itemCount: menuItems!.length,
                itemBuilder: (_, index) => MenuItemCard(
                  menuItem: menuItems![index],
                ),
              ),
            ),
          ),
      ],
    );
  }
}
