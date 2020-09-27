using System;

namespace NCCTalentManagement.APIs.MyProfile.Dto
{
    public class WorkingExperienceDto
    {
        public long? Id { get; set; }
        public string ProjectName { get; set; }
        public string Position { get; set; }
        public string ProjectDescription { get; set; }
        public string Responsibility { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long UserId { get; set; }
        public int? Order { get; set; }
        public string Technologies { get; set; }
    }
}
