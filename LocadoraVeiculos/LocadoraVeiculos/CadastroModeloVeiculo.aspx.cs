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
    public partial class CadastroModeloVeiculo : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public CadastroModeloVeiculo()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:57627");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private ModeloVeiculo getModelo(int idModeloVeiculo)
        {

            HttpResponseMessage response;
            var modeloVeiculo = new ModeloVeiculo();

            //chamando a api pela url            
            response = client.GetAsync("api/modeloVeiculo/?idModeloVeiculo=" + idModeloVeiculo).Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var dados = response.Content.ReadAsAsync<IEnumerable<ModeloVeiculo>>().Result.ToList();

                if (dados.Count > 0)
                {
                    modeloVeiculo.IdModeloVeiculo = idModeloVeiculo;
                    modeloVeiculo.nomeModeloVeiculo = dados[0].nomeModeloVeiculo.ToString();
                }
                else
                {
                    modeloVeiculo = null;
                }

            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }

            return modeloVeiculo;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["ModeloEditar"]))
                {
                    hdnIdModelo.Value = Session["ModeloEditar"].ToString();
                    ModeloVeiculo modeloVeiculo = new ModeloVeiculo();

                    modeloVeiculo = getModelo(Convert.ToInt32(hdnIdModelo.Value));

                    if (modeloVeiculo != null)
                    {
                        txtModelo.Text = modeloVeiculo.nomeModeloVeiculo.ToString();
                    }
                }
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/modeloVeiculo").Result;

            var modeloVeiculo = new ModeloVeiculo();

            modeloVeiculo.nomeModeloVeiculo = txtModelo.Text;

            if (!string.IsNullOrEmpty(hdnIdModelo.Value))
            {
                modeloVeiculo.IdModeloVeiculo = Convert.ToInt32(hdnIdModelo.Value);

                response = client.PutAsJsonAsync("api/modeloVeiculo", modeloVeiculo).Result;

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
                response = client.PostAsJsonAsync("api/modeloVeiculo", modeloVeiculo).Result;

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

            modeloVeiculo = null;
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/modeloVeiculo").Result;

            if (!string.IsNullOrEmpty(hdnIdModelo.Value))
            {

                response = client.DeleteAsync("api/modeloVeiculo/" + Convert.ToInt32(hdnIdModelo.Value)).Result;

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

        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaModelos.aspx");
        }

        private void LimpaCampos()
        {
            txtModelo.Text = "";
            hdnIdModelo.Value = "";
            Session["ModeloEditar"] = null;
        }
    }
}