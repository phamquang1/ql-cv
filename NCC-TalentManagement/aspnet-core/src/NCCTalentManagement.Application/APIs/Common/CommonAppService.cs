using Abp.Application.Services.Dto;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using NCCTalentManagement.APIs.Candidate.Dto;
using NCCTalentManagement.APIs.Common.Dto;
using NCCTalentManagement.APIs.MyProfile.Dto;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.Constants.Enum;
using NCCTalentManagement.Entities;
using NCCTalentManagement.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCCTalentManagement.APIs.Common
{
    [AbpAuthorize]
    public class CommonAppService : NCCTalentManagementAppServiceBase
    {
        public async Task<List<PositionTypeDto>> GetAllPositionType()
        {
            var lstPos = new List<PositionTypeDto>();
            foreach (var position in await WorkScope.GetAll<PositionType, long>().ToListAsync())
            {
                var pos = new PositionTypeDto
                {
                    Id = position.Id,
                    Name = position.Name
                };
                lstPos.Add(pos);
            };
            return lstPos;
        }

        public async Task<List<BranchDto>> GetAllBranch()
        {
            var lstBranch = new List<BranchDto>();
            foreach (var branch in await WorkScope.GetAll<Branch, long>().ToListAsync())
            {
                var bra = new BranchDto
                {
                    Id = branch.Id,
                    Name = branch.Name
                };
                lstBranch.Add(bra);
            };
            return lstBranch;
        }

        public async Task<List<GroupSkillDto>> GetComboboxGroupSkill()
        {
            return await WorkScope.GetAll<GroupSkills>()
                                       .Select(g => new GroupSkillDto
                                       {
                                           Id = g.Id,
                                           Name = g.Name
                                       })
                                       .ToListAsync();
        }

        public async Task<List<SkillDto>> GetCBBSkillByGroupSkillId(long id)
        {
            return await WorkScope.GetAll<Skills>()
                                  .Where(s => s.GroupSkillId == id)
                                  .Select(s => new SkillDto
                                  {
                                      Id = s.Id,
                                      GroupSkillId = s.GroupSkillId,
                                      Name = s.Name
                                  }).ToListAsync();
        }

        public async Task<PagedResultDto<InterviewerDto>> GetCBBInterviewer(string search, int SkipCount = 0, int MaxResultCount = 10)
        {
            var checkSearchId = long.TryParse(search, out long id);
            var query = await WorkScope.GetAll<User>()
                                       .Where(u => (u.Surname.ToLower()
                                                             .Contains((search ?? "").Trim().ToLower()))
                                                || (u.Name.ToLower()
                                                          .Contains((search ?? "").Trim().ToLower()))
                                                || (checkSearchId && u.Id == id))
                                       .Select(u => new InterviewerDto
                                       {
                                           Id = u.Id,
                                           Name = u.FullName
                                       })
                                       .ToListAsync();
            var total = query.Count();
            var rs = query.Skip(SkipCount).Take(MaxResultCount).ToList();
            return new PagedResultDto<InterviewerDto>(total, rs);
        }

        public async Task<PagedResultDto<SkillCandidateDto>> GetCBBSkillForCandidate(string search, int SkipCount = 0, int MaxResultCount = 10)
        {
            var query = await WorkScope.GetAll<Skills>()
                                       .Where(s => s.Name.ToLower().Contains((search ?? "").Trim().ToLower()))
                                       .Select(s => new SkillCandidateDto
                                       {
                                           Id = s.Id,
                                           Name = s.Name,
                                           GroupSkillId = s.GroupSkillId
                                       })
                                       .ToListAsync();
            var total = query.Count;
            var rs = query.Skip(SkipCount).Take(MaxResultCount).ToList();
            return new PagedResultDto<SkillCandidateDto>(total, rs);
        }

        public async Task<PagedResultDto<InterviewerDto>> GetCBBPresenter(string search, int SkipCount = 0, int MaxResultCount = 10)
        {
            var checkSearchId = long.TryParse(search, out long id);
            var query = await WorkScope.GetAll<User>()
                                       .Where(u => (u.Surname.ToLower()
                                                             .Contains((search ?? "").Trim().ToLower()))
                                                || (u.Name.ToLower()
                                                          .Contains((search ?? "").Trim().ToLower()))
                                                || (checkSearchId && u.Id == id))
                                       .Select(u => new InterviewerDto
                                       {
                                           Id = u.Id,
                                           Name = u.FullName
                                       })
                                       .ToListAsync();
            var total = query.Count();
            var rs = query.Skip(SkipCount).Take(MaxResultCount).ToList();
            return new PagedResultDto<InterviewerDto>(total, rs);
        }

        public async Task<PagedResultDto<OldCandidateDto>> GetCBBOldCVId(string search, int SkipCount = 0, int MaxResultCount = 10)
        {
            var query = await WorkScope.GetAll<CVCandidates>()
                                       .Where(c => c.FullName.ToLower().Contains((search ?? "").Trim().ToLower())
                                                || c.Email.ToLower().Contains((search ?? "").Trim().ToLower()))
                                       .Select(c => new OldCandidateDto
                                       {
                                           Id = c.Id,
                                           FullName = c.FullName,
                                           Email = c.Email
                                       })
                                       .ToListAsync();
            var total = query.Count();
            var rs = query.Skip(SkipCount).Take(MaxResultCount).ToList();
            return new PagedResultDto<OldCandidateDto>(total, rs);
        }

        public async Task<object> GetCBBStatusCandidate()
        {
            var dicVNStatus = new Dictionary<CandidateStatusEnum, string>
            {
                { CandidateStatusEnum.RefuseAcceptJob,"Từ chối nhận việc"},
                { CandidateStatusEnum.Pass,"Qua"},
                { CandidateStatusEnum.Fail,"Không qua"},
                { CandidateStatusEnum.RefuseInterview,"Từ chối phỏng vấn"},
                { CandidateStatusEnum.MissedInterviewAppointment,"Lỡ hẹn phỏng vấn"},
                { CandidateStatusEnum.ResetInterviewSchedule,"Thiết lập lại lịch phỏng vấn"},
                { CandidateStatusEnum.HadJob,"Đã có việc"},
                { CandidateStatusEnum.SaveCV,"Lưu CV"},
                { CandidateStatusEnum.CouldNotContact,"Không thể liên lạc"},
                { CandidateStatusEnum.OfferNotMatch,"Không hợp Offer"},
                { CandidateStatusEnum.ReadyInterview,"Sẵn sàng phỏng vấn"}
            };

            return Enum.GetValues(typeof(CandidateStatusEnum))
                                         .Cast<CandidateStatusEnum>()
                                         .Select(s => new
                                         {
                                             Key = (int)s,
                                             Value = s.ToString(),
                                             ValueVN = dicVNStatus.GetValueOrDefault(s)
                                         }).ToList();
        }
    }
}
