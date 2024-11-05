using InsightAI.Models.Models;
using InsightAI.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightAI.Repositories.Repositories
{
    public class ComplaintRepository : IRepository<Complaint>
    {
        private readonly InsightAIDbContext _context;

        public ComplaintRepository(InsightAIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Complaint>> GetAllAsync()
        {
            return await _context.Complaints.ToListAsync();
        }

        public async Task<Complaint> GetByIdAsync(int id)
        {
            return await _context.Complaints.FindAsync(id);

        }

        public async Task<Complaint> AddAsync(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }

        public async Task UpdateAsync(Complaint complaint)

        {
            _context.Entry(complaint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();

            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
