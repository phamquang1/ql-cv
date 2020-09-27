using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Authorization.Users;
using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Entities
{
    public partial class EmployeeWorkingExperiences : FullAuditedEntity<long>
    {
        public long UserId { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Position { get; set; }
        public string ProjectDescription { get; set; }
        public string Responsibilities { get; set; }
        public string Technologies { get; set; }
        public int? Order { get; set; }
        public virtual User User { get; set; }
    }
}
