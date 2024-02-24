import 'package:customer_client/login_control.dart';
import 'package:customer_client/src/views/screens/account_screen/account_screen.dart';
import 'package:customer_client/src/views/screens/favourite_menu_items_screen/favourite_menu_items_screen.dart';
import 'package:customer_client/src/views/screens/favourite_restaurants_screen/favourite_restaurants_screen.dart';
import 'package:customer_client/src/views/screens/order_history_screen/order_history_screen.dart';
import 'package:customer_client/src/views/screens/restaurants_screen/restaurants_screen.dart';
import 'package:customer_client/src/views/widgets/select_language_sheet.dart';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter_translate/flutter_translate.dart';

class MainDrawer extends StatefulWidget {
  const MainDrawer({super.key});

  @override
  State<MainDrawer> createState() => _MainDrawerState();
}

class _MainDrawerState extends State<MainDrawer> {
  final LoginControl _loginControl = const LoginControl();

  void _onChangeLanguage() async {
    await showModalBottomSheet(
      context: context,
      builder: (_) => const SelectLanguageSheet(),
    );
  }

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
            leading: const Icon(Icons.person),
            title: Text(translate("Account")),
            onTap: () async {
              final haveUser = await _loginControl.isUserLogged(context);
              if (haveUser && context.mounted) {
                Navigator.of(context).push(
                  MaterialPageRoute(
                    builder: (_) => const AccountScreen(),
                  ),
                );
              }
            },
          ),
          ListTile(
            leading: const Icon(Icons.storefront),
            title: Text(translate('Restaurants')),
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
            title: Text(translate('Order History')),
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
            title: Text(translate('Favourite Menu Items')),
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
            title: Text(translate('Favourite Restaurants')),
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
              leading: const Icon(Icons.language),
              title: Text(translate("Language")),
              onTap: _onChangeLanguage),
          ListTile(
            leading: const Icon(Icons.logout),
            title: Text(translate('Log Out')),
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
