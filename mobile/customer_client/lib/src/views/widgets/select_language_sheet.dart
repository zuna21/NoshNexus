import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:flutter_translate/flutter_translate.dart';

class SelectLanguageSheet extends StatelessWidget {
  const SelectLanguageSheet({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(
        horizontal: 10,
        vertical: 20,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          ElevatedButton(
            onPressed: () async {
              changeLocale(context, 'en');
              Navigator.of(context).pop("english");
              const storage = FlutterSecureStorage();
              await storage.write(key: "lang", value: "en");
            },
            style: ElevatedButton.styleFrom(
              minimumSize: const Size.fromHeight(40),
              backgroundColor: Theme.of(context).colorScheme.primaryContainer,
              foregroundColor: Theme.of(context).colorScheme.onPrimaryContainer
            ),
            child: const Text("English"),
          ),
          ElevatedButton(
            onPressed: () async {
              changeLocale(context, 'bs');
              Navigator.of(context).pop("bosanski");
              const storage = FlutterSecureStorage();
              await storage.write(key: "lang", value: "bs");
            },
            style: ElevatedButton.styleFrom(
              minimumSize: const Size.fromHeight(40),
              backgroundColor: Theme.of(context).colorScheme.primaryContainer,
              foregroundColor: Theme.of(context).colorScheme.onPrimaryContainer
            ),
            child: const Text("Bosanski"),
          ),
        ],
      ),
    );
  }
}
