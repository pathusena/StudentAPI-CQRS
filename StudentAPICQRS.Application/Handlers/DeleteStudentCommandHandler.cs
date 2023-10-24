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
    public class DeleteStudentCommandHandler
    {
        private readonly IStudentRepository _studentRepository;
        public DeleteStudentCommandHandler(IStudentRepository studentRepository) { 
            _studentRepository = studentRepository;
        }

        public async Task<Student?> Handle(DeleteStudentCommand command) {
            return await _studentRepository.DeleteStudentAsync(command.Id);
        }
    }
}
