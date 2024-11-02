namespace BestPracticeExceptionHandler.DTOs;

public readonly record struct RoleBaseInfo(string Name);

public readonly record struct RoleReadInfo(
    int Id,
    RoleBaseInfo RoleBaseInfo);

public readonly record struct RoleCreateInfo(
    RoleBaseInfo RoleBaseInfo);

public readonly record struct RoleUpdateInfo(
    int Id,
    RoleBaseInfo RoleBaseInfo);