using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;

namespace OptimizingParallelCompiler
{
    class Database
    {
        public string getStatementsByWord(string word)
        {
            string statement = "";
            word = word.ToUpper();
            //string connString = "server=(local)\\SQLEXPRESS;database=MyDatabase;Integrated Security=SSPI";
            string connString = "Persist Security Info = False; Data Source = 'Test.sdf'";
            string sql = "select STATEMENTS_TO_EXECUTE from RESERVED_WORDS WHERE LTRIM(RTRIM(UPPER(WORD))) = '" + word + "'";

            SqlCeConnection conn = new SqlCeConnection(connString);

            try
            {
                conn.Open();
                SqlCeCommand cmd = new SqlCeCommand(sql, conn);
                SqlCeDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    statement = reader[0].ToString();
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                statement = "Could not return Statement for word: " + word + " " + ex.Message;
            }
            finally
            {

            }
            if (statement.Trim() == "")
            {
                return "No statement was found for word: " + word;
            }
            return statement;
        }
    }
}
