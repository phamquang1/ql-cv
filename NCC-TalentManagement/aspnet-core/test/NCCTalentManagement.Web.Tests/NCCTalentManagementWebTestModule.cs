using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NCCTalentManagement.EntityFrameworkCore;
using NCCTalentManagement.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace NCCTalentManagement.Web.Tests
{
    [DependsOn(
        typeof(NCCTalentManagementWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class NCCTalentManagementWebTestModule : AbpModule
    {
        public NCCTalentManagementWebTestModule(NCCTalentManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NCCTalentManagementWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(NCCTalentManagementWebMvcModule).Assembly);
        }
    }
}