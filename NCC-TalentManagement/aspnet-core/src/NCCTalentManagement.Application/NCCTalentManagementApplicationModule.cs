using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NCCTalentManagement.Authorization;

namespace NCCTalentManagement
{
    [DependsOn(
        typeof(NCCTalentManagementCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class NCCTalentManagementApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<NCCTalentManagementAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(NCCTalentManagementApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
