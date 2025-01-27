using System;

namespace XmlToSqlApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Invoke the ProcessFilesAndMove method from the UserInterface class as the entry point
            // This method handles file processing and moving operations
            UserInterface.ProcessFilesAndMove();

            // Wait for the user to press Enter before closing the application
            Console.ReadLine();
        }
    }
}
