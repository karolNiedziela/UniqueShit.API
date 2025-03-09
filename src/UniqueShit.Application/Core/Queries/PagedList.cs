namespace UniqueShit.Application.Core.Queries
{
    public sealed class PagedList<T> : PagedBase
    {

        public IReadOnlyCollection<T> Items { get; }


        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
            : base(pageNumber, pageSize, totalCount)
        {
            Items = items.ToList();
        }
    }
}
