using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NCCTalentManagement.Configuration;
using NCCTalentManagement.Web;

namespace NCCTalentManagement.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class NCCTalentManagementDbContextFactory : IDesignTimeDbContextFactory<NCCTalentManagementDbContext>
    {
        public NCCTalentManagementDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NCCTalentManagementDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            NCCTalentManagementDbContextConfigurer.Configure(builder, configuration.GetConnectionString(NCCTalentManagementConsts.ConnectionStringName));

            return new NCCTalentManagementDbContext(builder.Options);
        }
    }
}
