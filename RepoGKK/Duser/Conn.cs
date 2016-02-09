using System.Data.SqlClient;

namespace RepoGKK
{
    public static class Conn
    {
        
        public static SqlConnection GetCon()
        {
            SqlConnection con = new SqlConnection("server=194.255.108.50;database=dbGKK;uid=GKK;pwd=hest;MultipleActiveResultSets=True");
            return con;
        }

        public static SqlConnection CreateConnection()
        {
            var cn = GetCon();
            cn.Open();
            return cn;
        }
    }
}
