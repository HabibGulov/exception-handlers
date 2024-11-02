namespace BestPracticeExceptionHandler.Services.RoleService;

public sealed class RoleService(DataContext.DataContext context) : IRoleService
{
    public async Task<Result<PagedResponse<IEnumerable<RoleReadInfo>>>> GetRoles(RoleFilter filter)
    {
        IQueryable<Role> roles = context.Roles;

        if (filter.Name is not null)
            roles = roles.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

        IQueryable<RoleReadInfo> result = roles
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).Select(x => x.ToReadInfo());
        
        int count = await result.CountAsync();

        PagedResponse<IEnumerable<RoleReadInfo>> response = PagedResponse<IEnumerable<RoleReadInfo>>
            .Create(filter.PageNumber, filter.PageSize, count, result);

        return Result<PagedResponse<IEnumerable<RoleReadInfo>>>.Success(response);
    }

    public async Task<BaseResult> CreateRole(RoleCreateInfo info)
    {
        bool conflict = await context.Roles.AnyAsync(x => x.Name.ToLower() == info.RoleBaseInfo.Name.ToLower());
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());
        await context.Roles.AddAsync(info.ToRole());
        int res = await context.SaveChangesAsync();

        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved !!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateRole(RoleUpdateInfo info)
    {
        //2.
        Role? existingRole = await context.Roles.AsTracking().FirstOrDefaultAsync(x => x.Id == info.Id);
        if (existingRole is null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = await context.Roles.AnyAsync(x
            => x.Id != info.Id && x.Name.ToLower() ==
            info.RoleBaseInfo.Name.ToLower());

        if (conflict)
            return BaseResult.Failure(Error.Conflict());

        //1.//context.Roles.Update(existingRole.ToUpdatedRole(info));
        existingRole.ToUpdatedRole(info);
        int res = await context.SaveChangesAsync();

        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteRole(int id)
    {
        //2.
        Role? existingRole = await context.Roles.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingRole is null)
            return BaseResult.Failure(Error.NotFound());

        existingRole.ToDeletedRole();
        //1.//context.Roles.Update(existingRole.ToDeletedRole());
        int res = await context.SaveChangesAsync();

        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted!!"))
            : BaseResult.Success();
    }

    public async Task<Result<RoleReadInfo>> GetRoleById(int id)
    {
        Role? result = await context.Roles.FirstOrDefaultAsync(x => x.Id == id);

        return result is null
            ? Result<RoleReadInfo>.Failure(Error.NotFound())
            : Result<RoleReadInfo>.Success(result.ToReadInfo());
    }
}