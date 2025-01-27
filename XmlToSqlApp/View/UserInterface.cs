using System;
using System.IO;
using XmlToSqlApp.Services;
using XmlToSqlApp.View;

namespace XmlToSqlApp
{
    public static class UserInterface
    {

        // TODO: 1. Just a test for the "Aufgabenliste".

        public static void ProcessFilesAndMove()
        {
            bool exitProgram = false; // Flag to check if the user wants to exit

            while (!exitProgram)
            {
                // Clear the screen and display the logo
                Console.Clear();
                // Import the Logo from LogoHandler class
                LogoHandler.DisplayLogo();

                // Display the menu
                Console.WriteLine("\n=== File Processing Menu ===");
                Console.WriteLine("1: Restore files from backup and clear database tables.");
                Console.WriteLine("2: Process files, save to database, and move to backup.");
                Console.WriteLine("3: View error logs.");
                Console.WriteLine("4: Exit.");
                Console.Write("\nChoose an option: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("\nRestoring files and cleaning the DB");
                        //FileProcessingHandlerDynamically.RestoreFiles();
                        RestoreFilesService.RestoreFiles();
                        Console.WriteLine("\nOperation complete. Press any key to return to the menu.");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("\nProcessing files...");
                        //FileProcessingHandlerDynamically.ProcessFiles();
                        ProcessFilesService.ProcessFilesMethod();
                        Console.WriteLine("\nOperation complete. Press any key to return to the menu.");
                        Console.ReadKey();
                        break;

                    case "3":
                        // View and delete logs
                        Console.WriteLine("\nDisplaying error logs...");
                        DisplayLogs();

                        // Ask user if they want to delete the logs
                        Console.WriteLine("\nDo you want to delete the error logs? (y/n)");
                        string deleteConfirmation = Console.ReadLine();
                        if (deleteConfirmation?.ToLower() == "y")
                        {
                            DeleteLogs();
                        }

                        Console.WriteLine("\nPress any key to return to the menu.");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.WriteLine("\nAre you sure you want to exit? (y/n)");
                        string exitConfirmation = Console.ReadLine();
                        if (exitConfirmation?.ToLower() == "y")
                        {
                            Console.WriteLine("Exiting application...");
                            exitProgram = true;
                        }
                        else
                        {
                            Console.WriteLine("Returning to menu...");
                        }
                        break;

                    default:
                        Console.WriteLine("\nInvalid input. Please choose a number between 1 and 5.");
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private static void DisplayLogs()
        {
            string logFilePath = Path.Combine(
                "C:\\Users\\SWE\\source\\repos\\10. MLC Migration Console Application\\XmlToSqlApp\\Data\\Logs",
                "exceptions.txt"
            );

            if (File.Exists(logFilePath))
            {
                try
                {
                    string logs = File.ReadAllText(logFilePath);
                    Console.WriteLine("\n=== Log File Content ===");
                    Console.WriteLine(logs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading the log file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\nLog file not found.");
            }
        }

        private static void DeleteLogs()
        {
            string logFilePath = Path.Combine(
                "C:\\Users\\SWE\\source\\repos\\10. MLC Migration Console Application\\XmlToSqlApp\\Data\\Logs",
                "exceptions.txt"
            );

            try
            {
                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath); // Delete the log file
                    Console.WriteLine("Log file deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Log file does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting the log file: {ex.Message}");
            }
        }
    }
}

/*
 Comment to the sql tables:
form = label
help = help
info = messages
various = various
 */



                                       