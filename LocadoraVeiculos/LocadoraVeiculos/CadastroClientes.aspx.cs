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
    public partial class CadastroClientes : System.Web.UI.Page
    {
        HttpClient client;
        Uri usuarioUri;

        public CadastroClientes()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:57627");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private Clientes getCliente(int idCliente)
        {

            HttpResponseMessage response;
            var cliente = new Clientes();

            //chamando a api pela url            
            response = client.GetAsync("api/clientes/?idCliente=" + idCliente).Result;

            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var dados = response.Content.ReadAsAsync<IEnumerable<Clientes>>().Result.ToList();

                if (dados.Count > 0)
                {
                    cliente.IdCliente = idCliente;
                    cliente.NomeCliente = dados[0].NomeCliente.ToString();
                    cliente.CpfCnpjCliente = dados[0].CpfCnpjCliente.ToString();
                }
                else
                {
                    cliente = null;
                }

            }
            else
            {
                Tratamentos.Alerta("Ocorreu um erro:" + response.StatusCode.ToString() + " - " + response.ReasonPhrase.ToString() + ".");
            }

            return cliente;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["ClienteEditar"]))
                {
                    hdnIdCliente.Value = Session["ClienteEditar"].ToString();

                    Clientes cliente = new Clientes();

                    cliente = getCliente(Convert.ToInt32(hdnIdCliente.Value));

                    if (cliente != null)
                    {
                        txtNome.Text = cliente.NomeCliente.ToString();
                        txtCpf.Text = cliente.CpfCnpjCliente.ToString();
                    }
                }
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/clientes").Result;

            var cliente = new Clientes();

            cliente.NomeCliente = txtNome.Text;
            cliente.CpfCnpjCliente = txtCpf.Text;

            if (!string.IsNullOrEmpty(hdnIdCliente.Value))
            {
                cliente.IdCliente = Convert.ToInt32(hdnIdCliente.Value);

                response = client.PutAsJsonAsync("api/clientes", cliente).Result;

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
                response = client.PostAsJsonAsync("api/clientes", cliente).Result;

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

            cliente = null;
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = client.GetAsync("api/clientes").Result;

            if (!string.IsNullOrEmpty(hdnIdCliente.Value))
            {

                response = client.DeleteAsync("api/clientes/" + Convert.ToInt32(hdnIdCliente.Value)).Result;

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
            Response.Redirect("ListaClientes.aspx");
        }

        private void LimpaCampos()
        {
            txtCpf.Text = "";
            txtNome.Text = "";
            hdnIdCliente.Value = "";
            Session["ClienteEditar"] = null;
        }
    }
}