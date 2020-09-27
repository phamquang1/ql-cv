using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace NCCTalentManagement.Controllers
{
    public abstract class NCCTalentManagementControllerBase: AbpController
    {
        protected NCCTalentManagementControllerBase()
        {
            LocalizationSourceName = NCCTalentManagementConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
