using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightAI.Models.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Type { get; set; }
        public string PredictionResult { get; set; }
        public DateTime GeneratedOn { get; set; }
        public Company? Company { get; set; }
    }

}
