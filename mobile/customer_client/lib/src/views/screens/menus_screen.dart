import 'package:customer_client/src/services/menu_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/cards/menu_card.dart';
import 'package:flutter/material.dart';

class MenusScreen extends StatelessWidget {
  const MenusScreen({super.key});

  final MenuService _menuService = const MenuService();

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: _menuService.getMenus(1),
        builder: (ctx, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const LoadingScreen();
          }

          if (snapshot.hasError) {
            return ErrorScreen(
                errorMessage: "Error: ${snapshot.error!.toString()}");
          }

          if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return const EmptyScreen(message: "There is no menus");
          }

          return ListView.builder(
            itemCount: snapshot.data!.length,
            itemBuilder: ((context, index) {
              return MenuCard(
                menu: snapshot.data![index],
              );
            }),
          );
        });
  }
}
