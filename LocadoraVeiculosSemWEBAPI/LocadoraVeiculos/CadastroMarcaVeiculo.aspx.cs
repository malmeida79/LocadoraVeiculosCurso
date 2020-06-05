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
    public partial class CadastroMarcaVeiculo : System.Web.UI.Page
    {
        MarcaVeiculo marcaVeiculo = MarcaVeiculo.GetMarcaVeiculo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["MarcaEditar"]))
                {
                    hdnIdMarca.Value = Session["MarcaEditar"].ToString();
                    ENTMarcaVeiculo entMarcaVeiculo = new ENTMarcaVeiculo();

                    entMarcaVeiculo.IdMarcaVeiculo = Convert.ToInt32(hdnIdMarca.Value);
                    List<ENTMarcaVeiculo> lista = marcaVeiculo.ConsultarDados(entMarcaVeiculo);

                    if (lista != null)
                    {
                        txtMarca.Text = lista[0].MarcaVeiculo.ToString();
                    }
                }
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            ENTMarcaVeiculo entMarcaVeiculo = new ENTMarcaVeiculo();

            entMarcaVeiculo.MarcaVeiculo = txtMarca.Text;
  
            if (!string.IsNullOrEmpty(hdnIdMarca.Value))
            {
                entMarcaVeiculo.IdMarcaVeiculo = Convert.ToInt32(hdnIdMarca.Value);

                if (marcaVeiculo.SalvarDados(entMarcaVeiculo))
                {
                    Tratamentos.Alerta("Dados alterados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = marcaVeiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }
            else
            {
                if (marcaVeiculo.CadastrarDados(entMarcaVeiculo))
                {
                    Tratamentos.Alerta("Dados cadastrados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = marcaVeiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }

            entMarcaVeiculo = null;
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdMarca.Value))
            {
                ENTMarcaVeiculo entMarcaVeiculo = new ENTMarcaVeiculo();
                entMarcaVeiculo.IdMarcaVeiculo = Convert.ToInt32(hdnIdMarca.Value);
                if (marcaVeiculo.ExcluirDados(entMarcaVeiculo))
                {
                    Tratamentos.Alerta("Dados Excluidos com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = marcaVeiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
                entMarcaVeiculo = null;
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