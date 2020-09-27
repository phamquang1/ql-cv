using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Hosting;
using Abp.Runtime.Session;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.MultiTenancy;
using Abp.Dependency;
using NCCTalentManagement.IoC;
using Abp.UI;
using Microsoft.Extensions.Configuration;
using System.IO;
using NCCTalentManagement.Web;


namespace NCCTalentManagement
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class NCCTalentManagementAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public IWorkScope WorkScope { get; set; }
        protected NCCTalentManagementAppServiceBase()
        {
            LocalizationSourceName = NCCTalentManagementConsts.LocalizationSourceName;
            WorkScope = IocManager.Instance.Resolve<IWorkScope>();
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new UserFriendlyException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        protected virtual string GetSpacingString(int charecter)
        {
            string result = new String(' ', charecter);
            return result;
        }
    }
}
