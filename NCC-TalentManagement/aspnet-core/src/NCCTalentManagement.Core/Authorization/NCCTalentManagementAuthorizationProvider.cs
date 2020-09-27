using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace NCCTalentManagement.Authorization
{
    public class NCCTalentManagementAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            foreach (var permission in SystemPermission.ListPermissions)
            {
                context.CreatePermission(permission.Permission, L(permission.DisplayName), multiTenancySides: permission.MultiTenancySides);
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, NCCTalentManagementConsts.LocalizationSourceName);
        }
    }
}
