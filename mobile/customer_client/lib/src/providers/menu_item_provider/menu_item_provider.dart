import 'package:customer_client/src/models/menu_item/menu_item_card_model.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'menu_item_provider.g.dart';

@riverpod
class MenuItem extends _$MenuItem {
  @override
  List<MenuItemCardModel> build() {
    return [];
  }

  void addMenuItem(MenuItemCardModel menuItem) {
    state = [menuItem, ...state];
  }

  void resetMenuItems() {
    state = [];
  }

  void removeMenuItem(int index) {
    final newList = [...state];
    newList.removeAt(index);
    state = [...newList];
  }
}