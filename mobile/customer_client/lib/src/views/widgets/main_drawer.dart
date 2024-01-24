import 'package:customer_client/login_control.dart';
import 'package:customer_client/src/views/screens/favourite_menu_items_screen/favourite_menu_items_screen.dart';
import 'package:customer_client/src/views/screens/favourite_restaurants_screen/favourite_restaurants_screen.dart';
import 'package:customer_client/src/views/screens/order_history_screen/order_history_screen.dart';
import 'package:customer_client/src/views/screens/restaurants_screen/restaurants_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class MainDrawer extends StatelessWidget {
  const MainDrawer({super.key});

  final LoginControl _loginControl = const LoginControl();

  @override
  Widget build(BuildContext context) {
    return Drawer(
      // Add a ListView to the drawer. This ensures the user can scroll
      // through the options in the drawer if there isn't enough vertical
      // space to fit everything.
      child: ListView(
        // Important: Remove any padding from the ListView.
        padding: EdgeInsets.zero,
        children: [
          DrawerHeader(
            decoration: BoxDecoration(
                color: Theme.of(context).colorScheme.primaryContainer),
            child: const Text('Nosh Nexus'),
          ),
          ListTile(
            leading: const Icon(Icons.storefront),
            title: const Text('Restaurants'),
            onTap: () {
              Navigator.of(context).push(
                MaterialPageRoute(
                  builder: (ctx) => const RestaurantsScreen(),
                ),
              );
            },
          ),
          ListTile(
            leading: const Icon(Icons.history),
            title: const Text('Order History'),
            onTap: () async {
              final haveUser = await _loginControl.isUserLogged(context);
              if (haveUser && context.mounted) {
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (_) => const OrderHistoryScreen(),
                  ),
                );
              }
            },
          ),
          ListTile(
            leading: const Icon(Icons.local_dining),
            title: const Text('Favourite Menu Items'),
            onTap: () async {
              final haveUser = await _loginControl.isUserLogged(context);
              if (haveUser && context.mounted) {
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (_) => const FavouriteMenuItemsScreen(),
                  ),
                );
              }
            },
          ),
          
          ListTile(
            leading: const Icon(Icons.chair),
            title: const Text('Favourite Restaurants'),
            onTap: () async {
              final haveUser = await _loginControl.isUserLogged(context);
              if (haveUser && context.mounted) {
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (_) => const FavouriteRestaurantsScreen(),
                  ),
                );
              }
            },
          ),
          ListTile(
            leading: const Icon(Icons.logout),
            title: const Text('Log Out'),
            onTap: () async {
              const storage = FlutterSecureStorage();
              final token = await storage.read(key: "token");
              if (token == null) return;
              await storage.deleteAll();
              if (context.mounted) {
                Navigator.of(context).pushAndRemoveUntil(
                  MaterialPageRoute(
                    builder: (context) => const RestaurantsScreen(),
                  ),
                  (route) => false,
                );
                ScaffoldMessenger.of(context).showSnackBar(
                  const SnackBar(
                    content: Text("You are successfully logged out"),
                  ),
                );
              }
            },
          ),
        ],
      ),
    );
  }
}
