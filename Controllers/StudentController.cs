using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;


namespace Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
    
        public StudentController(AppDbContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound(new { Message = "Student not found" });
            return Ok(student);
        }
    
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
    
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id) return BadRequest(new { Message = "ID mismatch" });
    
            _context.Entry(student).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id)) return NotFound(new { Message = "Student not found" });
                throw;
            }
            return NoContent();
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound(new { Message = "Student not found" });
    
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    
        private bool StudentExists(int id) => _context.Students.Any(e => e.Id == id);
    }
}


