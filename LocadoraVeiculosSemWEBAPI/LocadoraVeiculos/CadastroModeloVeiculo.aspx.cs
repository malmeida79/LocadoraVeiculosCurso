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
    public partial class CadastroModeloVeiculo : System.Web.UI.Page
    {
        ModeloVeiculo modeloVeiculo = ModeloVeiculo.GetModeloVeiculo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["ModeloEditar"]))
                {
                    hdnIdMarca.Value = Session["ModeloEditar"].ToString();
                    ENTModeloVeiculo entModeloVeiculo = new ENTModeloVeiculo();

                    entModeloVeiculo.IdModeloVeiculo = Convert.ToInt32(hdnIdMarca.Value);
                    List<ENTModeloVeiculo> lista = modeloVeiculo.ConsultarDados(entModeloVeiculo);

                    if (lista != null)
                    {
                        txtModelo.Text = lista[0].ModeloVeiculo.ToString();
                    }
                }
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            ENTModeloVeiculo entModeloVeiculo = new ENTModeloVeiculo();

            entModeloVeiculo.ModeloVeiculo = txtModelo.Text;
  
            if (!string.IsNullOrEmpty(hdnIdMarca.Value))
            {
                entModeloVeiculo.IdModeloVeiculo = Convert.ToInt32(hdnIdMarca.Value);

                if (modeloVeiculo.SalvarDados(entModeloVeiculo))
                {
                    Tratamentos.Alerta("Dados alterados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = modeloVeiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }
            else
            {
                if (modeloVeiculo.CadastrarDados(entModeloVeiculo))
                {
                    Tratamentos.Alerta("Dados cadastrados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = modeloVeiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }

            entModeloVeiculo = null;
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdMarca.Value))
            {
                ENTModeloVeiculo entModeloVeiculo = new ENTModeloVeiculo();
                entModeloVeiculo.IdModeloVeiculo = Convert.ToInt32(hdnIdMarca.Value);
                if (modeloVeiculo.ExcluirDados(entModeloVeiculo))
                {
                    Tratamentos.Alerta("Dados Excluidos com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = modeloVeiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
                entModeloVeiculo = null;
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
            hdnIdMarca.Value = "";
            Session["ModeloEditar"] = null;
        }
    }
}