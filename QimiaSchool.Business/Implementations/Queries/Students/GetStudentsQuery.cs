using MediatR;
using QimiaSchool.Business.Implementations.Queries.Students.StudentDtos;


namespace QimiaSchool.Business.Implementations.Queries.Students;

public class GetStudentsQuery : IRequest<List<StudentDto>>
{


}


// we didn't set an id because we want all the students to show when we started the query.