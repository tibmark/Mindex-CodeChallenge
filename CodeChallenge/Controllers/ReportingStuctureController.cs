using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reporting-structure")]
    public class ReportingStuctureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStuctureController(ILogger<EmployeeController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{employeeId}", Name = "getNumberOfReports")]
        public IActionResult GetNumberOfReports(string employeeId)
        {
            this._logger.LogDebug($"Recieved get reporting structure for employee {employeeId}");

            var reportingStructure = this._reportingStructureService.GetReportingStructure(employeeId);

            return reportingStructure == null ? NotFound() : Ok(reportingStructure);
        }
    }
}