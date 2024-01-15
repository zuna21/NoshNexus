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
  final int _pageSize = 10;
  final ScrollController _scrollController = ScrollController();

  List<MenuCardModel>? menus;
  int pageIndex = 0;
  bool isLoading = false;
  bool hasMore = true;
  String? error;

  @override
  void initState() {
    super.initState();
    _loadMenus();
    _onScrollToBottom();
  }

  void _loadMenus() async {
    if (!hasMore || isLoading) return;
    isLoading = true;
    try {
      final loadedMenus = await _menuService.getMenus(
          restaurantId: widget.restaurantId, pageIndex: pageIndex);

      if (loadedMenus.isEmpty || loadedMenus.length < _pageSize) {
        hasMore = false;
      }

      setState(() {
        if (menus == null) {
          menus = [...loadedMenus];
        } else {
          menus = [...menus!, ...loadedMenus];
        }
        isLoading = false;
      });
    } catch (err) {
      setState(() {
        error = err.toString();
      });
    }
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

  void _onScrollToBottom() {
    _scrollController.addListener(() {
      if (_scrollController.position.maxScrollExtent == _scrollController.offset) {
        pageIndex++;
        _loadMenus();
      }
    });
  }

  Future<void> _onRefresh() async {
    setState(() {
      pageIndex = 0;
      menus = null;
      isLoading = false;
      hasMore = true;
    });

    _loadMenus();
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    Widget? content;

    if (menus == null) {
      content = const LoadingScreen();
    } else if (menus!.isEmpty) {
      content = const EmptyScreen(message: "This restaurant haven't menus.");
    } else if (error != null) {
      content = ErrorScreen(errorMessage: "Error: $error");
    } else {
      content = RefreshIndicator(
        onRefresh: _onRefresh,
        child: ListView.builder(
            controller: _scrollController,
            itemCount: menus!.length + 1,
            itemBuilder: (_, index) {
              if (index < menus!.length) {
                return MenuCard(
                  menu: menus![index],
                  onMenu: _onMenu,
                );
              } else {
                return const Padding(
                  padding: EdgeInsets.all(15),
                  child: Center(
                    child: CircularProgressIndicator(),
                  ),
                );
              }
            }),
      );
    }

    return content;
  }
}
