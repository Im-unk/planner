using MySql.Data.MySqlClient;
using System.Data;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace planner
{
    public class DAL
    {
        #region Database
        MySqlConnection cnt;
        MySqlCommand cmd;
        MySqlDataAdapter da;
        DataTable dt;

        // getting my connection string from appsetting.json
        private readonly IConfiguration _config;

        private void DBOpen()
        {
            string connectionString = _config.GetConnectionString("MySqlConnection");
            cnt = new MySqlConnection(connectionString);

            cnt.Open();
            cmd = new MySqlCommand("", cnt);
            cmd.CommandTimeout = 0;
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();

        }

        private void DBClose()
        {
            if (dt != null)
            {
                dt.Dispose();
            }

            if (da != null)
            {
                da.Dispose();
            }

            if (cmd != null)
            {
                cmd.Dispose(); 
            }

            if (cnt != null && cnt.State == ConnectionState.Open)
            {
                cnt.Close();
                cnt.Dispose();
            }
        }

        private void TransactionQuery(string _Query)
        {
            DBOpen();

            cmd.CommandText = _Query;

            cmd.ExecuteNonQuery();

            DBClose(); 
        }

        private DataTable SelectQuery(string _Query)
        {
            dt = new DataTable();

            try
            {
                DBOpen();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = _Query;

                da.Fill(dt);
            }
            catch(Exception ex)
            { }
            finally
            {
                DBClose();
            }
            return dt;
        }


        #endregion
    }
}
