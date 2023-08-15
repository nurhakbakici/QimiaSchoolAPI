using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Courses;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Exceptions;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
{
    private readonly ICourseManager _courseManager;

    public UpdateCourseCommandHandler(ICourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {

        var course = await _courseManager.GetCourseByIdAsync(request.Id, cancellationToken);

        course.Title = request.Course.Title ?? course.Title;
        course.Credits = request.Course?.Credits ?? course.Credits;

        await _courseManager.UpdateCourseById(course, cancellationToken);

        return Unit.Value;
    }
}
