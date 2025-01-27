using System.IO;

namespace XmlToSqlApp
{
    public class XmlPath
    {
        // Define paths for different directories dynamically
        public string DynamicPath { get; private set; }

        // Constructor to initialize the paths and files
        public XmlPath()
        {
            // Set the path for the root directory where dynamic folders are stored
            DynamicPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\_mlc\dynamic\1");
        }
    }
}
