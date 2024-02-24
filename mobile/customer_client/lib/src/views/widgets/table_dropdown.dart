import 'package:customer_client/src/models/table/table_model.dart';
import 'package:customer_client/src/providers/menu_item_provider/menu_item_provider.dart';
import 'package:customer_client/src/services/table_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TableDropdown extends ConsumerStatefulWidget {
  const TableDropdown({super.key, required this.onSelectTable});

  final void Function(int tableId) onSelectTable;

  @override
  ConsumerState<TableDropdown> createState() => _TableDropdownState();
}

class _TableDropdownState extends ConsumerState<TableDropdown> {
  final TableService _tableService = const TableService();
  late Future<List<TableModel>> futureTables;
  int? selectedTable;

  @override
  void initState() {
    super.initState();
    getTables();
  }

  // Ovo radi trenutnu ali ne svidja mi se rjesenje ni na web aplikaciji
  // ni ovdje
  void getTables() {
    final menuItems = ref.read(menuItemProvider);
    final restaurantId = menuItems.isNotEmpty ? menuItems[0].restaurantId : -1;
    if (restaurantId == -1) return;
    futureTables = _tableService.getTables(restaurantId!);
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: futureTables,
        builder: (_, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const CircularProgressIndicator();
          } else if (snapshot.hasError) {
            return Text(
              "Error: ${snapshot.error}",
              style: const TextStyle(color: Colors.white),
            );
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return const Text("Restaurant has no tables.");
          } else {
            return DropdownMenu<TableModel>(
              textStyle: Theme.of(context)
                  .textTheme
                  .titleMedium!
                  .copyWith(color: Theme.of(context).colorScheme.primary),
              menuHeight: 350,
              label: Text(
                "Select your Table",
                style: Theme.of(context).textTheme.labelLarge!.copyWith(
                      color: Theme.of(context).colorScheme.onBackground,
                    ),
              ),
              expandedInsets: EdgeInsets.zero,
              onSelected: (TableModel? table) {
                if (table == null) return;
                selectedTable = table.id!;
                widget.onSelectTable(selectedTable!);
              },
              dropdownMenuEntries:
                  snapshot.data!.map<DropdownMenuEntry<TableModel>>((TableModel table) {
                return DropdownMenuEntry<TableModel>(value: table, label: table.name!);
              }).toList(),
            );
          }
        });
  }
}
