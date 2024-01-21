import 'package:flutter/material.dart';

class InvalidUOP extends StatelessWidget {
  const InvalidUOP({super.key, required this.onTryAgain, required this.message});

  final void Function() onTryAgain;
  final String message;

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        Text(
          message,
          style: Theme.of(context).textTheme.titleLarge!.copyWith(
                color: Colors.red,
              ),
        ),
        const SizedBox(
          height: 20,
        ),
        ElevatedButton.icon(
          onPressed: () {
            onTryAgain();
          },
          icon: const Icon(Icons.restore),
          label: const Text("Try again"),
        ),
      ],
    );
  }
}
