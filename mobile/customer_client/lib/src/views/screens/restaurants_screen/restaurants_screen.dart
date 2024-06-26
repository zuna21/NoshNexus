import 'dart:async';

import 'package:customer_client/src/models/restaurant/restaurant_card_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:customer_client/src/services/restaurant_service.dart';
import 'package:customer_client/src/views/screens/empty_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen.dart';
import 'package:customer_client/src/views/widgets/cards/restaurant_card.dart';
import 'package:customer_client/src/views/widgets/dialogs/notifications/order_notification_dialog.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter_translate/flutter_translate.dart';

class RestaurantsScreen extends StatefulWidget {
  const RestaurantsScreen({super.key});

  @override
  State<RestaurantsScreen> createState() => _RestaurantsScreenState();
}

class _RestaurantsScreenState extends State<RestaurantsScreen> {
  final RestaurantService _restaurantService = const RestaurantService();
  final AccountService _accountService = const AccountService();
  final _controller = ScrollController();
  final int _pageSize = 10;
  int pageIndex = 0;
  List<RestaurantCardModel>? restaurants;
  bool hasMore = true;
  bool isLoading = false;
  String? error;

  StreamSubscription? sub1;
  StreamSubscription? sub2;
  StreamSubscription? sub3;

  @override
  void initState() {
    super.initState();
    _initDefaultLanguage();
    _configureFCMListeners();
    _loadRestaurants();
    _onScrollToBottom();
    _newFcmTokenListener();
  }

  void _initDefaultLanguage() async {
    const storage = FlutterSecureStorage();
    final lang = await storage.read(key: "lang");
    if (!context.mounted) return;
    changeLocale(context, lang ?? 'en');
  }

  void _newFcmTokenListener() {
    sub3 = FirebaseMessaging.instance.onTokenRefresh.listen((event) {
      _accountService.updateFcmToken();
    });
  }

  void _configureFCMListeners() async {
    sub1 = FirebaseMessaging.onMessage.listen((RemoteMessage message) {
      // Ovo je kad je unutar aplikacije i dobije notifikaciju
      if (message.notification == null ||
          message.notification!.title == null ||
          message.notification!.body == null) return;

      showDialog(
        context: context,
        builder: (_) => OrderNotificationDialog(
          title: message.notification!.title!,
          content: message.notification!.body!,
          onOk: () => Navigator.of(context).pop(),
        ),
      );
    });

    sub2 = FirebaseMessaging.onMessageOpenedApp.listen((RemoteMessage message) {
      // Ovo je kad je van aplikacije pa dobije notifikaciju

      if (message.notification == null ||
          message.notification!.title == null ||
          message.notification!.body == null) return;

      showDialog(
        context: context,
        builder: (_) => OrderNotificationDialog(
            onOk: () => Navigator.of(context).pop(),
            title: message.notification!.title!,
            content: message.notification!.body!),
      );
    });
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
    sub1?.cancel();
    sub2?.cancel();
    sub3?.cancel();
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
                return hasMore
                    ? const Padding(
                        padding: EdgeInsets.all(20),
                        child: Center(
                          child: CircularProgressIndicator(),
                        ),
                      )
                    : null;
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
