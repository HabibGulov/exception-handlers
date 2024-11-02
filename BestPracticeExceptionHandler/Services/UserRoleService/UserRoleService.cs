
using BestPracticeExceptionHandler.DataContext;

public class UserRoleService(DataContext context) : IUserRoleService
{
    public async Task<BaseResult> CreateUserRole(UserRoleCreateInfo userRole)
    {
        bool conflict = await context.UserRoles.AnyAsync(x => x.RoleId == userRole.UserRoleBaseInfo.RoleId && x.UserId == userRole.UserRoleBaseInfo.UserId);
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());
        await context.UserRoles.AddAsync(userRole.ToUserRole());
        int res = await context.SaveChangesAsync();

        return res is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved!!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteUserRole(int id)
    {
        UserRole? existingUserRole = await context.UserRoles.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingUserRole is null)
            return BaseResult.Failure(Error.NotFound());

        existingUserRole.ToDeletedUserRole();

        int res = await context.SaveChangesAsync();

        return res is 0
           ? BaseResult.Failure(Error.InternalServerError("Data not deleted!!!"))
            : BaseResult.Success();
    }

    public async Task<Result<UserReadInfo>> GetUserRoleById(int id)
    {
        UserRole? userRole = await context.UserRoles.FirstOrDefaultAsync(x => x.Id == id);

        return userRole is null
           ? Result<UserReadInfo>.Failure(Error.NotFound())
            : Result<UserReadInfo>.Success(userRole.ToReadInfo());
    }

    public async Task<Result<PagedResponse<IEnumerable<UserRoleReadInfo>>>> GetUsersRoles(UserRoleFilter filter)
    {
        IQueryable<UserRoleReadInfo> usersRoles = context.UserRoles;

        IQueryable<UserReadInfo> res = context.Skip((filter.PageNumber - 1) * filter.PageSize).Select(x => x.ToReadInfo);

        int count = await res.CountAsync();

        PagedResponse<IEnumerable<UserRoleReadInfo>> response = PagedResponse<IEnumerable<UserRoleReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, res);
        return Result<PagedResponse<IEnumarable<UserRoleReadInfo>>>.Success(response);

    }

    public async Task<BaseResult> UpdateUserRole(UserRoleUpdateInfo userRole)
    {
        UserRole? existingUserRole = await context.UserRoles.AsTracking().FirstOrDefaultAsync(x => x.id == userRole.Id);
        if (existingUserRole == null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = context.UserRoles.AnyAsync(x => x.UserId == userRole.UserRoleBaseInfo.UserId && x.RoleId == userRole.UserRoleBaseInfo.RoleId);
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());

        existingUserRole.ToUpdatedUserRole(userRole);
        int result = await context.SaveChangesAsync();

        return result is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!!!"))
            : BaseResult.Success();
    }
}