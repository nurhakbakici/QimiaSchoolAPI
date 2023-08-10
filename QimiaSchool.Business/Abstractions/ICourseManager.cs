using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Abstractions;

public interface ICourseManager
{
    public Task CreateCourseAsync(Course course, CancellationToken cancellationToken);

    public Task<Course> GetCourseByIdAsync(int courseId, CancellationToken cancellationToken);

    public Task DeleteCourseById(int courseId, CancellationToken cancellationToken);

    public Task UpdateCourseById(Course course, CancellationToken cancellationToken);
}