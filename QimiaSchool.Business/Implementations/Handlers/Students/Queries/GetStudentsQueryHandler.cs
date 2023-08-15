using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Queries.Students;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Students.Queries;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentDto>>
{
    private readonly IStudentManager _studentManager;
    private readonly IMapper _mapper;

    public GetStudentsQueryHandler(
        IStudentManager studentManager,
        IMapper mapper)
    {
        _studentManager = studentManager;
        _mapper = mapper;
    }

    public async Task<List<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentManager.GetAllStudentsAsync(cancellationToken);

        return students.Select(s => _mapper.Map<StudentDto>(s)).ToList();
    }
}

// unlike getStudentQueryHandler we didn't ask for an id this time because we are not going to get just one student but all.