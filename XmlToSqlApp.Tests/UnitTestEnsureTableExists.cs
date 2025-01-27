using System;
using System.Data.SqlClient;
using Xunit;

namespace XmlToSqlApp.Tests
{
    public class UnitTestsEnsureTableExists
    {
        private readonly string _connectionString = @"Data Source=NB242F34R44\SQLEXPRESS;Initial Catalog=XmlToSqlApp1;User ID=sa;Password=alk123;Encrypt=True;TrustServerCertificate=True";

        [Fact]
        public void EnsureTablesExist_ShouldCreateMissingTables()
        {
            // Arrange: Ensure tables exist or create them if they do not
            EnsureTablesExist();

            // Act: Check if all required tables exist
            bool tablesExist = CheckTablesExist();

            // Assert: Verify that all required tables exist
            Assert.True(tablesExist);
        }

        private void EnsureTablesExist()
        {
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
                        if (tableCount == 0)
                        {
                            return false; // Return false if any table does not exist
                        }
                    }
                }
            }

            return true; // Return true if all tables exist
        }
    }
}
