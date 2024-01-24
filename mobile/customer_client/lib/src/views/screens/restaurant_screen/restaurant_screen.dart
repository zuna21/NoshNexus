import 'package:customer_client/login_control.dart';
import 'package:customer_client/src/models/restaurant/restaurant_details_model.dart';
import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen_child.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

class RestaurantScreen extends StatefulWidget {
  const RestaurantScreen({super.key, required this.restaurantId});

  final int restaurantId;

  @override
  State<RestaurantScreen> createState() => _RestaurantScreenState();
}

class _RestaurantScreenState extends State<RestaurantScreen> {
  final RestaurantService _restaurantService = const RestaurantService();
  final LoginControl _loginControl = const LoginControl();

  RestaurantDetailsModel? restaurant;
  String? error;

  @override
  void initState() {
    super.initState();
    _loadRestaurant();
  }

  void _loadRestaurant() async {
    try {
      final loadedRestaurant =
          await _restaurantService.getRestaurant(widget.restaurantId);
      setState(() {
        restaurant = loadedRestaurant;
      });
    } catch (err) {
      setState(() {
        error = err.toString();
      });
    }
  }

  void _addToFavourite() async {
    if (restaurant == null) return;
    try {
      final hasUser = await _loginControl.isUserLogged(context);
      if (!hasUser) return;
      final response =
          await _restaurantService.addFavouriteRestaurant(restaurant!.id!);
      if (!response || !context.mounted) return;
      setState(() {
        restaurant!.isFavourite = !restaurant!.isFavourite!;
      });
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Successfully added restaurant to favourite"),
        ),
      );
    } catch (err) {
      if (context.mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Failed to add restaurant to favourite"),
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    Widget? content;
    if (restaurant != null) {
      content = RestaurantScreenChild(restaurant: restaurant!);
    } else if (error != null) {
      content = ErrorScreen(errorMessage: "Error: $error");
    } else {
      content = const LoadingScreen();
    }

    return Scaffold(
        appBar: AppBar(
          title: restaurant != null
              ? Text(restaurant!.name!)
              : const Text("Nosh Nexus"),
          actions: [
            if (restaurant != null)
              restaurant!.isFavourite!
                  ? IconButton(
                      onPressed: () {},
                      icon: const Icon(Icons.favorite),
                    )
                  : IconButton(
                      onPressed: () {
                        _addToFavourite();
                      },
                      icon: const Icon(Icons.favorite_outline),
                    ),
          ],
        ),
        drawer: const MainDrawer(),
        body: content);
  }
}
