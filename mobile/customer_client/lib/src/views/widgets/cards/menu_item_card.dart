import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:flutter/material.dart';

class MenuItemCard extends StatelessWidget {
  const MenuItemCard({
    super.key,
    required this.menuItem,
    this.onAddMenuItem,
    this.onRemoveItem,
    this.canRemoveItem = false,
  });

  final MenuItemCardModel menuItem;
  final bool canRemoveItem;
  final void Function(MenuItemCardModel menuItem)? onAddMenuItem;
  final void Function()? onRemoveItem;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Container(
        decoration: BoxDecoration(
          border: menuItem.hasSpecialOffer!
              ? Border.all(
                  color: Theme.of(context).colorScheme.primary,
                )
              : null,
          borderRadius: BorderRadius.circular(10),
        ),
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Column(
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    menuItem.name!,
                    style: Theme.of(context).textTheme.titleLarge!.copyWith(
                        color: Theme.of(context).colorScheme.onBackground),
                  ),
                  canRemoveItem
                      ? IconButton(
                          onPressed: onRemoveItem,
                          icon: const Icon(Icons.remove),
                        )
                      : IconButton(
                          onPressed: () {
                            if (onAddMenuItem == null) {
                              return;
                            }
                            onAddMenuItem!(menuItem);
                          },
                          icon: const Icon(Icons.add),
                        ),
                ],
              ),
              Text(
                menuItem.description!,
                style: Theme.of(context).textTheme.bodySmall!.copyWith(
                      color: Theme.of(context).colorScheme.onPrimaryContainer,
                    ),
              ),
              const SizedBox(
                height: 10,
              ),
              Row(
                children: [
                  SizedBox(
                    height: 50,
                    width: 50,
                    child: Image(
                      fit: BoxFit.cover,
                      filterQuality: FilterQuality.low,
                      image: NetworkImage(menuItem.profileImage!),
                    ),
                  ),
                  const Spacer(),
                  if (menuItem.hasSpecialOffer!)
                    Text(
                      menuItem.price!.toStringAsFixed(2),
                      style: Theme.of(context).textTheme.titleMedium!.copyWith(
                            color: Colors.red,
                            decoration: TextDecoration.lineThrough,
                          ),
                    ),
                  const SizedBox(
                    width: 15,
                  ),
                  Text(
                    menuItem.hasSpecialOffer!
                        ? menuItem.specialOfferPrice!.toStringAsFixed(2)
                        : menuItem.price!.toStringAsFixed(2),
                    style: Theme.of(context).textTheme.titleLarge!.copyWith(
                        color: Theme.of(context).colorScheme.onBackground),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }
}
