using Application.Common;

namespace Application.Subjects;

public interface ISubjectService
{
    Task<IList<SubjectDto>> GetByGradeAsync(int gradeId);
    Task<Result<SubjectDto>> GetByIdAsync(int id);
    Task<Result<SubjectDto>> CreateAsync(CreateSubjectDto dto);
    Task<Result<SubjectDto>> UpdateAsync(int id, UpdateSubjectDto dto);
    Task<Result> DeactivateAsync(int id);
}
