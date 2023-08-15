using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Queries.Enrollments;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;
using QimiaSchool.Business.Implementations.Queries.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Queries;


public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDto>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IMapper _mapper;

    public GetEnrollmentQueryHandler(IEnrollmentManager enrollmentManager, IMapper mapper)
    {
        _enrollmentManager = enrollmentManager;
        _mapper = mapper;
    }

    public async Task<EnrollmentDto> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentManager.GetEnrollmentByIdAsync(request.Id, cancellationToken);

        return _mapper.Map<EnrollmentDto>(enrollment);
    }
}