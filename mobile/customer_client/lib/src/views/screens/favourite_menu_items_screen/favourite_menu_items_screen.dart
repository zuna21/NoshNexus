import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/providers/menu_item_provider/menu_item_provider.dart';
import 'package:customer_client/src/services/menu_item_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:customer_client/src/views/widgets/order_navigation_bar.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class FavouriteMenuItemsScreen extends ConsumerStatefulWidget {
  const FavouriteMenuItemsScreen({super.key});

  @override
  ConsumerState<FavouriteMenuItemsScreen> createState() =>
      _FavouriteMenuItemsScreenState();
}

class _FavouriteMenuItemsScreenState extends ConsumerState<FavouriteMenuItemsScreen> {
  final MenuItemService _menuItemService = const MenuItemService();
  late Future<List<MenuItemCardModel>> futureMenuItems;

  @override
  void initState() {
    super.initState();
    futureMenuItems = _menuItemService.getFavouriteMenuItems();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Favourite items"),
      ),
      body: FutureBuilder(
          future: futureMenuItems,
          builder: (_, snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return const LoadingScreen();
            } else if (snapshot.hasError) {
              return ErrorScreen(errorMessage: "Error ${snapshot.error}");
            } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
              return const EmptyScreen(
                  message: "You haven't favourite menu items");
            } else {
              return ListView.builder(
                  itemCount: snapshot.data!.length,
                  itemBuilder: (_, index) {
                    return MenuItemCard(
                      menuItem: snapshot.data![index],
                      onAddMenuItem: (menuItem) => ref.read(menuItemProvider.notifier).addMenuItem(menuItem),
                    );
                  });
            }
          }),
          bottomNavigationBar: const OrderNavigationBar(),
    );
  }
}
