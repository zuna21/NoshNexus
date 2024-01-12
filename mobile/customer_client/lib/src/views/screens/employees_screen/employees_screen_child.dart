import 'package:customer_client/src/models/employee/employee_card_model.dart';
import 'package:customer_client/src/views/screens/employee_screen/employee_screen.dart';
import 'package:customer_client/src/views/screens/restaurant_screen/restaurant_screen.dart';
import 'package:customer_client/src/views/widgets/cards/employee_card.dart';
import 'package:flutter/material.dart';

class EmployeesScreenChild extends StatefulWidget {
  const EmployeesScreenChild({super.key, required this.employees});

  final List<EmployeeCardModel> employees;

  @override
  State<EmployeesScreenChild> createState() => _EmployeesScreenChildState();
}

class _EmployeesScreenChildState extends State<EmployeesScreenChild> {
  void _onRestaurant(int restaurantId) {
    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (_) => RestaurantScreen(restaurantId: restaurantId),
      ),
    );
  }

  void _onViewMore(int employeeId) {
    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (_) => const EmployeeScreen(),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: widget.employees.length,
      itemBuilder: ((context, index) {
        return EmployeeCard(
          employee: widget.employees[index],
          onRestaurant: _onRestaurant,
          onViewMore: _onViewMore,
        );
      }),
    );
  }
}
