using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using XmlToSqlApp;

namespace XmlToSqlApp.Helpers
{
    public class SqlRepositoryHelper
    {
        public string connectionString = "Data Source=NB242F34R44\\SQLEXPRESS;Initial Catalog=XmlToSqlApp1;User ID=sa;Password=alk123;TrustServerCertificate=True";

        public void EnsureTablesExist()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // help table
                CreateTable(con, "tms_mlc_help", "[mhlp_id] INT NULL, [mhlp_app_id] INT NULL, [mhlp_ref_id] INT NULL, [mhlp_language] INT NULL, [mhlp_title] CHAR(150) NULL, [mhlp_text] NVARCHAR(MAX) NULL");
                // form table
                CreateTable(con, "tms_mlc_label", "[mlbl_id] INT NULL, [mlbl_app_id] INT NULL, [mlbl_ref_id] INT NULL, [mlbl_language] INT NULL, [mlbl_label] CHAR(150) NULL, [mlbl_tooltip] NVARCHAR(MAX) NULL");
                // messages table
                CreateTable(con, "tms_mlc_info", "[minf_id] INT NULL, [minf_caption] CHAR(150) NULL, [minf_type] INT NULL, [minf_ref_id] INT NULL, [minf_language] INT NULL, [minf_text] CHAR(150) NULL");
                // various table
                CreateTable(con, "tms_mlc_various", "[mvar_id] INT NULL, [mvar_app_id] INT NULL, [mvar_ref_id] INT NULL, [mvar_language] INT NULL, [mvar_type] INT NULL, [mvar_ongoing] INT NULL, [mvar_ongoingoffset] INT NULL, [mvar_text] CHAR(150) NULL, [mvar_textoffset] INT NULL, [mvar_handling] INT NULL");
            }
        }

        private void CreateTable(SqlConnection con, string tableName, string columns)
        {
            string query = $@"
        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}')
        BEGIN
            CREATE TABLE [dbo].[{tableName}] ({columns});
        END";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void ClearTables()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                TruncateTable(con, "tms_mlc_help");
                TruncateTable(con, "tms_mlc_label");
                TruncateTable(con, "tms_mlc_info");
                TruncateTable(con, "tms_mlc_various");
            }
        }

        private void TruncateTable(SqlConnection con, string tableName)
        {
            string query = $"TRUNCATE TABLE [dbo].[{tableName}];";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void ExecuteSqlCommand(string query, SqlConnection con, XMLData xmlData, string prefix)
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                SetCommandParameters(cmd, xmlData, prefix);
                cmd.ExecuteNonQuery();
            }
        }

        private void SetCommandParameters(SqlCommand cmd, XMLData xmlData, string prefix)
        {
            switch (prefix)
            {
                case "hlp":
                    cmd.Parameters.AddWithValue("@mhlp_id", xmlData.Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mhlp_app_id", xmlData.App_id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mhlp_title", xmlData.Title ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mhlp_text", xmlData.Text ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mhlp_ref_id", xmlData.Verweis_Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mhlp_language", xmlData.Language_Id ?? (object)DBNull.Value);
                    break;
                case "frm":
                    cmd.Parameters.AddWithValue("@frm_id", xmlData.Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@frm_app_id", xmlData.App_id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@frm_caption", xmlData.Caption ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@frm_tooltip", xmlData.Tooltip ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@frm_ref_id", xmlData.Verweis_Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@frm_language", xmlData.Language_Id ?? (object)DBNull.Value);
                    break;
                case "msg":
                    cmd.Parameters.AddWithValue("@minf_id", xmlData.Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@minf_app_id", xmlData.App_id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@minf_text", xmlData.Text ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@minf_type", xmlData.Type ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@minf_ref_id", xmlData.Verweis_Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@minf_caption", xmlData.Caption ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@minf_language", xmlData.Language_Id ?? (object)DBNull.Value);
                    break;
                case "all":
                    cmd.Parameters.AddWithValue("@mvar_id", xmlData.Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_app_id", xmlData.App_id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_ref_id", xmlData.Verweis_Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_type", xmlData.Type ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_text", xmlData.Text ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_language", xmlData.Language_Id ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_ongoing", xmlData.Laufnummer ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_ongoingoffset", xmlData.Startwert ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_textoffset", xmlData.Textabweichung ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mvar_handling", xmlData.Bearbeitung ?? (object)DBNull.Value);
                    break;
            }
        }
    }
}
