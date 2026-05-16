using Application.AcademicYears;
using Application.Common;
using Domain.AcademicYears;
using Domain.Exceptions;

namespace Infrastructure.AcademicYears;

public class AcademicYearService(IAcademicYearRepository repository) : IAcademicYearService
{
    public async Task<PaginatedResult<AcademicYearDto>> GetAllAsync(int page, int pageSize)
    {
        var (items, total) = await repository.GetPagedAsync(page, pageSize);
        return new PaginatedResult<AcademicYearDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<Result<AcademicYearDto>> GetByIdAsync(int id)
    {
        var year = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Academic year not found.");

        return Result<AcademicYearDto>.Ok(MapToDto(year));
    }

    public async Task<Result<AcademicYearDto>> GetCurrentAsync()
    {
        var year = await repository.GetCurrentAsync()
            ?? throw new NotFoundException("No current academic year is set.");

        return Result<AcademicYearDto>.Ok(MapToDto(year));
    }

    public async Task<Result<AcademicYearDto>> CreateAsync(CreateAcademicYearDto dto)
    {
        if (await repository.HasOverlappingNameAsync(dto.Name))
            throw new ConflictException($"An academic year named '{dto.Name}' already exists.");

        if (dto.SetAsCurrent)
            await repository.ClearCurrentAsync();

        var year = AcademicYear.Create(dto.Name, dto.StartDate, dto.EndDate);

        if (dto.SetAsCurrent)
            year.MarkAsCurrent();

        await repository.AddAsync(year);
        await repository.SaveChangesAsync();

        return Result<AcademicYearDto>.Ok(MapToDto(year));
    }

    public async Task<Result<AcademicYearDto>> UpdateAsync(int id, UpdateAcademicYearDto dto)
    {
        var year = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Academic year not found.");

        if (dto.Name is not null && dto.Name != year.Name)
            if (await repository.HasOverlappingNameAsync(dto.Name, excludeId: id))
                throw new ConflictException($"An academic year named '{dto.Name}' already exists.");

        year.Update(dto.Name, dto.StartDate, dto.EndDate);

        repository.Update(year);
        await repository.SaveChangesAsync();

        return Result<AcademicYearDto>.Ok(MapToDto(year));
    }

    public async Task<Result> SetCurrentAsync(int id)
    {
        var year = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Academic year not found.");

        if (year.IsCurrent)
            return Result.Ok();

        await repository.ClearCurrentAsync();
        year.MarkAsCurrent();

        repository.Update(year);
        await repository.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeactivateAsync(int id)
    {
        var year = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Academic year not found.");

        year.Deactivate();

        repository.Update(year);
        await repository.SaveChangesAsync();

        return Result.Ok();
    }

    private static AcademicYearDto MapToDto(AcademicYear year) => new()
    {
        Id = year.Id,
        Name = year.Name,
        StartDate = year.StartDate,
        EndDate = year.EndDate,
        IsCurrent = year.IsCurrent,
        IsActive = year.IsActive
    };
}
