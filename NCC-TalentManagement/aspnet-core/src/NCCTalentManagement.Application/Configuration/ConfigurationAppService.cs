using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using NCCTalentManagement.Configuration.Dto;

namespace NCCTalentManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : NCCTalentManagementAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
        public async Task<string> GetGoogleClientAppId()
        {
            return await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.ClientAppId);
        }
    }
}
