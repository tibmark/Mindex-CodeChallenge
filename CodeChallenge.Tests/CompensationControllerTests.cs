using CodeChallenge.Dto;
using CodeChallenge.Models;
using CodeChallenge.Tests.Integration.Extensions;
using CodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests : TestBase
    {
        [TestMethod]
        [DataRow("16a596ae-edd3-4847-99fe-c4518e82c86f", 5, 15, 2023, 21.23)]
        [DataRow("b7839309-3348-463b-a7e3-5de1c168beb3", 7, 6, 2000, 1456.35)]
        [DataRow("03aa1462-ffa9-4978-901b-7c001562cf6f", 11, 19, 2022, 685.35)]
        [DataRow("62c1084e-6e34-4630-93fd-9153afb65309", 1, 24, 2014, 52.03)]
        public void AddCompensation_Returns_Created(string employeeId, int month, int day, int year, double salary)
        {
            var comp = this.createDto(employeeId, month, day, year, salary);

            var requestContent = new JsonSerialization().ToJson(comp);

            var getRequestTask = _httpClient.PostAsync($"api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = getRequestTask.Result;

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created);

            var compensation = response.DeserializeContent<CompensationDto>();

            Assert.IsNotNull(compensation);
            Assert.AreEqual(compensation.EmployeeId, employeeId);
            Assert.AreEqual(compensation.Salary, salary);
            Assert.AreEqual(compensation.EffectiveDate, new DateTime(year, month, day));
        }


        [TestMethod]
        public void AddCompensation_Returns_NotFound()
        {
            var comp = new CompensationDto()
            {
                EmployeeId = "",
                EffectiveDate = DateTime.Now,
                Salary = 1.00
            };

            var requestContent = new JsonSerialization().ToJson(comp);

            var postRequestTask = _httpClient.PostAsync($"api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        public void AddCompensation_Returns_Created()
        {
            var comp = new CompensationDto()
            {
                EmployeeId =  "16a596ae-edd3-4847-99fe-c4518e82c86f",
                EffectiveDate = DateTime.Now,
                Salary = 235.85
            };

            var requestContent = new JsonSerialization().ToJson(comp);

            var postRequestTask = _httpClient.PostAsync($"api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created);

            var compensation = response.DeserializeContent<CompensationDto>();

            Assert.AreEqual(comp.EmployeeId, compensation.EmployeeId);
            Assert.AreEqual(comp.Salary, compensation.Salary);
            Assert.AreEqual(comp.EffectiveDate, compensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensation_Returns_SuppliedValues()
        {
            var comp = new CompensationDto()
            {
                EmployeeId =  "c0c2293d-16bd-4603-8e08-638a9d18b22c",
                EffectiveDate = DateTime.Now,
                Salary = 235.85
            };

            var requestContent = new JsonSerialization().ToJson(comp);

            var postRequestTask = _httpClient.PostAsync($"api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;

            Assert.IsTrue(postResponse.IsSuccessStatusCode);

            var getRequestTask = _httpClient.GetAsync($"api/compensation/{comp.EmployeeId}");
            var response = getRequestTask.Result;
            var compensation = response.DeserializeContent<List<CompensationDto>>().FirstOrDefault();

            Assert.AreEqual(compensation.EmployeeId, comp.EmployeeId);
            Assert.AreEqual(compensation.Salary, comp.Salary);
            Assert.AreEqual(compensation.EffectiveDate, comp.EffectiveDate);
        }

       [TestMethod]
        public void GetCompensation_Returns_EmptyList()
        {
            var comp = new CompensationDto()
            {
                EmployeeId =  "FakeEmployeeId",
                EffectiveDate = DateTime.Now,
                Salary = 235.85
            };

            var requestContent = new JsonSerialization().ToJson(comp);

            var postRequestTask = _httpClient.PostAsync($"api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;

            var getRequestTask = _httpClient.GetAsync($"api/compensation/{comp.EmployeeId}");
            var response = getRequestTask.Result;

            Assert.IsTrue(response.IsSuccessStatusCode);

            var compensation = response.DeserializeContent<List<CompensationDto>>();

            Assert.IsTrue(compensation.Count == 0);
        }


        private CompensationDto createDto(string employeeId, int month, int day, int year, double salary)
        {
            return new CompensationDto()
            {
                EmployeeId = employeeId,
                EffectiveDate = new DateTime(year, month, day),
                Salary = salary
            };
        }
    }
}