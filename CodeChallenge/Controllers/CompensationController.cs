using CodeChallenge.Dto;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private ICompensationService _compensationService;
        private ILogger<CompensationController> _logger;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            this._compensationService = compensationService;
            this._logger = logger;
        }

        [HttpGet("{employeeId}", Name = "getcompensation")]
        public ActionResult GetCompensation(string employeeId)
        {
            _logger.LogDebug($"Received get compensation for employee {employeeId}");

            var comp = this._compensationService.GetCompensations(employeeId);

            return Ok(comp);
        }

        [HttpPost]
        public ActionResult AddCompensation([FromBody] CompensationDto compensation)
        {
            _logger.LogDebug($"Received add compensation for employee {compensation.EmployeeId}");

            var comp = this._compensationService.AddCompensation(compensation);

            if (comp == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("getcompensation", new { employeeId = comp.EmployeeId }, comp);
        }
    }
}