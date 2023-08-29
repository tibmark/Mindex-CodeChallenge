using CodeChallenge.Models;
using CodeChallenge.Tests.Integration.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests : TestBase
    {
        [TestMethod]
        [DataRow("16a596ae-edd3-4847-99fe-c4518e82c86f", 4)]
        [DataRow("03aa1462-ffa9-4978-901b-7c001562cf6f", 2)]
        public void GetReportingStructure_MultipleEmployees_Returns_Ok(string employeeId, int directReports)
        {
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var response = getRequestTask.Result;

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var reportingStructure = response.DeserializeContent<ReportingStructure>();

            Assert.IsNotNull(reportingStructure.Employee);

            Assert.AreEqual(employeeId, reportingStructure.Employee.EmployeeId);

            Assert.AreEqual(reportingStructure.NumberOfReports, directReports);
        }

        [TestMethod]
        [DataRow("fakeEmployeeId", 2)]
        public void GetReportingStructure_ThrowsError_NotFound(string employeeId, int numberOfReports)
        {
            var getRequestTask = _httpClient.GetAsync($"api/reporting-structure/{employeeId}");
            var response = getRequestTask.Result;

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}