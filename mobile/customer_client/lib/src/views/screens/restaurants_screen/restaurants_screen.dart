import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen.dart';
import 'package:customer_client/src/views/widgets/cards/restaurant_card.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantsScreen extends StatefulWidget {
  const RestaurantsScreen({super.key});

  @override
  State<RestaurantsScreen> createState() => _RestaurantsScreenState();
}

class _RestaurantsScreenState extends State<RestaurantsScreen> {
  final RestaurantService _restaurantService = const RestaurantService();
  final _controller = ScrollController();
  final int _pageSize = 10;
  int pageIndex = 0;
  List<RestaurantCardModel>? restaurants;
  bool hasMore = true;
  bool isLoading = false;
  String? error;

  @override
  void initState() {
    super.initState();
    _loadRestaurants();
    _onScrollToBottom();
  }

  void _loadRestaurants() async {
    if (!hasMore || isLoading) return;
    isLoading = true;
    try {
      final loadedRestaurants =
          await _restaurantService.getRestaurants(pageIndex: pageIndex);


      if (loadedRestaurants.isEmpty || loadedRestaurants.length < _pageSize) {
        hasMore = false;
      }

      setState(() {
        if (restaurants == null) {
          restaurants = [...loadedRestaurants];
        } else {
          restaurants = [...restaurants!, ...loadedRestaurants];
        }
        isLoading = false;
      });
    } catch (err) {
      setState(() {
        error = err.toString();
      });
    }
  }

  void _onScrollToBottom() {
    _controller.addListener(() {
      if (_controller.position.maxScrollExtent == _controller.offset) {
        pageIndex++;
        _loadRestaurants();
      }
    });
  }

  Future<void> _onRefresh() async {
    setState(() {
      isLoading = false;
      hasMore = true;
      restaurants = null;
      pageIndex = 0;
    });

    _loadRestaurants();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    Widget? content;

    if (restaurants == null) {
      content = const LoadingScreen();
    } else if (error != null) {
      content = ErrorScreen(errorMessage: "Error: $error");
    } else if (restaurants!.isEmpty) {
      content = const EmptyScreen(message: "There are no restaurants");
    } else {
      content = RefreshIndicator(
        onRefresh: _onRefresh,
        child: ListView.builder(
            controller: _controller,
            itemCount: restaurants!.length + 1,
            itemBuilder: (_, index) {
              if (index < restaurants!.length) {
                return InkWell(
                  onTap: () {
                    Navigator.of(context).push(
                      MaterialPageRoute(
                        builder: (_) => RestaurantScreen(
                            restaurantId: restaurants![index].id!),
                      ),
                    );
                  },
                  child: RestaurantCard(
                    restaurantCard: restaurants![index],
                  ),
                );
              } else {
                return hasMore ? const Padding(
                  padding: EdgeInsets.all(20),
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
        title: const Text("Restaurants"),
      ),
      drawer: const MainDrawer(),
      body: content,
    );
  }
}
