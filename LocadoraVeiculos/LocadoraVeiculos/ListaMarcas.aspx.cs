using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.UTILS;

namespace LocadoraVeiculos
{
    public partial class ListaMarcas : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public ListaMarcas()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:57627");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getAll();
            }
        }

        private void getAll()
        {

            HttpResponseMessage response;

            //chamando a api pela url            
            response = client.GetAsync("api/marcaveiculo").Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var marcaVeiculo = response.Content.ReadAsAsync<IEnumerable<MarcaVeiculo>>().Result;

                //preenchendo a lista com os dados retornados da variável
                gdvMarcaVeiculo.DataSource = marcaVeiculo;
                gdvMarcaVeiculo.DataBind();
            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }
        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            getAll();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["MarcaEditar"] = null;
            Response.Redirect("cadastroMarcaVeiculo.aspx");
        }

        protected void gdvMarcaVeiculo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvMarcaVeiculo.PageIndex = e.NewPageIndex;
            getAll();
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