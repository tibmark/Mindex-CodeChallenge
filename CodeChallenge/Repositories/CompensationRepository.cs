using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private ILogger<CompensationRepository> _logger;
        private EmployeeContext _employeeContext;

        public CompensationRepository(ILogger<CompensationRepository> logger, EmployeeContext employeeContext)
        {
            this._logger = logger;
            this._employeeContext = employeeContext;
        }

        public Compensation AddCompensation( string employeeId, DateTime effectiveDate, double salary)
        {
            var employee = this._employeeContext.Employees.Where(emp => emp.EmployeeId == employeeId).FirstOrDefault();

            if (employee == null)
            {
                return null;
            }

            var compensation = new Compensation()
            {
                Employee = employee,
                EffectiveDate = effectiveDate,
                Salary = salary
            };

            this._employeeContext.Compensations.Add(compensation);

            return compensation;
        }

        public List<Compensation> GetCompensations(string employeeId)
        {
            return this._employeeContext.Compensations.Include(emp => emp.Employee).Where(emp => emp.Employee.EmployeeId == employeeId).ToList();
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

    }
}
