using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NCCTalentManagement.Configuration;

namespace NCCTalentManagement.Web.Host.Startup
{
    [DependsOn(
       typeof(NCCTalentManagementWebCoreModule))]
    public class NCCTalentManagementWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public NCCTalentManagementWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NCCTalentManagementWebHostModule).GetAssembly());
        }
    }
}
