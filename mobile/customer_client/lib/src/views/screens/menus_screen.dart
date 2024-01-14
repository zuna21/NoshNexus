import 'package:customer_client/src/models/menu/menu_card_model.dart';
import 'package:customer_client/src/services/menu_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/menu_screen/menu_screen.dart';
import 'package:customer_client/src/views/widgets/cards/menu_card.dart';
import 'package:flutter/material.dart';

class MenusScreen extends StatefulWidget {
  const MenusScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  State<MenusScreen> createState() => _MenusScreenState();
}

class _MenusScreenState extends State<MenusScreen> {
  final MenuService _menuService = const MenuService();
  late Future<List<MenuCardModel>> futureMenus;

  @override
  void initState() {
    super.initState();
    futureMenus = _menuService.getMenus(widget.restaurantId);
  }

  void _onMenu(int menuId) {
    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (_) => MenuScreen(
          menuId: menuId,
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: futureMenus,
        builder: (ctx, snapshot) {
          if (snapshot.hasData && snapshot.data!.isNotEmpty) {
            return ListView.builder(
              itemCount: snapshot.data!.length,
              itemBuilder: ((context, index) {
                return MenuCard(
                  menu: snapshot.data![index],
                  onMenu: _onMenu,
                );
              }),
            );
          } else if (snapshot.hasError) {
            return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
          } else if (snapshot.hasData && snapshot.data!.isEmpty) {
            return const EmptyScreen(message: "This restaurant doesn't have menus.");
          }

          return const LoadingScreen();
        });
  }
}
