using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NCCTalentManagement.APIs.Candidate.Dto
{
    public class FilterDto
    {
        public string Search { get; set; }
        public string Skill { get; set; }
        public long? BranchId { get; set; }
        public CandidateStatusEnum? Status { get; set; }
        public string MonthReceived { get; set; }
        public string YearReceived { get; set; }
        [DefaultValue(10)]
        public int MaxResultCount { get; set; }
        [DefaultValue(0)]
        public int SkipCount { get; set; }
    }
}
