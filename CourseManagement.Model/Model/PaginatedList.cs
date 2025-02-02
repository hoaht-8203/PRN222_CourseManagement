namespace CourseManagement.Model.Model
{
    public class PaginatedList<T>
    {
       
        public List<T> Items { get; private set; }

        public int CurrentPage { get; private set; }
  
        public int TotalCount { get; private set; }
       
        public int PageSize { get; private set; }
   
        public int TotalPages { get; private set; }

        public bool HasPreviousPage => CurrentPage > 1;
  
        public bool HasNextPage => CurrentPage < TotalPages;

       
        public PaginatedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
        
    }
}
