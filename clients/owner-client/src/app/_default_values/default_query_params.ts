import {
  IBlockedCustomersParams,
  IEmployeesQueryParams,
  IMenuItemsQueryParams,
  IMenusQueryParams,
  IOrdersHistoryQueryParams,
  IOrdersQueryParams,
  IRestaurantsQueryParams,
  ITablesQueryParams,
} from '../_interfaces/query_params.interface';

export const MENUS_QUERY_PARAMS: IMenusQueryParams = {
  activity: 'all',
  pageIndex: 0,
  restaurant: null,
  search: null,
};

export const TABLES_QUERY_PARAMS: ITablesQueryParams = {
  pageIndex: 0,
  pageSize: 25,
  search: null,
  restaurant: null,
};

export const RESTAURANTS_QUERY_PARAMS: IRestaurantsQueryParams = {
  search: null,
};

export const EMPLOYEES_QUERY_PARAMS: IEmployeesQueryParams = {
  pageIndex: 0,
  search: null,
  restaurant: null,
};

export const MENU_ITEMS_QUERY_PARAMS: IMenuItemsQueryParams = {
  pageIndex: 0,
  search: null,
  offer: 'all',
};

export const ORDERS_HISTORY_QUERY_PARAMS: IOrdersHistoryQueryParams = {
  pageIndex: 0,
  restaurant: null,
  status: 'all',
  search: null,
};

export const ORDERS_QUERY_PARAMS: IOrdersQueryParams = {
  restaurant: null,
  search: null,
};

export const BLOCKED_CUSTOMERS_PARAMS: IBlockedCustomersParams = {
  pageIndex: 0,
  restaurant: null,
  search: null
};