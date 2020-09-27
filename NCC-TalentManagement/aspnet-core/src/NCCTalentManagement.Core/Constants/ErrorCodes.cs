using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NCCTalentManagement.Constants
{
    public static class ErrorCodes
    {
        public static class NotFound
        {
            public const string UserLogin = "4040001";
            public const string UserNotExist = "4040002";
            public const string Candidate = "4040003";
            public const string WorkingExperience = "4040004";
            public const string FileUpload = "4040005";
        }
        public static class Forbidden
        {
            public const string AccessOtherProfile = "4030001";
            public const string EditOtherProfile = "4030002";
            public const string AddOrEditCandidate = "4030003";
            public const string DeleteAnotherFiles = "4030004";

        }
        public static class Unauthorized
        {
            public const string UserNotLogin = "4010001";
        }
        public static class Duplicated
        {

        }
        public static class NoContent
        {
            public const string Candidate = "2040001";
        }

        public static class NotAcceptable
        {
            public const string FileNameExtensions = "4060001";
            public const string YearIsNotValid = "406002";
            public const string YearOutOfRange = "406003";
        }
    }
}
