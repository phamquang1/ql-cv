using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace NCCTalentManagement.EntityFrameworkCore
{
    public static class NCCTalentManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<NCCTalentManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<NCCTalentManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
