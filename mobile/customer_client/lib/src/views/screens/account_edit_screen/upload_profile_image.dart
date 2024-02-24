import 'dart:io';

import 'package:customer_client/src/models/account/get_account_edit_model.dart';
import 'package:customer_client/src/models/account/image_card_model.dart';
import 'package:customer_client/src/services/account_service.dart';
import 'package:customer_client/src/views/screens/account_edit_screen/select_image_sheet.dart';
import 'package:flutter/material.dart';
import 'package:flutter_translate/flutter_translate.dart';
import 'package:image_picker/image_picker.dart';

class UploadProfileImage extends StatefulWidget {
  const UploadProfileImage({super.key, this.profileImage});

  final ProfileImage? profileImage;

  @override
  State<UploadProfileImage> createState() => _UploadProfileImageState();
}

class _UploadProfileImageState extends State<UploadProfileImage> {
  final AccountService _accountService = const AccountService();
  File? _image;
  final picker = ImagePicker();
  ImageCardModel? imageCardModel;

  

  Future<void> _onUpload() async {
    final result = await showModalBottomSheet(
      context: context,
      builder: (_) => const SelectImageSheet(),
    );
    if (result == "gallery") {
      await getImageFromGallery();
    } else if (result == "camera") {
      await getImageFromCamera();
    } else {
      return;
    }
  }

  Future<void> getImageFromGallery() async {
    final pickedFile = await picker.pickImage(source: ImageSource.gallery);
    if (pickedFile == null) return;
    setState(() {
      _image = File(pickedFile.path);
    });
  }

  Future<void> getImageFromCamera() async {
    final pickedFile = await picker.pickImage(source: ImageSource.camera);
    if (pickedFile == null) return;
    setState(() {
      _image = File(pickedFile.path);
    });
  }

  void _onSave() async {
    if (_image == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text("Please select image to save."),
        ),
      );
      return;
    }
    try {
      imageCardModel = await _accountService.uploadImage(_image!);
      _image = null;
      setState(() {
        ScaffoldMessenger.of(context).clearSnackBars();
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text("Successfully added image"),
          ),
        );
      });
    } catch (err) {
      print(err.toString());
    }
  }

  @override
  void initState() {
    super.initState();
    widget.profileImage != null
    ? imageCardModel = ImageCardModel(
      id: widget.profileImage!.id,
      size: 0,
      url: widget.profileImage!.url
    )
    : imageCardModel = null;
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Container(
          width: 150,
          height: 150,
          clipBehavior: Clip.hardEdge,
          decoration: BoxDecoration(
            border: Border.all(
              color: Theme.of(context).colorScheme.primary,
            ),
            borderRadius: BorderRadius.circular(100),
          ),
          child: _image == null && imageCardModel == null
              ? Image.network(
                  "https://noshnexus.com/images/default/default-profile.png",
                  fit: BoxFit.cover,
                )
              : imageCardModel != null && _image == null
                  ? Image.network(imageCardModel!.url!)
                  : Image.file(_image!),
        ),
        const SizedBox(
          width: 20,
        ),
        Expanded(
          child: Column(
            children: [
              ElevatedButton(
                onPressed: () async {
                  await _onUpload();
                },
                style: ElevatedButton.styleFrom(
                  minimumSize: const Size.fromHeight(40),
                  backgroundColor: Colors.blue[600],
                  foregroundColor: Colors.white,
                ),
                child: Text(translate("Add")),
              ),
              ElevatedButton(
                onPressed: () {
                  setState(() {
                    _image = null;
                  });
                },
                style: ElevatedButton.styleFrom(
                    minimumSize: const Size.fromHeight(40),
                    backgroundColor: Colors.red.shade600,
                    foregroundColor: Colors.white),
                child: Text(translate("Remove")),
              ),
              ElevatedButton(
                onPressed: () {
                  _onSave();
                },
                style: ElevatedButton.styleFrom(
                    minimumSize: const Size.fromHeight(40),
                    backgroundColor: Theme.of(context).colorScheme.primary,
                    foregroundColor: Theme.of(context).colorScheme.onPrimary),
                child: Text(translate("Save"),),
              ),
            ],
          ),
        ),
      ],
    );
  }
}
