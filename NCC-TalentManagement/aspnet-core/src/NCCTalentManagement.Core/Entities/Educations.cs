using Abp.Domain.Entities.Auditing;
using NCCTalentManagement.Authorization.Users;
using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NCCTalentManagement.Entities
{
    public partial class Educations : FullAuditedEntity<long>
    {
        public long? CvemployeeId { get; set; }
        public string SchoolOrCenterName { get; set; }
        public DegreeTypeEnum DegreeType { get; set; }
        public string Major { get; set; }
        [StringLength(4)]
        public string StartYear { get; set; }
        [StringLength(4)]
        public string EndYear { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }

        public virtual User Cvemployee { get; set; }
    }
}
