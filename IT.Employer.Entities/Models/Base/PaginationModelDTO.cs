using System;
using System.ComponentModel.DataAnnotations;

namespace IT.Employer.Entities.Models.Base
{
    public class PaginationModelDTO
    {
        [Range(0, Int32.MaxValue)]
        public int? Page { get; set; }

        [Range(0, Int32.MaxValue)]
        public int? PerPage { get; set; }
    }
}
