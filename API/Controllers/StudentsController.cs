using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/students")]
    public class StudentsController : Controller
    {
        private readonly StudentSystemDbContext _studentSystemDbContext;

        public StudentsController(StudentSystemDbContext studentSystemDbContext)
        {
            _studentSystemDbContext = studentSystemDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentSystemDbContext.Students.ToListAsync();

            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student studentRequest)
        {
            await _studentSystemDbContext.Students.AddAsync(studentRequest);
            await _studentSystemDbContext.SaveChangesAsync();

            return Ok(studentRequest);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var student = await _studentSystemDbContext.Students.FirstOrDefaultAsync(x => x.id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet]
        [Route("/lastname/{lastName}")]
        public async Task<IActionResult> GetStudentsByLastName([FromRoute] string lastName)
        {
            var students = await _studentSystemDbContext.Students.Where(x => x.lastName == lastName).ToListAsync();
            //var students = await _studentSystemDbContext.Students.;
                //.FirstOrDefaultAsync(x => x.lastName == lastName);
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet]
        [Route("/firstname/{firstName}/graddate/{gradDate}")]
        public async Task<IActionResult> GetStudentsByFirstNameAndGradDate([FromRoute] string firstName, string gradDate)
        {
            var students = await _studentSystemDbContext.Students.Where(x => x.firstName == firstName).Where(x => x.gradDate == gradDate).ToListAsync();
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }
        

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute]int id, Student updateStudentRequest)
        {
            var student = await _studentSystemDbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.firstName = updateStudentRequest.firstName;
            student.lastName = updateStudentRequest.lastName;
            student.courseNb = updateStudentRequest.courseNb;
            student.gradDate = updateStudentRequest.gradDate;

            await _studentSystemDbContext.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await _studentSystemDbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentSystemDbContext.Students.Remove(student);
            await _studentSystemDbContext.SaveChangesAsync();
            return Ok(student);
        }
    }
}
