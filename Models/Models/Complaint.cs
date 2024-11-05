using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightAI.Models.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public DateTime DateFiled { get; set; }
        public Company? Company { get; set; }
    }
}
