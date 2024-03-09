import 'package:customer_client/src/models/account/account_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:customer_client/src/views/widgets/login_dialog.dart';
import 'package:flutter/material.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class LoginControl {
  const LoginControl();
  final AccountService _accountService = const AccountService();

  Future<bool> isUserLogged(BuildContext context) async {
    // provjeri iz secure storage, ako ima smjesi u (nesto) i return true
    const storage = FlutterSecureStorage();
    if (await storage.read(key: "token") != null) {
      return true;
    }
    ////
    if (context.mounted) {
      final dialogResult = await showDialog<AccountModel>(
          context: context,
          builder: (_) {
            return const LoginDialog();
          });

      if (dialogResult == null) {
        return false;
      } else {
        // Sacuvaj u secure storage
        await storage.write(key: "token", value: dialogResult.token!);
        await _accountService.updateFcmToken();
        return true;
      }
    } else {
      return false;
    }
  }
}
