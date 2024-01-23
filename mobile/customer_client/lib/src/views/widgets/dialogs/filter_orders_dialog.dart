import 'package:customer_client/src/models/query_params/orders_query_params.dart';
import 'package:flutter/material.dart';

const List<Widget> statusOptions = <Widget>[
  Text('All'),
  Text('Accepted'),
  Text('Declined')
];

class FilterOrdersDialog extends StatefulWidget {
  const FilterOrdersDialog({super.key, required this.ordersQueryPrams});

  final OrdersQueryPrams ordersQueryPrams;

  @override
  State<FilterOrdersDialog> createState() => _FilterOrdersDialogState();
}

class _FilterOrdersDialogState extends State<FilterOrdersDialog> {
  List<bool> _statuses = <bool>[false, false, false];
  final TextEditingController _searchController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _initButtons();
    _searchController.text = widget.ordersQueryPrams.search;
  }

  void _initButtons() {
    setState(() {
      _statuses = <bool>[
        widget.ordersQueryPrams.status == "all",
        widget.ordersQueryPrams.status == "accepted",
        widget.ordersQueryPrams.status == "declined"
      ];
    });
  }

  void _onToggle(int index) {
    switch (index) {
      case 1:
        widget.ordersQueryPrams.status = "accepted";
        break;
      case 2:
        widget.ordersQueryPrams.status = "declined";
        break;
      default:
        widget.ordersQueryPrams.status = "all";
    }
    setState(() {
      for (int i = 0; i < _statuses.length; i++) {
        _statuses[i] = i == index;
      }
    });
  }

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: Padding(
        padding: const EdgeInsets.all(10),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ToggleButtons(
              onPressed: (int index) {
                _onToggle(index);
              },
              borderRadius: const BorderRadius.all(Radius.circular(8)),
              selectedBorderColor: Colors.orange[700],
              selectedColor: Colors.white,
              fillColor: Colors.orange[200],
              color: Colors.orange[400],
              constraints: const BoxConstraints(
                minHeight: 40.0,
                minWidth: 80.0,
              ),
              isSelected: _statuses,
              children: statusOptions,
            ),
            const SizedBox(
              height: 10,
            ),
            SearchBar(
              trailing: [
                IconButton(onPressed: () {
                  _searchController.text = "";
                }, icon: const Icon(Icons.close),),
              ],
              controller: _searchController,
              hintText: "Search by name or city",
              leading: const Icon(Icons.search),
              backgroundColor: MaterialStatePropertyAll(
                Theme.of(context).colorScheme.secondaryContainer,
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                ElevatedButton.icon(
                  style: ElevatedButton.styleFrom(
                    backgroundColor:
                        Theme.of(context).colorScheme.primaryContainer,
                    foregroundColor:
                        Theme.of(context).colorScheme.onPrimaryContainer,
                  ),
                  onPressed: () {
                    widget.ordersQueryPrams.search = _searchController.text;
                    Navigator.of(context).pop(widget.ordersQueryPrams);
                  },
                  icon: const Icon(Icons.tune),
                  label: const Text("Filter"),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }
}
