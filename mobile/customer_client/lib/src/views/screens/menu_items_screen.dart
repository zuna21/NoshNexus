import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/providers/order_provider.dart';
import 'package:customer_client/src/services/menu_item_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
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
  final MenuItemService _menuItemService = const MenuItemService();

  void _onAddMenuItem(MenuItemCardModel menuItem) {
    ref.read(orderProvider.notifier).addMenuItem(menuItem);
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
      future: _menuItemService.getBestMenuItems(widget.restaurantId),
      builder: ((context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const LoadingScreen();
        }

        if (snapshot.hasError) {
          return ErrorScreen(errorMessage: "Error: ${snapshot.error!.toString()}");
        }

        if (!snapshot.hasData || snapshot.data!.isEmpty) {
          return const EmptyScreen(message: "There is no menu items.");
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
