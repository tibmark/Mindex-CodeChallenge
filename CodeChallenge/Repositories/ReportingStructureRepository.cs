using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CodeChallenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly EmployeeContext _employeeContext;

        public ReportingStructureRepository( EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        private Employee getEmployee(string employeeId)
        {
            return this._employeeContext.Employees.Include(emp => emp.DirectReports).Where(emp => emp.EmployeeId == employeeId).FirstOrDefault();
        }

        public ReportingStructure GetReportingStructure(string employeeId)
        {
            var employee = this.getEmployee(employeeId);

            if (employee == null)
            {
                return null;
            }

            return new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = this.getDirectReportsCount(employee)
            };
        }

        private int getDirectReportsCount(Employee employee)
        {
            if (employee != null && employee.DirectReports != null)
            {
                var reportsCount = employee.DirectReports.Count;

                foreach (var directReport in employee.DirectReports)
                {
                    var emp = this.getEmployee(directReport.EmployeeId);

                    reportsCount += this.getDirectReportsCount(emp);
                }

                return reportsCount;
            }
            else
            {
                return 0;
            }
        }
    }
}