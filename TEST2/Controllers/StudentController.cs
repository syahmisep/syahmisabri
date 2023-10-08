using TEST2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TEST2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _dbContext;

        public StudentController(StudentContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_dbContext.Students == null)
            {
                return NotFound();
            }
            return await _dbContext.Students.ToListAsync();

        }

        // GET: api/Students/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_dbContext.Students == null)
            {
                return NotFound();
            }
            var student = await _dbContext.Students.FindAsync(id);

            if (student == null)

            {
                return NotFound();
            }
            return student;
        }

        // GET: api/Students/
        [HttpPost]

        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);

        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(student).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_dbContext.Students == null)
            {
                return NotFound();
            }

            var student = await _dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        private bool StudentExists(long id)
        {
            return (_dbContext.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}




