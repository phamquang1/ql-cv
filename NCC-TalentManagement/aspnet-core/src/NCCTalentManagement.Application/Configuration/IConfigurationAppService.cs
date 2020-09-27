using System.Threading.Tasks;
using NCCTalentManagement.Configuration.Dto;

namespace NCCTalentManagement.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
