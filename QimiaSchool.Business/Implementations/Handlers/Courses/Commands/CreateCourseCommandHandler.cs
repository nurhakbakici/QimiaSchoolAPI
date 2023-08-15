using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly ICourseManager _courseManager;

    public CreateCourseCommandHandler(ICourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Title = request.Course.Title,
            Credits = request.Course.Credits,
        };

        await _courseManager.CreateCourseAsync(course, cancellationToken);

        return course.ID;
    }
}
