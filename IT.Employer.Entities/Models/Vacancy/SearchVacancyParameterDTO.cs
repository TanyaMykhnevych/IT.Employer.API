using IT.Employer.Entities.Enums;
using IT.Employer.Entities.Models.Base;

namespace IT.Employer.Entities.Models.VacancyN
{
    public class SearchVacancyParameterDTO : PaginationModelDTO
    {
        public ProfessionDTO Profession { get; set; }
        public PositionDTO Position { get; set; }
        public TechnologyDTO PrimaryTechnology { get; set; }

        public int? ExperienceFrom { get; set; }
        public int? ExperienceTo { get; set; }
    }
}
