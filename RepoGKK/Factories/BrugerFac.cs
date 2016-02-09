using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoGKK.Models.BaseModels;


namespace RepoGKK.Factories
{
    public class BrugerFac:AutoFac<Gkkbruger>
    {
        public Gkkbruger Login(string Name, string Password)
        {
            using (var CMD = new SqlCommand("SELECT * FROM Gkkbruger WHERE Name=@Name AND Password=@Password", Conn.CreateConnection()))
            {
                CMD.Parameters.AddWithValue("@Name", Name);
                CMD.Parameters.AddWithValue("@Password", Password);

                Mapper<Gkkbruger> mapper = new Mapper<Gkkbruger>();

                var r = CMD.ExecuteReader();
                Gkkbruger bruger = new Gkkbruger();


                if (r.Read())
                {
                    bruger = mapper.Map(r);
                }

                r.Close();
                CMD.Connection.Close();
                return bruger;
            }
        }
    }


}

