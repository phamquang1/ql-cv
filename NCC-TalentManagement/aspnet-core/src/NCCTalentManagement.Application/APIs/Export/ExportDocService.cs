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
    public class ExportDocService : NCCTalentManagementAppServiceBase
    {
        private readonly MyProfileAppService _myProfileAppService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ExportDocService(MyProfileAppService myProfileAppService, IHostingEnvironment hostingEnvironment)
        {
            _myProfileAppService = myProfileAppService;
            _hostingEnvironment = hostingEnvironment;
        }
        public string SetLinkForSaveFile(string fileName)
        {
            var folder = RootConstant.Documents;
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, folder);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.GetFullPath(filePath);
            var path = Path.Combine(filePath, fileName + ".docx");
            return path;
        }
        public string GetFileDownLoadType(string path, string fileName, AttachmentTypeEnum typeOffile)
        {
            string fileDownloadName;
            var folder = RootConstant.Documents;
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, folder);
            var ServerRootAddress = RootConstant.ServerRootAddress;
            filePath = Path.GetFullPath(filePath);
            var filePDF = Path.Combine(filePath, fileName + ".pdf");
            RichEditDocumentServer server = new RichEditDocumentServer();
            server.LoadDocument(path);
            server.ExportToPdf(filePDF);
            if (typeOffile == AttachmentTypeEnum.DOCX)
            {
                fileDownloadName = $"{ServerRootAddress}{folder}{fileName}.docx";
            }
            else
            {
                fileDownloadName = $"{ServerRootAddress}{folder}{fileName}.pdf";
            }
            return fileDownloadName;
        }
        [HttpGet]
        public async Task<string> ExportCV(long userId, AttachmentTypeEnum typeOffile, bool isHiddenYear)
        {
            var userInfor = await _myProfileAppService.GetUserGeneralInfo(userId);
            var fileName = $"CV_{userInfor.Surname}{userInfor.Name}_{userId}";
            var path = SetLinkForSaveFile(fileName);
            var doc = DocX.Create(path);
            doc.SetDefaultFont(new Xceed.Document.NET.Font("Times new roman"));
            CreateHeaderAndFooter(doc);

            Formatting formatting = new Formatting();
            formatting.Bold = true;
            formatting.Size = 14;

            doc.InsertParagraph("Professional Resume", false, formatting).SpacingBefore(5).Alignment = Alignment.right;
            doc.InsertParagraph($"Last updated: {DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("en-US"))}").FontSize(12).SpacingBefore(5).Alignment = Alignment.right;

            CreateContactDetails(doc, userInfor);
            doc.InsertParagraph().SpacingAfter(20);
            await CreateEducationBackground(doc, userId, isHiddenYear);
            doc.InsertParagraph().SpacingAfter(20);
            await CreateTechnicalExpertises(doc, userId);
            doc.InsertParagraph().SpacingAfter(20);
            await CreatePersonalAtributes(doc, userId);
            doc.InsertParagraph().SpacingAfter(20);
            await CreateWorkingExperiences(doc, userId);

            doc.Save();

            var fileDownloadName = GetFileDownLoadType(path, fileName, typeOffile);
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
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Name:", textFormatting).Bold().Append($"{GetSpacingString(13)}{userInfor.Surname} {userInfor.Name}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Address:", textFormatting).Bold().Append($"{GetSpacingString(9)}{userInfor.Address}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Mobile:", textFormatting).Bold().Append($"{GetSpacingString(11)}{userInfor.PhoneNumber}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
            doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Append("Email:", textFormatting).Bold().Append($"{GetSpacingString(13)}{userInfor.EmailAddressInCV}", textFormatting).SpacingBefore(5).IndentationBefore = 20;
        }
        private async Task CreateEducationBackground(DocX doc, long userId, bool isHiddenYear)
        {
            var educationBg = await _myProfileAppService.GetEducationInfo(userId);
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
                    doc.InsertParagraph().Append(tick.ToString(), symbolFormatting).Spacing(15).Culture(new System.Globalization.CultureInfo("en-US")).Append($"{item.StartYear} - {item.EndYear}").Bold().Append($"{GetSpacingString(6)}School:").FontSize(12).Bold().Append($" {item.SchoolOrCenterName}").FontSize(12).SpacingBefore(5).IndentationBefore = 20;
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
        private async Task CreateTechnicalExpertises(DocX doc, long userId)
        {
            var technicalExpertises = await _myProfileAppService.GetTechnicalExpertise(userId);
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
        private async Task CreatePersonalAtributes(DocX doc, long userId)
        {
            var personalAtributes = await _myProfileAppService.GetPersonalAttribute(userId);
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
        private async Task CreateWorkingExperiences(DocX doc, long userId)
        {
            var workingExperiences = await _myProfileAppService.GetUserWorkingExperience(userId);
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
