using IT.Employer.Domain.Models.Base;
using System;

namespace IT.Employer.Domain.Models.EmployeeN
{
    public class Characteristic : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public Employee Employee { get; set; }
    }
}
