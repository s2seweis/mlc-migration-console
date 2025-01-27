using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using XmlToSqlApp.Helpers;

namespace XmlToSqlApp
{
    public class SqlRepositoryService
    {
        private readonly SqlRepositoryHelper _sqlHelper;

        public SqlRepositoryService()
        {
            _sqlHelper = new SqlRepositoryHelper();
            _sqlHelper.EnsureTablesExist();
        }

        public bool SaveDataToDB(List<XMLData> convertedXML, string prefix)
        {
            bool isSuccess = true;
            if (convertedXML != null)
            {
                using (SqlConnection con = new SqlConnection(_sqlHelper.connectionString))
                {
                    con.Open();

                    foreach (var xmlData in convertedXML)
                    {
                        try
                        {
                            string query = GetQuery(prefix);
                            _sqlHelper.ExecuteSqlCommand(query, con, xmlData, prefix);
                        }
                        catch (Exception ex)
                        {
                            //Console.WriteLine($"Error inserting data: {ex.Message}");
                            LoggerHelper.Log($"Error inserting data into {prefix} table: {ex.Message}");
                            isSuccess = false;
                            break;
                        }
                    }
                }
            }
            return isSuccess;
        }

        private string GetQuery(string prefix)
        {
            switch (prefix)
            {
                case "hlp":
                    return "INSERT INTO tms_mlc_help (mhlp_id, mhlp_app_id, mhlp_title, mhlp_text, mhlp_ref_id, mhlp_language) VALUES (@mhlp_id,@mhlp_app_id,@mhlp_title,@mhlp_text,@mhlp_ref_id, @mhlp_language)";
                case "frm":
                    return "INSERT INTO tms_mlc_label (mlbl_id, mlbl_app_id, mlbl_label, mlbl_tooltip, mlbl_ref_id, mlbl_language) VALUES (@frm_id,@frm_app_id,@frm_caption,@frm_tooltip,@frm_ref_id, @frm_language)";
                case "msg":
                    return "INSERT INTO tms_mlc_info (minf_id, minf_text, minf_type, minf_ref_id, minf_caption, minf_language) VALUES (@minf_id,@minf_text,@minf_type,@minf_ref_id,@minf_caption, @minf_language)";
                case "all":
                    return "INSERT INTO tms_mlc_various (mvar_id, mvar_app_id, mvar_ref_id, mvar_type, mvar_text, mvar_language, mvar_ongoing, mvar_ongoingoffset, mvar_textoffset, mvar_handling) VALUES (@mvar_id, @mvar_app_id, @mvar_ref_id, @mvar_type, @mvar_text, @mvar_language, @mvar_ongoing, @mvar_ongoingoffset, @mvar_textoffset, @mvar_handling)";
                default:
                    return string.Empty;
            }
        }
    }
}

/*interacts with the database to save XML data*/