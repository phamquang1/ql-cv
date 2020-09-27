using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.APIs.Candidate.Dto
{
    public class CandidateBaseDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BranchName { get; set; }
        public string ApplyPosition { get; set; }
        public CandidateStatusEnum Status { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
