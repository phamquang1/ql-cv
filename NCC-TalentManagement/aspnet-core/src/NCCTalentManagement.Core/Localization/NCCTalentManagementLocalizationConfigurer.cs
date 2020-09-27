using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace NCCTalentManagement.Localization
{
    public static class NCCTalentManagementLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(NCCTalentManagementConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(NCCTalentManagementLocalizationConfigurer).GetAssembly(),
                        "NCCTalentManagement.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
