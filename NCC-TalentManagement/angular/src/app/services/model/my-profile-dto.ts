export class InformDto {
   name: string;
  surname: string;
  phoneNumber: string;
  emailAddressInCV: string;
  imgPath: any;
  currentPosition: any;
  userId: any;
  address: string;
  branch: any;
}

export class EducationDto {
  cvcandidateId: number;
  cvemployeeId: number;
  schoolOrCenterName: string;
  degreeType: any;
  major: string;
  startYear: Date;
  endYear: Date;
  description: string;
  order: number;
  id: number;
}

export class WorkingExperienceDto {
  id: number;
  order: number;
  projectName: string;
  position: string;
  projectDescription: string;
  responsibility: string;
  startTime: Date;
  endTime: Date;
  userId: number;
  technologies : string;
}
export class IObjectFile {
  path: string;
  file: File;
  buffer: any;
}

export class EducationOderDto{
  id: number;
  order: number;
}

export class TechnicalExpertise {
  userId: number;
  groupSkills: GroupTechnical[];
}
export class GroupTechnical {
  cvSkills: CvSkill[];
  groupSkillId: any;
  name: string;
}
export class CvSkill{
  id: number;
  level: number;
  order: number;
  skillId: number;
  skillName: string;
}