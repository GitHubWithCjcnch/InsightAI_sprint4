﻿using InsightAI.Models.Models;
using InsightAI.Repositories.Data;
using InsightAI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightAI.Services.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly InsightAIDbContext _context;

        public CompanyService(InsightAIDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Company>>GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task UpdateAsync(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();

            }
        }
    }

}
