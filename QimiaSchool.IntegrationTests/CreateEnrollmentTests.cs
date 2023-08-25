using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Queries.Enrollments.EnrollmentDtos;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.IntegrationTests
{
    [TestFixture]
    internal class CreateEnrollmentTests : IntegrationTestBase
    {
        public CreateEnrollmentTests() : base()
        {
        }

        [Test]
        public async Task CreateEnrollment_WhenValidData_ReturnsCreatedEnrollment()
        {
            // Arrange

            var newEnrollment = new EnrollmentDto
            {
                CourseID = 1,
                StudentID = 1,
                Grade = Grade.A
            };
            var content = new StringContent(JsonConvert.SerializeObject(newEnrollment), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/enrollments", content);
            var result = await response.Content.ReadAsStringAsync();
            var createdEnrollment = JsonConvert.DeserializeObject<EnrollmentDto>(result);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            createdEnrollment.Should().BeEquivalentTo(newEnrollment,
                options => options.Excluding(e => e.EnrollmentID));
        }
    }
}

