using Abp.AutoMapper;
using NCCTalentManagement.Authentication.External;

namespace NCCTalentManagement.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
