using InsightAI.Models.Models;
using InsightAI.Repositories.Repositories;
using InsightAI_MLModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InsightAI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictionController : ControllerBase
    {
        private readonly IRepository<Prediction> _predictionRepository;
        private readonly IRepository<Company> _companyBaseRepository;
        private readonly ICompanyRepository _companyRepository;

        public PredictionController(IRepository<Prediction> predictionRepository, IRepository<Company> companyBaseRepository, ICompanyRepository companyRepository)
        {
            _predictionRepository = predictionRepository;
            _companyBaseRepository = companyBaseRepository;
            _companyRepository = companyRepository;
        }

        [HttpGet("{companyName}")]
        public async Task<IActionResult> GetSentiment(string companyName, [FromQuery] string reclamacao)
        {
            if (string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(reclamacao))
            {
                return BadRequest("Nome da empresa e reclamação são obrigatórios");
            }

            var company = await _companyRepository.GetAsync(companyName);
            if (company == null)
            {
                return NotFound("Empresa não encontrada");
            }

            var sampleData = new MLModel.ModelInput()
            {
                Reclamacao = reclamacao
            };

            var result = MLModel.Predict(sampleData);

            var prediction = new Prediction
            {
                CompanyId = company.Id,
                PredictionResult = reclamacao,
                Company = company,
                GeneratedOn = DateTime.Now
            };
            await _predictionRepository.AddAsync(prediction);
            await _predictionRepository.SaveChangesAsync();

            return Ok(new { Sentimento = result.PredictedLabel });
        }
    }
}
