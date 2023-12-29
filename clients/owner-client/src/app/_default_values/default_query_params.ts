import {
  IMenusQueryParams,
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
  restaurant: null
};
