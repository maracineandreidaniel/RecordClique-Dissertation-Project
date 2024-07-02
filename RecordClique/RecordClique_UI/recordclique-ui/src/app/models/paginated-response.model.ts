export interface PaginatedResponse<T> {
    Items: T[];
    TotalItems: number;
    PageNumber: number;
    PageSize: number;
}