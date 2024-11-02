public interface IUserService
{
    Task<Result<PagedResponse<IEnumerable<UserReadInfo>>>> GetUsers(UserFilter userFilter);
    Task<Result<UserReadInfo>> GetUserById(int id);
    Task<BaseResult> CreateUser(UserCreateInfo user);
    Task<BaseResult> UpdateUser(UserUpdateInfo user);
    Task<BaseResult> DeleteUser(int id);
}