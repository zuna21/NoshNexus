import 'package:customer_client/src/services/menu_item_service.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:flutter/material.dart';

class MenuItemsScreen extends StatefulWidget {
  const MenuItemsScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  State<MenuItemsScreen> createState() => _MenuItemsScreenState();
}

class _MenuItemsScreenState extends State<MenuItemsScreen> {
  final MenuItemService _menuItemService =
      const MenuItemService(baseUrl: 'http://192.168.0.107:3000/menu-items');

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
              menuItem: snapshot.data![index],
            );
          }),
        );
      }),
    );
  }
}
