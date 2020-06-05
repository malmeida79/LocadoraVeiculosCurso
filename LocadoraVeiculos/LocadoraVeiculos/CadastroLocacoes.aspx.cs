using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using LocadoraVeiculos.Models;
using LocadoraVeiculos.UTILS;


namespace LocadoraVeiculos
{
    public partial class cadastroLocacoes : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public cadastroLocacoes()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:57627");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private void getAllClientes()
        {
            HttpResponseMessage response;

            //chamando a api pela url                       
            response = client.GetAsync("api/clientes").Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var clientes = response.Content.ReadAsAsync<IEnumerable<Clientes>>().Result;

                //preenchendo a lista com os dados retornados da variável
                gdvClientes.DataSource = clientes;
                gdvClientes.DataBind();
            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }
        }

        private void getAllVeiculos()
        {
            HttpResponseMessage response;

            //chamando a api pela url            
            response = client.GetAsync("api/veiculos").Result;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getAllClientes();
                getAllVeiculos();
            }
        }

        protected void gdvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvClientes.PageIndex = e.NewPageIndex;
            getAllClientes();
        }

        protected void gdvVeiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvVeiculos.PageIndex = e.NewPageIndex;
            getAllVeiculos();
        }

        protected void gdvVeiculos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    addButton.Text = "Selecionar";

                    string status = e.Row.Cells[6].Text;

                    if (status.ToUpper() == "LOCADO")
                    {
                        e.Row.BackColor = System.Drawing.Color.LightCoral;
                        addButton.Visible = false;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                        addButton.Visible = true;
                    }
                }
            }
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
            hdnIdClienteSelecionado.Value = gdvClientes.Rows[e.NewEditIndex].Cells[1].Text;
            lblNome.Text = gdvClientes.Rows[e.NewEditIndex].Cells[2].Text;
            gdvClientes.EditIndex = -1;
            getAllClientes();
        }

        protected void gdvVeiculos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            hdnIdVeiculoSelecionado.Value = gdvVeiculos.Rows[e.NewEditIndex].Cells[1].Text;
            lblVeiculo.Text = gdvVeiculos.Rows[e.NewEditIndex].Cells[2].Text;
            gdvVeiculos.EditIndex = -1;
            getAllVeiculos();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        protected void btnLocacao_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(hdnIdClienteSelecionado.Value)) || (hdnIdClienteSelecionado.Value == "0"))
            {
                Tratamentos.Alerta("Necessário selecionar um cliente para locação");
                return;
            }

            if ((string.IsNullOrEmpty(hdnIdVeiculoSelecionado.Value)) || (hdnIdVeiculoSelecionado.Value == "0"))
            {
                Tratamentos.Alerta("Necessário selecionar um veiculo para locação");
                return;
            }

            HttpResponseMessage response = client.GetAsync("api/locacoes").Result;

            var locacaco = new Locacoes();

            locacaco.IdCliente = Convert.ToInt32(hdnIdClienteSelecionado.Value);
            locacaco.IdVeiculo = Convert.ToInt32(hdnIdVeiculoSelecionado.Value);

            response = client.PostAsJsonAsync("api/locacoes", locacaco).Result;

            if (response.IsSuccessStatusCode)
            {
                Tratamentos.Alerta("Locação realizada com sucesso !!!");
                LimpaCampos();
            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }

            locacaco = null;
        }

        private void LimpaCampos() {
            hdnIdClienteSelecionado.Value = string.Empty;
            hdnIdVeiculoSelecionado.Value = string.Empty;
            lblNome.Text = string.Empty;
            lblVeiculo.Text = string.Empty;
            gdvClientes.EditIndex = -1;
            gdvVeiculos.EditIndex = -1;
            getAllClientes();
            getAllVeiculos();
        }
    }
}