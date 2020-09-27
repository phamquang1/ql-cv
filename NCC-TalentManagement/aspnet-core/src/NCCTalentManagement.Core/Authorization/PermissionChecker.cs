using Abp.Authorization;
using NCCTalentManagement.Authorization.Roles;
using NCCTalentManagement.Authorization.Users;

namespace NCCTalentManagement.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
