export interface PaginatedList<T> {
    items: any[];  // Use `any[]` or a more specific type if you have a defined member type
    pageNumber: number;
    totalPages: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
  }
  

  