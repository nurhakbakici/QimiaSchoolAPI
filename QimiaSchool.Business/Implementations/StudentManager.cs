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
        private readonly ICacheService _cacheService; // injection for caching with redis.

        public StudentManager(IStudentRepository studentRepository, ICacheService cacheService)
        {
            _studentRepository = studentRepository;
            _cacheService = cacheService;
        }


        public Task CreateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            student.ID = default;
            // we shouldn't provide an id while we inserting.
            return _studentRepository.CreateAsync(student, cancellationToken);
        }


        public async Task<Student> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken)
        {
            var cacheKey = $"student-{studentId}";
            var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);

            if (cachedStudent != null)
            {
                return cachedStudent;
            }

            var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken);
            if (student != null)
            {
                await _cacheService.SetAsync(cacheKey,student,TimeSpan.FromMinutes(5), cancellationToken);
            }

            return student;
        }

        
        public async Task DeleteStudentByIdAsync(int studentId, CancellationToken cancellationToken)
        {
            var cacheKey = $"student-{studentId}";
            var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey,cancellationToken);

            if (cachedStudent != null)
            {
                await _cacheService.RemoveAsync(cacheKey, cancellationToken);
            }

           await _studentRepository.DeleteByIdAsync(studentId, cancellationToken);
        }


        public async Task UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            var cacheKey = $"student-{student.ID}";
            var cachedStudent = await _cacheService.GetAsync<Student>(cacheKey, cancellationToken);
            if (cachedStudent != null)
            {
                await _cacheService.RemoveAsync(cacheKey, cancellationToken);
            }

            await UpdateStudentAsync(student, cancellationToken);
        }

        public Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            return _studentRepository.GetAllAsync(cancellationToken);
        }
    }
}

