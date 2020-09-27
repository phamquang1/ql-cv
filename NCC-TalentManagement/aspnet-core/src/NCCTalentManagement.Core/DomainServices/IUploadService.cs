using Abp.Domain.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NCCTalentManagement.DomainServices
{
    public interface IUploadService : IDomainService
    {
        public Task<string> UpdateAvatar(IFormFile file);
    }
}
