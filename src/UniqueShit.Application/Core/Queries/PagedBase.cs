namespace UniqueShit.Application.Core.Queries
{
    public abstract class PagedBase
    {
        public const int MaxPageSize = 100;

        public const int MinPageSize = 10;

        public const int DefaultPageSize = 10;

        public const int DefaultPageNumber = 1;

        public int PageNumber { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public bool HasNextPage => PageNumber * PageSize < TotalCount;

        public bool HasPreviousPage => PageNumber > 1;

        protected PagedBase()
        {
            PageNumber = 1;
            PageSize = MinPageSize;
        }
        protected PagedBase(int pageNumber, int pageSize, int totalItems)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (pageSize < MinPageSize)
            {
                pageSize = DefaultPageSize;
            }

            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalItems;
        }
    }
}
