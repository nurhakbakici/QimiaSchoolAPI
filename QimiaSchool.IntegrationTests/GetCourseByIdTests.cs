using FluentAssertions;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;
using QimiaSchool.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool.IntegrationTests;

internal class GetCourseByIdTests : IntegrationTestBase
{
    public GetCourseByIdTests() : base(){

    }

    [Test]
    public async Task GetCourseById_WhenCalled_ReturnsCorrectCourse()
    {
        var courseList = new List<Course>()
        {
            new ()
            {
                Title = "Test",
                Credits = 1
            },
            new()
            {
                Title= "Test",
                Credits= 1,
            }
        };

        databaseContext.Courses.AddRange(courseList);
        await databaseContext.SaveChangesAsync();

        //ACT

        var response = await client.GetAsync("/courses/" + courseList[0].ID);
        var result = await response.Content.ReadAsStringAsync();
        var responseCourse = JsonConvert.DeserializeObject<CourseDto>(result);

        //ASSERT
        responseCourse
            .Should()
            .BeEquivalentTo(courseList[0],
            options => options.Excluding(s=> s.Enrollments));

    }

    [Test]
    public async Task GetCourses_WhenCourseIsNotExist_ReturnsNotFound()
    {
        var response = await client.GetAsync("/course/NonExistingId");

        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }
}