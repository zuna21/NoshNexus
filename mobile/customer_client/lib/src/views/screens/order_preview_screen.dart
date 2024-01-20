import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/models/order/create_order_model.dart';
import 'package:customer_client/src/providers/menu_item_provider/menu_item_provider.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:customer_client/src/views/widgets/table_dropdown.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class OrderPreviewScreen extends ConsumerStatefulWidget {
  const OrderPreviewScreen({super.key});

  @override
  ConsumerState<OrderPreviewScreen> createState() => _OrderPreviewScreenState();
}

class _OrderPreviewScreenState extends ConsumerState<OrderPreviewScreen> {
  final order = CreateOrderModel(menuItemIds: [], note: null, tableId: null);
  final _note = TextEditingController();

  void _onAddTable(int tableId) {
    order.tableId = tableId;
  }

  void _onSubmit() {
    order.menuItemIds = ref.read(menuItemProvider).map((e) => e.id!).toList();
    order.note = _note.text;
    if (order.menuItemIds == null || order.menuItemIds!.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Please select at least one menu item"),
        ),
      );
    } else if (order.tableId == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Please select table where you sit"),
        ),
      );
    } else {
      print(order.menuItemIds);
      print(order.tableId);
      print(order.note);
    }
  }

  @override
  void dispose() {
    _note.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    List<MenuItemCardModel> menuItems = ref.watch(menuItemProvider);
    double totalPrice = 0;
    for (final item in menuItems) {
      if (item.hasSpecialOffer!) {
        totalPrice += item.specialOfferPrice!;
      } else {
        totalPrice += item.price!;
      }
    }

    Widget content = menuItems.isEmpty
        ? Center(
            child: Text(
              "You did not select any item.",
              style: Theme.of(context)
                  .textTheme
                  .bodyLarge!
                  .copyWith(color: Theme.of(context).colorScheme.onBackground),
            ),
          )
        : Column(children: [
            Padding(
              padding: const EdgeInsets.symmetric(vertical: 10),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  Container(
                    padding: const EdgeInsets.symmetric(vertical: 10),
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10),
                      color: Theme.of(context).colorScheme.secondaryContainer,
                      border: Border.all(
                          color: Theme.of(context).colorScheme.primary),
                    ),
                    height: 100,
                    width: 100,
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          "Total Items",
                          style: Theme.of(context)
                              .textTheme
                              .bodyLarge!
                              .copyWith(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .onBackground),
                        ),
                        const Spacer(),
                        Text(
                          ref.watch(menuItemProvider).length.toString(),
                          style: Theme.of(context)
                              .textTheme
                              .titleLarge!
                              .copyWith(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .onSecondaryContainer),
                        ),
                      ],
                    ),
                  ),
                  Container(
                    padding: const EdgeInsets.symmetric(vertical: 10),
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10),
                      color: Theme.of(context).colorScheme.secondaryContainer,
                      border: Border.all(
                          color: Theme.of(context).colorScheme.primary),
                    ),
                    height: 100,
                    width: 100,
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          "Price",
                          style: Theme.of(context)
                              .textTheme
                              .bodyLarge!
                              .copyWith(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .onBackground),
                        ),
                        const Spacer(),
                        Text(
                          totalPrice.toStringAsFixed(2),
                          style: Theme.of(context)
                              .textTheme
                              .titleLarge!
                              .copyWith(
                                  color: Theme.of(context)
                                      .colorScheme
                                      .onSecondaryContainer),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.symmetric(
                vertical: 10,
                horizontal: 30,
              ),
              child: Column(
                children: [
                  TableDropdown(
                    onSelectTable: _onAddTable,
                  ),
                  const SizedBox(
                    height: 15,
                  ),
                  TextField(
                    enableSuggestions: false,
                    controller: _note,
                    autocorrect: false,
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.onBackground,
                    ),
                    decoration: const InputDecoration(
                        border: OutlineInputBorder(),
                        labelText: "Note for waiter (optional)",
                        hintText: "primjer: Kafa sa mlijeko"),
                  )
                ],
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            Expanded(
              child: ListView.builder(
                itemCount: menuItems.length,
                itemBuilder: ((context, index) {
                  return MenuItemCard(
                    canRemoveItem: true,
                    onRemoveItem: () {
                      ref.read(menuItemProvider.notifier).removeMenuItem(index);
                    },
                    menuItem: menuItems[index],
                  );
                }),
              ),
            ),
          ]);

    return Scaffold(
      appBar: AppBar(
        title: const Text("Order Preview"),
      ),
      body: content,
      floatingActionButton: Draggable(
        feedback: ElevatedButton.icon(
          onPressed: () {},
          icon: const Icon(Icons.send_outlined),
          label: Text(
            "Naruci",
            style: Theme.of(context)
                .textTheme
                .bodyLarge!
                .copyWith(fontWeight: FontWeight.bold),
          ),
          style: ElevatedButton.styleFrom(
              minimumSize: const Size(150, 50),
              backgroundColor: Theme.of(context).colorScheme.onBackground,
              foregroundColor: Theme.of(context).colorScheme.background),
        ),
        childWhenDragging: Container(),
        child: ElevatedButton.icon(
          onPressed: () {
            _onSubmit();
          },
          icon: const Icon(Icons.send_outlined),
          label: Text(
            "Naruci",
            style: Theme.of(context)
                .textTheme
                .bodyLarge!
                .copyWith(fontWeight: FontWeight.bold),
          ),
          style: ElevatedButton.styleFrom(
              minimumSize: const Size(150, 50),
              backgroundColor: Theme.of(context).colorScheme.onBackground,
              foregroundColor: Theme.of(context).colorScheme.background),
        ),
      ),
    );
  }
}
