using System;
using System.Data.SqlClient;
using Xunit;

namespace XmlToSqlApp.Tests
{
    public class UnitTestsDropTableIfExists
    {
        private readonly string _connectionString = @"Data Source=NB242F34R44\SQLEXPRESS;Initial Catalog=XmlToSqlApp1;User ID=sa;Password=alk123;Encrypt=True;TrustServerCertificate=True";

        [Fact]
        public void DropTablesIfExist_ShouldDropExistingTables()
        {
            // Arrange: Ensure that the tables exist before testing the drop functionality
            CreateTestTablesIfNotExist();

            // Act: Drop the tables if they exist
            DropTablesIfExist();

            // Assert: Verify that tables no longer exist
            bool tablesExist = CheckTablesExist();
            Assert.False(tablesExist, "The tables should be dropped, but they still exist.");
        }

        private void DropTablesIfExist()
        {
            string[] dropTableQueries =
            {
                @"
                IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_help' AND xtype = 'U')
                BEGIN
                    DROP TABLE [dbo].[tms_mlc_help];
                END
                ",

                @"
                IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_info' AND xtype = 'U')
                BEGIN
                    DROP TABLE [dbo].[tms_mlc_info];
                END
                ",

                @"
                IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_label' AND xtype = 'U')
                BEGIN
                    DROP TABLE [dbo].[tms_mlc_label];
                END
                ",

                @"
                IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_various' AND xtype = 'U')
                BEGIN
                    DROP TABLE [dbo].[tms_mlc_various];
                END
                "
            };

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                foreach (var query in dropTableQueries)
                {
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private bool CheckTablesExist()
        {
            string[] tableNames = { "tms_mlc_help", "tms_mlc_label", "tms_mlc_info", "tms_mlc_various" };

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                foreach (var tableName in tableNames)
                {
                    string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        int tableCount = (int)cmd.ExecuteScalar();
                        if (tableCount > 0)
                        {
                            return true; // Return true if any table exists
                        }
                    }
                }
            }

            return false; // Return false if no tables exist
        }

        private void CreateTestTablesIfNotExist()
        {
            // This method ensures that the tables exist for testing the drop functionality.
            string[] createTableQueries =
            {
                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_help' AND xtype = 'U')
                BEGIN
                    CREATE TABLE [dbo].[tms_mlc_help] (
                        [mhlp_id]       INT            NULL,
                        [mhlp_app_id]   INT            NULL,
                        [mhlp_ref_id]   INT            NULL,
                        [mhlp_language] INT            NULL,
                        [mhlp_title]    CHAR (150)     NULL,
                        [mhlp_text]     NVARCHAR (MAX) NULL
                    );
                END
                ",

                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_info' AND xtype = 'U')
                BEGIN
                    CREATE TABLE [dbo].[tms_mlc_info] (
                        [minf_id]      INT        NULL,
                        [minf_caption] CHAR (150) NULL,
                        [minf_type]    INT        NULL,
                        [minf_ref_id]  INT        NULL,
                        [minf_text]    CHAR (150) NULL
                    );
                END
                ",

                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_label' AND xtype = 'U')
                BEGIN
                    CREATE TABLE [dbo].[tms_mlc_label] (
                        [mlbl_id]       INT            NULL,
                        [mlbl_app_id]   INT            NULL,
                        [mlbl_ref_id]   INT            NULL,
                        [mlbl_language] INT            NULL,
                        [mlbl_label]    CHAR (150)     NULL,
                        [mlbl_tooltip]  NVARCHAR (MAX) NULL
                    );
                END
                ",

                @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'tms_mlc_various' AND xtype = 'U')
                BEGIN
                    CREATE TABLE [dbo].[tms_mlc_various] (
                        [mvar_id]            INT        NULL,
                        [mvar_app_id]        INT        NULL,
                        [mvar_ref_id]        INT        NULL,
                        [mvar_language]      INT        NULL,
                        [mvar_type]          INT        NULL,
                        [mvar_ongoing]       INT        NULL,
                        [mvar_ongoingoffset] INT        NULL,
                        [mvar_text]          CHAR (150) NULL,
                        [mvar_textoffset]    INT        NULL,
                        [mvar_handling]      INT        NULL
                    );
                END
                "
            };

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                foreach (var query in createTableQueries)
                {
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
