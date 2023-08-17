using QimiaSchool.Business.Abstractions;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using QimiaSchool.DataAccess.Repositories.Implementations;
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
        private readonly ICacheService _cacheService;
        public CourseManager(ICourseRepository courseRepository, ICacheService cacheService)
        {
            _courseRepository = courseRepository;
            _cacheService = cacheService;
        }


        public Task CreateCourseAsync(Course course, CancellationToken cancellationToken)
        {
            course.ID = default;
            return _courseRepository.CreateAsync(course, cancellationToken);
        }

        public async Task<Course> GetCourseByIdAsync(int courseId, CancellationToken cancellationToken)
        {
            var cachkeKey = $"course-{courseId}";
            var cachedCourse = await _cacheService.GetAsync<Course>(cachkeKey, cancellationToken);
            if (cachedCourse != null)
            {
                return cachedCourse;
            }

            var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);
            await _cacheService.SetAsync(cachkeKey, course, TimeSpan.FromMinutes(5), cancellationToken);

            return course;

            // if there is cached course, this code will retrieve it from cache, and if there is no cached couse with the id it will get it from repository.
        }

        public async Task DeleteCourseById(int courseId, CancellationToken cancellationToken)
        {
            var cacheKey = $"course-{courseId}";
            var cachedCourse = await _cacheService.GetAsync<Course>(cacheKey, cancellationToken);

            if (cachedCourse != null)
            {
                await _cacheService.RemoveAsync(cacheKey, cancellationToken);
            }
            await _courseRepository.DeleteByIdAsync(courseId, cancellationToken);

            // this codes checks if the course is in the cache, removes it if it is there. Lastly deletes it from repository. 
        }
       
        public async Task UpdateCourseById(Course course, CancellationToken cancellationToken)
        {
            var cacheKey = $"course-{course.ID}";
            var cachedCourse = _cacheService.GetAsync<Course>(cacheKey, cancellationToken) ;

            if (cachedCourse != null)
            {
                await _cacheService.RemoveAsync(cacheKey , cancellationToken);
                // we want to update any cached representations of this course to be updated too. So we want to remove it from cache to propoerly update it. By that we prevent any inconsistencies between data it the cache and the data in the repository.
            }

            await _courseRepository.UpdateAsync(course, cancellationToken);
        }

        public Task<IEnumerable<Course>> GetAllCoursesAsync(CancellationToken cancellationToken)
        {
            return _courseRepository.GetAllAsync(cancellationToken);
        }
    }
}
