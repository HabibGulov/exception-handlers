namespace BestPracticeExceptionHandler.Controllers;

[Route("api/user-roles")]
public sealed class UserRoleController(IUserRoleService userRoleService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetUsersRoles([FromQuery] UserRoleFilter filter)
    => (await userRoleService.GetUsersRoles(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserRoleById(int id)
        => (await userRoleService.GetUserRoleById(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> CreateUserRole([FromBody] UserRoleCreateInfo info)
        => (await userRoleService.CreateUserRole(info)).ToActionResult();

    [HttpPut]
    public async Task<IActionResult> UpdateUserRole([FromBody] UserRoleUpdateInfo info)
        => (await userRoleService.UpdateUserRole(info)).ToActionResult();

    [HttpDelete]
    public async Task DeleteUserRole(int id)
        => (await userRoleService.DeleteUserRole(id)).ToActionResult();
}