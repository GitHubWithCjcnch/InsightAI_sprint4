using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InsightAI.Models.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }

        [JsonIgnore]
        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

        [JsonIgnore]
        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}
