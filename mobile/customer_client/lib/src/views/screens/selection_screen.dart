import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class SelectionScreen extends StatelessWidget {
  const SelectionScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      initialIndex: 1,
      length: 2,
      child: Scaffold(
        appBar: AppBar(
          title: const Text("Nosh Nexus"),
          bottom: const TabBar(
            tabs: [
              Tab(
                child: Text("Menu Items"),
              ),
              Tab(
                child: Text("Menus"),
              ),
            ],
          ),
        ),
        drawer: const MainDrawer(),
        body: const TabBarView(children: [
          Center(child: Text("First", style: TextStyle(color: Colors.white),),),
          Center(child: Text("Seconst", style: TextStyle(color: Colors.white),),),
        ]),
      ),
    );
  }
}
