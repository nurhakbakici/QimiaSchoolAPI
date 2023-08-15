using MediatR;
using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations.Commands.Enrollments.EnrollmentDtos
{
   public class CreateEnrollmentDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Grade Grade { get; set; }
    }
}
