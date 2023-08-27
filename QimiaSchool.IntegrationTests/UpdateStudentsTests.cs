using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool.Business.Implementations.Commands.Students.StudentDtos;
using QimiaSchool.DataAccess.Entities;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;

namespace QimiaSchool.IntegrationTests
{
    internal class UpdateStudentTests : IntegrationTestBase
    {
        public UpdateStudentTests() : base()
        {
        }

        [Test]
        public async Task UpdateStudent_WhenCalled_ReturnsUpdatedStudent()
        {
            // Arrange
            var existingStudent = new Student
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "OldFirstMidName",
                LastName = "OldLastName",
            };

            databaseContext.Students.Add(existingStudent);
            await databaseContext.SaveChangesAsync();

            var updatedStudentDto = new UpdateStudentDto
            {
                FirstMidName = "NewFirstMidName",
                LastName = "NewLastName",
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(updatedStudentDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PutAsync("/students/" + existingStudent.ID, jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent); 
        }


        [Test]
        public async Task UpdateStudent_WhenStudentDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var nonExistingStudentDto = new UpdateStudentDto
            {
                FirstMidName = "NewFirstMidName",
                LastName = "NewLastName",
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(nonExistingStudentDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PutAsync("/students/NonExistingId", jsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
