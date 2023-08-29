using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public Guid CompensationId { get; set; }
        public Employee Employee { get; set; }

        public double Salary { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}