import 'package:customer_client/src/models/account/activate_account_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:flutter/material.dart';

class ActivateAccountDialog extends StatefulWidget {
  const ActivateAccountDialog({super.key});

  @override
  State<ActivateAccountDialog> createState() => _ActivateAccountDialogState();
}

class _ActivateAccountDialogState extends State<ActivateAccountDialog> {
  final _formKey = GlobalKey<FormState>();
  bool _showPassword = false;
  bool _showRepeatPassword = false;
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController= TextEditingController();
  final TextEditingController _repeatPasswordController = TextEditingController();
  final _accountService = const AccountService();


  void _onActivate() async {
    if (!_formKey.currentState!.validate()) return;
    ActivateAccountModel activateAccount = ActivateAccountModel(
      username: _usernameController.text,
      password: _passwordController.text,
      repeatPassword: _repeatPasswordController.text
    );

    try {
      final response = await _accountService.activateAccount(activateAccount);
      if (context.mounted) {  
        Navigator.of(context).pop(response);
      }
    } catch(err) {
      print(err);
    }
  }

  @override
  void dispose() {
    _usernameController.dispose();
    _passwordController.dispose();
    _repeatPasswordController.dispose();
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
        child: Form(
          key: _formKey,
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              TextFormField(
                style: TextStyle(
                  color: Theme.of(context).colorScheme.onBackground,
                ),
                textCapitalization: TextCapitalization.none,
                controller: _usernameController,
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return "Please enter valid username";
                  }
                  return null;
                },
                autocorrect: false,
                decoration: const InputDecoration(
                  border: OutlineInputBorder(),
                  labelText: "Username",
                  hintText: "Enter your username",
                ),
              ),
              


              const SizedBox(
                height: 20,
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
                          return "Enter valid password (min 6 characters).";
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                        border: const OutlineInputBorder(),
                        labelText: "Password",
                        hintText: "Enter your password",
                        suffixIcon: IconButton(
                          onPressed: () {
                            setState(() {
                            _showPassword = !_showPassword;
                            });
                          },
                          icon: Icon(
                            _showPassword
                                ? Icons.visibility
                                : Icons.visibility_off,
                          ),
                        ),
                      ),
                    ),


              const SizedBox(height: 20,),





              TextFormField(
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onBackground,
                      ),
                      textCapitalization: TextCapitalization.none,
                      obscureText: _showRepeatPassword ? false : true,
                      autocorrect: false,
                      controller: _repeatPasswordController,
                      validator: (value) {
                        if (value == null || value.length < 6) {
                          return "Enter valid password (min 6 characters).";
                        } 
                        if (value != _passwordController.text) {
                          return "Passwords doesn't match.";
                        }
                        return null;
                      },
                      decoration: InputDecoration(
                        border: const OutlineInputBorder(),
                        labelText: "Repeat Password",
                        hintText: "Repeat your password",
                        suffixIcon: IconButton(
                          onPressed: () {
                            setState(() {
                            _showRepeatPassword = !_showRepeatPassword;
                            });
                          },
                          icon: Icon(
                            _showRepeatPassword
                                ? Icons.visibility
                                : Icons.visibility_off,
                          ),
                        ),
                      ),
                    ),



              const SizedBox(height: 20,),

              ElevatedButton(
                onPressed: () {
                  _onActivate();
                }, 
                style: ElevatedButton.styleFrom(
                  backgroundColor: Theme.of(context).colorScheme.primaryContainer,
                  foregroundColor: Theme.of(context).colorScheme.onPrimaryContainer
                ),
                child: const Text("Activate"),
                ),
            ],
          ),
        ),
      ),
    );
  }
}
