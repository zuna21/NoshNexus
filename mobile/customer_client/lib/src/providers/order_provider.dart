import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class OrderProviderNotifier extends StateNotifier<List<MenuItemCardModel>> {
  OrderProviderNotifier() : super([]);

  void addMenuItem(MenuItemCardModel menuItem) {
    state = [...state, menuItem];
  }

  void removeMenuItem(int menuItemIndex) {
    state = [
      for (int i = 0; i < state.length; i++)
        if (i != menuItemIndex) state[i],
    ];
  }

  void resetOrder() {
    state = [];
  }
}

final orderProvider =
    StateNotifierProvider<OrderProviderNotifier, List<MenuItemCardModel>>(
        (ref) {
  return OrderProviderNotifier();
});
