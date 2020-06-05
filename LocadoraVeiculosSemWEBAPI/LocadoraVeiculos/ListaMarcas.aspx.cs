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
    public partial class ListaMarcas : System.Web.UI.Page
    {
        MarcaVeiculo marcaVeiculo = MarcaVeiculo.GetMarcaVeiculo();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["MarcaEditar"] = null;
            Response.Redirect("cadastroMarcaVeiculo.aspx");
        }

        protected void CarregaGrid(List<ENTMarcaVeiculo> lista = null, string nome = "")
        {
            // descarregar grid
            gdvMarcaVeiculo.Descarregar();

            // se não vier uma lista, criar uma
            if (lista == null)
            {
                ENTMarcaVeiculo entMarcaVeiculo = new ENTMarcaVeiculo();
                if (!string.IsNullOrEmpty(nome))
                {
                    entMarcaVeiculo.MarcaVeiculo = nome;
                }
                lista = marcaVeiculo.ListarDados(entMarcaVeiculo);
                entMarcaVeiculo = null;
            }
            
            // carregar grid
            gdvMarcaVeiculo.Preencher<ENTMarcaVeiculo>(lista);
        }

        protected void gdvMarcaVeiculo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvMarcaVeiculo.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvMarcaVeiculo_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gdvMarcaVeiculo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["MarcaEditar"] = gdvMarcaVeiculo.Rows[e.NewEditIndex].Cells[1].Text;
            Response.Redirect("cadastroMarcaVeiculo.aspx");
        }
    }
}