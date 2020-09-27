using Abp.Application.Services;
using NCCTalentManagement.MultiTenancy.Dto;

namespace NCCTalentManagement.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

