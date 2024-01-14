class OrderCardModel {
  int? id;
  User? user;
  Restaurant? restaurant;
  String? tableName;
  String? note;
  double? totalPrice;
  int? totalItems;
  List<Items>? items;
  String? status;
  String? declineReason;
  String? createdAt;

  OrderCardModel(
      {this.id,
      this.user,
      this.restaurant,
      this.tableName,
      this.note,
      this.totalPrice,
      this.totalItems,
      this.items,
      this.status,
      this.declineReason,
      this.createdAt});

  OrderCardModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    user = json['user'] != null ? User.fromJson(json['user']) : null;
    restaurant = json['restaurant'] != null
        ? Restaurant.fromJson(json['restaurant'])
        : null;
    tableName = json['tableName'];
    note = json['note'];
    totalPrice = (json['totalPrice'] as num).toDouble();
    totalItems = json['totalItems'];
    if (json['items'] != null) {
      items = <Items>[];
      json['items'].forEach((v) {
        items!.add(Items.fromJson(v));
      });
    }
    status = json['status'];
    declineReason = json['declineReason'];
    createdAt = json['createdAt'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    if (user != null) {
      data['user'] = user!.toJson();
    }
    if (restaurant != null) {
      data['restaurant'] = restaurant!.toJson();
    }
    data['tableName'] = tableName;
    data['note'] = note;
    data['totalPrice'] = totalPrice;
    data['totalItems'] = totalItems;
    if (items != null) {
      data['items'] = items!.map((v) => v.toJson()).toList();
    }
    data['status'] = status;
    data['declineReason'] = declineReason;
    data['createdAt'] = createdAt;
    return data;
  }
}

class User {
  int? id;
  String? username;
  String? profileImage;
  String? firstName;
  String? lastName;

  User(
      {this.id,
      this.username,
      this.profileImage,
      this.firstName,
      this.lastName});

  User.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    username = json['username'];
    profileImage = json['profileImage'];
    firstName = json['firstName'];
    lastName = json['lastName'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['username'] = username;
    data['profileImage'] = profileImage;
    data['firstName'] = firstName;
    data['lastName'] = lastName;
    return data;
  }
}

class Restaurant {
  int? id;
  String? name;

  Restaurant({this.id, this.name});

  Restaurant.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    name = json['name'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    return data;
  }
}

class Items {
  int? id;
  String? name;
  double? price;

  Items({this.id, this.name, this.price});

  Items.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    name = json['name'];
    price = (json['price'] as num).toDouble() ;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    data['price'] = price;
    return data;
  }
}
