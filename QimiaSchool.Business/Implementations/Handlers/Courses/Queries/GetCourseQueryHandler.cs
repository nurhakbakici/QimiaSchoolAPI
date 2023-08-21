using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Queries.Courses;
using QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Queries;
public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
{
    private readonly ICourseManager _courseManager;
    private readonly IMapper _mapper;

    public GetCourseQueryHandler(
        ICourseManager courseManager,
        IMapper mapper)
    {
        _courseManager = courseManager;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {


        var course = await _courseManager.GetCourseByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<CourseDto>(course);


    }
}
