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
    public class CourseManager : ICourseManager
    {

        private readonly ICourseRepository _courseRepository;
        public CourseManager(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        public Task CreateCourseAsync(Course course, CancellationToken cancellationToken)
        {
            course.ID = default;
            return _courseRepository.CreateAsync(course, cancellationToken);
        }

        public Task<Course> GetCourseByIdAsync(int courseId, CancellationToken cancellationToken)
        {
            return _courseRepository.GetByIdAsync(courseId, cancellationToken);
        }

        public Task DeleteCourseById(int courseId, CancellationToken cancellationToken)
        {
            return _courseRepository.DeleteByIdAsync(courseId, cancellationToken);
        }
       
        public Task UpdateCourseById(Course course, CancellationToken cancellationToken)
        {
            return _courseRepository.UpdateAsync(course, cancellationToken);
        }
    }
}
