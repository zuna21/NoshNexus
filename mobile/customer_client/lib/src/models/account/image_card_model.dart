class ImageCardModel {
  int? id;
  String? url;
  int? size;

  ImageCardModel({this.id, this.url, this.size});

  ImageCardModel.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    url = json['url'];
    size = json['size'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['url'] = url;
    data['size'] = size;
    return data;
  }
}