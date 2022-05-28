using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BestCompanyWebApp.Models
{
    public class Employee
    {
        [Key]
        [JsonPropertyName("EmployeeId")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("EmployeeName")]
        public string EmployeeName { get; set; }

        [JsonPropertyName("EmployeePhoneNumber")]
        public string EmployeePhoneNumber{ get; set; }
        
    }
}
