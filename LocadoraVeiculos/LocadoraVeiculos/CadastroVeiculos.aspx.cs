using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using LocadoraVeiculos.UTILS;
using LocadoraVeiculos.Models;
using System.Net.Http;

namespace LocadoraVeiculos
{
    public partial class cadastroVeiculos : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public cadastroVeiculos()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:57627");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private Veiculos getVeiculo(int idVeiculo)
        {

            HttpResponseMessage response;
            var veiculo = new Veiculos();

            //chamando a api pela url            
            response = client.GetAsync("api/veiculos/?idveiculo=" + idVeiculo).Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var dados = response.Content.ReadAsAsync<IEnumerable<Veiculos>>().Result.ToList();

                if (dados.Count > 0)
                {
                    veiculo.IdVeiculo = idVeiculo;
                    veiculo.PlacaVeiculo = dados[0].PlacaVeiculo.ToString();
                    veiculo.CorVeiculo = dados[0].CorVeiculo.ToString();
                    veiculo.IdMarcaVeiculo = dados[0].IdMarcaVeiculo;
                    veiculo.IdModeloVeiculo = dados[0].IdModeloVeiculo;
                }
                else
                {
                    veiculo = null;
                }

            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }

            return veiculo;
        }

        private void getAllMarcas(string selecionado = "")
        {

            HttpResponseMessage response;

            //chamando a api pela url            
            response = client.GetAsync("api/MarcaVeiculo").Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var marcaVeiculo = response.Content.ReadAsAsync<IEnumerable<MarcaVeiculo>>().Result;

                //preenchendo a lista com os dados retornados da variável
                cboMarca.Preencher(marcaVeiculo.ToList(), "nomeMarcaVeiculo", "idMarcaVeiculo", true);

                if (!string.IsNullOrEmpty(selecionado))
                {
                    cboMarca.SetSelectedValue(selecionado);
                }
            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }
        }

        private void getAllModelos(string selecionado = "")
        {

            HttpResponseMessage response;

            //chamando a api pela url            
            response = client.GetAsync("api/ModeloVeiculo").Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var modeloVeiculo = response.Content.ReadAsAsync<IEnumerable<ModeloVeiculo>>().Result;

                //preenchendo a lista com os dados retornados da variável
                cboModelo.Preencher(modeloVeiculo.ToList(), "nomeModeloVeiculo", "idModeloVeiculo", true);

                if (!string.IsNullOrEmpty(selecionado))
                {
                    cboModelo.SetSelectedValue(selecionado);
                }

                modeloVeiculo = null;
            }

            //Se der erro na chamada, mostra o status do código de erro.
            else
                Response.Write(response.StatusCode.ToString() + " - " + response.ReasonPhrase);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["VeiculoEditar"]))
                {
                    hdnIdVeiculo.Value = Session["VeiculoEditar"].ToString();
                    Veiculos veiculo = new Veiculos();

                    veiculo = getVeiculo(Convert.ToInt32(hdnIdVeiculo.Value));

                    if (veiculo != null)
                    {
                        txtPlaca.Text = veiculo.PlacaVeiculo.ToString();
                        txtCor.Text = veiculo.CorVeiculo.ToString();
                        getAllMarcas(veiculo.IdMarcaVeiculo.ToString());
                        getAllModelos(veiculo.IdModeloVeiculo.ToString());
                    }

                }
                else
                {
                    getAllMarcas();
                    getAllModelos();
                }
            }
        }

        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaVeiculos.aspx");
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/veiculos").Result;

            if (!string.IsNullOrEmpty(hdnIdVeiculo.Value))
            {

                response = client.DeleteAsync("api/veiculos/" + Convert.ToInt32(hdnIdVeiculo.Value)).Result;

                if (response.IsSuccessStatusCode)
                {
                    Tratamentos.Alerta("Dados excluidos com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
                }
            }
            else
            {
                Tratamentos.Alerta("Necessário selecionar um item para poder excluir.");
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {

            HttpResponseMessage response = client.GetAsync("api/veiculos").Result;

            Veiculos veiculos = new Veiculos();
            veiculos.PlacaVeiculo = txtPlaca.Text;
            veiculos.CorVeiculo = txtCor.Text;
            veiculos.IdMarcaVeiculo = Convert.ToInt32(cboMarca.SelectedValue);
            veiculos.IdModeloVeiculo = Convert.ToInt32(cboModelo.SelectedValue);

            if (!string.IsNullOrEmpty(hdnIdVeiculo.Value))
            {

                veiculos.IdVeiculo = Convert.ToInt32(hdnIdVeiculo.Value);

                response = client.PutAsJsonAsync("api/veiculos", veiculos).Result;

                if (response.IsSuccessStatusCode)
                {
                    Tratamentos.Alerta("Dados alterados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
                }
            }
            else
            {
                response = client.PostAsJsonAsync("api/veiculos", veiculos).Result;

                if (response.IsSuccessStatusCode)
                {
                    Tratamentos.Alerta("Dados cadastrados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
                }
            }

            veiculos = null;
        }

        private void LimpaCampos()
        {
            txtPlaca.Text = "";
            txtCor.Text = "";
            hdnIdVeiculo.Value = "";
            Session["VeiculoEditar"] = null;
            cboMarca.SelectedIndex = 0;
            cboModelo.SelectedIndex = 0;
        }
    }
}