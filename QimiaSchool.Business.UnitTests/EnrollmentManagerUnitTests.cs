using Moq;
using QimiaSchool.Business.Abstractions;
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
    private readonly Mock<ICacheService> _cacheService;
    private readonly EnrollmentManager _EnrollmentManager;

    public EnrollmentManagerUnitTests()
    {
        _cacheService = new Mock<ICacheService>();
        _mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
        _EnrollmentManager = new EnrollmentManager(_mockEnrollmentRepository.Object, _cacheService.Object);
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


    [Test]
    public async Task GetEnrollmentByIdAsync_WhenCalled_ReturnsEnrollment()
    {
        // Arrange
        var enrollmentId = 1;
        var expectedEnrollment = new Enrollment
        {
            ID = enrollmentId,
            CourseID = 2,
            StudentID = 3,
            Grade = Grade.B
        };

        _mockEnrollmentRepository
            .Setup(e => e.GetByIdAsync(enrollmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedEnrollment);

        // Act
        var resultEnrollment = await _EnrollmentManager.GetEnrollmentByIdAsync(enrollmentId, default);

        // Assert
        Assert.AreEqual(expectedEnrollment, resultEnrollment);
    }

    [Test]
    public async Task UpdateEnrollmentAsync_WhenCalled_UpdatesandReturnsSuccess()
    {
        var enrollmentToUpdate = new Enrollment
        {
            ID = 2,
            CourseID= 2,
            StudentID = 3,
            Grade = Grade.B
        };

        _mockEnrollmentRepository
        .Setup(e => e.UpdateAsync(
            It.Is<Enrollment>(e => e == enrollmentToUpdate),
            It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

        await _EnrollmentManager.UpdateEnrollmentAsync(enrollmentToUpdate, default);
    }
}