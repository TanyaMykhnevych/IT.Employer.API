using System.Collections.Generic;

namespace IT.Employer.Entities.Models.Base
{
    public class SearchResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
