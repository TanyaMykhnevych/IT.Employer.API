using AutoMapper;
using IT.Employer.Domain.Enums;
using IT.Employer.Domain.Models.EmployeeN;
using IT.Employer.Domain.Models.Hiring;
using IT.Employer.Domain.Models.TeamN;
using IT.Employer.Entities.Models.Hiring;
using IT.Employer.Services.Services.PricePolicies;
using IT.Employer.Services.Stores;
using IT.Employer.Services.Stores.Hiring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT.Employer.Services.Services.Hiring
{
    public class HireService : IHireService
    {
        private readonly IPricePolicyService _pricePolicyService;
        private readonly IHireStore _hireStore;
        private readonly ITeamStore _teamStore;
        private readonly IEmployeeStore _employeeStore;
        private readonly IMapper _mapper;

        public HireService(
            IPricePolicyService pricePolicyService,
            IHireStore hireStore,
            ITeamStore teamStore,
            IEmployeeStore employeeStore,
            IMapper mapper)
        {
            _pricePolicyService = pricePolicyService;
            _hireStore = hireStore;
            _teamStore = teamStore;
            _employeeStore = employeeStore;
            _mapper = mapper;
        }

        public async Task ApproveHire(Guid id)
        {
            Hire hire = _hireStore.GetById(id);
            hire.Status = HireStatus.Approved;

            if (hire.TeamId.HasValue)
            {
                hire.Team.Members.ForEach(e => e.Inactive = true);
                await _teamStore.Update(hire.Team);
            }
            else
            {
                hire.Employee.Inactive = true;
                await _employeeStore.Update(hire.Employee);
            }

            await _hireStore.Update(hire);
        }

        public Task<Guid> CreateHire(HireDTO hireDTO)
        {
            Hire hire = _mapper.Map<HireDTO, Hire>(hireDTO);
            hire.Status = HireStatus.Open;
            hire.TotalHiringRate = GetTotalHiringRate(hire);

            return _hireStore.Create(hire);
        }

        public Task DeclineHire(Guid id)
        {
            Hire hire = _hireStore.GetById(id);
            hire.Status = HireStatus.Declined;

            return _hireStore.Update(hire);
        }

        public List<HireDTO> GetCompanyOffers(Guid companyId)
        {
            List<Hire> companyHires = _hireStore.GetHiresByCompanyId(companyId);

            return companyHires.Select(_mapper.Map<Hire, HireDTO>).ToList();
        }

        public List<HireDTO> GetCompanySentOffers(Guid companyId)
        {
            List<Hire> companyHires = _hireStore.GetSentHiresByCompanyId(companyId);

            return companyHires.Select(_mapper.Map<Hire, HireDTO>).ToList();
        }

        public HireDTO GetHireById(Guid id)
        {
            Hire hire = _hireStore.GetById(id);

            return _mapper.Map<Hire, HireDTO>(hire);
        }

        private Decimal GetTotalHiringRate(Hire hire)
        {
            if (hire.TeamId.HasValue)
            {
                Team team = _teamStore.GetById(hire.TeamId.Value);

                return _pricePolicyService.CalculateTeamHiringHourPrice(
                    team.Members.Select(e => e.HourRate).ToArray());
            }

            Employee employee = _employeeStore.GetById(hire.EmployeeId.Value);

            return _pricePolicyService.CalculateHiringHourPrice(employee.HourRate, 1);
        }
    }
}
