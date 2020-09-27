using System.ComponentModel.DataAnnotations;

namespace NCCTalentManagement.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}