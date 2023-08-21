using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.IntegrationTests;

internal class GetStudentsTests : IntegrationTestBase
{
    public GetStudentsTests() : base()
    {
    }

    [Test]
    public async Task GetStudent_WhenCalled_ReturnsListOfStudents()
    {
        // Arrange
        var studentList = new List<Student>()
        {
            new ()
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "Test",
                LastName = "Test",
            },
            new ()
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "Test",
                LastName = "Test",
            }
        };

        databaseContext.Students.AddRange(studentList);
        await databaseContext.SaveChangesAsync();

        // Act
        var response = await client.GetAsync("/students");
        var result = await response.Content.ReadAsStringAsync();
        var resultList = JsonConvert.DeserializeObject<List<Student>>(result);

        // Assert
        resultList
            .Should()
            .BeEquivalentTo(studentList,
                options => options
                    .Excluding(s => s.EnrollmentDate)
                    .Excluding(s => s.Enrollments));
    }

    [Test]
    public async Task GetStudents_WhenThereIsNoStudent_ReturnsEmptyList()
    {
        // Act
        var response = await client.GetAsync("/students");
        var result = await response.Content.ReadAsStringAsync();
        var resultList = JsonConvert.DeserializeObject<List<Student>>(result);

        // Assert
        resultList
            .Should()
            .BeEmpty();
    }
}

