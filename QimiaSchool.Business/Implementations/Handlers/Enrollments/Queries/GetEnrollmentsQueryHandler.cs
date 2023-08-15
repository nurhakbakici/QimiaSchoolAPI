using AutoMapper;
using MediatR;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations.Queries.Enrollments;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Handlers.Enrollments.Queries;

public class GetEnrollmentsQueryHandler : IRequestHandler<GetEnrollmentsQuery, List<EnrollmentDto>>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IMapper _mapper;

    public GetEnrollmentsQueryHandler(
        IEnrollmentManager enrollmentManager,
        IMapper mapper)
    {
        _enrollmentManager = enrollmentManager;
        _mapper = mapper;
    }

    public async Task<List<EnrollmentDto>> Handle(GetEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentManager.GetAllEnrollmentsAsync(cancellationToken);

        return enrollments.Select(e => _mapper.Map<EnrollmentDto>(e)).ToList();
    }
}
