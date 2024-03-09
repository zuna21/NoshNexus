import 'package:flutter/material.dart';

class OrderNotificationDialog extends StatelessWidget {
  const OrderNotificationDialog(
      {super.key, required this.title, required this.content, required this.onOk});

  final String title;
  final String content;
  final void Function() onOk;

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      backgroundColor: Theme.of(context).colorScheme.primaryContainer,
      shape: BeveledRectangleBorder(
          side: BorderSide(color: Theme.of(context).colorScheme.primary),
          borderRadius: BorderRadius.circular(2)),
      title: Text(
        title,
        style: Theme.of(context)
            .textTheme
            .titleLarge!
            .copyWith(color: Theme.of(context).colorScheme.onPrimaryContainer),
      ),
      content: Text(
        content,
        style: Theme.of(context)
            .textTheme
            .bodyMedium!
            .copyWith(color: Theme.of(context).colorScheme.onPrimaryContainer),
      ),
      actions: [
        ElevatedButton(
          onPressed: onOk,
          style: ElevatedButton.styleFrom(
              backgroundColor: Colors.blue[600], foregroundColor: Colors.white),
          child: const Text("Ok"),
        ),
      ],
    );
  }
}
