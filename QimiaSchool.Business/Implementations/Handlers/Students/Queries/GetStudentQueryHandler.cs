using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;
using QimiaSchool.Business.Implementations.Queries.Students;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Queries;
public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentDto>
{
    private readonly IStudentManager _studentManager;
    private readonly IMapper _mapper;

    public GetStudentQueryHandler(
        IStudentManager studentManager,
        IMapper mapper)
    {
        _studentManager = studentManager;
        _mapper = mapper;
    }
    // we used constructor injection to create instances and connect them with getStudentQueryHandler method. 


    public async Task<StudentDto> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentManager.GetStudentByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<StudentDto>(student);
    }
}
