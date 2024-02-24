import 'package:customer_client/src/views/screens/menu_items_screen.dart';
import 'package:customer_client/src/views/screens/menus_screen.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:customer_client/src/views/widgets/order_navigation_bar.dart';
import 'package:flutter/material.dart';
import 'package:flutter_translate/flutter_translate.dart';

class SelectionScreen extends StatelessWidget {
  const SelectionScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 0,
      length: 2,
      child: Scaffold(
        appBar: AppBar(
          title: const Text("Nosh Nexus"),
          bottom: TabBar(
            tabs: [
              Tab(
                child: Text(translate("Menu Items")),
              ),
              Tab(
                child: Text(translate("Menus")),
              ),
            ],
          ),
        ),
        drawer: const MainDrawer(),
        body: TabBarView(
          children: [
            MenuItemsScreen(restaurantId: restaurantId),
            MenusScreen(
              restaurantId: restaurantId,
            )
          ],
        ),
        bottomNavigationBar: const OrderNavigationBar(),
      ),
    );
  }
}
