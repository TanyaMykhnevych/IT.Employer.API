using System;

namespace IT.Employer.Entities.Models.Base
{
    public class BaseEntityDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
