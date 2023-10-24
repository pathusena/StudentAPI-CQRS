using StudentAPICQRS.Application.Queries;
using StudentAPICQRS.Data.Repositories;
using StudentAPICQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPICQRS.Application.Handlers
{
    public class GetStudentByIdQueryHandler
    {
        private readonly IStudentRepository _studentRepository;
        public GetStudentByIdQueryHandler(IStudentRepository studentRepository) { 
            _studentRepository = studentRepository;
        }

        public async Task<Student?> Handle(GetStudentByIdQuery query) { 
            return await _studentRepository.GetStudentAsync(query.Id);
        }
    }
}
