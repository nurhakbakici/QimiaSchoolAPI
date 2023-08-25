using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Queries.Courses.CourseDtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QimiaSchool.IntegrationTests
{
    [TestFixture]
    internal class CreateCourseTests : IntegrationTestBase
    {
        public CreateCourseTests() : base()
        {
        }

        [Test]
        public async Task CreateCourse_WhenValidData_ReturnsCreatedCourse()
        {
            // Arrange
            var newCourse = new CourseDto
            {
                Title = "New Course",
                Credits = 2,
                ID = 2
            };
            var content = new StringContent(JsonConvert.SerializeObject(newCourse), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/courses", content);
            var result = await response.Content.ReadAsStringAsync();
            var createdCourse = JsonConvert.DeserializeObject<CourseDto>(result);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            createdCourse.Should().BeEquivalentTo(newCourse,
                options => options.Excluding(c => c.ID));
        }
    }
}
