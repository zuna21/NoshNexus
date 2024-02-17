import 'package:flutter/material.dart';

class AccountEditScreen extends StatelessWidget {
  const AccountEditScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Update Account"),
      ),
      body: const Center(
        child: Text("Update account"),
      ),
    );
  }
}