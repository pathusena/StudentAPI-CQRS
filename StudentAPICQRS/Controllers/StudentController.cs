using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPICQRS.Application.Commands;
using StudentAPICQRS.Application.Handlers;
using StudentAPICQRS.Application.Queries;
using StudentAPICQRS.Domain;
using StudentAPICQRS.Models;

namespace StudentAPICQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly GetAllStudentsQueryHandler _getAllStudentsQueryHandler;
        private readonly GetStudentByIdQueryHandler _getStudentByIdQueryHandler;
        private readonly CreateStudentCommandHandler _createStudentCommandHandler;
        private readonly UpdateStudentCommandHandler _updateStudentCommandHandler;
        private readonly DeleteStudentCommandHandler _deleteStudentCommandHandler;

        public StudentController(
            GetAllStudentsQueryHandler getAllStudentsQueryHandler,
            GetStudentByIdQueryHandler getStudentByIdQueryHandler,
            CreateStudentCommandHandler createStudentCommandHandler,
            UpdateStudentCommandHandler updateStudentCommandHandler,
            DeleteStudentCommandHandler deleteStudentCommandHandler
        ) {
            _getAllStudentsQueryHandler = getAllStudentsQueryHandler;
            _getStudentByIdQueryHandler = getStudentByIdQueryHandler;
            _createStudentCommandHandler = createStudentCommandHandler;
            _updateStudentCommandHandler = updateStudentCommandHandler;
            _deleteStudentCommandHandler = deleteStudentCommandHandler;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents() {
            List<Student> students = await _getAllStudentsQueryHandler.Handle();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Student>>> GetStudent(int id)
        {
            Student? student = await _getStudentByIdQueryHandler.Handle(new GetStudentByIdQuery { Id = id});
            if (student == null) {
                return NotFound("Student Not Found");
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(StudentCreateDto studentDto)
        {
            try
            {
                if (studentDto == null)
                {
                    return BadRequest("Invalid Student Data");
                }
                Student student = await _createStudentCommandHandler.Handle(new CreateStudentCommand { Name = studentDto.Name, Description = studentDto.Description, Age = studentDto.Age });
                return CreatedAtAction("GetStudent", new { id = student.Id }, student);
            }
            catch
            {
                return StatusCode(500, "An error occurred while saving data!");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Student>> UpdateStudent(StudentUpdateDto studentDto)
        {
            try
            {
                if (studentDto == null)
                {
                    return BadRequest("Invalid Student Data");
                }
                Student? student = await _updateStudentCommandHandler.Handle(new UpdateStudentCommand { Id = studentDto.Id, Name = studentDto.Name, Description = studentDto.Description, Age = studentDto.Age });
                if (student == null)
                {
                    return NotFound("Student Not Found");
                }
                return Ok(student);
            }
            catch
            {
                return StatusCode(500, "An error occurred while saving data!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            try
            {
                Student? student = await _deleteStudentCommandHandler.Handle(new DeleteStudentCommand { Id = id });
                if (student == null)
                {
                    return NotFound("Student Not Found");
                }
                return Ok(student);
            }
            catch
            {
                return StatusCode(500, "An error occurred while processing the request!");
            }
        }
    }
}
