import 'package:customer_client/src/models/menu/menu_model.dart';
import 'package:customer_client/src/services/menu_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/menu_screen/menu_screen_child.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:customer_client/src/views/widgets/order_navigation_bar.dart';
import 'package:flutter/material.dart';

class MenuScreen extends StatefulWidget {
  const MenuScreen({super.key, required this.menuId});

  final int menuId;

  @override
  State<MenuScreen> createState() => _MenuScreenState();
}

class _MenuScreenState extends State<MenuScreen> {
  final MenuService _menuService = const MenuService();
  late Future<MenuModel> futureMenu;

  @override
  void initState() {
    super.initState();
    futureMenu = _menuService.getMenu(widget.menuId);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Ime menija"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(future: futureMenu, builder: (_, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const LoadingScreen();
        } else if (snapshot.hasError) {
          return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
         } else if (!snapshot.hasData) {
          return const EmptyScreen(message: "This restaurant doesn't have this menu.");
        } else {
          return MenuScreenChild(menu: snapshot.data!);
        }
      }),
      bottomNavigationBar: const OrderNavigationBar(),
    );
  }
}
