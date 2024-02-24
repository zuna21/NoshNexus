import 'package:customer_client/src/models/account/account_model.dart';
import 'package:customer_client/src/models/account/login_account_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:customer_client/src/views/screens/invalid_uop.dart';
import 'package:flutter/material.dart';
import 'package:flutter_translate/flutter_translate.dart';

class LoginDialog extends StatefulWidget {
  const LoginDialog({super.key});

  @override
  State<LoginDialog> createState() => _LoginDialogState();
}

class _LoginDialogState extends State<LoginDialog> {
  final _formKey = GlobalKey<FormState>();
  final AccountService _accountService = const AccountService();
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  bool _showPassword = false;
  bool _isInvalidUsernameOrPassword = false;
  String? error;

  void _onShowPassword() {
    setState(() {
      _showPassword = !_showPassword;
    });
  }

  void _onSubmit() async {
    if (!_formKey.currentState!.validate()) return;
    final loginAccount = LoginAccountModel(
        username: _usernameController.text, password: _passwordController.text);
    try {
      final response = await _accountService.login(loginAccount);
      if (response.token == null) {
        setState(() {
          _formKey.currentState?.reset();
          _isInvalidUsernameOrPassword = true;
        });
      } else {
        if (context.mounted) {
          final user = AccountModel(
              token: response.token!, username: response.username!);
          Navigator.of(context).pop(user);
        }
      }
    } catch (err) {
      setState(() {
        _formKey.currentState?.reset();
        error = "$err";
        _isInvalidUsernameOrPassword = true;
      });
    }
  }

  void _onLoginAsGuest() async {
    try {
      final response = await _accountService.loginAsGuest();
      if (response.token == null) {
        setState(() {
          error = "Failed to create an account";
        });
      } else {
        if (context.mounted) {
          final user = AccountModel(
              token: response.token!,
              profileImage: response.profileImage!,
              username: response.username!);
          Navigator.of(context).pop(user);
        }
      }
    } catch (err) {
      error = err.toString();
      print(error);
    }
  }

  void _onTryAgain() {
    setState(() {
      _isInvalidUsernameOrPassword = false;
    });
  }

  @override
  void dispose() {
    _usernameController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      shape: BeveledRectangleBorder(
        borderRadius: BorderRadius.circular(5),
        side: BorderSide(
          color: Theme.of(context).colorScheme.primary,
        ),
      ),
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: _isInvalidUsernameOrPassword
            ? InvalidUOP(
                onTryAgain: _onTryAgain,
                message: error ?? "Invalid Username or Password!",
              )
            : Form(
                key: _formKey,
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    Text(
                      translate("Login"),
                      style: Theme.of(context).textTheme.titleLarge!.copyWith(
                            color: Theme.of(context).colorScheme.primary,
                          ),
                    ),
                    const SizedBox(
                      height: 15,
                    ),
                    TextFormField(
                      controller: _usernameController,
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onBackground,
                      ),
                      textCapitalization: TextCapitalization.none,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return translate("Please enter valid username");
                        }
                        return null;
                      },
                      autocorrect: false,
                      decoration: InputDecoration(
                        border: const OutlineInputBorder(),
                        labelText: translate("Username"),
                        hintText: translate("Enter your username"),
                      ),
                    ),
                    const SizedBox(
                      height: 15,
                    ),
                    TextFormField(
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onBackground,
                      ),
                      textCapitalization: TextCapitalization.none,
                      obscureText: _showPassword ? false : true,
                      autocorrect: false,
                      controller: _passwordController,
                      validator: (value) {
                        if (value == null || value.length < 6) {
                          return translate("Enter valid password.");
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                        border: const OutlineInputBorder(),
                        labelText: translate("Password"),
                        hintText: translate("Enter your password"),
                        suffixIcon: IconButton(
                          onPressed: () {
                            _onShowPassword();
                          },
                          icon: Icon(
                            _showPassword
                                ? Icons.visibility
                                : Icons.visibility_off,
                          ),
                        ),
                      ),
                    ),
                    const SizedBox(
                      height: 15,
                    ),
                    ElevatedButton(
                      onPressed: () {
                        _onSubmit();
                      },
                      style: ElevatedButton.styleFrom(
                        backgroundColor:
                            Theme.of(context).colorScheme.primaryContainer,
                        foregroundColor:
                            Theme.of(context).colorScheme.onPrimaryContainer,
                        minimumSize: const Size.fromHeight(40),
                      ),
                      child: Text(
                        translate("Log In"),
                      ),
                    ),
                    ElevatedButton(
                      onPressed: () {
                        _onLoginAsGuest();
                      },
                      style: ElevatedButton.styleFrom(
                        backgroundColor:
                            Theme.of(context).colorScheme.secondaryContainer,
                        foregroundColor:
                            Theme.of(context).colorScheme.onSecondaryContainer,
                        minimumSize: const Size.fromHeight(40),
                      ),
                      child: Text(translate("Quickly create an account")),
                    ),
                  ],
                ),
              ),
      ),
    );
  }
}
