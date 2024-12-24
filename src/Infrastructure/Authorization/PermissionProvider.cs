using Application.Abstractions.Data;

namespace Infrastructure.Authorization;

internal sealed class PermissionProvider(IApplicationDbContext applicationDbContext)
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        await applicationDbContext.Users.FindAsync(userId);
        HashSet<string> permissionsSet = [];

        return permissionsSet;
    }
}
