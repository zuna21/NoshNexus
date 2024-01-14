import 'package:customer_client/src/views/widgets/cards/order_card/order_card_info.dart';
import 'package:customer_client/src/views/widgets/cards/order_card/order_card_menu_items.dart';
import 'package:flutter/material.dart';

class OrderCard extends StatelessWidget {
  const OrderCard({super.key});

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Container(
        padding: const EdgeInsets.all(10),
        decoration: BoxDecoration(
            border: Border.all(color: Colors.red),
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
                    image: const DecorationImage(
                      image: NetworkImage('https://picsum.photos/600/600'),
                    ),
                  ),
                ),
                const SizedBox(
                  width: 20,
                ),
                Text(
                  "Username",
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
                    "Rupa",
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
                    "sto-4",
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
            Container(
              padding: const EdgeInsets.all(5),
              width: double.infinity,
              decoration: BoxDecoration(
                  border:
                      Border.all(color: Theme.of(context).colorScheme.primary),
                  borderRadius: BorderRadius.circular(5),
                  color: const Color.fromARGB(255, 250, 235, 215)),
              child: Text(
                "Ovo je neki note za konobara",
                style: Theme.of(context)
                    .textTheme
                    .bodyLarge!
                    .copyWith(color: Colors.black),
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            const SizedBox(
              height: 200,
              child: DefaultTabController(
                length: 2,
                child: Column(
                  children: [
                    TabBar(
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
                          OrderCardMenuItems(),
                          OrderCardInfo(),
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
              "Declined",
              style: Theme.of(context).textTheme.titleLarge!.copyWith(
                    color: Colors.red,
                    fontSize: 30
                  ),
            ),
            const SizedBox(height: 15,),
            Text("This user was blocked by the owner.", style: Theme.of(context).textTheme.bodyLarge!.copyWith(
              color: Theme.of(context).colorScheme.onBackground,
            ),),
          ],
        ),
      ),
    );
  }
}
