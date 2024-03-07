import 'package:customer_client/src/views/screens/restaurants_screen/restaurants_screen.dart';
import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_translate/flutter_translate.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:firebase_core/firebase_core.dart';
import 'firebase_options.dart';

final theme = ThemeData(
  useMaterial3: true,
  colorScheme: ColorScheme.fromSeed(
    brightness: Brightness.dark,
    primary: const Color.fromRGBO(245, 124, 0, 1),
    seedColor: const Color.fromARGB(255, 1, 101, 104),
  ),
  textTheme: GoogleFonts.latoTextTheme(),
);

void main() async {
  var delegate = await LocalizationDelegate.create(
    fallbackLocale: 'en',
    supportedLocales: ['bs', 'en'],
  );
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform
  );
  _initializeFCM();
  runApp(LocalizedApp(delegate, const ProviderScope(child: MyApp())));
}

void _initializeFCM() {
  FirebaseMessaging.instance.requestPermission();
  FirebaseMessaging.instance.getToken().then((token) {
    print("FCM Token: $token");
    // Store the token on your server for sending targeted messages
  });
}


class MyApp extends StatelessWidget {
  const MyApp({super.key});


  @override
  Widget build(BuildContext context) {
    var localizationDelegate = LocalizedApp.of(context).delegate;
    return LocalizationProvider(
      state: LocalizationProvider.of(context).state,
      child: MaterialApp(
        title: 'Nosh Nexus',
        localizationsDelegates: [
          GlobalMaterialLocalizations.delegate,
          GlobalWidgetsLocalizations.delegate,
          GlobalCupertinoLocalizations.delegate,
          localizationDelegate
        ],
        supportedLocales: localizationDelegate.supportedLocales,
        locale: localizationDelegate.currentLocale,
        theme: theme,
        home: const RestaurantsScreen(),
        ),
      );
  }
}


