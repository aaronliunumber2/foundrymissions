using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionExportHelper
    {
        private const string ExportKlingon = "Allegiance_Klingon";
        private const string ExportFederation = "Allegiance_Starfleet";

        private const string ExportFileName = "export.txt";
        private const string ExportZipName = "export.zip";

        public static Mission ParseExportToMission(ApplicationDbContext context, string exportText)
        {
            //this gets a foundry export mission type, need to convert it to our mission
            var exportmission = StarbaseUGC.Foundry.Engine.Serializers.FoundryMissionSerializer.ParseMissionText(exportText);
            var mission = new Mission();

            mission.AuthorUserId = exportmission.Project.AccountName; //we don't actually use this field but can use it to find user match
            mission.Description = FormatDescription(exportmission.Project.Description);
            mission.Faction = GetFactionFromExportFaction(exportmission.Project.RestrictionProperties.Faction);
            mission.MinimumLevel = string.IsNullOrWhiteSpace(exportmission.Project.RestrictionProperties.MinLevel) ? 1 : Convert.ToInt32(exportmission.Project.RestrictionProperties.MinLevel);
            //mission.MissionExportText = exportText;
            mission.Name = GetMissionName(exportmission.Project.PublicName);

            //try and match the userid to an actual user, if it exists use that
            var user = context.Users.Where(u => u.CrypticTag.Equals(exportmission.Project.AccountName)).FirstOrDefault();

            if (user != null)
            {
                mission.Author = user;
            }
            
            return mission;

        }

        private const string StartChars = "<&";
        private const string EndChars = "&>";
        private static string FormatDescription(string description)
        {
            /*formatting 
              1. gets rid of <& and &> from the beginnings
              2. gets rid of " from the beginning and ending
              3. replaces \n with new line
            */
            if (description.StartsWith(StartChars) && description.EndsWith(EndChars))
            {
                description = description.Substring(2, description.Length - 4);
            }
            else if (description.StartsWith("\"") && description.EndsWith("\""))
            {
                description = description.Substring(1, description.Length - 1);
            }

            description = description.Replace("\\n", Environment.NewLine);

            return description;
        }

        private static Random random = new Random();
        public static string GenerateRandomID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 9)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GetMissionName(string exportMissionName)
        {
            //remove the first and last character because they are "asdfasdr"
            var name = exportMissionName.Substring(1);
            name = name.Substring(0, name.Length - 1);
            return name;

        }

        private static Faction GetFactionFromExportFaction(string exportFaction)
        {
            switch(exportFaction)
            {
                case ExportFederation:
                    return Faction.Federation;
                case ExportKlingon:
                    return Faction.Klingon;
                default:
                    return Faction.Federation;

            }
        }

        public static bool HasExport(int missionId)
        {
            var path = GetExportFileFolder(missionId);

            return File.Exists(Path.Combine(path, ExportZipName));
        }

        public static string GetExportFileFolder(int missionId)
        {
            return Path.Combine(
                    HostingEnvironment.MapPath("~/Content/missions/"),
                    missionId.ToString(),
                    "exports");
        }

        public static void SaveExportFile(string exportData, int missionId)
        {
            var path = GetExportFileFolder(missionId);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                //empty the directory out and recreate it
                DeleteDirectory(path);
                Directory.CreateDirectory(path);
            }
            var filePath = Path.Combine(path, ExportFileName);
            var firstzipPath = Path.Combine(path, ExportZipName).Replace("exports", "");
            var finalzipPath = Path.Combine(path, ExportZipName);
            File.WriteAllText(filePath, exportData); //create the export text file in the exports folder
            //check if hte file exists, if it does get rid of it
            if (File.Exists(firstzipPath))
            {
                File.Delete(firstzipPath);
            }
            ZipFile.CreateFromDirectory(path, firstzipPath); //zip the exports folder
            //move the zip file to the exports folder
            //check if the final zip file path exists, if it does delete it
            if (File.Exists(finalzipPath))
            {
                File.Delete(finalzipPath);
            }
            File.Move(firstzipPath, finalzipPath);
            File.Delete(filePath);  //delete the export text file
        }

        public static string GetExportText(int missionId)
        {
            var path = GetExportFileFolder(missionId);
            var filePath = Path.Combine(path, ExportFileName);
            var zipPath = Path.Combine(path, ExportZipName);
            var exportText = string.Empty;

            //if the file already exists delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            ZipFile.ExtractToDirectory(zipPath, path);
            exportText = File.ReadAllText(filePath);
            File.Delete(filePath);

            return exportText;
        }

        public static void DeleteDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(path, false);
        }
    }
}