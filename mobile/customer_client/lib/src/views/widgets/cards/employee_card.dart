import 'package:customer_client/src/models/employee/employee_card_model.dart';
import 'package:flutter/material.dart';

class EmployeeCard extends StatelessWidget {
  const EmployeeCard({super.key, required this.employee, this.onRestaurant, this.onViewMore});

  final EmployeeCardModel employee;
  final void Function(int restaurantId)? onRestaurant;
  final void Function(int employeeId)? onViewMore;

  @override
  Widget build(BuildContext context) {return Card(
      child: Container(
        height: 500,
        decoration: BoxDecoration(
          border: Border.all(color: Theme.of(context).colorScheme.primary),
          borderRadius: BorderRadius.circular(5),
        ),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Stack(
              clipBehavior: Clip.none,
              children: [
                Container(
                  height: 150,
                  width: double.infinity,
                  decoration: BoxDecoration(
                    image: DecorationImage(
                      image: NetworkImage(employee.restaurant!.profileImage!),
                    ),
                    color: Colors.black,
                  ),
                ),
                Positioned(
                  bottom: -50,
                  left: 20,
                  child: Container(
                    height: 150,
                    width: 150,
                    decoration: BoxDecoration(
                      border: Border.all(
                          color: Theme.of(context).colorScheme.primary),
                      borderRadius: BorderRadius.circular(100),
                      image: DecorationImage(
                        image: NetworkImage(employee.profileImage!),
                      ),
                    ),
                  ),
                ),
              ],
            ),
            Padding(
              padding: const EdgeInsets.only(right: 10),
              child: SizedBox(
                width: double.infinity,
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      employee.username!,
                      style: Theme.of(context).textTheme.titleLarge!.copyWith(
                          color: Theme.of(context).colorScheme.onBackground),
                    ),
                    Text(
                      "${employee.lastName!} ${employee.firstName!}",
                      style: Theme.of(context)
                          .textTheme
                          .titleMedium!
                          .copyWith(color: Theme.of(context).colorScheme.primary),
                    ),
                  ],
                ),
              ),
            ),
            Padding(
              padding: const EdgeInsets.symmetric(
                horizontal: 10
              ),
              child: Text(
                employee.description!.length > 400
                    ? "${employee.description!.substring(0, 400)}..."
                    : employee.description!,
                style: Theme.of(context).textTheme.bodyMedium!.copyWith(
                    color: Theme.of(context).colorScheme.onPrimaryContainer),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(10),
              child: Column(
                children: [
                  SizedBox(
                    width: double.infinity,
                    child: ElevatedButton(
                      onPressed: () {
                        onViewMore!(employee.id!);
                      },
                      style: ElevatedButton.styleFrom(
                          backgroundColor:
                              Theme.of(context).colorScheme.primaryContainer,
                          foregroundColor:
                              Theme.of(context).colorScheme.onPrimaryContainer),
                      child: const Text("More Info"),
                    ),
                  ),
                  SizedBox(
                    width: double.infinity,
                    child: ElevatedButton(
                      onPressed: (){
                        onRestaurant!(employee.restaurant!.id!);
                      },
                      style: ElevatedButton.styleFrom(
                          backgroundColor: Theme.of(context).colorScheme.primary,
                          foregroundColor: Colors.black,
                          textStyle:
                              const TextStyle(fontWeight: FontWeight.bold)),
                      child: Text(employee.restaurant!.name!),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
