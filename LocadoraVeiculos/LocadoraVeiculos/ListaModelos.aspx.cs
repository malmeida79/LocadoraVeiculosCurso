using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.UTILS;

namespace LocadoraVeiculos
{
    public partial class ListaModelos : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public ListaModelos()
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
            response = client.GetAsync("api/modeloveiculo").Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var modeloVeiculo = response.Content.ReadAsAsync<IEnumerable<ModeloVeiculo>>().Result;

                //preenchendo a lista com os dados retornados da variável
                gdvModeloVeiculo.DataSource = modeloVeiculo;
                gdvModeloVeiculo.DataBind();
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
            Session["ModeloEditar"] = null;
            Response.Redirect("cadastroModeloVeiculo.aspx");
        }

        protected void gdvModeloVeiculo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvModeloVeiculo.PageIndex = e.NewPageIndex;
            getAll();
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