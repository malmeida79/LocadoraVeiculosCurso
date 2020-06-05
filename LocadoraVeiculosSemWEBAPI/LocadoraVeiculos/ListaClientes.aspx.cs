using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LocadoraVeiculos.ENT;
using LocadoraVeiculos.UTILS;
using LocadoraVeiculos.Class;

namespace LocadoraVeiculos
{
    public partial class ListaClientes : System.Web.UI.Page
    {
        Clientes clientes = Clientes.GetClientes();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdPesquisa_Click(object sender, EventArgs e)
        {
            CarregaGrid(null,txtNomeCliente.Text);
        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = null;
            Response.Redirect("cadastroClientes.aspx");
        }

        protected void CarregaGrid(List<ENTClientes> lista = null, string nome = "")
        {
            // descarregar grid
            gdvClientes.Descarregar();

            // se não vier uma lista, criar uma
            if (lista == null)
            {
                ENTClientes entCliente = new ENTClientes();
                if (!string.IsNullOrEmpty(nome))
                {
                    entCliente.NomeCliente = nome;
                }
                lista = clientes.ListarDados(entCliente);
                entCliente = null;
            }
            
            // carregar grid
            gdvClientes.Preencher<ENTClientes>(lista);
        }

        protected void gdvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvClientes.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    addButton.Text = "Selecionar";
                }
            }
        }

        protected void gdvClientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["ClienteEditar"] = gdvClientes.Rows[e.NewEditIndex].Cells[1].Text;
            Response.Redirect("cadastroClientes.aspx");
        }
    }
}