using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Commands.Courses;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Commands;
public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly ICourseManager _courseManager;

    public DeleteCourseCommandHandler(ICourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        await _courseManager.DeleteCourseById(request.Id, cancellationToken);

        return Unit.Value;
    }
}
