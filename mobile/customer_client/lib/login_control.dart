import 'package:customer_client/src/views/widgets/login_dialog.dart';
import 'package:flutter/material.dart';

class LoginControl {
  const LoginControl();

  Future<bool> isUserLogged(BuildContext context) async {
    // provjeri iz secure storage, ako ima smjesi u (nesto) i return true
    final dialogResult = await showDialog(
        context: context,
        builder: (_) {
          return const LoginDialog();
        });
    
    if (dialogResult == null) {
      return false;
    } else {
      // Sacuvaj u secure storage
      return true;
    }
  }
}
