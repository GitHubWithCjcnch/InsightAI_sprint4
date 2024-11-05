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
    public class PredictionRepository : IRepository<Prediction>
    {
        private readonly InsightAIDbContext _context;

        public PredictionRepository(InsightAIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prediction>> GetAllAsync()
        {
            return await _context.Predictions.ToListAsync();
        }

        public async Task<Prediction> GetByIdAsync(int id)
        {
            return await _context.Predictions.FindAsync(id);

        }

        public async Task<Prediction> AddAsync(Prediction prediction)
        {
            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();
            return prediction;
        }

        public async Task UpdateAsync(Prediction prediction)

        {
            _context.Entry(prediction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var prediction = await _context.Predictions.FindAsync(id);
            if (prediction != null)
            {
                _context.Predictions.Remove(prediction);
                await _context.SaveChangesAsync();

            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
