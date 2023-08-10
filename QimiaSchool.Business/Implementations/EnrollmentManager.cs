using QimiaSchool.Business.Abstractions;
using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations
{
    public class EnrollmentManager : IEnrollmentManager
    {

        private readonly IEnrollmentManager _enrollmentManager;
        public EnrollmentManager(IEnrollmentManager enrollmentManager)
        {
            _enrollmentManager = enrollmentManager;
        }

        public Task CreateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken)
        {
            enrollment.ID = default;
            return _enrollmentManager.CreateEnrollmentAsync(enrollment, cancellationToken);
        }

        public Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken)
        {
            return _enrollmentManager.GetEnrollmentByIdAsync(enrollmentId, cancellationToken);
        }

        public Task DeleteEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken)
        {
            return _enrollmentManager.DeleteEnrollmentByIdAsync(enrollmentId, cancellationToken);
        }
      
        public Task UpdateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken)
        {
            return _enrollmentManager.UpdateEnrollmentAsync(enrollment, cancellationToken);
        }
    }
}
