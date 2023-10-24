using StudentAPICQRS.Application.Commands;
using StudentAPICQRS.Data.Repositories;
using StudentAPICQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPICQRS.Application.Handlers
{
    public class CreateStudentCommandHandler
    {
        private readonly IStudentRepository _studentRepository;
        public CreateStudentCommandHandler(IStudentRepository studentRepository) { 
            _studentRepository = studentRepository;
        }

        public async Task<Student> Handle(CreateStudentCommand command) {
            var student = new Student { 
                Name = command.Name, 
                Description = command.Description,
                Age = command.Age,
            };
            return await _studentRepository.CreateStudentAsync(student);
        }
    }
}
