﻿using MediatR;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Queries.Enrollments;

public class GetEnrollmentQuery : IRequest<EnrollmentDto>
{
    public int Id { get; }

    public GetEnrollmentQuery(int id)
    {
        Id = id;
    }
}
