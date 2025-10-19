using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Stdentapidemo.Models;

namespace Stdentapidemo.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private static List<Student> students = new List<Student>()
        {
            new Student{Id=1, Name= "Shubham", Age=25, Course="Dot net"},
            new Student{Id=1, Name= "Mayuresh", Age=24, Course="Dot net"},
        };

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll() => Ok(students);

        [HttpGet("id")]
        public ActionResult<Student> GetById(int id)
        {
            var s = students.FirstOrDefault(x => x.Id == id);
            if (s == null) return NotFound();
            return Ok(s); 
        }

        [HttpPost]
        public ActionResult<Student> AddStudent([FromBody] Student student)
        {
            var nextId = students.Count == 0 ? 1 : students.Max(s => s.Id) + 1;
            student.Id = nextId;
            students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student updated)
        {
            var s = students.FirstOrDefault(x => x.Id == id);
            if(s==null) return NotFound();

            s.Name = updated.Name;
            s.Age = updated.Age;
            s.Course = updated.Course;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletStudent(int id)
        {
            var s = students.FirstOrDefault(x => x.Id == id);
            if (s == null) return NotFound();
            students.Remove(s);
            return NoContent();
        }
    }
}
