using NCCTalentManagement.APIs.SingleSignOnSetting.Dto;
using NCCTalentManagement.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NCCTalentManagement.APIs.SingleSignOnSetting
{
    public class SingleSignOnSettingService : NCCTalentManagementAppServiceBase
    {
        public async Task<SingleSignOnSettingDto> Get()
        {
            return new SingleSignOnSettingDto
            {
                ClientAppId = await SettingManager.GetSettingValueAsync(AppSettingNames.ClientAppId),
                RegisterSecretCode = await SettingManager.GetSettingValueAsync(AppSettingNames.SecretRegisterCode)
            };
        }

        public async Task<SingleSignOnSettingDto> Change(SingleSignOnSettingDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.ClientAppId, input.ClientAppId);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SecretRegisterCode, input.RegisterSecretCode);
            return input;
        }
    }
}
