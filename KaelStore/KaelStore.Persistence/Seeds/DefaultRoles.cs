using KaelStore.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace KaelStore.Persistence.Seeds
{
    public static class DefaultRoles
    {
        public static List<IdentityRole> IdentityRoleList()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = Constants.SuperAdmin,
                    Name = nameof(Roles.SuperAdmin),
                    NormalizedName = nameof(Roles.SuperAdmin)
                },
                new IdentityRole
                {
                    Id = Constants.Admin,
                    Name = nameof(Roles.Admin),
                    NormalizedName = nameof(Roles.Admin)
                },
                new IdentityRole
                {
                    Id = Constants.Moderator,
                    Name = nameof(Roles.Moderator),
                    NormalizedName = nameof(Roles.Moderator)
                },
                new IdentityRole
                {
                    Id = Constants.Basic,
                    Name = nameof(Roles.Basic),
                    NormalizedName = nameof(Roles.Basic)
                }
            };
        }
    }
}
