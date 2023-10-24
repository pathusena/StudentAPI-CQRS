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
    public class UpdateStudentCommandHandler
    {
        private readonly IStudentRepository _studentRepository;
        public UpdateStudentCommandHandler(IStudentRepository studentRepository) { 
            _studentRepository = studentRepository;
        }

        public async Task<Student?> Handle(UpdateStudentCommand command) { 
            var student = new Student { 
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                Age = command.Age,
            };
            return await _studentRepository.UpdateStudentAsync(student);
        }
    }
}
