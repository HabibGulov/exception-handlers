namespace BestPracticeExceptionHandler.Controllers;

[Route("/api/roles")]
public sealed class RoleController(IRoleService roleService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetRoles([FromQuery] RoleFilter filter)
        => (await roleService.GetRoles(filter)).ToActionResult();


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoleById(int id)
        => (await roleService.GetRoleById(id)).ToActionResult();


    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateInfo info)
        => (await roleService.CreateRole(info)).ToActionResult();

    [HttpPut]
    public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateInfo info)
        => (await roleService.UpdateRole(info)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task DeleteRole(int id)
        => (await roleService.DeleteRole(id)).ToActionResult();
}