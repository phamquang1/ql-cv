using System.Threading.Tasks;
using NCCTalentManagement.Models.TokenAuth;
using NCCTalentManagement.Web.Controllers;
using Shouldly;
using Xunit;

namespace NCCTalentManagement.Web.Tests.Controllers
{
    public class HomeController_Tests: NCCTalentManagementWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}