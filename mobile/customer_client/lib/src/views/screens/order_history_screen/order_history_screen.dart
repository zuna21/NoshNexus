import 'package:customer_client/src/models/order/order_card_model.dart';
import 'package:customer_client/src/services/order_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/cards/order_card/order_card.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class OrderHistoryScreen extends StatefulWidget {
  const OrderHistoryScreen({super.key});

  @override
  State<OrderHistoryScreen> createState() => _OrderHistoryScreenState();
}

class _OrderHistoryScreenState extends State<OrderHistoryScreen> {
  final OrderService _orderService = const OrderService();

  late Future<List<OrderCardModel>> futureOrders;

  @override
  void initState() {
    super.initState();
    futureOrders = _orderService.getOrders();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Order History"),
      ),
      drawer: const MainDrawer(),
      body: FutureBuilder(future: futureOrders, builder: (_, snapshot) {
        if (snapshot.hasData && snapshot.data!.isNotEmpty) {
          return ListView.builder(
            itemCount: snapshot.data!.length,
            itemBuilder: (_, index) {
            return OrderCard(
              order: snapshot.data![index],
            );
          });
        } else if (snapshot.hasError) {
          return ErrorScreen(errorMessage: "Error: ${snapshot.error}");
        } else if (snapshot.hasData && snapshot.data!.isEmpty) {
          return const EmptyScreen(message: "You did not make any order.");
        }

        return const LoadingScreen();
      }),
    );
  }
}
