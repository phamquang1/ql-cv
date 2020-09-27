using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NCCTalentManagement.APIs.MyCV;
using NCCTalentManagement.APIs.MyProfile.Dto;
using NCCTalentManagement.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;
using NCCTalentManagement.Constants;
using NCCTalentManagement.Extensions;
using Xceed.Document.NET;
using System.Drawing;
using System.Linq;
using NCCTalentManagement.APIs.MyCV.Dto;
using NCCTalentManagement.Constants.Enum;
using DevExpress.XtraRichEdit;

namespace NCCTalentManagement.APIs.Export
{
    public class ExportFakeForSaleService : NCCTalentManagementAppServiceBase
    {
        private readonly ExportDocService _exportDocService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ExportFakeForSaleService(ExportDocService exportDocService, IHostingEnvironment hostingEnvironment)
        {
            _exportDocService = exportDocService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<string> ExportCVFake(MyProfileDto myProfileDto)
        {
            var fileName = $"CV_{myProfileDto.EmployeeInfo.Surname}{myProfileDto.EmployeeInfo.Name}_{myProfileDto.EmployeeInfo.UserId}";
            var path = _exportDocService.SetLinkForSaveFile(fileName);
            var doc = DocX.Create(path);
            doc.SetDefaultFont(new Xceed.Document.NET.Font("Times new roman"));
            CreateHeaderAndFooter(doc);

            Formatting formatting = new Formatting();
            formatting.Bold = true;
            formatting.Size = 14;

            doc.InsertParagraph("Professional Resume", false, formatting).SpacingBefore(5).Alignment = Alignment.right;
            doc.InsertParagraph($"Last updated: {DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-US"))}").FontSize(12).SpacingBefore(5).Alignment = Alignment.right;

            CreateContactDetails(doc, myProfileDto.EmployeeInfo);
            doc.InsertParagraph().SpacingAfter(20);
            await CreateEducationBackground(doc, myProfileDto.EducationBackGround, myProfileDto.isHiddenYear);
            doc.InsertParagraph().SpacingAfter(20);
            await CreateTechnicalExpertises(doc, myProfileDto.TechnicalExpertises);
            doc.InsertParagraph().SpacingAfter(20);
            await CreatePersonalAtributes(doc, myProfileDto.PersonalAttributes);
            doc.InsertParagraph().SpacingAfter(20);
            await CreateWorkingExperiences(doc, myProfileDto.WorkingExperiences);

            doc.Save();

            var fileDownloadName = _exportDocService.GetFileDownLoadType(path, fileName, myProfileDto.typeOffile);
            return fileDownloadName;
        }
        private void CreateHeaderAndFooter(DocX doc)
        {
            var pngPath = Path.Combine(_hostingEnvironment.WebRootPath, "nccsoftlogo.png");
            string titleHeader = "NCCPLUS VIET NAM JSC";

            doc.AddHeaders();
            Header h = doc.Headers.Odd;
            Xceed.Document.NET.Image image = doc.AddImage(pngPath);
            Xceed.Document.NET.Picture p = image.CreatePicture(29, 72);

            h.InsertParagraph(titleHeader).Alignment = Alignment.left;
            h.InsertParagraph().AppendPicture(p).Alignment = Alignment.right;
            h.InsertParagraph();

            doc.AddFooters();
            Footer footer = doc.Footers.Odd;
            footer.InsertParagraph().Append("Page ").AppendPageNumber(PageNumberFormat.normal).Append(" of  ").AppendPageCount(PageNumberFormat.normal).Alignment = Alignment.center;

        }
        private void CreateContactDetails(DocX doc, UserGeneralInfoDto userInfor)
        {
            string Headingtitle = "CONTACT DETAILS";
            var p = doc.InsertParagraph(Headingtitle);
            p.StyleName = "Heading1";
            p.FontSize(12);
            p.Font("Times new roman");
            p.SpacingBefore(5);



            Formatting symbolFormatting = new Formatting();
            symbolFormatting.FontFamily = new Xceed.Document.NET.Font("Symbol");
            symbolFormatting.Size = 10;

            Formatting textFormatting = new Formatting();
            textFormatting.Size = 12;

            char tick = (char)183;
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Name:", textFormatting).Bold().Append($"{GetSpacingString(13)}{userInfor.Name} {userInfor.Surname}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Address:", textFormatting).Bold().Append($"{GetSpacingString(9)}{userInfor.Address}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Mobile:", textFormatting).Bold().Append($"{GetSpacingString(11)}{userInfor.PhoneNumber}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Email:", textFormatting).Bold().Append($"{GetSpacingString(13)}{userInfor.EmailAddressInCV}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
        }
        private async Task CreateEducationBackground(DocX doc, IEnumerable<EducationDto> educationBg, bool isHiddenYear)
        {
            string Headingtitle = "EDUCATION BACKGROUND";
            var p = doc.InsertParagraph(Headingtitle);
            p.StyleName = "Heading1";
            p.FontSize(12);
            p.Font("Times new roman");
            p.SpacingBefore(5);
            Formatting symbolFormatting = new Formatting();
            symbolFormatting.FontFamily = new Xceed.Document.NET.Font("Symbol");
            symbolFormatting.Size = 10;

            char tick = (char)183;
            if (isHiddenYear == false)
            {
                foreach (var item in educationBg)
                {
                    doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append($"{item.StartYear} - {item.EndYear}").Bold().Append($"{GetSpacingString(6)}School:").FontSize(12).Bold().Append($" {item.SchoolOrCenterName}").FontSize(12).SpacingBefore(5).IndentationBefore = 20;
                    doc.InsertParagraph().Append("Degree:").FontSize(12).Bold().Append($" {item.DegreeType}").FontSize(12).SpacingBefore(5).IndentationBefore = 110;
                    doc.InsertParagraph().Append("Field:").FontSize(12).Bold().Append($" {item.Major}").FontSize(12).SpacingBefore(5).IndentationBefore = 110;
                }
            }
            else
            {
                foreach (var item in educationBg)
                {
                    doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("School:").FontSize(12).Bold().Append($" {item.SchoolOrCenterName}").FontSize(12).SpacingBefore(5).IndentationBefore = 20;
                    doc.InsertParagraph().Append("Degree:").FontSize(12).Bold().Append($" {item.DegreeType}").FontSize(12).SpacingBefore(5).IndentationBefore = 40;
                    doc.InsertParagraph().Append("Field: ").FontSize(12).Bold().Append($" {item.Major}").FontSize(12).SpacingBefore(5).IndentationBefore = 40;
                }
            }
        }
        private async Task CreateTechnicalExpertises(DocX doc, TechnicalExpertiseDto technicalExpertises)
        {
            string Headingtitle = "TECHNICAL EXPERTISES";
            var p = doc.InsertParagraph(Headingtitle);
            p.StyleName = "Heading1";
            p.FontSize(12);
            p.Font("Times new roman");
            p.SpacingBefore(5);

            Formatting symbolFormatting = new Formatting();
            symbolFormatting.FontFamily = new Xceed.Document.NET.Font("Symbol");
            symbolFormatting.Size = 10;

            Formatting courierFormatting = new Formatting();
            courierFormatting.FontFamily = new Xceed.Document.NET.Font("Courier New");
            courierFormatting.Size = 10;

            char tickParent = (char)183;
            char tickChild = '\u25CB';
            foreach (var grpSkill in technicalExpertises.GroupSkills)
            {
                doc.InsertParagraph().Append(tickParent.ToString(), symbolFormatting).Spacing(15).Append($"{grpSkill.Name}").FontSize(12).Bold().SpacingBefore(5).IndentationBefore = 20;
                foreach (var cvSkill in grpSkill.CVSkills)
                {
                    doc.InsertParagraph().Append(tickChild.ToString(), courierFormatting).Spacing(15).Append($"{cvSkill.SkillName}").FontSize(12).SpacingBefore(5).IndentationBefore = 40;
                }
            }
        }
        private async Task CreatePersonalAtributes(DocX doc, PersonalAttributeDto personalAtributes)
        {
            string Headingtitle = "PERSONAL ATTRIBUTES";

            var p = doc.InsertParagraph(Headingtitle);
            p.StyleName = "Heading1";
            p.FontSize(12);
            p.Font("Times new roman");
            p.SpacingBefore(5);

            Formatting symbolFormatting = new Formatting();
            symbolFormatting.FontFamily = new Xceed.Document.NET.Font("Symbol");
            symbolFormatting.Size = 10;

            char tick = (char)183;
            foreach (var atribute in personalAtributes.PersonalAttributes)
            {
                doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append($"{atribute}").FontSize(12).SpacingBefore(5).IndentationBefore = 20;
            }
        }
        private async Task CreateWorkingExperiences(DocX doc, IEnumerable<WorkingExperienceDto> workingExperiences)
        {
            int stt = 1;
            string Headingtitle = "WORKING EXPERIENCES";

            var p = doc.InsertParagraph(Headingtitle);
            p.StyleName = "Heading1";
            p.FontSize(12);
            p.Font("Times new roman");
            foreach (var item in workingExperiences)
            {
                var projectName = doc.InsertParagraph();
                projectName.StyleName = "Heading3";
                projectName.Append($"{stt.ConvertIntToRoman()}.{GetSpacingString(5)}{item.ProjectName}");
                projectName.Bold(false);
                projectName.FontSize(11);
                projectName.Font("Times new roman");
                projectName.SpacingAfter(5);

                // Check time in current project 
                var endTime = item.EndTime != null ? item.EndTime.Value.ToString("MMMM yyyy", new System.Globalization.CultureInfo("en-US")) : "Now";

                Xceed.Document.NET.Table table = doc.AddTable(5, 2);
                table.SetColumnWidth(0, 130d);
                table.SetColumnWidth(1, 325d);

                table.Rows[0].Cells[0].Paragraphs.First().Append("Duration").FontSize(12).Bold();
                table.Rows[1].Cells[0].Paragraphs.First().Append("Position").FontSize(12).Bold();
                table.Rows[2].Cells[0].Paragraphs.First().Append("Project Description").FontSize(12).Bold();
                table.Rows[3].Cells[0].Paragraphs.First().Append("My responsibilities").FontSize(12).Bold();
                table.Rows[4].Cells[0].Paragraphs.First().Append("Technologies").FontSize(12).Bold();

                table.Rows[0].Cells[1].Paragraphs.First().Append($"{item.StartTime.Value.ToString("MMMM yyyy", new System.Globalization.CultureInfo("en-US"))} - {endTime}").FontSize(12);
                table.Rows[1].Cells[1].Paragraphs.First().Culture(new System.Globalization.CultureInfo("en-US")).Append($"{item.Position}").FontSize(12);
                table.Rows[2].Cells[1].Paragraphs.First().Culture(new System.Globalization.CultureInfo("en-US")).Append($"{item.ProjectDescription}").FontSize(12);
                table.Rows[3].Cells[1].Paragraphs.First().Culture(new System.Globalization.CultureInfo("en-US")).Append($"{item.Responsibility}").FontSize(12);
                table.Rows[4].Cells[1].Paragraphs.First().Culture(new System.Globalization.CultureInfo("en-US")).Append($"{item.Technologies}").FontSize(12);
                projectName.InsertTableAfterSelf(table);
                stt++;
            }
        }

    }
}
