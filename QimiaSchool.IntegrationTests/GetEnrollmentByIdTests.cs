using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos; // Assuming the correct namespace
using QimiaSchool.DataAccess.Entities;
using System.Net;
using NUnit.Framework;

namespace QimiaSchool.IntegrationTests
{
    internal class GetEnrollmentByIdTests : IntegrationTestBase
    {
        public GetEnrollmentByIdTests() : base()
        {
        }

        [Test]
        public async Task GetEnrollmentById_WhenCalled_ReturnsCorrectEnrollment()
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
                    Grade = Grade.A 
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
            var response = await client.GetAsync("/enrollments/" + enrollmentList[0].ID);
            var result = await response.Content.ReadAsStringAsync();
            var responseEnrollment = JsonConvert.DeserializeObject<EnrollmentDto>(result);

            // Assert
            responseEnrollment
                .Should()
                .BeEquivalentTo(enrollmentList[0],
                    options => options.Excluding(e => e.Student)
                                     .Excluding(e => e.Course)
                                     .Excluding(e=> e.ID));
        }

        [Test]
        public async Task GetEnrollment_WhenEnrollmentIsNotExist_ReturnsNotFound()
        {
            // Act
            var response = await client.GetAsync("/enrollment/NonExistingId");

            // Assert
            response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.NotFound);
        }
    }
}
