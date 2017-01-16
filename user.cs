using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using ADM;

namespace ADM
{

    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int Ativo { get; set; }        
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }        
        public DateTime DataLogin { get; set; }
        public bool Autenticar(string usuario, string senha)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MySqlConnection sqlConn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT idUsuario FROM usuario WHERE Login = @plogin AND Senha = @psenha AND Status = 1", sqlConn);
            cmd.Parameters.AddWithValue("@plogin", usuario);
            cmd.Parameters.AddWithValue("@psenha", senha);
            sqlConn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                int IdUser = Convert.ToInt32(dr["idUsuario"].ToString());
                HttpContext.Current.Session["usuario"] = IdUser.ToString();
                sqlConn.Close();


                MySqlCommand cmdUpdate = new MySqlCommand("Update usuario SET DataLogin = @pDatalogin WHERE idUsuario = @idUsuario", sqlConn);
                cmdUpdate.Parameters.AddWithValue("@pDatalogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmdUpdate.Parameters.AddWithValue("@idUsuario", IdUser.ToString());
                sqlConn.Open();
                cmdUpdate.ExecuteNonQuery();
                sqlConn.Close();

                return true;
            }
            else
            {
                HttpContext.Current.Session["usuario"] = null;
                sqlConn.Close();
                return false;
            }

        }
	
    }

}