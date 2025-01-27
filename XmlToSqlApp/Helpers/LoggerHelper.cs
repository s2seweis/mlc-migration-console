using System;
using System.IO;
using System.Collections.Generic;

namespace XmlToSqlApp.Helpers
{
    public static class LoggerHelper
    {
        private static readonly string _logFilePath =
            Path.Combine("C:\\Users\\SWE\\source\\repos\\10. MLC Migration Console Application\\XmlToSqlApp\\Data\\Logs", "exceptions.txt");

        // A HashSet to track already logged messages, it only stores unique values, preventing duplicates.
        private static HashSet<string> _loggedMessages = new HashSet<string>();

        public static void Log(string message)
        {
            // Check if the log file exists; if not, create it
            if (!File.Exists(_logFilePath))
            {
                try
                {
                    // Create the file if it does not exist
                    using (FileStream fs = File.Create(_logFilePath))
                    {
                        // Optional: Add an initial log entry
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Log file created.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating log file: {ex.Message}");
                    return; // Exit the method if file creation fails
                }
            }

            // Check if this message has been logged before
            if (_loggedMessages.Contains(message))
            {
                return; // Skip logging if the message is already logged
            }

            // Get the user and the current path
            string user = Environment.UserName; // Current user
            string path = Directory.GetCurrentDirectory(); // Current working directory

            // Format the message with Date, Time, User, Error Message, and Path
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | User: {user} | Error: {message}";

            // Truncate the message if it's too long for the console window
            int maxWidth = Console.WindowWidth - 1; // Leave space for the last character
            if (logMessage.Length > maxWidth)
            {
                logMessage = logMessage.Substring(0, maxWidth - 3) + "..."; // Add ellipsis if the message is too long
            }

            // Show the message to the user (Console output), avoid adding a newline
            //Console.Write(logMessage); // Using Write instead of WriteLine

            // Write the message to the log file
            try
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle any potential errors when writing to the log file
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }

            // Add the message to the HashSet to prevent it from being logged again
            _loggedMessages.Add(message);
        }

        // Handle exceptions
        public static void HandleError(Exception ex, string userFriendlyMessage = "An error occurred. Please try again later.")
        {
            // Display a user-friendly message, making sure it is in the same line as the error message.
            Log(ex.Message); // Log error message on the same line as 'Error:'

            Console.Write(userFriendlyMessage); // Show the user-friendly message inline, without adding a newline
        }
    }
}
