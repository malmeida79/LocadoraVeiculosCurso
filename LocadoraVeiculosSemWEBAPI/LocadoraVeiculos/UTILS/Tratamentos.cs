using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LocadoraVeiculos.ENT;

namespace LocadoraVeiculos.UTILS
{

    public static class Tratamentos
    {
        public static string MsgErro = string.Empty;

        public static void Alerta(string mensagem)
        {
            var page = HttpContext.Current.CurrentHandler as Page;

            string sMessage = "AbreModal('" + mensagem + "');";

            mensagem = mensagem.Replace(@"''", @"'").Replace(@"\\", @"\").Replace(@"""", "");

            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", sMessage, true);
        }


    }
}

