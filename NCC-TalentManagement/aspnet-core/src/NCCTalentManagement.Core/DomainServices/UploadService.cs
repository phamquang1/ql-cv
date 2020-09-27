using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NCCTalentManagement.Authorization.Users;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NCCTalentManagement.DomainServices
{
    public class UploadService : BaseDomainService,IUploadService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager _userManager;


        public UploadService(IHostingEnvironment hostingEnvironment, UserManager userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }


        //[AbpAuthorize(Ncc.Authorization.PermissionNames.Pages_Users)]
        public async Task<string> UpdateAvatar(IFormFile file)
        {
            String path = Path.Combine(_hostingEnvironment.WebRootPath, "avatars");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (file != null && file.Length > 0)
            {
                string FileExtension = Path.GetExtension(file.FileName).ToLower();

                if (FileExtension == ".jpeg" || FileExtension == ".png" || FileExtension == ".jpg" || FileExtension == ".gif")
                {
                    if (file.Length > (1048576 * 5))
                    {
                        throw new UserFriendlyException(String.Format("File needs to be less than 5MB!"));
                    }
                    else
                    {
                        //get user to take name + code
                        //User user = await _userManager.GetUserByIdAsync(input.UserId);
                        //set avatar name = milisecond + id + name + extension
                        String avatarPath = DateTimeOffset.Now.ToUnixTimeMilliseconds()
                            + Path.GetExtension(file.FileName);
                        using (var stream = System.IO.File.Create(Path.Combine(_hostingEnvironment.WebRootPath, "avatars", avatarPath)))
                        {
                            await file.CopyToAsync(stream);
                        }
                        return avatarPath;
                    }
                }
                else
                {
                    throw new UserFriendlyException(String.Format("File can not upload!"));
                }
            }
            else
            {
                throw new UserFriendlyException(String.Format("No file upload!"));
            }
        }
    }
}
