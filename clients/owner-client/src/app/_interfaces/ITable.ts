export interface ITableCard {
  id: number;
  name: string;
  restaurant: {
    id: number;
    name: string;
  };
}

export interface ITable {
  id: number;
  name: string;
}

export interface IGetTableQrCode {
  id: number;
  name: string;
  url: string;
}