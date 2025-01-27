using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlToSqlApp.Helpers;

namespace XmlToSqlApp.Services
{
    internal class ProcessFilesService
    {
        //  common naming convention in C# to indicate that the variable is a private field
        private static readonly XmlPath _xmlPath = new XmlPath(); // Initialize XMLPathsDynamic

        public ProcessFilesService()
        {
            ProcessFilesMethod();
        }

        public static void ProcessFilesMethod()
        {
            // Check if the root directory exists
            if (Directory.Exists(_xmlPath.DynamicPath))
            {

                // Extract the last three parts of the path
                string[] pathParts = _xmlPath.DynamicPath.TrimEnd(Path.DirectorySeparatorChar)
                                                             .Split(Path.DirectorySeparatorChar);
                string lastThreeParts = string.Join(@"\", pathParts.Skip(Math.Max(0, pathParts.Length - 3)));

                // Process the directory recursively
                ProcessDirectoryRecursively(_xmlPath.DynamicPath);

            }
            else
            {
                Console.WriteLine($"Root directory does not exist: {_xmlPath.DynamicPath}");
            }
        }

        public static void ProcessDirectoryRecursively(string currentDirectory)
        {
            try
            {
                FileHelper fileHelper = new FileHelper();
                // Get and process all XML files in the current directory
                string[] xmlFiles = Directory.GetFiles(currentDirectory, "*.xml");
                //Console.WriteLine(xmlFiles.Length);

                string[] validXmlFiles = xmlFiles.Where(file => fileHelper.CheckIfFileFollowsPattern(file)).ToArray();
                //Console.WriteLine(validXmlFiles.Length);

                string[] pathParts = currentDirectory.TrimEnd(Path.DirectorySeparatorChar)
                                                     .Split(Path.DirectorySeparatorChar);


                if (validXmlFiles.Length > 0)
                {
                    string currentPath = currentDirectory;

                    // Extract the last three parts of the path
                    string lastThreeParts = string.Join(@"\", pathParts.Skip(Math.Max(0, pathParts.Length - 5)));

                    string filePathNew = string.Join(@"\", pathParts.Skip(Math.Max(0, pathParts.Length + -3)));

                    // Extract the name of the current directory
                    string parentFolder = Path.GetFileName(currentDirectory.TrimEnd(Path.DirectorySeparatorChar));

                    // Construct the backup folder name within the current directory
                    string backupPath = Path.Combine(currentDirectory, $"{parentFolder}Backup");
                    string backupFolder = Path.GetFileName(backupPath.TrimEnd(Path.DirectorySeparatorChar));

                    // Create the backup directory
                    Directory.CreateDirectory(backupFolder);

                    string _fileType = FileHelper.DetermineFileType(currentDirectory);

                    // Initialize XmlProcessor and SqlRepository
                    XmlProcessorService processor = new XmlProcessorService();
                    SqlRepositoryService repository = new SqlRepositoryService();

                    string _filepath = lastThreeParts;

                    ProcessFilesMethod(xmlFiles, _fileType, processor, repository, parentFolder, backupFolder);
                }
                else
                {
                    string lastThreeParts = string.Join(@"\", pathParts.Skip(Math.Max(0, pathParts.Length - 5)));

                    Console.WriteLine("\r No Xml files found to process in directory: " + lastThreeParts);
                    Thread.Sleep(100);
                    LoggerHelper.Log("No Xml files found to process in directory: " + lastThreeParts);
                }

                // Get the name of the backup folder for the current directory
                string parentFolderName = new DirectoryInfo(currentDirectory).Name;
                string backupFolderName = $"{parentFolderName}Backup";

                // Recursively process all subdirectories except the backup folder
                foreach (string subdirectory in Directory.GetDirectories(currentDirectory))
                {
                    // Compare the subdirectory name directly with the backup folder name
                    string subdirectoryName = Path.GetFileName(subdirectory);
                    if (!string.Equals(subdirectoryName, backupFolderName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Recursively call ProcessDirectoryRecursively to process subdirectories
                        ProcessDirectoryRecursively(subdirectory);

                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered during directory processing
                LoggerHelper.Log($"Error processing directory {currentDirectory}: {ex.Message}");
            }
        }

        // ### Move the ProcessFiles method into a helper class? 

        public static void ProcessFilesMethod(string[] files, string prefix, XmlProcessorService processor, SqlRepositoryService repository, string parentFolder, string backupFolder)
        {
            bool isAnyFileProcessed = false; // Flag to track if any file was processed
            int validXmlFileCount = 0; // Counter to track files matching the pattern
            //XmlProcessorHelper helper = new XmlProcessorHelper();
            FileHelper fileHelper = new FileHelper();

            // Count files matching the pattern using CheckIfFileFollowsPattern
            foreach (string file in files)
            {
                if (fileHelper.CheckIfFileFollowsPattern(file))
                {
                    validXmlFileCount++; // Increment count for valid XML files
                }
            }

            // Process only valid XML files
            foreach (string file in files)
            {
                // Check if the file matches the pattern (this is where CheckIfFileFollowsPattern is used)
                if (fileHelper.CheckIfFileFollowsPattern(file))
                {
                    // here is currently a error !
                    var convertedXML = processor.ConvertXML(file, prefix, validXmlFileCount, parentFolder, backupFolder);
                    if (convertedXML != null)
                    {
                        // has to be true
                        bool isSuccess = repository.SaveDataToDB(convertedXML, prefix);
                        if (isSuccess)
                        {
                            ProcessFileHelper.MoveFileToNewLocation(file); // Use helper method to move the file
                            isAnyFileProcessed = true; // Mark that a file has been processed
                        }
                    }
                }
            }

            // Only log the message outside the loop when a file was processed
            if (isAnyFileProcessed)
            {
                // Log success message
                //Console.WriteLine($"Successfully processed " +
                //$"{prefix.ToUpper()} " +
                //$"files.");
            }
            else
            {
                // Log the message when no valid files were processed
                LoggerHelper.Log($"No valid {prefix.ToUpper()} files were processed.");
            }
        }
    }
}
