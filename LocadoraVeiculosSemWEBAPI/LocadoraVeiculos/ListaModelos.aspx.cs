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
    public partial class ListaModelos : System.Web.UI.Page
    {
        ModeloVeiculo modeloVeiculo = ModeloVeiculo.GetModeloVeiculo();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["ModeloEditar"] = null;
            Response.Redirect("cadastroModeloVeiculo.aspx");
        }

        protected void CarregaGrid(List<ENTModeloVeiculo> lista = null, string nome = "")
        {
            // descarregar grid
            gdvModeloVeiculo.Descarregar();

            // se não vier uma lista, criar uma
            if (lista == null)
            {
                ENTModeloVeiculo entModeloVeiculo = new ENTModeloVeiculo();
                if (!string.IsNullOrEmpty(nome))
                {
                    entModeloVeiculo.ModeloVeiculo = nome;
                }
                lista = modeloVeiculo.ListarDados(entModeloVeiculo);
                entModeloVeiculo = null;
            }
            
            // carregar grid
            gdvModeloVeiculo.Preencher<ENTModeloVeiculo>(lista);
        }

        protected void gdvModeloVeiculo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvModeloVeiculo.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvModeloVeiculo_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gdvModeloVeiculo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["ModeloEditar"] = gdvModeloVeiculo.Rows[e.NewEditIndex].Cells[1].Text;
            Response.Redirect("cadastroModeloVeiculo.aspx");
        }
    }
}