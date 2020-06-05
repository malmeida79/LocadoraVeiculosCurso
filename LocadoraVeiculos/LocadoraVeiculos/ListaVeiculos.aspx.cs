using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.UTILS;

namespace LocadoraVeiculos
{
    public partial class ListaVeiculos : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public ListaVeiculos()
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

        private void getAll(string placa = "")
        {

            HttpResponseMessage response;

            //chamando a api pela url            
            if (!string.IsNullOrEmpty(placa))
            {
                response = client.GetAsync("api/veiculos/?placa=" + placa).Result;
            }
            else
            {
                response = client.GetAsync("api/veiculos").Result;
            }

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var veiculos = response.Content.ReadAsAsync<IEnumerable<Veiculos>>().Result;

                //preenchendo a lista com os dados retornados da variável
                gdvVeiculos.DataSource = veiculos;
                gdvVeiculos.DataBind();
            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }
        }

        protected void cmdPesquisa_Click(object sender, EventArgs e)
        {
            getAll(txtPlaca.Text);
        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            getAll();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["VeiculoEditar"] = null;
            Response.Redirect("cadastroVeiculos.aspx");
        }

        protected void gdvVeiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvVeiculos.PageIndex = e.NewPageIndex;
            getAll();
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