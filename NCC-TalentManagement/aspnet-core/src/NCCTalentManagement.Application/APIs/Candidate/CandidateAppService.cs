using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCCTalentManagement.APIs.Candidate.Dto;
using NCCTalentManagement.APIs.Common.Dto;
using NCCTalentManagement.Authorization;
using NCCTalentManagement.Authorization.Roles;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.Constants;
using NCCTalentManagement.Constants.Enum;
using NCCTalentManagement.Entities;
using NCCTalentManagement.Extensions;
using NCCTalentManagement.IoC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NCCTalentManagement.APIs.Candidate
{
    [AbpAuthorize]
    public class CandidateAppService : NCCTalentManagementAppServiceBase
    {
        private IHostingEnvironment _hostingEnvironment;
        public CandidateAppService(IHostingEnvironment environment)
        {
            this._hostingEnvironment = environment;
        }

        [AbpAuthorize(PermissionNames.Pages_CVCandidate)]
        public async Task<CandidateDetailDto> GetCandidateInfoById(long candidateId)
        {
            var candidate = await WorkScope.GetAll<CVCandidates>()
                                           .Include(c => c.Branch)
                                           .Include(c => c.Cvattachments)
                                           .Include(c => c.Cvskills)
                                           .Include(c => c.InterviewCandidates)
                                           .Include(c => c.OldCv)
                                           .Include(c => c.Position)
                                           .Include(c => c.Presenter)
                                           .FirstOrDefaultAsync(c => c.Id == candidateId);
            var skills = await WorkScope.GetAll<Skills>().ToListAsync();

            var lstInter = new List<InterviewCandidateDto>();
            if (candidate.InterviewCandidates != null)
            {
                foreach (var i in candidate.InterviewCandidates)
                {
                    var inter = WorkScope.Get<User>(i.InterviewerId);
                    lstInter.Add(new InterviewCandidateDto
                    {
                        Id = i.Id,
                        CvcandidateId = i.CvcandidateId,
                        InterviewerId = i.InterviewerId,
                        Name = inter.FullName
                    });
                }
            }

            if (candidate == default)
            {
                throw new UserFriendlyException(ErrorCodes.NotFound.Candidate);
            }

            return new CandidateDetailDto
            {
                Id = candidate.Id,
                Attachments = candidate.Cvattachments.Select(a => a.Path).ToList(),
                Branch = new BranchDto
                {
                    Id = candidate.Branch.Id,
                    Name = candidate.Branch.Name
                },
                CVSkills = candidate.Cvskills.Select(cv => new CVCandidateSkillDto
                {
                    Id = cv.Id,
                    CVCandidateId = cv.CvcandidateId,
                    GroupSkillId = cv.GroupSkillId,
                    Level = cv.Level,
                    SkillId = cv.SkillId,
                    SkillName = cv.SkillId.HasValue ? skills.FirstOrDefault(s => s.Id == cv.SkillId)?.Name : cv.SkillName
                }).ToList(),
                DegreeType = candidate.DegreeType,
                Email = candidate.Email,
                FullName = candidate.FullName,
                InterviewCandidates = lstInter,
                InterviewTime = candidate.InterviewTime,
                OldCVId = candidate.OldCvid,
                Phone = candidate.Phone,
                Position = new PositionDto { Id = candidate.Position.Id, Name = candidate.Position.Name },
                ReceiveTime = candidate.ReceiveTime,
                Source = candidate.Source,
                StartWorkingTime = candidate.StartWorkingTime,
                Status = candidate.Status,
                CreationTime = candidate.CreationTime,
                AttachmentPath = candidate.AttachmentPatch,
                Presenter = candidate.PresenterId == null ? null : new PresenterDto { Id = candidate.Presenter.Id, Name = candidate.Presenter.FullName },
                WorkExperience = candidate.WorkExperience
            };
        }

        [AbpAuthorize(PermissionNames.Pages_CVCandidate)]
        public async Task<PagedResultDto<CandidateBaseDto>> GetAllCandidatePagingAsync(FilterDto input)
        {
            var isParseLong = long.TryParse(input.Search, out long id);
            var skills = await WorkScope.GetAll<Skills>().ToListAsync();
            var checkBranch = await WorkScope.GetAll<Branch>().AnyAsync(b => b.Id == input.BranchId);

            var checkMonth = int.TryParse(input.MonthReceived, out int month);
            if (checkMonth && (month < 1 || month > 12))
            {
                checkMonth = false;
            }

            var checkYear = int.TryParse(input.YearReceived, out int year);
            if (checkYear && (year < 1970 || year > DateTime.Now.Year))
            {
                checkYear = false;
            }

            List<long> skillIdFilter = new List<long>();
            if (!string.IsNullOrWhiteSpace(input.Skill))
            {
                skillIdFilter = skills.Where(s => s.Name.ToLower().Contains(input.Skill.Trim().ToLower()))
                                      .Select(s => s.Id)
                                      .ToList();
            }

            var query = WorkScope.GetAll<CVCandidates>()
                                      .Include(c => c.Branch)
                                      .Include(c => c.Cvattachments)
                                      .Include(c => c.Cvskills)
                                      .Include(c => c.InterviewCandidates)
                                      .Include(c => c.OldCv)
                                      .Include(c => c.Position)
                                      .WhereIf(isParseLong, c => c.Id == id)
                                      .WhereIf(checkBranch, c => c.BranchId == input.BranchId)
                                      .WhereIf(input.Status.HasValue, c => c.Status == input.Status)
                                      .WhereIf(checkMonth, c => c.ReceiveTime.Month == month)
                                      .WhereIf(checkYear, c => c.ReceiveTime.Year == year)
                                      .WhereIf(!string.IsNullOrWhiteSpace(input.Search), c => c.FullName.ToLower().Contains(input.Search.Trim().ToLower()))
                                      .WhereIf(!string.IsNullOrWhiteSpace(input.Skill) && skillIdFilter.Count <= 0, c => c.Cvskills.Any(s => s.SkillName.ToLower().Contains(input.Skill.Trim().ToLower())))
                                      .WhereIf(skillIdFilter.Count > 0, c => c.Cvskills.Any(s => s.SkillId.HasValue && skillIdFilter.Contains(s.SkillId.Value)));

            //    if (!query.Any())
            //    {
            //        throw new UserFriendlyException(ErrorCodes.NoContent.Candidate);
            //    }

            var result = query.Select(c => new CandidateBaseDto
            {
                Id = c.Id,
                BranchName = c.Branch.Name,
                Email = c.Email,
                FullName = c.FullName,
                Phone = c.Phone,
                ApplyPosition = c.Position.Name,
                Status = c.Status,
                CreationTime = c.CreationTime
            });

            var totalCount = result.Count();
            var rs = result.OrderByDescending(c => c.Id).ThenBy(c => c.FullName).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<CandidateBaseDto>(totalCount, rs);
        }

        [HttpPost]
        public async Task<FileDto> UploadAttachments([FromForm]IList<IFormFile> files, [FromForm] TypeUploadFileCandidate typeUpload)
        {
            var currentUser = await GetCurrentUserAsync();
            if (!(await UserManager.IsInRoleAsync(currentUser, StaticRoleNames.Host.HR) || await UserManager.IsInRoleAsync(currentUser, StaticRoleNames.Host.Admin)))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AddOrEditCandidate);
            }

            string folderName = "attachmentscandidate";
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, folderName);

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var typeAccept = Enum.GetValues(typeof(AttachmentTypeEnum))
                                         .Cast<AttachmentTypeEnum>()
                                         .Select(v => v.ToString())
                                         .ToList();

            List<string> paths = new List<string>();


            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var timestamp = (DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    var fileExtension = Path.GetExtension(file.FileName);
                    if (!typeAccept.Any(t => ("." + t).ToLower().Equals(fileExtension.ToLower())))
                    {
                        throw new UserFriendlyException(ErrorCodes.NotAcceptable.FileNameExtensions);
                    }
                    var fileName = string.Format("{0}_{1}{2}", timestamp.ToString().Replace(".", ""), typeUpload.ToString(), fileExtension);
                    var filePath = Path.Combine(uploads, fileName);
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                    paths.Add(string.Format(@"{0}/{1}", folderName, fileName));
                    if (typeUpload == TypeUploadFileCandidate.Document)
                    {
                        break;
                    }
                }
                else
                {
                    throw new UserFriendlyException(ErrorCodes.NotFound.FileUpload);
                }
            }

            return new FileDto { TypeFile = typeUpload, Paths = paths };
        }

        [HttpPost]
        public async Task CancelUploadFile(FileDto input)
        {
            var pathRoot = _hostingEnvironment.WebRootPath;

            var currentUser = await GetCurrentUserAsync();
            if (!(await UserManager.IsInRoleAsync(currentUser, StaticRoleNames.Host.HR) || await UserManager.IsInRoleAsync(currentUser, StaticRoleNames.Host.Admin)))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.DeleteAnotherFiles);
            }

            foreach (var item in input.Paths)
            {
                var file = Path.Combine(pathRoot, item.Replace("/", @"\"));
                if (File.Exists(file) && Path.GetFileNameWithoutExtension(file).Contains(string.Format("_{0}.", input.TypeFile.ToString())))
                {
                    File.Delete(file);
                }
                else
                {
                    continue;
                }
            }
        }

        [AbpAuthorize(PermissionNames.Pages_CVCandidate)]
        public async Task InsertOrUpdateCandidate(CandidateDto input)
        {
            var currentUser = await GetCurrentUserAsync();
            if (!(await UserManager.IsInRoleAsync(currentUser, StaticRoleNames.Host.HR) || await UserManager.IsInRoleAsync(currentUser, StaticRoleNames.Host.Admin)))
            {
                throw new UserFriendlyException(ErrorCodes.Forbidden.AddOrEditCandidate);
            }

            if (input.Id != 0)
            {
                var interviewers = WorkScope.GetAll<InterviewCandidates>().Where(c => c.CvcandidateId == input.Id.Value);
                foreach (var inter in interviewers)
                {
                    await WorkScope.GetRepo<InterviewCandidates, long>().DeleteAsync(inter);
                }

                var skills = WorkScope.GetAll<CVSkills>().Where(c => c.CvcandidateId == input.Id.Value);
                foreach (var skill in skills)
                {
                    await WorkScope.GetRepo<CVSkills, long>().DeleteAsync(skill);
                }
            }

            var candidateId = await WorkScope.InsertOrUpdateAndGetIdAsync(ObjectMapper.Map<CVCandidates>(input));

            if (input.Attachments != null && input.Attachments.Count > 0)
            {
                await InsertOrUpdateAttachments(input.Attachments, candidateId);
            }
        }

        private void InsertOrUpdateInterviewerCandidate(List<InterviewCandidateDto> interviewCandidates)
        {
            if (interviewCandidates != null && interviewCandidates.Count > 0)
            {
                foreach (var item in interviewCandidates)
                {
                    WorkScope.InsertOrUpdateAndGetId(ObjectMapper.Map<InterviewCandidates>(item));
                }
            }
        }

        private void InsertOrUpdateCVSkill(List<CVCandidateSkillDto> cvSkills)
        {
            if (cvSkills != null && cvSkills.Count > 0)
            {
                foreach (var item in cvSkills)
                {
                    WorkScope.InsertOrUpdateAndGetId(ObjectMapper.Map<CVSkills>(item));
                }
            }
        }

        private async Task InsertOrUpdateAttachments(List<string> attachments, long candidateId)
        {
            if (attachments != null && attachments.Count > 0)
            {
                var typeAccept = Enum.GetValues(typeof(AttachmentTypeEnum))
                                         .Cast<AttachmentTypeEnum>()
                                         .Select(v => v.ToString())
                                         .ToList();

                List<long> attachmentInserts = new List<long>();

                foreach (var item in attachments)
                {
                    var fileExtension = Path.GetExtension(item);

                    if (fileExtension.ToLower() != ".png" && fileExtension.ToLower() != ".jpg")
                    {
                        throw new UserFriendlyException(ErrorCodes.NotAcceptable.FileNameExtensions);
                    }

                    attachmentInserts.Add(await WorkScope.InsertOrUpdateAndGetIdAsync(new CVAttachments
                    {
                        Path = item,
                        Type = (AttachmentTypeEnum)Enum.Parse(typeof(AttachmentTypeEnum), fileExtension.Replace(".", "").ToUpper()),
                        CvcandidateId = candidateId
                    }));
                }

                await WorkScope.DeleteAsync<CVAttachments>(at => at.CvcandidateId == candidateId && !attachmentInserts.Contains(at.Id));
            }
        }

        [AbpAuthorize(PermissionNames.Pages_CVCandidate)]
        public async Task DeleteCandidate(long candidateId)
        {
            var skills = WorkScope.GetAll<CVSkills>().Where(c => c.CvcandidateId == candidateId);
            foreach (var skill in skills)
            {
                await WorkScope.GetRepo<CVSkills, long>().DeleteAsync(skill);
            }

            var candidate = await WorkScope.GetAll<CVCandidates>().FirstOrDefaultAsync(c => c.Id == candidateId);

            if (candidate == default)
            {
                throw new UserFriendlyException(ErrorCodes.NotFound.Candidate);
            }

            var pathRoot = _hostingEnvironment.WebRootPath;
            if (candidate.AttachmentPatch != null)
            {

                var docPath = Path.Combine(pathRoot, candidate.AttachmentPatch.Replace("/", @"\"));
                if (File.Exists(docPath))
                {
                    File.Delete(docPath);
                }
            }

            await WorkScope.Repository<CVCandidates>().DeleteAsync(candidate);

            var attachments = WorkScope.GetAll<CVAttachments>().Where(c => c.CvcandidateId == candidateId);
            foreach (var att in attachments)
            {
                await WorkScope.GetRepo<CVAttachments, long>().DeleteAsync(att);
            }

            var interviewers = WorkScope.GetAll<InterviewCandidates>().Where(c => c.CvcandidateId == candidateId);
            foreach (var inter in interviewers)
            {
                await WorkScope.GetRepo<InterviewCandidates, long>().DeleteAsync(inter);
            }

            foreach (var att in attachments)
            {
                var file = Path.Combine(pathRoot, att.Path.Replace("/", @"\"));
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
