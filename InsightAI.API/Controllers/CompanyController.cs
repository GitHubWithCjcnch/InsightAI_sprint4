using InsightAI.Models.Models;
using InsightAI.Repositories.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightAI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company> _companyRepository;

        public CompanyController(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _companyRepository.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            await _companyRepository.AddAsync(company);
            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            try
            {
                await _companyRepository.UpdateAsync(company);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Consider improving error handling
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}