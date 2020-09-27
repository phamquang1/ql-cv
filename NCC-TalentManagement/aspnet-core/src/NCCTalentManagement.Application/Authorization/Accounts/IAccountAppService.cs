using System.Threading.Tasks;
using Abp.Application.Services;
using NCCTalentManagement.Authorization.Accounts.Dto;

namespace NCCTalentManagement.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
