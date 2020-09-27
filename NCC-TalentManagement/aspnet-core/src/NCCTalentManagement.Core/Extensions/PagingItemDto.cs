using System.Collections.Generic;

namespace NCCTalentManagement.Extensions
{
    public class PagingItemDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
