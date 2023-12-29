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