using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XmlToSqlApp.Helpers
{
    internal class ProcessFileHelper
    {
        public static void MoveFileToNewLocation(string file)
        {
            try
            {
                var currentDirectory = Path.GetDirectoryName(file);
                string[] listOfDirectories = Directory.GetDirectories(currentDirectory);

                // Search for a directory that ends with "Backup"
                string backupDirectory = null;
                foreach (string directory in listOfDirectories)
                {
                    if (Regex.IsMatch(directory, @"Backup$"))
                    {
                        backupDirectory = directory;
                        break; // Stop searching once we find the Backup directory
                    }
                }

                if (backupDirectory == null)
                {
                    Console.WriteLine("Backup folder not found.");
                    LoggerHelper.Log("Backup folder not found.");
                    return;
                }

                string newLocation = Path.Combine(backupDirectory, Path.GetFileName(file));

                if (!Directory.Exists(Path.GetDirectoryName(newLocation)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(newLocation));
                }

                File.Move(file, newLocation);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
        }
    }
}
