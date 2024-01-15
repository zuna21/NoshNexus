import 'package:customer_client/src/models/menu/menu_model.dart';
import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/providers/order_provider.dart';
import 'package:customer_client/src/services/menu_item_service.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class MenuScreenChild extends ConsumerStatefulWidget {
  const MenuScreenChild({super.key, required this.menu});

  final MenuModel menu;

  @override
  ConsumerState<MenuScreenChild> createState() => _MenuScreenChildState();
}

class _MenuScreenChildState extends ConsumerState<MenuScreenChild> {
  final MenuItemService _menuItemService = const MenuItemService();
  final _pageSize = 10;
  final ScrollController _scrollController = ScrollController();

  List<MenuItemCardModel>? menuItems;
  String? error;
  int pageIndex = 0;
  bool isLoading = false;
  bool hasMore = true;

  @override
  void initState() {
    super.initState();
    _loadMenuItems();
    _onScrollToBottom();
  }

  void _loadMenuItems() async {
    if (!hasMore || isLoading) return;
    isLoading = true;
    try {
      final loadedMenuItems = await _menuItemService.getMenuMenuItems(
          menuId: widget.menu.id!, pageIndex: pageIndex);

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

  void _onScrollToBottom() {
    _scrollController.addListener(() {
      if (_scrollController.position.maxScrollExtent == _scrollController.offset) {
        pageIndex++;
        _loadMenuItems();
      }
    });
  }

  Future<void> _onRefresh() async {
    setState(() {
      pageIndex = 0;
      isLoading = false;
      hasMore = true;
      menuItems = null;
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
    Widget? menuItemsContent;
    if (menuItems == null) {
      menuItemsContent = const Padding(
        padding: EdgeInsets.only(
          top: 50,
        ),
        child: Center(
          child: CircularProgressIndicator(),
        ),
      );
    } else if (error != null) {
      menuItemsContent = Padding(
        padding: const EdgeInsets.only(
          top: 50,
        ),
        child: Center(
          child: Text("Error: $error"),
        ),
      );
    } else if (menuItems!.isEmpty) {
      menuItemsContent = Padding(
        padding: const EdgeInsets.only(top: 50),
        child: Center(
          child: Text(
            "This menu doesn't have menu items",
            style: Theme.of(context)
                .textTheme
                .titleLarge!
                .copyWith(color: Theme.of(context).colorScheme.onBackground),
          ),
        ),
      );
    } else {
      menuItemsContent = Expanded(
        child: Padding(
          padding: const EdgeInsets.only(top: 20),
          child: RefreshIndicator(
            onRefresh: _onRefresh,
            child: ListView.builder(
                controller: _scrollController,
                itemCount: menuItems!.length + 1,
                itemBuilder: (_, index) {
                  if (index < menuItems!.length) {
                    return MenuItemCard(
                      onAddMenuItem: (menuItem) {
                        ref.read(orderProvider.notifier).addMenuItem(menuItem);
                      },
                      menuItem: menuItems![index],);
                  } else {
                    return const Padding(
                      padding: EdgeInsets.all(15),
                      child: Center(
                        child: CircularProgressIndicator(),
                      ),
                    );
                  }
                }),
          ),
        ),
      );
    }

    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.only(
            top: 10,
            left: 10,
            right: 10,
          ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                widget.menu.restaurant!.name!,
                style: Theme.of(context).textTheme.titleMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onBackground),
              ),
              SizedBox(
                child: Row(
                  children: [
                    Text(
                      widget.menu.totalMenuItems!.toString(),
                      style: Theme.of(context).textTheme.titleMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    const SizedBox(
                      width: 10,
                    ),
                    const Icon(Icons.restaurant, size: 15),
                  ],
                ),
              )
            ],
          ),
        ),
        const SizedBox(
          height: 15,
        ),
        Container(
          padding: const EdgeInsets.all(10),
          width: double.infinity,
          decoration: BoxDecoration(
            border: Border.all(
              color: Theme.of(context).colorScheme.primary,
            ),
            borderRadius: BorderRadius.circular(5),
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                "Description",
                style: Theme.of(context)
                    .textTheme
                    .titleMedium!
                    .copyWith(color: Theme.of(context).colorScheme.primary),
              ),
              const SizedBox(
                height: 10,
              ),
              Text(
                widget.menu.description!,
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onPrimaryContainer),
              ),
            ],
          ),
        ),
        menuItemsContent
      ],
    );
  }
}
