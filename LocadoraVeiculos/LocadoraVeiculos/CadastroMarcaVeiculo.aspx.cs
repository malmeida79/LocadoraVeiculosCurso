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
     public partial class CadastroMarcaVeiculo : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public CadastroMarcaVeiculo()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:57627");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private MarcaVeiculo getMarca(int idMarcaVeiculo)
        {

            HttpResponseMessage response;
            var marcaVeiculo = new MarcaVeiculo();

            //chamando a api pela url            
            response = client.GetAsync("api/marcaVeiculo/?idMarcaVeiculo=" + idMarcaVeiculo).Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var dados = response.Content.ReadAsAsync<IEnumerable<MarcaVeiculo>>().Result.ToList();

                if (dados.Count > 0)
                {
                    marcaVeiculo.IdMarcaVeiculo = idMarcaVeiculo;
                    marcaVeiculo.nomeMarcaVeiculo = dados[0].nomeMarcaVeiculo.ToString();
                }
                else
                {
                    marcaVeiculo = null;
                }

            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }

            return marcaVeiculo;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["MarcaEditar"]))
                {
                    hdnIdMarca.Value = Session["MarcaEditar"].ToString();
                    MarcaVeiculo marcaVeiculo = new MarcaVeiculo();

                    marcaVeiculo = getMarca(Convert.ToInt32(hdnIdMarca.Value));

                    if (marcaVeiculo != null)
                    {
                        txtMarca.Text = marcaVeiculo.nomeMarcaVeiculo.ToString();
                    }
                }
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/marcaVeiculo").Result;

            var marcaVeiculo = new MarcaVeiculo();

            marcaVeiculo.nomeMarcaVeiculo = txtMarca.Text;

            if (!string.IsNullOrEmpty(hdnIdMarca.Value))
            {
                marcaVeiculo.IdMarcaVeiculo = Convert.ToInt32(hdnIdMarca.Value);

                response = client.PutAsJsonAsync("api/marcaVeiculo", marcaVeiculo).Result;

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
                response = client.PostAsJsonAsync("api/marcaVeiculo", marcaVeiculo).Result;

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

            marcaVeiculo = null;
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/marcaVeiculo").Result;

            if (!string.IsNullOrEmpty(hdnIdMarca.Value))
            {

                response = client.DeleteAsync("api/marcaVeiculo/" + Convert.ToInt32(hdnIdMarca.Value)).Result;

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
            Response.Redirect("ListaMarcas.aspx");
        }

        private void LimpaCampos()
        {
            txtMarca.Text = "";
            hdnIdMarca.Value = "";
            Session["MarcaEditar"] = null;
        }
    }
}