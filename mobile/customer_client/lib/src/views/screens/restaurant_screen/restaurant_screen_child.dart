import 'package:customer_client/src/models/restaurant/restaurant_details_model.dart';
import 'package:customer_client/src/views/screens/employees_screen/employees_screen.dart';
import 'package:customer_client/src/views/screens/selection_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter_translate/flutter_translate.dart';

class RestaurantScreenChild extends StatelessWidget {
  const RestaurantScreenChild({super.key, required this.restaurant});

  final RestaurantDetailsModel restaurant;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Column(
        children: [
          Stack(
            children: [
              SizedBox(
                width: double.infinity,
                child: Image(
                  image: restaurant.restaurantImages!.isNotEmpty 
                    ? NetworkImage(restaurant.restaurantImages![0],)
                    : const NetworkImage("https://noshnexus.com/images/default/default.png"),
                  fit: BoxFit.cover,
                ),
              ),
              Positioned(
                bottom: 5,
                right: 5,
                child: ElevatedButton.icon(
                  onPressed: () {},
                  icon: const Icon(Icons.image),
                  label: Text(translate("Images")),
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 15,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text(
                restaurant.name!,
                style: Theme.of(context).textTheme.titleLarge!.copyWith(
                    color: Theme.of(context).colorScheme.onBackground,
                    fontSize: 35),
              ),
              const SizedBox(
                width: 10,
              ),
              Icon(
                restaurant.isOpen! ? Icons.lock_open : Icons.lock,
                color: restaurant.isOpen! ? Colors.green : Colors.red,
              )
            ],
          ),
          const SizedBox(
            height: 10,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            children: [
              ElevatedButton.icon(
                onPressed: () {
                  Navigator.of(context).push(
                    MaterialPageRoute(
                      builder: (_) => EmployeesScreen(
                        restaurantId: restaurant.id!,
                      ),
                    ),
                  );
                },
                icon: const Icon(Icons.people),
                label: Text("${translate("Employees")} ${restaurant.employeesNumber!}"),
              ),
              ElevatedButton.icon(
                onPressed: () {
                  Navigator.of(context).push(
                    MaterialPageRoute(
                      builder: (_) =>
                          SelectionScreen(restaurantId: restaurant.id!),
                    ),
                  );
                },
                icon: const Icon(Icons.restaurant_menu),
                label: Text("${translate("Menus")} ${restaurant.menusNumber!}"),
              ),
            ],
          ),
          const SizedBox(
            height: 15,
          ),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 25),
            width: double.infinity,
            decoration: BoxDecoration(
                border:
                    Border.all(color: Theme.of(context).colorScheme.primary),
                borderRadius: BorderRadius.circular(5)),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  translate("Description"),
                  style: Theme.of(context)
                      .textTheme
                      .titleLarge!
                      .copyWith(color: Theme.of(context).colorScheme.primary),
                ),
                const SizedBox(
                  height: 15,
                ),
                Text(
                  restaurant.description!,
                  style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                      color: Theme.of(context).colorScheme.onBackground),
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 10,
          ),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 20, vertical: 25),
            width: double.infinity,
            decoration: BoxDecoration(
                border:
                    Border.all(color: Theme.of(context).colorScheme.primary),
                borderRadius: BorderRadius.circular(5)),
            child: Column(
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      translate("Country"),
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    Text(
                      restaurant.country!,
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                  ],
                ),
                const Divider(),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      translate("City"),
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    Text(
                      restaurant.city!,
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                  ],
                ),
                const Divider(),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      translate("Address"),
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    Text(
                      restaurant.address!,
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                  ],
                ),
                const Divider(),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      translate("Postal Code"),
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    Text(
                      restaurant.postalCode!.toString(),
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                  ],
                ),
                const Divider(),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      translate("Phone"),
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    Text(
                      restaurant.phoneNumber!,
                      style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                  ],
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 10,
          ),
          Container(
            padding: const EdgeInsets.symmetric(vertical: 10),
            width: double.infinity,
            decoration: BoxDecoration(
              border: Border.all(color: Theme.of(context).colorScheme.primary),
              borderRadius: BorderRadius.circular(5),
            ),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                ElevatedButton(
                  onPressed: () {},
                  child: const Text("Facebook"),
                ),
                ElevatedButton(
                  onPressed: () {},
                  child: const Text("Instagram"),
                ),
                ElevatedButton(
                  onPressed: () {},
                  child: const Text("Website"),
                ),
              ],
            ),
          )
        ],
      ),
    );
  }
}
