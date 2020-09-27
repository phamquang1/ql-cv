using Abp.Authorization;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.Constants;
using NCCTalentManagement.Entities;
using NCCTalentManagement.Sessions;
using System.Collections.Generic;
using NCCTalentManagement.Constants.Enum;
using NCCTalentManagement.DomainServices;
using NCCTalentManagement.Extensions;
using System.Linq;
using System.Threading.Tasks;
using NCCTalentManagement.APIs.MyProfile.Dto;
using NCCTalentManagement.APIs.MyCV.Dto;
using NCCTalentManagement.Authorization;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace NCCTalentManagement.APIs.MyCV
{
    [AbpAuthorize]
    public class MyProfileAppService : NCCTalentManagementAppServiceBase
    {
        private SessionAppService _sessionAppService;
        private readonly IUploadService _uploadService;
        private IHostingEnvironment _hostingEnvironment;
        public MyProfileAppService(SessionAppService sessionAppService,
                                   IUploadService uploadService,
                                   IHostingEnvironment hostingEnvironment)
        {
            _sessionAppService = sessionAppService;
            _uploadService = uploadService;
            _hostingEnvironment = hostingEnvironment;
        }

        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task<TechnicalExpertiseDto> GetTechnicalExpertise(long userId)
        {
            if ((AbpSession.UserId.Value != userId) && !await UserManager.IsGrantedAsync(AbpSession.UserId.Value, PermissionNames.Pages_View_CVEmployee))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }

            var query = await WorkScope.GetAll<User>()
                                       .Include(u => u.Cvskills)
                                       .ThenInclude(u => u.GroupSkill)
                                       .ThenInclude(cvs => cvs.Skills)
                                       .FirstOrDefaultAsync(u => u.Id == userId);

            var userGroupSkills = query.Cvskills.GroupBy(s => s.GroupSkillId)
                                 .Select(g => new GroupSkillAndSkillDto
                                 {
                                     GroupSkillId = g.Key.Value,
                                     Name = g.FirstOrDefault().GroupSkill.Name,
                                     CVSkills = g.Select(s => new CVSkillDto
                                     {
                                         Id = s.Id,
                                         SkillId = s.SkillId,
                                         SkillName = string.IsNullOrWhiteSpace(s.SkillName) ? s.GroupSkill.Skills.FirstOrDefault(sk => sk.Id == s.SkillId)?.Name : s.SkillName,
                                         Level = s.Level,
                                     }).ToList()
                                 })
                                .ToList();

            return new TechnicalExpertiseDto
            {
                UserId = userId,
                GroupSkills = userGroupSkills
            };
        }

        public async Task UpdateTechnicalExpertise(TechnicalExpertiseDto input)
        {
            if (AbpSession.UserId != input.UserId)
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.EditOtherProfile);
            }

            var dbCVSkills = WorkScope.GetRepo<CVSkills, long>();

            List<long> listCurrentId = new List<long>();
            foreach (var grs in input.GroupSkills)
            {
                var cvSkills = grs.CVSkills;

                foreach (var s in cvSkills)
                {
                    listCurrentId.Add(await dbCVSkills.InsertOrUpdateAndGetIdAsync(new CVSkills
                    {
                        CvemployeeId = input.UserId,
                        GroupSkillId = grs.GroupSkillId,
                        Id = s.Id ?? 0,
                        Level = s.Level,
                        SkillId = s.SkillId,
                        SkillName = s.SkillName
                    }));
                }
            }

            await dbCVSkills.DeleteAsync(c => !listCurrentId.Contains(c.Id) && c.CvemployeeId == input.UserId);
        }

        [AbpAuthorize(PermissionNames.Pages_MyProfile)]
        public async Task<List<WorkingExperienceDto>> GetUserWorkingExperience(long userId)
        {
            if ((AbpSession.UserId.Value != userId) && !await UserManager.IsGrantedAsync(AbpSession.UserId.Value, PermissionNames.Pages_View_CVEmployee))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }

            return await WorkScope.GetAll<EmployeeWorkingExperiences>()
                                  .Where(w => w.UserId == userId)
                                  .Select(w => new WorkingExperienceDto
                                  {
                                      Id = w.Id,
                                      Position = w.Position,
                                      ProjectDescription = w.ProjectDescription,
                                      ProjectName = w.ProjectName,
                                      Responsibility = w.Responsibilities,
                                      EndTime = w.EndTime,
                                      StartTime = w.StartTime,
                                      UserId = w.UserId,
                                      Order = w.Order,
                                      Technologies = w.Technologies
                                  })
                                  .OrderBy(w => w.Order)
                                  .ToListAsync();
        }

        public async Task UpdateWorkingExperience(WorkingExperienceDto input)
        {
            if (AbpSession.UserId != input.UserId)
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.EditOtherProfile);
            }

            await WorkScope.InsertOrUpdateAndGetIdAsync(ObjectMapper.Map<EmployeeWorkingExperiences>(input));
        }

        public async Task DeleteWorkingExperience(long id)
        {
            var exp = await WorkScope.GetAll<EmployeeWorkingExperiences>().FirstOrDefaultAsync(e => e.Id == id);

            if (AbpSession.UserId != exp.UserId)
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.EditOtherProfile);
            }

            await WorkScope.DeleteAsync<EmployeeWorkingExperiences>(id);
        }

        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task<UserGeneralInfoDto> GetUserGeneralInfo(long userId)
        {
            if ((AbpSession.UserId.Value != userId) && !await UserManager.IsGrantedAsync(AbpSession.UserId.Value, PermissionNames.Pages_View_CVEmployee))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }
            var userGeneralInfo = await WorkScope.GetAll<User, long>()
                                                 .Include(u => u.Position)
                                                 .Include(u => u.Branch)
                                                 .FirstOrDefaultAsync(u => u.Id == userId);

            return new UserGeneralInfoDto
            {
                UserId = userGeneralInfo.Id,
                Surname = userGeneralInfo.Surname,
                Name = userGeneralInfo.Name,
                CurrentPosition = userGeneralInfo.Position.Name,
                EmailAddressInCV = userGeneralInfo.EmailAddressInCV,
                Address = userGeneralInfo.Address,
                ImgPath = userGeneralInfo.AvatarPath,
                PhoneNumber = userGeneralInfo.PhoneNumber,
                Branch = userGeneralInfo.Branch.Name
            };
        }

        [HttpPost]
        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task<UpdateUserGeneralInfoDto> SaveUserGeneralInfo([FromForm] UpdateUserGeneralInfoDto input)
        {
            var user = await WorkScope.GetAll<User, long>()
                                                 .Include(u => u.Position)
                                                 .Include(u => u.Branch)
                                                 .FirstOrDefaultAsync(u => u.Id == input.UserId);
            if (user == null)
            {
                throw new UserFriendlyException(ErrorCodes.NotFound.UserNotExist);
            }
            if (AbpSession.UserId.Value != input.UserId)
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }
            user.Name = input.Name;
            user.Surname = input.Surname;
            user.PhoneNumber = input.PhoneNumber;
            user.EmailAddressInCV = input.EmailAddressInCV;
            user.PositionId = input.CurrentPositionId;
            user.Address = input.Address;
            user.BranchId = input.BranchId;
            
            if (input.File == null)
            {
                user.AvatarPath = input.Path;
            }
            else
            {
                if (user.AvatarPath != null)
                {
                    var pathRoot = _hostingEnvironment.WebRootPath;
                    var file = Path.Combine(pathRoot, user.AvatarPath.Replace("/", @"\"));
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                var imgPath = await _uploadService.UpdateAvatar(input.File);
                user.AvatarPath = $"{RootConstant.ImgRoot}{imgPath.Replace("/", @"\")}";
            }
            user.SetNormalizedNames();
            await WorkScope.GetRepo<User, long>().UpdateAsync(user);
            return input;
        }

        [AbpAuthorize(PermissionNames.Pages_MyProfile)]
        public async Task<List<EducationDto>> GetEducationInfo(long userId)
        {
            if ((AbpSession.UserId.Value != userId) && !await UserManager.IsGrantedAsync(AbpSession.UserId.Value, PermissionNames.Pages_View_CVEmployee))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }
            var lstEdu = new List<EducationDto>();
            foreach (var edu in WorkScope.GetAll<Educations>().Where(e => e.CvemployeeId.Value == userId && e.IsDeleted == false).OrderBy(e => e.Order))
            {
                var temp = new EducationDto
                {
                    Id = edu.Id,
                    SchoolOrCenterName = edu.SchoolOrCenterName,
                    CvemployeeId = userId,
                    DegreeType = edu.DegreeType,
                    StartYear = edu.StartYear,
                    EndYear = edu.EndYear,
                    Description = edu.Description,
                    Major = edu.Major,
                    Order = edu.Order,
                };
                lstEdu.Add(temp);
            }
            return lstEdu;
        }

        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task DeleteEducation(long id)
        {
            var edu = await WorkScope.GetAsync<Educations>(id);
            if (edu == null)
            {
                throw new UserFriendlyException(ErrorCodes.NotFound.UserNotExist);
            }
            if (AbpSession.UserId.Value != edu.CvemployeeId)
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }
            await WorkScope.DeleteAsync<Educations>(id);
        }

        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task<EducationDto> SaveEducation(EducationDto input)
        {
            if ((!WorkScope.GetAll<User, long>().Any(u => u.Id == input.CvemployeeId) || (!input.CvemployeeId.HasValue)))
            {
                throw new UserFriendlyException(ErrorCodes.NotFound.UserNotExist);
            }
            if (AbpSession.UserId.Value != input.CvemployeeId)
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }
            int startYear, endYear;
            if (!(int.TryParse(input.StartYear, out startYear) && int.TryParse(input.EndYear, out endYear)))
            {
                throw new UserFriendlyException(ErrorCodes.NotAcceptable.YearIsNotValid);
            }
            if (startYear > endYear)
            {
                throw new UserFriendlyException(ErrorCodes.NotAcceptable.YearOutOfRange);
            }
            if (input.Id <= 0)
            {
                var education = new Educations
                {
                    CvemployeeId = input.CvemployeeId,
                    SchoolOrCenterName = input.SchoolOrCenterName,
                    DegreeType = input.DegreeType,
                    Major = input.Major,
                    StartYear = input.StartYear,
                    EndYear = input.EndYear,
                    Description = input.Description,
                    Order = input.Order
                };
                await WorkScope.GetRepo<Educations, long>().InsertAsync(education);
                return input;
            }
            else
            {
                var education = await WorkScope.GetAsync<Educations>(input.Id);
                education.CvemployeeId = input.CvemployeeId;
                education.SchoolOrCenterName = input.SchoolOrCenterName;
                education.DegreeType = input.DegreeType;
                education.Major = input.Major;
                education.StartYear = input.StartYear;
                education.EndYear = input.EndYear;
                education.Description = input.Description;
                education.Order = input.Order;
                await WorkScope.GetRepo<Educations, long>().UpdateAsync(education);
                return input;

            }
        }

        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task UpdateOrderWorkingExperience(List<OrderDto> input)
        {
            var exps = WorkScope.GetAll<EmployeeWorkingExperiences>().Where(s => s.UserId == AbpSession.UserId);
            foreach (var item in input)
            {
                var exp = exps.FirstOrDefault(s => s.Id == item.Id);
                if (exp == default)
                {
                    continue;
                }
                exp.Order = item.Order;
                await WorkScope.UpdateAsync(exp);
            }
        }

        public async Task UpdatePersonalAttribute(PersonalAttributeDto input)
        {
            var user = await GetCurrentUserAsync();
            user.PersonalAttribute = JsonConvert.SerializeObject(input.PersonalAttributes);
            await WorkScope.UpdateAsync(user);
        }

        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task<PersonalAttributeDto> GetPersonalAttribute(long userId)
        {
            if ((AbpSession.UserId.Value != userId) && !await UserManager.IsGrantedAsync(AbpSession.UserId.Value, PermissionNames.Pages_View_CVEmployee))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
            }
            var user = await UserManager.GetUserByIdAsync(userId);
            if (string.IsNullOrWhiteSpace(user.PersonalAttribute))
            {
                return new PersonalAttributeDto {PersonalAttributes = new List<string>() };
            }
            return new PersonalAttributeDto
            {
                PersonalAttributes = JsonConvert.DeserializeObject<List<string>>(user.PersonalAttribute)
            };
        }

        [AbpAuthorize(Authorization.PermissionNames.Pages_MyProfile)]
        public async Task UpdateOrderEducation(List<OrderDto> input)
        {
            var lstEdu = WorkScope.GetAll<Educations>().Where(e => e.CvemployeeId == AbpSession.UserId);
            foreach (var item in input)
            {
                var edu = lstEdu.FirstOrDefault(s => s.Id == item.Id);
                if (AbpSession.UserId.Value != edu.CvemployeeId)
                {
                    throw new UserFriendlyException(ErrorCodes.Forbidden.AccessOtherProfile);
                }
                edu.Order = item.Order;
                await WorkScope.UpdateAsync(edu);
            }
        }
    }
}
