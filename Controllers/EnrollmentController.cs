using Microsoft.AspNetCore.Mvc;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            return Ok(await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(Enrollment enrollment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllEnrollments), new { id = enrollment.Id }, enrollment);
        }
    }
}
