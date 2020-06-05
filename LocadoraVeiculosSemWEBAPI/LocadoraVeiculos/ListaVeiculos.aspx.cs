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
    public partial class ListaVeiculos : System.Web.UI.Page
    {
        Veiculos veiculos = Veiculos.GetVeiculos();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdPesquisa_Click(object sender, EventArgs e)
        {
            CarregaGrid(null,txtPlaca.Text);
        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["VeiculoEditar"] = null;
            Response.Redirect("cadastroVeiculos.aspx");
        }

        protected void CarregaGrid(List<ENTVeiculos> lista = null, string placa = "")
        {
            // descarregar grid
            gdvVeiculos.Descarregar();

            // se não vier uma lista, criar uma
            if (lista == null)
            {
                ENTVeiculos entVeiculo = new ENTVeiculos();
                if (!string.IsNullOrEmpty(placa))
                {
                    entVeiculo.PlacaVeiculo = placa;
                }
                lista = veiculos.ListarDados(entVeiculo);
                entVeiculo = null;
            }

            gdvVeiculos.Columns[2].Visible = true;
            gdvVeiculos.Columns[4].Visible = true;

            // carregar grid
            gdvVeiculos.Preencher<ENTVeiculos>(lista);

            gdvVeiculos.Columns[2].Visible = false;
            gdvVeiculos.Columns[4].Visible = false;

        }

        protected void gdvVeiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvVeiculos.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvVeiculos_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gdvVeiculos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["VeiculoEditar"] = gdvVeiculos.Rows[e.NewEditIndex].Cells[1].Text;
            Response.Redirect("cadastroVeiculos.aspx");
        }
    }
}