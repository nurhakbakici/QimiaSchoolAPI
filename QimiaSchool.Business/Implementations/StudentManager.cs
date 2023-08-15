using QimiaSchool.Business.Abstractions;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations
{
    public class StudentManager: IStudentManager
    {
        private readonly IStudentRepository _studentRepository;
        public StudentManager(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public Task CreateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            student.ID = default;
            // we shouldn't provide an id while we inserting.
            return _studentRepository.CreateAsync(student, cancellationToken);
        }


        public Task<Student> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken)
        {
            return _studentRepository.GetByIdAsync(studentId, cancellationToken);
        }

        
        public Task DeleteStudentByIdAsync(int studentId, CancellationToken cancellationToken)
        {
           return _studentRepository.DeleteByIdAsync(studentId, cancellationToken);
        }


        public Task UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            return UpdateStudentAsync(student, cancellationToken);
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            return _studentRepository.GetAllAsync(cancellationToken);
        }
    }
}

