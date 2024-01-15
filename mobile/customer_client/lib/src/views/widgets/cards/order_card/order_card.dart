import 'package:customer_client/src/models/order/order_card_model.dart';
import 'package:customer_client/src/views/widgets/cards/order_card/order_card_info.dart';
import 'package:customer_client/src/views/widgets/cards/order_card/order_card_menu_items.dart';
import 'package:flutter/material.dart';

class OrderCard extends StatelessWidget {
  const OrderCard({super.key, required this.order});

  final OrderCardModel order;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Container(
        padding: const EdgeInsets.all(10),
        decoration: BoxDecoration(
            border: Border.all(
              color: order.status! == "Accepted"
                  ? Colors.green
                  : order.status! == "Declined"
                      ? Colors.red
                      : Theme.of(context).colorScheme.primary,
            ),
            borderRadius: BorderRadius.circular(5)),
        child: Column(
          children: [
            Row(
              children: [
                const SizedBox(
                  width: 15,
                ),
                Container(
                  height: 100,
                  width: 100,
                  decoration: BoxDecoration(
                    border: Border.all(
                        color: Theme.of(context).colorScheme.primary),
                    borderRadius: BorderRadius.circular(100),
                    image: DecorationImage(
                      image: NetworkImage(order.user!.profileImage!),
                    ),
                  ),
                ),
                const SizedBox(
                  width: 20,
                ),
                Text(
                  order.user!.username!,
                  style: Theme.of(context).textTheme.titleLarge!.copyWith(
                      color: Theme.of(context).colorScheme.primary,
                      fontSize: 30),
                ),
              ],
            ),
            const SizedBox(
              height: 5,
            ),
            const Divider(),
            const SizedBox(
              height: 5,
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 25),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  const Icon(Icons.storefront),
                  Text(
                    order.restaurant!.name!,
                    style: Theme.of(context).textTheme.titleLarge!.copyWith(
                          color: Theme.of(context).colorScheme.primary,
                        ),
                  ),
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 25),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  const Icon(Icons.table_bar),
                  Text(
                    order.tableName!,
                    style: Theme.of(context).textTheme.titleLarge!.copyWith(
                          color: Theme.of(context).colorScheme.primary,
                        ),
                  ),
                ],
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            if (order.note != null && order.note!.isNotEmpty)
              Container(
                padding: const EdgeInsets.all(5),
                width: double.infinity,
                decoration: BoxDecoration(
                    border: Border.all(
                        color: Theme.of(context).colorScheme.primary),
                    borderRadius: BorderRadius.circular(5),
                    color: const Color.fromARGB(255, 250, 235, 215)),
                child: Text(
                  order.note!,
                  style: Theme.of(context)
                      .textTheme
                      .bodyLarge!
                      .copyWith(color: Colors.black),
                ),
              ),
            const SizedBox(
              height: 10,
            ),
            SizedBox(
              height: 200,
              child: DefaultTabController(
                length: 2,
                child: Column(
                  children: [
                    const TabBar(
                      tabs: [
                        Tab(
                          child: Text("Menu Items"),
                        ),
                        Tab(
                          child: Text("Info"),
                        ),
                      ],
                    ),
                    Expanded(
                      child: TabBarView(
                        children: [
                          OrderCardMenuItems(
                            menuItems: order.items!,
                          ),
                          OrderCardInfo(
                            createdAt: order.createdAt!,
                            totalItems: order.totalItems!,
                            totalPrice: order.totalPrice!,
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
            ),
            const SizedBox(
              height: 15,
            ),
            Text(
              order.status!,
              style: Theme.of(context).textTheme.titleLarge!.copyWith(
                  color: order.status! == "Accepted"
                      ? Colors.green
                      : order.status! == "Declined"
                          ? Colors.red
                          : Theme.of(context).colorScheme.primary,
                  fontSize: 30),
            ),
            const SizedBox(
              height: 15,
            ),
            if (order.status! == "Declined")
              Text(
                order.declineReason!,
                style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                      color: Theme.of(context).colorScheme.onBackground,
                    ),
              ),
          ],
        ),
      ),
    );
  }
}
