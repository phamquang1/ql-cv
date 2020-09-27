using Abp.Application.Services.Dto;
using NCCTalentManagement.APIs.Employee.Dto;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.Entities;
using NCCTalentManagement.IoC;
using NCCTalentManagement.Paging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;
using Abp.Collections.Extensions;
using NCCTalentManagement.APIs.MyProfile.Dto;

namespace NCCTalentManagement.APIs.Employee
{
    public class EmployeeAppService : NCCTalentManagementAppServiceBase
    {
        [HttpPost]
        [AbpAuthorize(Authorization.PermissionNames.Pages_View_CVEmployee)]
        public async Task<PagedResultDto<EmployeeDto>> GetAllEmployeePaging(GetEmployeeParam param)
        {
            var query =  (from u in WorkScope.GetAll<User>().Where(u => !u.IsDeleted).ToList()
                         join b in WorkScope.GetAll<Branch>().Where(b => !b.IsDeleted).ToList() on u.BranchId equals b.Id
                         join p in WorkScope.GetAll<PositionType>().Where(p => !p.IsDeleted).ToList() on u.PositionId equals p.Id
                         select new EmployeeDto
                         {
                             UserId = u.Id,
                             Name = u.FullName,
                             BranchId = u.BranchId,
                             PositionId = u.PositionId
                         })
                        .WhereIf(!param.Name.IsNullOrEmpty(), u => u.Name.Contains(param.Name, StringComparison.OrdinalIgnoreCase))
                        .WhereIf(param.BranchId.HasValue, u => u.BranchId == param.BranchId)
                        .WhereIf(param.PositionId.HasValue, u => u.PositionId == param.PositionId);
            var totalCount = query.Count();
            var result = query.OrderBy(p => p.Name).Skip(param.SkipCount).Take(param.MaxResultCount).ToList(); 
            return new PagedResultDto<EmployeeDto>(totalCount, result);
        }

        public async Task<List<WorkingExperienceDto>> GetWorkingExperiencePaging(string technologies)
        {
            if(technologies.IsNullOrEmpty())
            {
                return new List<WorkingExperienceDto>();
            }

            var query = WorkScope.GetAll<EmployeeWorkingExperiences>().Where(u => !u.IsDeleted).Select(
                         w => new WorkingExperienceDto
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
                         }).Where(u => u.Technologies.ToLower().Trim().Contains(technologies.ToLower().Trim()))
                         .ToList();
            
            return query;
        }
    }
}
