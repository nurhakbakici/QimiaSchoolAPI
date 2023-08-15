
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Abstractions;

public interface IEnrollmentManager
{
    public Task CreateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken);

    public Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken);

    public Task UpdateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken);

    public Task DeleteEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken);

    public Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync(CancellationToken cancellationToken);
}