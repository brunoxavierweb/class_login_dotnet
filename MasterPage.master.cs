using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADM;

public partial class MasterAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            if (Request.Cookies["login"] != null && Request.Cookies["senha"] != null)
            {
                Session["usuario"] = null;
                ADM.Usuario ObjUsuario = new ADM.Usuario();
                if (ObjUsuario.Autenticar(Request.Cookies["login"].Value.ToString(), Request.Cookies["senha"].Value.ToString()) == true)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                Session["usuario"] = null;
                Response.Redirect("Default.aspx");
            }

        }
    }
    protected void LbSair_Click(object sender, EventArgs e)
    {
        Session["usuario"] = null;
        Response.Cookies["login"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["senha"].Expires = DateTime.Now.AddDays(-1);

        Response.Redirect("Default.aspx");

    }
}
