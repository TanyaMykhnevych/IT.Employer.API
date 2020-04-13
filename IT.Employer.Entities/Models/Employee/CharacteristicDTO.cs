using IT.Employer.Entities.Models.Base;
using System;

namespace IT.Employer.Entities.Models.EmployeeN
{
    public class CharacteristicDTO : BaseEntityDTO
    {
        public Guid EmployeeId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public EmployeeDTO Employee { get; set; }
    }
}
