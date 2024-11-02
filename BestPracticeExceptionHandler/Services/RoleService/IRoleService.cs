namespace BestPracticeExceptionHandler.Services.RoleService;

public interface IRoleService
{
    Task<Result<PagedResponse<IEnumerable<RoleReadInfo>>>> GetRoles(RoleFilter filter);
    Task<BaseResult> CreateRole(RoleCreateInfo info);
    Task<BaseResult> UpdateRole(RoleUpdateInfo info);
    Task<BaseResult> DeleteRole(int id);
    Task<Result<RoleReadInfo>> GetRoleById(int id);
}