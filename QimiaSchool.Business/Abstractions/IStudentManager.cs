using QimiaSchool.DataAccess.Entities;

namespace QimiaSchool.Business.Abstractions;

public interface IStudentManager
{
    public Task CreateStudentAsync(Student student, CancellationToken cancellationToken);

    public Task<Student> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken);

    public Task UpdateStudentAsync(Student student, CancellationToken cancellationToken);

    public Task DeleteStudentByIdAsync(int studentId, CancellationToken cancellationToken);

    public Task<IEnumerable<Student>> GetAllStudentsAsync(CancellationToken cancellationToken);
}