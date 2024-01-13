import 'package:customer_client/src/models/menu/menu_model.dart';
import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/services/menu_item_service.dart';
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
  final MenuItemService _menuItemService = const MenuItemService();

  Widget content = const LoadingScreen();
  MenuModel? menu;
  List<MenuItemCardModel>? menuItems;

  @override
  void initState() {
    super.initState();
    _loadMenuAndMenuItems();
  }

  void _loadMenuAndMenuItems() async {
    try {
      menu = await _menuService.getMenu(widget.menuId);
      menuItems = await _menuItemService.getMenuMenuItems(widget.menuId);

      if (menu == null) {
        setState(() {
          content = const EmptyScreen(message: "There is no menu");
        });
      } else if (menu != null && (menuItems == null || menuItems!.isEmpty)) {
        setState(() {
          content = MenuScreenChild(menu: menu!);
        });
      } else {
        setState(() {
          content = MenuScreenChild(
            menu: menu!,
            menuItems: menuItems!,
          );
        });
      }
    } catch (error) {
      setState(() {
        content = ErrorScreen(errorMessage: "Error $error");
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Ime menija"),
      ),
      drawer: const MainDrawer(),
      body: content,
      bottomNavigationBar: const OrderNavigationBar(),
    );
  }
}
