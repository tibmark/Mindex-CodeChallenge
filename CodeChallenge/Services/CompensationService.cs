using CodeChallenge.Dto;
using CodeChallenge.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private ICompensationRepository _compensationRepository;

        public CompensationService(ICompensationRepository compensationRepository)
        {
            this._compensationRepository = compensationRepository;
        }

        public CompensationDto AddCompensation(CompensationDto compensation)
        {
            if (string.IsNullOrEmpty(compensation.EmployeeId))
            {
                return null;
            }

            var comp = this._compensationRepository.AddCompensation(compensation.EmployeeId, compensation.EffectiveDate, compensation.Salary);

            if (comp != null)
            {
                this._compensationRepository.SaveAsync();

                return new CompensationDto()
                {
                    EffectiveDate = comp.EffectiveDate,
                    EmployeeId = comp.Employee.EmployeeId,
                    Salary = comp.Salary,
                };
            }

            return null;
        }

        public List<CompensationDto> GetCompensations(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                new List<CompensationDto>();
            }

           return this._compensationRepository.GetCompensations(employeeId).Select(comp => new CompensationDto()
            {
                EffectiveDate = comp.EffectiveDate,
                EmployeeId = comp.Employee.EmployeeId,
                Salary = comp.Salary
            }).ToList();
        }
    }
}