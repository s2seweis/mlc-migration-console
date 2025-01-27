using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XmlToSqlApp.Helpers
{
    internal class RestoreFilesHelper
    {
        // Helper method to get the parent directory for the backup
        public static string GetParentDirectoryForBackup(string backupDirectory)
        {
            // Assuming backup directories are named after the original directories with "-Backup" suffix
            string parentDirectory = backupDirectory.Replace("-Backup", "");
            return Directory.Exists(parentDirectory) ? parentDirectory : null;
        }

        public static void ProcessRestoreFiles(string parentDirectory, string backupDirectory)
        {
            string parentFolder = Path.GetFileName(parentDirectory);
            //Console.WriteLine(parentFolder);
            string backupFolder = Path.GetFileName(backupDirectory);
            //Console.WriteLine(backupFolder);

            string[] pathParts = backupDirectory.TrimEnd(Path.DirectorySeparatorChar)
                                                             .Split(Path.DirectorySeparatorChar);

            string firstTwoParts = string.Join(@"\", pathParts.Take(1)); // Get the first two parts

            string lastThreePartsOld = string.Join(@"\", pathParts.Skip(Math.Max(0, pathParts.Length - 7)));

            string lastThreeParts = $"{firstTwoParts}...{lastThreePartsOld}";

            try
            {
                // Get all files in the backup directory
                string[] backupFiles = Directory.GetFiles(backupDirectory);

                // Check if backup files exist
                if (backupFiles.Length > 0)
                {
                    //Console.WriteLine("Found " + backupFiles.Length + " files in directory: " + lastThreeParts);
                    var restoreFileHelper = new RestoreFilesHelper();

                    foreach (var file in backupFiles)
                    {
                        // Define the target path in the parent directory
                        string targetPath = Path.Combine(parentDirectory, Path.GetFileName(file));

                        // Check if the file already exists in the target directory
                        if (File.Exists(targetPath))
                        {
                            //Console.WriteLine($"File {file} already exists in {parentDirectory}, skipping restore.");
                        }
                        else
                        {
                            // If the file doesn't exist, process it using FileHelper's CheckAndMoveFiles method
                            bool fileMoved = restoreFileHelper.CheckAndMoveFiles(backupFiles, parentFolder, backupFolder, lastThreeParts);

                            if (fileMoved)
                            {
                                //Console.WriteLine($"Restored file {file} to {parentDirectory}");
                                var repositoryHelper = new SqlRepositoryHelper();
                                repositoryHelper.ClearTables();
                            }
                            else
                            {
                                Console.WriteLine($"Failed to move file {file}. Skipping.");
                            }
                        }
                    }

                    // If files were successfully moved, clear the database tables
                    bool filesMovedSuccessfully = restoreFileHelper.CheckAndMoveFiles(backupFiles, parentFolder, backupFolder, lastThreeParts);

                    if (filesMovedSuccessfully)
                    {
                        var repositoryHelper = new SqlRepositoryHelper();
                        repositoryHelper.ClearTables();
                        Console.WriteLine($" files successfully restored and database cleared.");
                    }
                    else
                    {
                        //LoggerHelper.Log($" file move operation failed. Database cleared!");
                    }
                }
                else
                {
                    Thread.Sleep(100);
                    Console.Write($"\rNo XML files found to process in directory: {lastThreeParts}" + "        ");
                    LoggerHelper.Log("No Xml files found to process in directory: " + lastThreeParts + "        ");
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Log($"Error restoring  files: {ex.Message}");
            }
        }

        public static void MoveFileToParentDirectory(string file)
        {
            try
            {
                var currentDirectory = Path.GetDirectoryName(file);
                string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;

                if (string.IsNullOrEmpty(parentDirectory))
                {
                    Console.WriteLine("Parent directory not found.");
                    return;
                }

                string newLocation = Path.Combine(parentDirectory, Path.GetFileName(file));

                if (!Directory.Exists(parentDirectory))
                {
                    Console.WriteLine("Parent directory does not exist.");
                    return;
                }

                File.Move(file, newLocation);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
        }
        public bool CheckAndMoveFiles(string[] files, string parentFolder, string backupFolder, string lastThreeParts)
        {
            if (files == null || files.Length == 0)
            {
                Console.WriteLine("No files to move.");
                return false;
            }

            int fileCount = files.Length;
            bool filesMoved = false;

            // Create an instance of XmlProcessorHelper to call showProgress
            FileHelper fileHelper = new FileHelper();

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    MoveFileToParentDirectory(file);
                    filesMoved = true; // Set to true if at least one file is moved
                    string message = "empty";

                    // Call showProgress after moving each file, passing the appropriate case string
                    fileHelper.showProgress(fileCount, "case1", message, parentFolder, backupFolder); // For example, case1 for "moved back to parent folder"
                }
            }

            return filesMoved; // Return whether any files were moved
        }

    }
}
