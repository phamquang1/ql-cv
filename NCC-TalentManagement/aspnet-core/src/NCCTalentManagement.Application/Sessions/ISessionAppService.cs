using System.Threading.Tasks;
using Abp.Application.Services;
using NCCTalentManagement.Sessions.Dto;

namespace NCCTalentManagement.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
