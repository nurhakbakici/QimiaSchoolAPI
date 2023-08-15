using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Queries.Courses;
using QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;

namespace QimiaSchool.Business.Implementations.Handlers.Courses.Queries;
public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{
    private readonly ICourseManager _courseManager;
    private readonly IMapper _mapper;

    public GetCoursesQueryHandler(
        ICourseManager courseManager,
        IMapper mapper)
    {
        _courseManager = courseManager;
        _mapper = mapper;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseManager.GetAllCoursesAsync(cancellationToken);

        return courses.Select(c => _mapper.Map<CourseDto>(c)).ToList();
    }
}
