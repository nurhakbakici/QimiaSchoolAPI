using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.DataAccess.Entities;
using NUnit.Framework;

namespace QimiaSchool.IntegrationTests;

internal class GetCoursesTests : IntegrationTestBase
{
    public GetCoursesTests() : base()
    {
    }

    [Test]
    public async Task GetCourse_WhenCalled_ReturnsListOfCourses()
    {
        // Arrange
        var courseList = new List<Course>()
        {
            new ()
            {     
                Title = "Test",
                Credits = 1,
            },
            new ()
            {               
                Title = "Test",
                Credits = 1,
            }
        };

        databaseContext.Courses.AddRange(courseList);
        await databaseContext.SaveChangesAsync();

        // Act
        var response = await client.GetAsync("/courses");
        var result = await response.Content.ReadAsStringAsync();
        var resultList = JsonConvert.DeserializeObject<List<Course>>(result);

        // Assert
        resultList
            .Should()
            .BeEquivalentTo(courseList,
                options => options
                    .Excluding(s => s.Enrollments));
    }

    [Test]
    public async Task GetCourses_WhenThereIsNoCourse_ReturnsEmptyList()
    {
        // Act
        var response = await client.GetAsync("/courses");
        var result = await response.Content.ReadAsStringAsync();
        var resultList = JsonConvert.DeserializeObject<List<Course>>(result);

        // Assert
        resultList
            .Should()
            .BeEmpty();
    }
}

