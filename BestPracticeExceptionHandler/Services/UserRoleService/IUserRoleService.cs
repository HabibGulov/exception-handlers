public interface IUserRoleService
{
    Task<Result<PagedResponse<IEnumerable<UserReadInfo>>>> GetUsersRoles(UserRoleFilter filter);
    Task<Result<UserReadInfo>> GetUserRoleById(int id);
    Task<BaseResult> CreateUserRole(UserRoleCreateInfo userRole);
    Task<BaseResult> UpdateUserRole(UserRoleUpdateInfo userRole);
    Task<BaseResult> DeleteUserRole(int id);
}