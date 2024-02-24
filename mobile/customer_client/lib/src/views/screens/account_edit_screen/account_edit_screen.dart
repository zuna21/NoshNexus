import 'package:customer_client/src/models/account/edit_account_model.dart';
import 'package:customer_client/src/models/account/get_account_edit_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:customer_client/src/views/screens/account_edit_screen/upload_profile_image.dart';
import 'package:customer_client/src/views/screens/loading_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_translate/flutter_translate.dart';

class AccountEditScreen extends StatefulWidget {
  const AccountEditScreen({super.key});

  @override
  State<AccountEditScreen> createState() => _AccountEditScreenState();
}

class _AccountEditScreenState extends State<AccountEditScreen> {
  final AccountService _accountService = const AccountService();
  GetAccountEditModel? accountEdit;
  final _formKey = GlobalKey<FormState>();
  TextEditingController _usernameController = TextEditingController();
  TextEditingController _firstNameController = TextEditingController();
  TextEditingController _lastNameController = TextEditingController();
  TextEditingController _cityController = TextEditingController();
  TextEditingController _countryController = TextEditingController();
  TextEditingController _descriptionController = TextEditingController();
  int _selectedCountry = -1;
  bool _isDirty = false;

  @override
  void initState() {
    super.initState();
    _getAccountEdit();
  }

  void _getAccountEdit() async {
    try {
      accountEdit = await _accountService.getAccountEdit();
      if (accountEdit == null) return;
      _initForm(accountEdit!);
    } catch (err) {
      print(err);
    }
  }

  void _initForm(GetAccountEditModel getAccountEditModel) {
    _selectedCountry = getAccountEditModel.countryId!;
    if (_selectedCountry != -1) {
      final country = getAccountEditModel.countries!
          .firstWhere((element) => element.id == _selectedCountry);
      _countryController = TextEditingController(text: country.name);
    }
    _usernameController =
        TextEditingController(text: getAccountEditModel.username);
    _firstNameController =
        TextEditingController(text: getAccountEditModel.firstName);
    _lastNameController =
        TextEditingController(text: getAccountEditModel.lastName);
    ;
    _cityController = TextEditingController(text: getAccountEditModel.city);
    _descriptionController =
        TextEditingController(text: getAccountEditModel.description);
    setState(() {});
  }

  void _onSubmit() async {
    if (!_formKey.currentState!.validate() || !_isDirty) return;

    EditAccountModel editAccountModel = EditAccountModel(
        username: _usernameController.text,
        city: _cityController.text,
        countryId: _selectedCountry,
        description: _descriptionController.text,
        firstName: _firstNameController.text,
        lastName: _lastNameController.text);

    try {
      await _accountService.editAccount(editAccountModel);
      if (context.mounted) {
        Navigator.of(context).pop(editAccountModel);
        ScaffoldMessenger.of(context).clearSnackBars();
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Successfully updated account"),
          ),
        );
      }
    } catch (err) {
      print(err);
    }
  }

  @override
  void dispose() {
    _usernameController.dispose();
    _firstNameController.dispose();
    _lastNameController.dispose();
    _cityController.dispose();
    _descriptionController.dispose();
    _countryController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(translate("Edit Account")),
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(vertical: 20, horizontal: 10),
        child: accountEdit == null
            ? const LoadingScreen()
            : Form(
                key: _formKey,
                child: SingleChildScrollView(
                  child: Column(
                    children: [
                      UploadProfileImage(
                        profileImage: accountEdit!.profileImage,
                      ),
                      const SizedBox(
                        height: 30,
                      ),
                      TextFormField(
                        onChanged: (value) => _isDirty = true,
                        controller: _usernameController,
                        style: TextStyle(
                          color: Theme.of(context).colorScheme.onBackground,
                        ),
                        textCapitalization: TextCapitalization.none,
                        validator: (value) {
                          if (value == null ||
                              value.isEmpty ||
                              value.length < 4) {
                            return "Please enter valid username";
                          }
                          return null;
                        },
                        autocorrect: false,
                        decoration: InputDecoration(
                          border: const OutlineInputBorder(),
                          labelText: translate("Username"),
                          hintText: "Enter your username",
                        ),
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      Row(
                        children: [
                          Expanded(
                            child: TextFormField(
                              onChanged: (value) => _isDirty = true,
                              controller: _firstNameController,
                              style: TextStyle(
                                color:
                                    Theme.of(context).colorScheme.onBackground,
                              ),
                              textCapitalization: TextCapitalization.none,
                              autocorrect: false,
                              decoration: InputDecoration(
                                border: const OutlineInputBorder(),
                                labelText: translate("Name"),
                                hintText: "Enter your first name",
                              ),
                            ),
                          ),
                          const SizedBox(
                            width: 10,
                          ),
                          Expanded(
                            child: TextFormField(
                              controller: _lastNameController,
                              onChanged: (value) => _isDirty = true,
                              style: TextStyle(
                                color:
                                    Theme.of(context).colorScheme.onBackground,
                              ),
                              textCapitalization: TextCapitalization.none,
                              autocorrect: false,
                              decoration: InputDecoration(
                                border: const OutlineInputBorder(),
                                labelText: translate("Last Name"),
                                hintText: "Enter your last name",
                              ),
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      DropdownMenu<Countries>(
                        controller: _countryController,
                        textStyle: Theme.of(context)
                            .textTheme
                            .titleMedium!
                            .copyWith(
                                color: Theme.of(context).colorScheme.primary),
                        menuHeight: 350,
                        label: Text(
                          translate("Select Country"),
                          style: Theme.of(context)
                              .textTheme
                              .labelLarge!
                              .copyWith(
                                color:
                                    Theme.of(context).colorScheme.onBackground,
                              ),
                        ),
                        expandedInsets: EdgeInsets.zero,
                        onSelected: (Countries? country) {
                          if (country == null) return;
                          _selectedCountry = country.id!;
                          _isDirty = true;
                        },
                        dropdownMenuEntries: accountEdit!.countries!
                            .map<DropdownMenuEntry<Countries>>(
                                (Countries country) {
                          return DropdownMenuEntry<Countries>(
                              value: country, label: country.name!);
                        }).toList(),
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      TextFormField(
                        controller: _cityController,
                        onChanged: (value) => _isDirty = true,
                        style: TextStyle(
                          color: Theme.of(context).colorScheme.onBackground,
                        ),
                        textCapitalization: TextCapitalization.none,
                        autocorrect: false,
                        decoration: InputDecoration(
                          border: const OutlineInputBorder(),
                          labelText: translate("City"),
                          hintText: "Enter City",
                        ),
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      TextFormField(
                        controller: _descriptionController,
                        onChanged: (value) => _isDirty = true,
                        style: TextStyle(
                          color: Theme.of(context).colorScheme.onBackground,
                        ),
                        textCapitalization: TextCapitalization.none,
                        autocorrect: false,
                        maxLines: 3,
                        keyboardType: TextInputType.multiline,
                        decoration: InputDecoration(
                          border: const OutlineInputBorder(),
                          labelText: translate("Description"),
                          hintText: "Something about you...",
                        ),
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      ElevatedButton(
                        onPressed: () {
                          _onSubmit();
                        },
                        style: ElevatedButton.styleFrom(
                            minimumSize: const Size.fromHeight(40),
                            backgroundColor: Colors.blue[600],
                            foregroundColor: Colors.white),
                        child: Text(
                          translate("Update"),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
      ),
    );
  }
}
