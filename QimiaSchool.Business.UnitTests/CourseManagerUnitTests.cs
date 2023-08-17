using Moq;
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
    private readonly CourseManager _courseManager;

    public CourseManagerUnitTests()
    {
        _mockCourseRepository = new Mock<ICourseRepository>();
        _courseManager = new CourseManager(_mockCourseRepository.Object);
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
    

}