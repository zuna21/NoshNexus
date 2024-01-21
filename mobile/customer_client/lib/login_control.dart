import 'package:customer_client/src/views/widgets/login_dialog.dart';
import 'package:flutter/material.dart';

class LoginControl {
  const LoginControl();

  void openLoginDialog(BuildContext context) {
    showDialog(
        context: context,
        builder: (_) {
          return LoginDialog();
        });
  }
}
