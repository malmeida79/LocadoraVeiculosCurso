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
    public partial class ListaLocacoes : System.Web.UI.Page
    {
        Locacoes locacoes = Locacoes.GetLocacoes();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdPesquisa_Click(object sender, EventArgs e)
        {
            CarregaGrid(null, txtNomeCliente.Text, txtPlaca.Text);
        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["LocacaoEditar"] = null;
            Response.Redirect("cadastroLocacoes.aspx");
        }

        protected void CarregaGrid(List<ENTLocacoes> lista = null, string nome = "", string placa = "")
        {
            // descarregar grid
            gdvLocacoes.Descarregar();

            // se não vier uma lista, criar uma
            if (lista == null)
            {
                ENTLocacoes entLocacoes = new ENTLocacoes();
                if (!string.IsNullOrEmpty(nome))
                {
                    entLocacoes.NomeCliente = nome;
                }
                if (!string.IsNullOrEmpty(placa))
                {
                    entLocacoes.PlacaVeiculo = placa;
                }
                lista = locacoes.ListarDados(entLocacoes);
                entLocacoes = null;
            }

            // carregar grid
            gdvLocacoes.Preencher<ENTLocacoes>(lista);
        }

        protected void gdvLocacoes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvLocacoes.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvLocacoes_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gdvLocacoes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["LocacaoEditar"] = gdvLocacoes.Rows[e.NewEditIndex].Cells[1].Text;
            Response.Redirect("cadastroLocacoes.aspx");
        }
    }
}