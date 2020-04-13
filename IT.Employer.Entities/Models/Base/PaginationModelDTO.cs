using System;
using System.ComponentModel.DataAnnotations;

namespace IT.Employer.Entities.Models.Base
{
    public class PaginationModelDTO
    {
        [Required]
        [Range(0, Int32.MaxValue)]
        public Int32 Page { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public Int32 PerPage { get; set; }
    }
}
