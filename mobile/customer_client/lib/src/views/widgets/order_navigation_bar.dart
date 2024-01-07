import 'package:customer_client/src/providers/order_provider.dart';
import 'package:customer_client/src/views/screens/order_preview_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class OrderNavigationBar extends ConsumerWidget {
  const OrderNavigationBar({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final count = ref.watch(orderProvider).length;

    return NavigationBar(
      onDestinationSelected: (value) {
        if (value == 0) {
          Navigator.of(context).push(
            MaterialPageRoute(
              builder: (_) => const OrderPreviewScreen(),
            ),
          );
        } else if (value == 1) {
          ref.read(orderProvider.notifier).resetOrder();
        }
      },
      destinations: [
        if (count > 0)
          NavigationDestination(
              icon: Badge(
                label: Text(
                  count.toString(),
                ),
                backgroundColor: Theme.of(context).colorScheme.primary,
                child: const Icon(Icons.list_alt),
              ),
              label: "Your Order"),
        if (count <= 0)
          const NavigationDestination(
              icon: Icon(Icons.list_alt), label: "Your Order"),
        const NavigationDestination(
            icon: Icon(Icons.delete), label: "Reset Order"),
      ],
    );
  }
}
