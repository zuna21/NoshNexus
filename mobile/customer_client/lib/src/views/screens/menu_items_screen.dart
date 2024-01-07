import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/providers/order_provider.dart';
import 'package:customer_client/src/services/menu_item_service.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class MenuItemsScreen extends ConsumerStatefulWidget {
  const MenuItemsScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  ConsumerState<MenuItemsScreen> createState() => _MenuItemsScreenState();
}

class _MenuItemsScreenState extends ConsumerState<MenuItemsScreen> {
  final MenuItemService _menuItemService =
      const MenuItemService(baseUrl: 'http://192.168.0.107:3000/menu-items');

  void _onAddMenuItem(MenuItemCardModel menuItem) {
    ref.read(orderProvider.notifier).addMenuItem(menuItem);
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: _menuItemService.getBestMenuItems(widget.restaurantId),
      builder: ((context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Center(
            child: CircularProgressIndicator(),
          );
        }

        if (snapshot.hasError) {
          return Center(
            child: Text(
              "${snapshot.error}",
              style: const TextStyle(color: Colors.white),
            ),
          );
        }

        if (!snapshot.hasData) {
          return const Center(
            child: Text(
              "There is no menu Items",
              style: TextStyle(color: Colors.white),
            ),
          );
        }

        return ListView.builder(
          itemCount: snapshot.data!.length,
          itemBuilder: ((context, index) {
            return MenuItemCard(
              onAddMenuItem: _onAddMenuItem,
              menuItem: snapshot.data![index],
            );
          }),
        );
      }),
    );
  }
}
