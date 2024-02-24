import 'package:customer_client/src/models/account/account_details_model.dart';
import 'package:customer_client/src/models/account/account_model.dart';
import 'package:customer_client/src/models/account/edit_account_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:customer_client/src/views/screens/account_edit_screen/account_edit_screen.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/dialogs/activate_account_dialog.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';
import 'package:flutter_translate/flutter_translate.dart';
import 'package:intl/intl.dart';

class AccountScreen extends StatefulWidget {
  const AccountScreen({super.key});

  @override
  State<AccountScreen> createState() => _AccountScreenState();
}

class _AccountScreenState extends State<AccountScreen> {
  AccountDetailsModel? account;
  final AccountService _accountService = const AccountService();
  bool isLoading = true;
  String? error;

  @override
  void initState() {
    super.initState();
    _loadAccount();
  }

  void _loadAccount() async {
    try {
      account = await _accountService.getAccountDetails();
      setState(() {
        isLoading = false;
      });
    } catch (e) {
      print(e.toString());
      error = e.toString();
      setState(() {
        isLoading = false;
      });
    }
  }

  void _onActivateAccount() async {
    final user = await showDialog<AccountModel>(
      context: context,
      builder: (_) => const ActivateAccountDialog(),
    );

    if (user == null && context.mounted) {
      ScaffoldMessenger.of(context).clearSnackBars();
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(translate("Please Activate Account")),
        ),
      );
    } else {
      if (context.mounted) {
        setState(() {
          account!.username = user!.username;
          account!.isActivated = true;
        });
        ScaffoldMessenger.of(context).clearSnackBars();
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Successfully activated account."),
          ),
        );
      }
    }
  }

  void _onEditAccount() async {
    if (!account!.isActivated!) return;
    final EditAccountModel? response = await Navigator.of(context).push(
      MaterialPageRoute(
        builder: (_) => const AccountEditScreen(),
      ),
    );

    if (response == null) return;
    setState(() {
      account!.username = response.username;
      account!.city = response.city;
      account!.description = response.description;
      account!.firstName = response.firstName;
      account!.lastName = response.lastName;
    });
  }

  @override
  Widget build(BuildContext context) {
    Widget? content;

    if (error != null) {
      content = ErrorScreen(errorMessage: error!);
    } else if (account != null) {
      content = SingleChildScrollView(
        padding: const EdgeInsets.all(10),
        child: Column(
          children: [
            const SizedBox(
              height: 10,
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Container(
                  height: 150,
                  width: 150,
                  decoration: BoxDecoration(
                    color: Colors.black,
                    borderRadius: BorderRadius.circular(100),
                    border: Border.all(
                        color: Theme.of(context).colorScheme.primary),
                    image: DecorationImage(
                      image: NetworkImage(
                        account!.profileImage ??
                            "https://noshnexus.com/images/default/default-profile.png",
                      ),
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 20,
            ),
            if (!account!.isActivated!)
              ElevatedButton(
                onPressed: () {
                  _onActivateAccount();
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor:
                      Theme.of(context).colorScheme.onPrimaryContainer,
                  foregroundColor: Colors.black,
                ),
                child: Text(
                  translate("Activate Account"),
                ),
              ),
            const SizedBox(
              height: 20,
            ),
            Container(
              padding: const EdgeInsets.all(10),
              width: double.infinity,
              decoration: BoxDecoration(
                color: const Color.fromARGB(255, 97, 95, 95),
                border:
                    Border.all(color: Theme.of(context).colorScheme.primary),
                borderRadius: BorderRadius.circular(5),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        account!.username!,
                        style: Theme.of(context).textTheme.titleLarge!.copyWith(
                            color: Theme.of(context).colorScheme.onBackground),
                      ),
                    ],
                  ),
                  const SizedBox(
                    height: 10,
                  ),
                  if (account!.firstName != null && account!.lastName != null)
                    Text(
                      "${account!.lastName!} ${account!.firstName!}",
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                  Text(
                    "${translate("Joined")}:   ${DateFormat("dd-MM-yyyy").format(DateTime.parse(account!.joined!))}",
                    style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                        color: Theme.of(context).colorScheme.onBackground),
                  ),
                  const SizedBox(
                    height: 10,
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      const Icon(Icons.public),
                      const SizedBox(
                        width: 10,
                      ),
                      Text(
                        account!.country == null
                            ? "User didn't select country"
                            : "${account!.country!}, ${account!.city}",
                        style: Theme.of(context)
                            .textTheme
                            .bodyLarge!
                            .copyWith(color: Colors.white),
                      ),
                    ],
                  )
                ],
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            Container(
              padding: const EdgeInsets.all(10),
              width: double.infinity,
              decoration: BoxDecoration(
                color: const Color.fromARGB(255, 97, 95, 95),
                border:
                    Border.all(color: Theme.of(context).colorScheme.primary),
                borderRadius: BorderRadius.circular(5),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    translate("Description"),
                    style: Theme.of(context).textTheme.titleLarge!.copyWith(
                          color: Theme.of(context).colorScheme.primary,
                        ),
                  ),
                  const SizedBox(
                    height: 10,
                  ),
                  Text(
                    account!.description == null
                        ? "User didn't enter description"
                        : account!.description!,
                    style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground,
                        ),
                  ),
                ],
              ),
            ),
            const SizedBox(
              height: 20,
            ),
            if (account!.isActivated!)
              ElevatedButton(
                onPressed: () {
                  _onEditAccount();
                },
                style: ElevatedButton.styleFrom(
                    minimumSize: const Size.fromHeight(40),
                    backgroundColor: Colors.blue,
                    foregroundColor: Colors.white),
                child: Text(
                  translate("Edit Account"),
                ),
              ),
          ],
        ),
      );
    } else {
      content = const LoadingScreen();
    }

    return Scaffold(
      drawer: const MainDrawer(),
      appBar: AppBar(
        title: Text(
          translate("Account"),
        ),
      ),
      body: content,
    );
  }
}
