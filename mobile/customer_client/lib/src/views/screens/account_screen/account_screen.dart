import 'package:customer_client/src/models/account/account_details_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:customer_client/src/views/screens/error_screen.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:customer_client/src/views/widgets/main_drawer.dart';
import 'package:flutter/material.dart';

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

  _loadAccount() async {
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

  @override
  Widget build(BuildContext context) {
    Widget? content;

    if (error != null) {
      content = ErrorScreen(errorMessage: error!);
    } else if (account != null) {
      content = SingleChildScrollView(
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
                    image: const DecorationImage(
                      image: NetworkImage(
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
                onPressed: () {},
                style: ElevatedButton.styleFrom(
                  backgroundColor:
                      Theme.of(context).colorScheme.onPrimaryContainer,
                  foregroundColor: Colors.black,
                ),
                child: const Text("Activate account"),
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
                  Text(
                    "${account!.lastName!} ${account!.firstName!}",
                    style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                        color: Theme.of(context).colorScheme.onBackground),
                  ),
                  Text(
                    "Joined: ${account!.joined}",
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
                    "Description",
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
                onPressed: () {},
                style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.blue,
                    foregroundColor: Colors.white),
                child: const Text("Edit Account"),
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
        title: const Text("Account"),
      ),
      body: content,
    );
  }
}
