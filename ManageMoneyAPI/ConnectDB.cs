using System.Data.SqlClient;

namespace ManageMoneyAPI
{
    public class ConnectDB
    {
        private static SqlConnection Connection;

        private ConnectDB()
        {

        }
        public static SqlConnection Connect()
        {
            try
            {
                if (Connection == null)
                {
                    Connection = new SqlConnection("Data Source=SQL5080.site4now.net;Initial Catalog=db_a833e3_opendi;User Id=db_a833e3_opendi_admin;Password=q1w2e3r4t5y6u7");
                    Connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Connection;
        }
    }
}
