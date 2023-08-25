using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Commands.Enrollments.EnrollmentDtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QimiaSchool.IntegrationTests;

namespace QimiaSchool.IntegrationTests
{
    internal class UpdateEnrollmentTests : IntegrationTestBase
    {
        public UpdateEnrollmentTests() : base()
        {
        }

        [Test]
        public async Task UpdateEnrollment_WhenCalled_ReturnsNoContent()
        {
            // Arrange
            var student = new Student
            {
                FirstMidName = "Test",
                LastName = "Test",
                EnrollmentDate = DateTime.Now,    
            };

            var existingEnrollment = new Enrollment
            {
                Student = student,
                Course = new Course { Title = "Math" },
                Grade = Grade.A
            };

            databaseContext.Enrollments.Add(existingEnrollment);
            await databaseContext.SaveChangesAsync();

            var updatedEnrollmentDto = new UpdateEnrollmentDto
            {
               Grade = Grade.B,
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(updatedEnrollmentDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PutAsync("/enrollments/" + existingEnrollment.ID, jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task UpdateEnrollment_WhenEnrollmentDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var nonExistingEnrollmentDto = new UpdateEnrollmentDto
            {
                Grade = Grade.C
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(nonExistingEnrollmentDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PutAsync("/enrollments/NonExistingId", jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
