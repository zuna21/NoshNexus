import 'package:customer_client/src/models/menu_card_model.dart';
import 'package:customer_client/src/services/menu_service.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
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
                );
              }),
            );
          } else if (snapshot.hasError) {
            return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
          }

          return const LoadingScreen();
        });
  }
}
