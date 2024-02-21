import 'package:flutter/material.dart';

class SelectImageSheet extends StatelessWidget {
  const SelectImageSheet({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(
        horizontal: 10,
        vertical: 50,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          ElevatedButton.icon(
            onPressed: () {
              if (context.mounted) {
                Navigator.of(context).pop("gallery");
              }
            },
            icon: const Icon(Icons.image),
            label: const Text("gallery"),
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.orange[300],
              foregroundColor: Colors.black,
              minimumSize: const Size.fromHeight(40)
            ),
          ),
          const SizedBox(height: 10,),
          ElevatedButton.icon(
            onPressed: () {
              if (context.mounted) {
                Navigator.of(context).pop("camera");
              }
            },
            icon: const Icon(Icons.camera),
            label: const Text("camera"),
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.orange[300],
              foregroundColor: Colors.black,
              minimumSize: const Size.fromHeight(40)
            ),
          ),
        ],
      ),
    );
  }
}
