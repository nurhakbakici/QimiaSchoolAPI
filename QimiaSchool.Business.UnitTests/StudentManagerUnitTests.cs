using Moq;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;

namespace QimiaSchool.Business.UnitTests;

[TestFixture]
internal class StudentManagerUnitTests
{
    private readonly Mock<IStudentRepository> _mockStudentRepository; // mocking is a process used for creating a mock object that can stimulate the behavior of the real object
    private readonly StudentManager _studentManager;

    public StudentManagerUnitTests()
    {
        _mockStudentRepository = new Mock<IStudentRepository>();
        _studentManager = new StudentManager(_mockStudentRepository.Object);
    }

    [Test]
    public async Task CreateStudentAsync_WhenCalled_CallsRepository()
    {
        // Arrange (set up the test)
        var testStudent = new Student
        {
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act (execute the test)
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert (verify the results)
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateStudentAsync_WhenStudentIdHasValue_RemovesAndCallsRepository()
    {
        // Arrange
        var testStudent = new Student
        {
            ID = 1,
            EnrollmentDate = DateTime.Now,
            FirstMidName = "Test",
            LastName = "Test"
        };

        // Act
        await _studentManager.CreateStudentAsync(testStudent, default);

        // Assert
        _mockStudentRepository
            .Verify(
                sr => sr.CreateAsync(
                    It.Is<Student>(s => s == testStudent),
                    It.IsAny<CancellationToken>()), Times.Once);
    }
}
