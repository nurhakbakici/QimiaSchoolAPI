using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Commands.Courses.CourseDtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QimiaSchool.IntegrationTests
{
    internal class UpdateCourseTests : IntegrationTestBase
    {
        public UpdateCourseTests() : base()
        {
        }

        [Test]
        public async Task UpdateCourse_WhenCalled_ReturnsNoContent()
        {
            // Arrange
            var existingCourse = new Course
            {
                Title = "OldTitle",
                Credits = 3,
            };

            databaseContext.Courses.Add(existingCourse);
            await databaseContext.SaveChangesAsync();

            var updatedCourseDto = new UpdateCourseDto
            {
                Title = "NewTitle",
                Credits = 4,
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(updatedCourseDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PutAsync("/courses/" + existingCourse.ID, jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task UpdateCourse_WhenCourseDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var nonExistingCourseDto = new UpdateCourseDto
            {
                Title = "NewTitle",
                Credits = 4,
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(nonExistingCourseDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PutAsync("/courses/NonExistingId", jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
