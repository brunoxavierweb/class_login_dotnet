using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADM;

public partial class admin_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] != null)
        {
            Response.Redirect("HomeAdmin.aspx");
        }
        else if (Request.Cookies["login"] != null && Request.Cookies["senha"] != null)
        {
            Session["usuario"] = null;
            ADM.Usuario ObjUsuario = new Usuario();
            if (ObjUsuario.Autenticar(Request.Cookies["login"].Value.ToString(), Request.Cookies["senha"].Value.ToString()) == true)
            {
                Response.Redirect("HomeAdmin.aspx");
            }
        }

    }
    protected void BtnEntrar_Click(object sender, EventArgs e)
    {
        ADM.Usuario ObjUsuario = new ADM.Usuario();

        if (ObjUsuario.Autenticar(TbLogin.Text, TbSenha.Text) == true)
        {
            //Botão lembrar Senha
            if (CBRemember.Checked == true)
            {
                Response.Cookies["login"].Value = TbLogin.Text;
                Response.Cookies["login"].Expires = DateTime.Now.AddDays(100);
                Response.Cookies["senha"].Value = TbSenha.Text;
                Response.Cookies["senha"].Expires = DateTime.Now.AddDays(100);
            }
            Response.Redirect("HomeAdmin.aspx");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Não foi possivel efetuar ação. Verifique os campos em destaque!');$('#TbLogin').parent().addClass('has-error');$('#TbSenha').parent().addClass('has-error');</script>'");
        }
    }
}
