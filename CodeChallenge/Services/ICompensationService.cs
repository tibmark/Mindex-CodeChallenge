using CodeChallenge.Dto;
using CodeChallenge.Models;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        CompensationDto AddCompensation(CompensationDto commpensation);

        List<CompensationDto> GetCompensations(string employeeId);
    }
}
