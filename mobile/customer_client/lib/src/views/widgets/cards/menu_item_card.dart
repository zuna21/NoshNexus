import 'package:customer_client/login_control.dart';
import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/services/menu_item_service.dart';
import 'package:flutter/material.dart';

class MenuItemCard extends StatefulWidget {
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
  State<MenuItemCard> createState() => _MenuItemCardState();
}

class _MenuItemCardState extends State<MenuItemCard> {
  final MenuItemService _menuItemService = const MenuItemService();
  final LoginControl _loginControl = const LoginControl();

  void _addToFavourite() async {
    var hasUser = await _loginControl.isUserLogged(context);
    if (!hasUser) return;
    try {
      final response =
          await _menuItemService.addFavouriteMenuItem(widget.menuItem.id!);
      if (!response || !context.mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Succesffully added to favourite"),
        ),
      );
    } catch (err) {
      if (context.mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Failed to add to favourite."),
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Container(
        decoration: BoxDecoration(
          border: widget.menuItem.hasSpecialOffer!
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
                    widget.menuItem.name!,
                    style: Theme.of(context).textTheme.titleLarge!.copyWith(
                        color: Theme.of(context).colorScheme.onBackground),
                  ),
                  Row(
                    children: [
                      IconButton(
                          onPressed: () {
                            _addToFavourite();
                          },
                          icon: const Icon(Icons.favorite_outline)),
                      widget.canRemoveItem
                          ? IconButton(
                              onPressed: widget.onRemoveItem,
                              icon: const Icon(Icons.remove),
                            )
                          : IconButton(
                              onPressed: () {
                                if (widget.onAddMenuItem == null) {
                                  return;
                                }
                                widget.onAddMenuItem!(widget.menuItem);
                              },
                              icon: const Icon(Icons.add),
                            ),
                    ],
                  )
                ],
              ),
              Text(
                widget.menuItem.description!,
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
                      image: NetworkImage(widget.menuItem.profileImage!),
                    ),
                  ),
                  const Spacer(),
                  if (widget.menuItem.hasSpecialOffer!)
                    Text(
                      widget.menuItem.price!.toStringAsFixed(2),
                      style: Theme.of(context).textTheme.titleMedium!.copyWith(
                            color: Colors.red,
                            decoration: TextDecoration.lineThrough,
                          ),
                    ),
                  const SizedBox(
                    width: 15,
                  ),
                  Text(
                    widget.menuItem.hasSpecialOffer!
                        ? widget.menuItem.specialOfferPrice!.toStringAsFixed(2)
                        : widget.menuItem.price!.toStringAsFixed(2),
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
