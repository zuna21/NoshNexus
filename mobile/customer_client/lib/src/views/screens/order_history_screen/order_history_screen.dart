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
  final ScrollController _scrollController = ScrollController();
  final _pageSize = 10;

  String? error;
  List<OrderCardModel>? orders;
  bool hasMore = true;
  bool isLoading = false;
  int pageIndex = 0;

  @override
  void initState() {
    super.initState();
    _loadOrders();
    _onScrollToBottom();
  }

  void _loadOrders() async {
    if (!hasMore || isLoading) return;
    isLoading = true;
    try {
      final List<OrderCardModel> loadedOrders = await _orderService.getOrders();

      if (loadedOrders.isEmpty || loadedOrders.length < _pageSize) {
        hasMore = false;
      }

      setState(() {
        if (orders == null) {
          orders = [...loadedOrders];
        } else {
          orders = [...orders!, ...loadedOrders];
        }
        isLoading = false;
      });
    } catch (err) {
      setState(() {
        error = err.toString();
      });
    }
  }

  Future<void> _onRefresh() async {
    setState(() {
      hasMore = true;
      isLoading = false;
      pageIndex = 0;
      orders!.clear();
    });

    _loadOrders();
  }

  void _onScrollToBottom() {
    _scrollController.addListener(() {
      if (_scrollController.position.maxScrollExtent ==
          _scrollController.offset) {
        pageIndex++;
        _loadOrders();
      }
    });
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    Widget? content;
    if (error != null) {
      content = ErrorScreen(errorMessage: "Error: $error");
    } else if (orders == null) {
      content = const LoadingScreen();
    } else if (orders!.isEmpty) {
      content = const EmptyScreen(message: "You have no orders");
    } else {
      content = RefreshIndicator(
        onRefresh: _onRefresh,
        child: ListView.builder(
            controller: _scrollController,
            itemCount: orders!.length + 1,
            itemBuilder: (_, index) {
              if (index < orders!.length) {
                return OrderCard(
                  order: orders![index],
                );
              } else {
                return hasMore ? const Padding(
                  padding: EdgeInsets.all(15),
                  child: Center(
                    child: CircularProgressIndicator(),
                  ),
                ) : null;
              }
            }),
      );
    }

    return Scaffold(
      appBar: AppBar(
        title: const Text("Order History"),
      ),
      drawer: const MainDrawer(),
      body: content,
    );
  }
}
