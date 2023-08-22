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
    private readonly ICacheService _cacheService;
    public EnrollmentManager(IEnrollmentRepository enrollmentRepository, ICacheService cacheService)
    {
        _enrollmentRepository = enrollmentRepository;
        _cacheService = cacheService;
    }



    public Task CreateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken)
    {
        enrollment.ID = default;
        return _enrollmentRepository.CreateAsync(enrollment, cancellationToken);
    }



    public async Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken)
    {
        var cacheKey = $"enrollment-{enrollmentId}";
        var cachedEnrollment = await _cacheService.GetAsync<Enrollment>(cacheKey, cancellationToken);

        if (cachedEnrollment != null)
        {
            return cachedEnrollment;
        }

        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId, cancellationToken);

        if (enrollment != null)
        {
            await _cacheService.SetAsync(cacheKey,enrollment,TimeSpan.FromMinutes(5),cancellationToken);
        }

        return enrollment;
    }



    public async Task DeleteEnrollmentByIdAsync(int enrollmentId, CancellationToken cancellationToken)
    {
        var cacheKey = $"enrollment-{enrollmentId}";
        var cachedEnrollment = await _cacheService.GetAsync<Enrollment>(cacheKey, cancellationToken);

        if (cachedEnrollment != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _enrollmentRepository.DeleteByIdAsync(enrollmentId, cancellationToken);
    }



    public async Task UpdateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken)
    {
        var cacheKey = $"enrollment-{enrollment.ID}";
        var cachedEnrollment = await _cacheService.GetAsync<Enrollment>(cacheKey, cancellationToken);

        if (cachedEnrollment != null)
        {
            await _cacheService.RemoveAsync(cacheKey, cancellationToken);
        }

        await _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);
    }



    public Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync(CancellationToken cancellationToken)
    {
        return _enrollmentRepository.GetAllAsync(cancellationToken);
    }
}
