import 'package:customer_client/src/providers/menu_item_provider/menu_item_provider.dart';
import 'package:customer_client/src/views/screens/order_preview_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_translate/flutter_translate.dart';

class OrderNavigationBar extends ConsumerWidget {
  const OrderNavigationBar({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final count = ref.watch(menuItemProvider).length;

    return NavigationBar(
      onDestinationSelected: (value) {
        if (value == 0) {
          Navigator.of(context).push(
            MaterialPageRoute(
              builder: (_) => const OrderPreviewScreen(),
            ),
          );
        } else if (value == 1) {
          ref.read(menuItemProvider.notifier).resetMenuItems();
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
            label: translate("Your Order"),
          ),
        if (count <= 0)
          NavigationDestination(
            icon: const Icon(Icons.list_alt),
            label: translate("Your Order"),
          ),
        NavigationDestination(
          icon: const Icon(Icons.delete),
          label: translate("Reset Order"),
        ),
      ],
    );
  }
}
