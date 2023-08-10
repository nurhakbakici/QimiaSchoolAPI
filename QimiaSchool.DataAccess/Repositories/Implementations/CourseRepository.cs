using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QimiaSchool.DataAccess.Entities;
using QimiaSchool.DataAccess.Repositories.Abstractions;

namespace QimiaSchool.DataAccess.Repositories.Implementations;
public class CourseRepository : RepositoryBase<Course>, ICourseRepository
{
    public CourseRepository(QimiaSchoolDbContext dbContext) : base(dbContext)
    {
    }
}
