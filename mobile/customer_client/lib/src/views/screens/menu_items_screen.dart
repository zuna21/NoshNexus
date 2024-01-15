import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/providers/order_provider.dart';
import 'package:customer_client/src/services/menu_item_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class MenuItemsScreen extends ConsumerStatefulWidget {
  const MenuItemsScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  ConsumerState<MenuItemsScreen> createState() => _MenuItemsScreenState();
}

class _MenuItemsScreenState extends ConsumerState<MenuItemsScreen> {
  final MenuItemService _menuItemService = const MenuItemService();
  final int _pageSize = 10;
  final ScrollController _scrollController = ScrollController();

  List<MenuItemCardModel>? menuItems;
  String? error;
  int pageIndex = 0;
  bool hasMore = true;
  bool isLoading = false;

  @override
  void initState() {
    super.initState();
    _loadMenuItems();
    _scrollToBottom();
  }

  void _loadMenuItems() async {
    if (!hasMore || isLoading) return;
    isLoading = true;
    try {
      final loadedMenuItems = await _menuItemService.getBestMenuItems(
        restaurantId: widget.restaurantId,
        pageIndex: pageIndex,
      );

      if (loadedMenuItems.isEmpty || loadedMenuItems.length < _pageSize) {
        hasMore = false;
      }

      setState(() {
        if (menuItems == null) {
          menuItems = [...loadedMenuItems];
        } else {
          menuItems = [...menuItems!, ...loadedMenuItems];
        }
        isLoading = false;
      });
    } catch (err) {
      setState(() {
        error = err.toString();
      });
    }
  }

  void _scrollToBottom() {
    _scrollController.addListener(() {
      if (_scrollController.position.maxScrollExtent ==
          _scrollController.offset) {
        pageIndex++;
        _loadMenuItems();
      }
    });
  }

  void _onAddMenuItem(MenuItemCardModel menuItem) {
    ref.read(orderProvider.notifier).addMenuItem(menuItem);
  }

  Future<void> _onRefresh() async {
    setState(() {
      pageIndex = 0;
      menuItems = null;
      hasMore = true;
      isLoading = false;
    });

    _loadMenuItems();
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    Widget? content;

    if (menuItems == null) {
      content = const LoadingScreen();
    } else if (menuItems!.isEmpty) {
      content = const EmptyScreen(
          message: "This restaurant doesn't have menu items.");
    } else if (error != null) {
      content = ErrorScreen(errorMessage: "Error: $error");
    } else {
      content = RefreshIndicator(
        onRefresh: _onRefresh,
        child: ListView.builder(
            controller: _scrollController,
            itemCount: menuItems!.length + 1,
            itemBuilder: (_, index) {
              if (index < menuItems!.length) {
                return MenuItemCard(
                  menuItem: menuItems![index],
                  onAddMenuItem: _onAddMenuItem,
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
