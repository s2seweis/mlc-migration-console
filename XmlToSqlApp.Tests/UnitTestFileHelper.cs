using System;
using System.IO;
using Xunit;

namespace XmlToSqlApp.Tests
{
    public class UnitTestsFileHelper
    {

        private const string TestDirectory = "C:\\Users\\SWE\\source\\repos\\XmlToSql\\XmlToSqlApp.Tests\\TestDirectory";

        //private const string BackupDirectory = "C:\\Users\\SWE\\source\\repos\\XmlToSql\\XmlToSqlApp.Tests\\TestDirectory\\Backup";
        private const string BackupDirectory = "C:\\Users\\SWE\\source\\repos\\XmlToSql\\XmlToSqlApp.Tests\\TestDirectory\\Backup\\_helpBackup";

        //private const string FilePath = "C:\\Users\\SWE\\source\\repos\\XmlToSql\\XmlToSqlApp.Tests\\TestDirectory\\8888.xml";
        private const string FilePath = "C:\\Users\\SWE\\source\\repos\\XmlToSql\\XmlToSqlApp\\_mlc\\dynamic\\1\\1\\_help\\8888.xml";

        public UnitTestsFileHelper()
        {
            UbitTestFileHelperMethod();
        }
      
        [Fact]
        public void UbitTestFileHelperMethod()
        {
            // Arrange: Create test directory and backup folder
            if (!Directory.Exists(TestDirectory))
                Directory.CreateDirectory(TestDirectory);
            if (!Directory.Exists(BackupDirectory))
                Directory.CreateDirectory(BackupDirectory);

            // Ensure the test file exists, create it if it doesn't
            if (!File.Exists(FilePath))
            {
                // Create the file and write some dummy XML data
                using (var stream = File.Create(FilePath))
                using (var writer = new StreamWriter(stream))
                {
                    // Writing a simple dummy XML structure
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    writer.WriteLine("<Root>");
                    writer.WriteLine("  <Item>");
                    writer.WriteLine("    <Name>Sample Item</Name>");
                    writer.WriteLine("    <Value>12345</Value>");
                    writer.WriteLine("  </Item>");
                    writer.WriteLine("  <Item>");
                    writer.WriteLine("    <Name>Another Item</Name>");
                    writer.WriteLine("    <Value>67890</Value>");
                    writer.WriteLine("  </Item>");
                    writer.WriteLine("</Root>");
                }
            }
        }

        //[Fact]
        //public void MoveFileToNewLocation_ShouldMoveFileToBackupFolder_WhenBackupFolderExists()
        //{
        //    // Arrange: Create a test file
        //    File.Create(FilePath).Dispose();

        //    // Act: Move the file to the backup folder
        //    FileHelper.MoveFileToNewLocation(FilePath);

        //    // Assert: Check if the file was moved to the backup folder
        //    string expectedFilePath = Path.Combine(BackupDirectory, Path.GetFileName(FilePath));
        //    Assert.True(File.Exists(expectedFilePath), "File should be moved to the backup folder.");

        //    // Cleanup: Delete the test files and directories
        //    //File.Delete(expectedFilePath);
        //    //Directory.Delete(BackupDirectory);
        //    //Directory.Delete(TestDirectory);
        //}

        //[Fact]
        //public void MoveFileToNewLocation_ShouldNotMoveFile_WhenBackupFolderDoesNotExist()
        //{
        //    // Arrange: Create a test file
        //    File.Create(FilePath).Dispose();

        //    // Remove the Backup folder to simulate the folder not existing
        //    Directory.Delete(BackupDirectory, true);

        //    // Act: Attempt to move the file (should not move since backup folder does not exist)
        //    FileHelper.MoveFileToNewLocation(FilePath);

        //    // Assert: Check that the file was not moved to the backup folder
        //    Assert.True(File.Exists(FilePath), "File should remain in the original location.");

        //    // Cleanup: Delete the test file and restore the directory structure
        //    File.Delete(FilePath);
        //    Directory.CreateDirectory(BackupDirectory);
        //}

        // Optionally, you could add more tests to cover other scenarios like permission issues, etc.
    }
}
