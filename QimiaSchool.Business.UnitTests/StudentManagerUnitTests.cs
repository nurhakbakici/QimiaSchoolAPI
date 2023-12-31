﻿using Moq;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using QimiaSchool.Business.Abstractions;

namespace QimiaSchool.Business.UnitTests;

[TestFixture]
internal class StudentManagerUnitTests
{
    private readonly Mock<IStudentRepository> _mockStudentRepository;// mocking is a process used for creating a mock object that can stimulate the behavior of the real object
    private readonly Mock<ICacheService> _cacheService;
    private readonly StudentManager _studentManager;

    public StudentManagerUnitTests()
    {
        _mockStudentRepository = new Mock<IStudentRepository>();
        _cacheService = new Mock<ICacheService>();
        _studentManager = new StudentManager(_mockStudentRepository.Object, _cacheService.Object);
    }

    [Test]
    public async Task CreateStudentAsync_WhenCalled_CallsRepository()
    {
        // Arrange
        var testStudent = new Student
        {
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateStudentAsync_WhenStudentIdHasValue_RemovesAndCallsRepository()
    {
        // Arrange
        var testStudent = new Student
        {
            ID = 1,
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetStudentByIdAsync_WhenCalled_ReturnsStudent()
    {
        var studentId = 1;
        var expectedStudent = new Student
        {
            ID = 1,
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Nurtest",
            LastName = "NuTester"
        };

        _mockStudentRepository
            .Setup(s=> s.GetByIdAsync(studentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedStudent);

        var resultStudent = await _studentManager.GetStudentByIdAsync(studentId, default);

        Assert.AreEqual(expectedStudent, resultStudent);

    }

    [Test]
    public async Task UpdateStudentAsync_WhenCalled_UpdatesandReturnsSuccess()
    {
        var studentToUpdate = new Student
        {
            ID = 2,
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Vicreation",
            LastName = "Qimia"
        };

        _mockStudentRepository
        .Setup(sr => sr.UpdateAsync(
            It.Is<Student>(s => s == studentToUpdate),
            It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

         await _studentManager.UpdateStudentAsync(studentToUpdate, default);
    }

    [Test]
    public async Task DeleteStudentAsync_WhenCalled_DeletesAndReturnsSuccess()
    {
        var studentToDelete = new Student
        {
            ID = 2,
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Vicreation",
            LastName = "Qimia"
        };

        _mockStudentRepository
        .Setup(sr => sr.DeleteAsync(
            It.Is<Student>(s => s == studentToDelete),
            It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

        await _studentManager.DeleteStudentByIdAsync(studentToDelete.ID, default);
    }
}


