using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LocadoraVeiculos.Class;
using LocadoraVeiculos.ENT;
using LocadoraVeiculos.UTILS;

namespace LocadoraVeiculos
{
    public partial class CadastroClientes : System.Web.UI.Page
    {
        Clientes cliente = Clientes.GetClientes();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["ClienteEditar"]))
                {
                    hdnIdCliente.Value = Session["ClienteEditar"].ToString();
                    ENTClientes entCliente = new ENTClientes();

                    entCliente.IdCliente = Convert.ToInt32(hdnIdCliente.Value);
                    List<ENTClientes> lista = cliente.ConsultarDados(entCliente);

                    if (lista != null)
                    {
                        txtNome.Text = lista[0].NomeCliente.ToString();
                        txtCpf.Text = lista[0].CpfCnpjCliente.ToString();
                    }
                }
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            ENTClientes entCliente = new ENTClientes();

            entCliente.NomeCliente = txtNome.Text;
            entCliente.CpfCnpjCliente= txtCpf.Text;
  
            if (!string.IsNullOrEmpty(hdnIdCliente.Value))
            {
                entCliente.IdCliente = Convert.ToInt32(hdnIdCliente.Value);

                if (cliente.SalvarDados(entCliente))
                {
                    Tratamentos.Alerta("Dados alterados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = cliente.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }
            else
            {
                if (cliente.CadastrarDados(entCliente))
                {
                    Tratamentos.Alerta("Dados cadastrados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = cliente.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }

            entCliente = null;
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdCliente.Value))
            {
                ENTClientes entCliente = new ENTClientes();
                entCliente.IdCliente = Convert.ToInt32(hdnIdCliente.Value);
                if (cliente.ExcluirDados(entCliente))
                {
                    Tratamentos.Alerta("Dados Excluidos com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = cliente.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
                entCliente = null;
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