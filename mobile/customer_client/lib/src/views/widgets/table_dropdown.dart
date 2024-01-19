import 'package:customer_client/src/models/table/table_model.dart';
import 'package:customer_client/src/services/table_service.dart';
import 'package:flutter/material.dart';

class TableDropdown extends StatefulWidget {
  const TableDropdown({super.key});

  @override
  State<TableDropdown> createState() => _TableDropdownState();
}

class _TableDropdownState extends State<TableDropdown> {
  final TableService _tableService = const TableService();
  late Future<List<TableModel>> futureTables;
  int? selectedTable;

  @override
  void initState() {
    super.initState();
    futureTables = _tableService.getTables(1);
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
              expandedInsets: const EdgeInsets.symmetric(horizontal: 50),
              onSelected: (TableModel? table) {
                if (table == null) return;
                selectedTable = table.id!;
                print(selectedTable);
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
