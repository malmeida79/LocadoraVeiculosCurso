using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.UTILS;

namespace LocadoraVeiculos
{
    public partial class ListaLocacoes : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public ListaLocacoes()
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

        private void getAll(string nome = "", string placa = "")
        {

            HttpResponseMessage response;

            //chamando a api pela url            
            if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(placa))
            {
                response = client.GetAsync("api/Locacoes/?nome=" + nome + "&placa=" + placa).Result;
            }
            else
            {
                response = client.GetAsync("api/Locacoes").Result;
            }

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var modeloVeiculo = response.Content.ReadAsAsync<IEnumerable<Locacoes>>().Result;

                //preenchendo a lista com os dados retornados da variável
                gdvLocacoes.DataSource = modeloVeiculo;
                gdvLocacoes.DataBind();
            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }
        }

        protected void cmdPesquisa_Click(object sender, EventArgs e)
        {
            getAll(txtNomeCliente.Text, txtPlaca.Text);
        }

        protected void cmdListar_Click(object sender, EventArgs e)
        {
            getAll();
        }

        protected void cmdNovo_Click(object sender, EventArgs e)
        {
            Session["LocacaoEditar"] = null;
            Response.Redirect("cadastroLocacoes.aspx");
        }

        protected void gdvLocacoes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvLocacoes.PageIndex = e.NewPageIndex;
            getAll();
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

                string dataLoc = e.Row.Cells[7].Text;
                e.Row.Cells[7].Text = TrataData(dataLoc);

                string dataDev = e.Row.Cells[8].Text;
                e.Row.Cells[8].Text = TrataData(dataDev);
            }
        }

        protected void gdvLocacoes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["LocacaoEditar"] = gdvLocacoes.Rows[e.NewEditIndex].Cells[1].Text;
            Response.Redirect("cadastroLocacoes.aspx");
        }

        public string TrataData(string dataRecebida)
        {
            DateTime datateste;
            string retorno;

            if (DateTime.TryParse(dataRecebida, out datateste))
            {
                retorno = datateste.ToShortDateString();
            }
            else
            {
                retorno = "";
            }

            if (retorno.Replace(" ", "") == @"01/01/0001")
            {
                retorno = "";
            }

            return retorno;
        }

        protected void gdvLocacoes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse((string)e.CommandArgument);

            if (e.CommandName == "devolver")
            {
                HttpResponseMessage response = client.GetAsync("api/locacoes").Result;

                var locacao = new Locacoes();
                locacao.IdLocacao = Convert.ToInt32(gdvLocacoes.Rows[index].Cells[1].Text);

                response = client.PutAsJsonAsync("api/locacoes", locacao).Result;

                if (response.IsSuccessStatusCode)
                {
                    Tratamentos.Alerta("Veiculo devolvido com sucesso.");
                }
                else
                {
                    Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
                }

            }

        }
    }
}