using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using System.Linq.Expressions;

namespace QimiaSchool.DataAccess.Repositories.Implementations;

public class StudentRepository : RepositoryBase<Student>, IStudentRepository
{
    public StudentRepository(QimiaSchoolDbContext dbContext) : base(dbContext)
    {
    }
}
