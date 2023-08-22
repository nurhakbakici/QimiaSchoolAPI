using Moq;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.Business.UnitTests;

internal class CourseManagerUnitTests
{
    private readonly Mock<ICourseRepository> _mockCourseRepository;
    private readonly Mock<ICacheService> _cacheService;
    private readonly CourseManager _courseManager;

    public CourseManagerUnitTests()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _cacheService = new Mock<ICacheService>();
        _courseManager = new CourseManager(_mockCourseRepository.Object, _cacheService.Object);
    }

    [Test]

    public async Task CreateCourseAsync_WhenCalled_CallsRepository()
    {
        var testCourse = new Course
        {
            Title = "Test",
            Credits = 1,
        };

        await _courseManager.CreateCourseAsync(testCourse, default);

        _mockCourseRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Course>(s => s == testCourse),
                    It.IsAny<CancellationToken>()), Times.Once());
    }

    [Test]
    public async Task CreateCourseAsync_WhenCourseIdHasValue_RemovesAndCallsRepository()
    {
        var testCourse = new Course
        {
            ID = 1,
            Title = "Test",
            Credits = 1,
        };

        await _courseManager.CreateCourseAsync(testCourse, default);

        _mockCourseRepository
           .Verify(
               sr => sr.CreateAsync(
                   It.Is<Course>(s => s == testCourse),
                   It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetCourseByIdAsync_WhenCalled_ReturnsCourse()
    {
        // Arrange
        var courseId = 1;
        var expectedCourse = new Course
        {
            ID = courseId,
            Title = "Sample Course",
            Credits = 2
        };

        _mockCourseRepository
            .Setup(sr => sr.GetByIdAsync(courseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCourse);

        // Act
        var resultCourse = await _courseManager.GetCourseByIdAsync(courseId, default);

        // Assert
        Assert.AreEqual(expectedCourse, resultCourse);
    }

    [Test]
    public async Task UpdateCourseAsync_WhenCalled_UpdatesandReturnsSuccess()
    {
        var courseToUpdate = new Course
        {
            ID = 2,
            Title = "Test Course",
            Credits = 1,
        };

        _mockCourseRepository
        .Setup(sr => sr.UpdateAsync(
            It.Is<Course>(s => s == courseToUpdate),
            It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

        await _courseManager.UpdateCourseById(courseToUpdate, default);
    }
}