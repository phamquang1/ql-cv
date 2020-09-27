using NCCTalentManagement.Constants.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCCTalentManagement.APIs.Candidate.Dto
{
    public class FileDto
    {
        public TypeUploadFileCandidate TypeFile { get; set; }
        public List<string> Paths { get; set; }
    }
}
