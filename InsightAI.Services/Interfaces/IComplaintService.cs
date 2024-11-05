using InsightAI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightAI.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<IEnumerable<Complaint>> GetAllAsync();
        Task<Complaint> GetAsync(int id);
        Task<Complaint> AddAsync(Complaint complaint);
        Task UpdateAsync(Complaint complaint);
        Task DeleteAsync(int id);
    }
}
