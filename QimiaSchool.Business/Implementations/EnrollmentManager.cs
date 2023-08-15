using QimiaSchool.Business.Abstractions;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.Implementations;

public class EnrollmentManager : IEnrollmentManager
{

    private readonly IEnrollmentRepository _enrollmentRepository;
    public EnrollmentManager(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public Task CreateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken)
    {
        enrollment.ID = default;
        return _enrollmentRepository.CreateAsync(enrollment, cancellationToken);
    }

    public Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken)
    {
        return _enrollmentRepository.GetByIdAsync(enrollmentId, cancellationToken);
    }

    public Task DeleteEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken)
    {
        return _enrollmentRepository.DeleteByIdAsync(enrollmentId, cancellationToken);
    }
  
    public Task UpdateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken)
    {
        return _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);
    }

    public Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync(CancellationToken cancellationToken)
    {
        return _enrollmentRepository.GetAllAsync(cancellationToken);
    }
}
