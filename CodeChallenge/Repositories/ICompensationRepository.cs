using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation AddCompensation(string employeeId, DateTime effectiveDate, double salary);

        List<Compensation> GetCompensations(string employeeId);
        Task SaveAsync();
    }
}
