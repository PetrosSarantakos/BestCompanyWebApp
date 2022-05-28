using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BestCompanyWebApp.Models
{
    public class ActionModel
    {
        [Key]
        [JsonPropertyName("ActionId")]
        public int ActionId { get; set; }

        [JsonPropertyName("ActionType")]
        public string ActionType { get; set; }

        [JsonPropertyName("EmployeeId")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("ActionDate")]
        public DateTime ActionDate { get; set; }

        [JsonPropertyName("Employee")]
        public virtual Employee? Employee { get; set; }
    }
}
