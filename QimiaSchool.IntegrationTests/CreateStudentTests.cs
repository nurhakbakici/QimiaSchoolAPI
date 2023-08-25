using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace QimiaSchool.IntegrationTests
{
    [TestFixture]
    internal class CreateStudentTests : IntegrationTestBase
    {
        public CreateStudentTests() : base()
        {
        }

        [Test]
        public async Task CreateStudent_WhenValidData_ReturnsCreatedStudent()
        {
            // Arrange
            var newStudent = new StudentDto
            {
                FirstMidName = "New",
                LastName = "Student",
            };
            var content = new StringContent(JsonConvert.SerializeObject(newStudent), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/students", content);
            var result = await response.Content.ReadAsStringAsync();
            var createdStudent = JsonConvert.DeserializeObject<StudentDto>(result);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            createdStudent.Should().BeEquivalentTo(newStudent,
                options => options.Excluding(s => s.ID));
        }
    }
}
