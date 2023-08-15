using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Enrollments.EnrollmentDtos
{
    public class UpdateEnrollmentDto
    {
       public Grade Grade {  get; set; }
    }
}

// only thing that we can update about a Enrollment is the Grade so we only have that in the dto.
