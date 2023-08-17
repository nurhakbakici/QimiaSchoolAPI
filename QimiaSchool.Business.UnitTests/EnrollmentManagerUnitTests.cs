using Moq;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.UnitTests;

[TestFixture]
internal class EnrollmentManagerUnitTests
{
    private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;
    private readonly EnrollmentManager _EnrollmentManager;

    public EnrollmentManagerUnitTests()
    {
        _mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
        _EnrollmentManager = new EnrollmentManager(_mockEnrollmentRepository.Object);
    }

    [Test]
    public async Task CreateEnrollmentAsync_WhenCalled_CallsRepository()
    {
        var testEnrollment = new Enrollment
        {
            CourseID = 1,
            StudentID = 2,
            Grade = Grade.A,
        };

        await _EnrollmentManager.CreateEnrollmentAsync(testEnrollment, default);

        _mockEnrollmentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Enrollment>(s => s == testEnrollment),
                    It.IsAny<CancellationToken>()), Times.Once);

    }

    [Test]
    public async Task CreateEnrollmentAsync_WhenEnrollmentIdHasValue_RemoveAndCallRepository()
    {
        var testEnrollment = new Enrollment
        {
            ID = 1,
            StudentID = 2,
            Grade = Grade.A,
            CourseID = 1,
        };

        await _EnrollmentManager.CreateEnrollmentAsync(testEnrollment, default);

        _mockEnrollmentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Enrollment>(s => s == testEnrollment),
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}