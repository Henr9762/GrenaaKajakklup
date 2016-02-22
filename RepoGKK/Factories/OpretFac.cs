using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoGKK.Models.BaseModels;

namespace RepoGKK.Factories
{
   public class OpretFac:AutoFac<Gkkbruger>
   {
     
        public bool UserExists(string Name)
        {
            using (var CMD = new SqlCommand("SELECT ID from Gkkbruger where Name=@Name", Conn.CreateConnection()))
            {
                CMD.Parameters.AddWithValue("@Name", Name);

                var r = CMD.ExecuteReader();

                if (r.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

    }
}


    
