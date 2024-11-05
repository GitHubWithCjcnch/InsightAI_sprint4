using InsightAI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightAI.Repositories.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> GetAsync(string companyName);
    }
}
