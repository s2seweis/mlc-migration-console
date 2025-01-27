using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using XmlToSqlApp.Helpers;

namespace XmlToSqlApp
{
    public class FileHelper
    {
        private static int counter = 0;
        public static string DetermineFileType(string currentDirectory)
        {
            if (currentDirectory.Contains("help"))
                return "hlp";
            if (currentDirectory.Contains("forms"))
                return "frm";
            if (currentDirectory.Contains("messages"))
                return "msg";
            if (currentDirectory.Contains("various"))
                return "all";

            return "unknown"; // Default fallback
        }

        public bool CheckIfFileFollowsPattern(string file)
        {
            string pattern = @"^\d+\.xml$";
            string fileName = Path.GetFileName(file);
            return Regex.IsMatch(fileName.ToLower(), pattern);
        }

        public void showProgress(int fileAmount, string operationCase, string nodePrefix, string parentFolder, string backupFolder)
        {
            counter++;

            string operationMessage = "";

            // Determine the message based on the operation case
            if (operationCase == "case1")
            {
                operationMessage = "moved back from " + backupFolder + " folder to " + parentFolder + " folder";
            }
            else if (operationCase == "case2")
            {
                // here it needs the information which files are processed
                operationMessage = $"processed" + " from " + parentFolder + " and moved to the backup folder: " + backupFolder;
            }
            else
            {
                operationMessage = "operation completed"; // Default message for other cases
                LoggerHelper.Log("operation completed");
            }
            // Display the progress message
            Console.Write($"\r{counter} of {fileAmount} files {operationMessage}");


            // Add a slight delay to slow down the progress updates
            Thread.Sleep(5); // Delay for 10 milliseconds between updates
            if (counter == fileAmount)
            {
                counter = 0; // Reset counter for future calls
                Console.WriteLine(); // Move to the next line after completion
            }
        }
    }
}
