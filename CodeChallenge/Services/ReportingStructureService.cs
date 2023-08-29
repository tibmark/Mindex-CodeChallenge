using CodeChallenge.Models;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private IReportingStructureRepository _reportingStructureRepository;
        public ReportingStructureService(IReportingStructureRepository reportingStructureRepository) 
        {
            this._reportingStructureRepository = reportingStructureRepository;
        }

        public ReportingStructure GetReportingStructure(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                return null;
            }

            return this._reportingStructureRepository.GetReportingStructure(employeeId);
        }
    }
}
