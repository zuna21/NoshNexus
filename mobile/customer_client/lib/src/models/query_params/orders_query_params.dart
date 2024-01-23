class OrdersQueryPrams {
  OrdersQueryPrams({this.pageSize = 10, this.pageIndex = 0, this.status = "all", this.search = ""});

  int pageSize;
  int pageIndex;
  String status;
  String search;
}