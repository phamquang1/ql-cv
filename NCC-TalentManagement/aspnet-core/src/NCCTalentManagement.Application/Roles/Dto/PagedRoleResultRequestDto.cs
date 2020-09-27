using Abp.Application.Services.Dto;

namespace NCCTalentManagement.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

