import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:customer_client/src/providers/order_provider.dart';
import 'package:customer_client/src/views/widgets/cards/menu_item_card.dart';
import 'package:customer_client/src/views/widgets/table_dropdown.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class OrderPreviewScreen extends ConsumerWidget {
  const OrderPreviewScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    List<MenuItemCardModel> menuItems = ref.watch(orderProvider);
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
                          ref.watch(orderProvider).length.toString(),
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
            const TableDropdown(),
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
                      ref.read(orderProvider.notifier).removeMenuItem(index);
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
      ),
    );
  }
}
