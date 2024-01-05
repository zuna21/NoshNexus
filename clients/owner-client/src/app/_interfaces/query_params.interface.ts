export interface IMenusQueryParams {
  pageIndex: number;
  search: string | null;
  activity: string;
  restaurant: number | null;
}

export interface ITablesQueryParams {
  pageIndex: number;
  pageSize: number;
  search: string | null;
  restaurant: number | null;
}

export interface IRestaurantsQueryParams {
  search: string | null;
}

export interface IEmployeesQueryParams {
  pageIndex: number;
  search: string | null;
  restaurant: number | null;
}

export interface IMenuItemsQueryParams {
  pageIndex: number;
  search: string | null;
  offer: string;
}

export interface IOrdersHistoryQueryParams {
  restaurant: number | null;
  status: string;
  search: string | null;
}

export interface IOrdersQueryParams {
  restaurant: number | null;
  search: string | null;
}

export interface IBlockedCustomersParams {
  pageIndex: number;
  restaurant: number | null;
  search: string | null;
}