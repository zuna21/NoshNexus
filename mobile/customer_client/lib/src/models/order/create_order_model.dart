class CreateOrderModel {
  int? tableId;
  String? note;
  List<int>? menuItemIds;

  CreateOrderModel({this.tableId, this.note, this.menuItemIds});

  CreateOrderModel.fromJson(Map<String, dynamic> json) {
    tableId = json['tableId'];
    note = json['note'];
    menuItemIds = json['menuItemIds'].cast<int>();
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['tableId'] = tableId;
    data['note'] = note;
    data['menuItemIds'] = menuItemIds;
    return data;
  }
}
