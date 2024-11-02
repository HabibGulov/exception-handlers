
using BestPracticeExceptionHandler.DataContext;

public sealed class UserService(DataContext context) : IUserService
{
    public async Task<BaseResult> CreateUser(UserCreateInfo user)
    {
        bool conflict = await context.Users.AnyAsync(x => x.UserName.ToLower() == user.UserBaseInfo.UserName.ToLower());
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());
        await context.Users.AddAsync(user.ToUser());
        int res = await context.SaveChangesAsync();

        return res is 0
           ? BaseResult.Failure(Error.InternalServerError("Data not saved!!!"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteUser(int id)
    {
        User? existingUser = await context.Users.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser is null)
            return BaseResult.Failure(Error.NotFound());

        existingUser.ToDeletedUser();

        int res = await context.SaveChangesAsync();

        return res is 0
           ? BaseResult.Failure(Error.InternalServerError("Data not deleted!!!"))
            : BaseResult.Success();
    }

    public async Task<Result<UserReadInfo>> GetUserById(int id) 
    {
        User? user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user is null
           ? Result<UserReadInfo>.Failure(Error.NotFound())
            : Result<UserReadInfo>.Success(user.ToReadInfo());
    }

    public async Task<Result<PagedResponse<IEnumerable<UserReadInfo>>>> GetUsers(UserFilter userFilter)
    {
        IQueryable<User> users = context.Users;

        if (userFilter.Username is not null)
            users = users.Where(x => x.UserName.ToLower().Contains(userFilter.Username.ToLower()));

        IQueryable<UserReadInfo> result = users
            .Skip((userFilter.PageNumber - 1) * userFilter.PageSize)
            .Take(userFilter.PageSize).Select(x => x.ToReadInfo());

        int count = await result.CountAsync();

        PagedResponse<IEnumerable<UserReadInfo>> response = PagedResponse<IEnumerable<UserReadInfo>>
            .Create(userFilter.PageNumber, userFilter.PageSize, count, result);

        return Result<PagedResponse<IEnumerable<UserReadInfo>>>.Success(response);
    }

    public async Task<BaseResult> UpdateUser(UserUpdateInfo user)
    {
        User? existingUser = await context.Users.AsTracking().FirstOrDefaultAsync(x => x.Id == user.UserId);
        if (existingUser is null)
            return BaseResult.Failure(Error.NotFound());

        bool conflict = await context.Users.AnyAsync(x => x.Id != user.UserId && x.UserName.ToLower() == user.UserBaseInfo.UserName.ToLower());
        if (conflict)
            return BaseResult.Failure(Error.AlreadyExist());

        bool conflict1 = await context.Users.AnyAsync(x => x.Email.ToLower() == user.UserBaseInfo.Email.ToLower());
        if (conflict1)
            return BaseResult.Failure(Error.AlreadyExist());

        existingUser.ToUpdatedUser(user);
        int result = await context.SaveChangesAsync();

        return result is 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated!!!"))
            : BaseResult.Success();
    }
}   