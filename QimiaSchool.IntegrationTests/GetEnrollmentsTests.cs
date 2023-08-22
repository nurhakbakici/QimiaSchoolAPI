using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.IntegrationTests
{
    internal class GetEnrollmentsTests : IntegrationTestBase
    {
        public GetEnrollmentsTests() : base()
        {
        }

        [Test]
        public async Task GetEnrollment_WhenCalled_ReturnsListOfEnrollments()
        {
            // Arrange
            var student = new Student
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "Test",
                LastName = "Test",
            };

            var enrollmentList = new List<Enrollment>()
            {
                new ()
                {
                    Student = student,
                    Course = new Course { Title = "Math" },
                    Grade = Grade.A // Adjust as needed
                },
                new ()
                {
                    Student = student,
                    Course = new Course { Title = "Science" },
                    Grade = Grade.B
                }
            };

            databaseContext.Enrollments.AddRange(enrollmentList);
            await databaseContext.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/enrollments");
            var result = await response.Content.ReadAsStringAsync();
            var resultList = JsonConvert.DeserializeObject<List<Enrollment>>(result);

            // Assert
            resultList
                .Should()
                .BeEquivalentTo(enrollmentList,
                    options => options
                        .Excluding(e => e.Student)
                        .Excluding(e => e.Course)
                        .Excluding(e=> e.ID));
        }

        [Test]
        public async Task GetEnrollments_WhenThereAreNoEnrollments_ReturnsEmptyList()
        {
            // Act
            var response = await client.GetAsync("/enrollments");
            var result = await response.Content.ReadAsStringAsync();
            var resultList = JsonConvert.DeserializeObject<List<Enrollment>>(result);

            // Assert
            resultList
                .Should()
                .BeEmpty();
        }
    }
}
