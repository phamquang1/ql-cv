using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NCCTalentManagement.Configuration;
using NCCTalentManagement.EntityFrameworkCore;
using NCCTalentManagement.Migrator.DependencyInjection;

namespace NCCTalentManagement.Migrator
{
    [DependsOn(typeof(NCCTalentManagementEntityFrameworkModule))]
    public class NCCTalentManagementMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public NCCTalentManagementMigratorModule(NCCTalentManagementEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(NCCTalentManagementMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                NCCTalentManagementConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NCCTalentManagementMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
