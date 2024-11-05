using InsightAI.Models.Models;
using InsightAI.Repositories.Data;
using InsightAI.Repositories.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsightAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly IRepository<Complaint> _complaintRepository;

        public ComplaintController(IRepository<Complaint> complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        // GET: api/Complaint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            var complaints = await _complaintRepository.GetAllAsync();
            return Ok(complaints);
        }

        // GET: api/Complaint/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Complaint>> GetComplaint(int id)
        {
            var complaint = await _complaintRepository.GetByIdAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        // POST: api/Complaint
        [HttpPost]
        public async Task<ActionResult<Complaint>> PostComplaint(Complaint complaint)
        {
            await _complaintRepository.AddAsync(complaint);
            await _complaintRepository.SaveChangesAsync();

            return CreatedAtAction("GetComplaint", new { id = complaint.Id }, complaint);
        }

        // PUT: api/Complaint/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaint(int id, Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return BadRequest();
            }

            try
            {
                await _complaintRepository.UpdateAsync(complaint);
                await _complaintRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Complaint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(int id)
        {
            await _complaintRepository.DeleteAsync(id);
            await _complaintRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}