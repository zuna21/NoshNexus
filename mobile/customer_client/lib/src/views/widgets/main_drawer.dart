import 'package:customer_client/login_control.dart';
import 'package:customer_client/src/views/screens/restaurants_screen/restaurants_screen.dart';
import 'package:flutter/material.dart';

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
            onTap: () {
              /* Navigator.of(context).push(
                MaterialPageRoute(
                  builder: (_) => const OrderHistoryScreen(),
                ),
              ); */
              _loginControl.openLoginDialog(context);
            },
          ),
        ],
      ),
    );
  }
}
