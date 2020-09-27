using NCCTalentManagement.APIs.Common.Dto;
using NCCTalentManagement.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.APIs.Employee.Dto
{
    public class GetEmployeeParam
    {
        public string Name { get; set; }
        public int? PositionId { get; set; }
        public int? BranchId { get; set; }
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
    }
}
