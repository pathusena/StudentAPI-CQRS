using Microsoft.EntityFrameworkCore;
using StudentAPICQRS.Data.Context;
using StudentAPICQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPICQRS.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StudentRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<List<Student>> GetStudentsAsync() { 
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentAsync(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if(student == null)
            {
                return null;
            }
            return student;
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            try {
                var createdStudent = _dbContext.Students.Add(student);
                await _dbContext.SaveChangesAsync();
                return student;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Student?> UpdateStudentAsync(Student student) {
            try
            {
                var existingStudent = await _dbContext.Students.FindAsync(student.Id);
                if(existingStudent == null)
                {
                    return null;
                }
                existingStudent.Name = student.Name;
                existingStudent.Description = student.Description;
                existingStudent.Age = student.Age;
                await _dbContext.SaveChangesAsync();
                return existingStudent;
            }
            catch {
                throw;
            }
        }

        public async Task<Student?> DeleteStudentAsync(int id)
        {
            try
            {
                var existingStudent = await _dbContext.Students.FindAsync(id);
                if (existingStudent == null) {
                    return null;
                }
                _dbContext.Students.Remove(existingStudent);
                await _dbContext.SaveChangesAsync();
                return existingStudent;
            }
            catch {
                throw;
            }
        }
    }
}
