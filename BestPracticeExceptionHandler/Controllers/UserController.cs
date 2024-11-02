namespace BestPracticeExceptionHandler.Controllers;

[Route("api/users")]
public sealed class UserController(IUserService userService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserFilter filter)
        => (await userService.GetUsers(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
        => (await userService.GetUserById(id)).ToActionResult();

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateInfo info)
        => (await userService.CreateUser(info)).ToActionResult();
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateInfo info)
        => (await userService.UpdateUser(info)).ToActionResult();

    [HttpDelete]
    public async Task DeleteUser(int id)
        => (await userService.DeleteUser(id)).ToActionResult();
}