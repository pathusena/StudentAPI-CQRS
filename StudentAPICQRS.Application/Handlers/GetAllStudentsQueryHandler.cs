using StudentAPICQRS.Data.Repositories;
using StudentAPICQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPICQRS.Application.Handlers
{
    public class GetAllStudentsQueryHandler
    {
        private readonly IStudentRepository _studentRepository;
        public GetAllStudentsQueryHandler(IStudentRepository studentRepository) { 
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> Handle() {
            return await _studentRepository.GetStudentsAsync();
        }
    }
}
