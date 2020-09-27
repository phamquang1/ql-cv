using Abp.MultiTenancy;
using NCCTalentManagement.Authorization.Users;

namespace NCCTalentManagement.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
