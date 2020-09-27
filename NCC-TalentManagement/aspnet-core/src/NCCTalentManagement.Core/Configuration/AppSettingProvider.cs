using System.Collections.Generic;
using Abp.Configuration;

namespace NCCTalentManagement.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.ClientAppId,"2726804614-0ess6uli7cluev0mfcin8kmbs4bk2v3c.apps.googleusercontent.com",scopes:SettingScopes.Application| SettingScopes.Tenant)
            };
        }
    }
}
