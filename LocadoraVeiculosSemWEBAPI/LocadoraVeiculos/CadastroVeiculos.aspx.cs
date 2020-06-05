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
    public partial class cadastroVeiculos : System.Web.UI.Page
    {
        Veiculos veiculo = Veiculos.GetVeiculos();
        MarcaVeiculo marcas = MarcaVeiculo.GetMarcaVeiculo();
        ModeloVeiculo modelos = ModeloVeiculo.GetModeloVeiculo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["VeiculoEditar"]))
                {
                    hdnIdVeiculo.Value = Session["VeiculoEditar"].ToString();
                    ENTVeiculos entVeiculo = new ENTVeiculos();

                    entVeiculo.IdVeiculo = Convert.ToInt32(hdnIdVeiculo.Value);
                    List<ENTVeiculos> lista = veiculo.ConsultarDados(entVeiculo);

                    if (lista != null)
                    {
                        txtPlaca.Text = lista[0].PlacaVeiculo.ToString();
                        txtCor.Text = lista[0].CorVeiculo.ToString();
                        CarregaMarcas(lista[0].IdMarcaVeiculo.ToString());
                        CarregaModelos(lista[0].IdModeloVeiculo.ToString());
                    }

                }
                else
                {
                    CarregaMarcas();
                    CarregaModelos();
                }
            }
        }

        public void CarregaMarcas(string selecionado = "")
        {
            ENTMarcaVeiculo marcaVeiculo = new ENTMarcaVeiculo();
            cboMarca.Preencher(marcas.ListarDados(marcaVeiculo), "marcaVeiculo", "idMarcaVeiculo", true);

            if (!string.IsNullOrEmpty(selecionado))
            {
                cboMarca.SetSelectedValue(selecionado);
            }

            marcaVeiculo = null;
        }

        public void CarregaModelos(string selecionado = "")
        {
            ENTModeloVeiculo modeloVeiculo = new ENTModeloVeiculo();
            cboModelo.Preencher(modelos.ListarDados(modeloVeiculo), "ModeloVeiculo", "idModeloVeiculo", true);

            if (!string.IsNullOrEmpty(selecionado))
            {
                cboModelo.SetSelectedValue(selecionado);
            }

            modeloVeiculo = null;
        }

        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaVeiculos.aspx");
        }

        protected void cmdExcluir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdVeiculo.Value))
            {
                ENTVeiculos entVeiculo = new ENTVeiculos();
                entVeiculo.IdVeiculo = Convert.ToInt32(hdnIdVeiculo.Value);
                if (veiculo.ExcluirDados(entVeiculo))
                {
                    Tratamentos.Alerta("Dados Excluidos com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = veiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
                entVeiculo = null;
            }
            else
            {
                Tratamentos.Alerta("Necessário selecionar um item para poder excluir.");
            }
        }

        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            ENTVeiculos entVeiculo = new ENTVeiculos();

            entVeiculo.PlacaVeiculo = txtPlaca.Text;
            entVeiculo.CorVeiculo = txtCor.Text;
            entVeiculo.IdMarcaVeiculo = Convert.ToInt32(cboMarca.SelectedValue);
            entVeiculo.IdModeloVeiculo = Convert.ToInt32(cboModelo.SelectedValue);

            if (!string.IsNullOrEmpty(hdnIdVeiculo.Value))
            {
                entVeiculo.IdVeiculo = Convert.ToInt32(hdnIdVeiculo.Value);

                if (veiculo.SalvarDados(entVeiculo))
                {
                    Tratamentos.Alerta("Dados alterados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = veiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }
            else
            {
                if (veiculo.CadastrarDados(entVeiculo))
                {
                    Tratamentos.Alerta("Dados cadastrados com sucesso.");
                    LimpaCampos();
                }
                else
                {
                    string erro = veiculo.GetRetornoErro();
                    Tratamentos.Alerta("Ocorreu um erro:" + erro + ".");
                    erro = null;
                }
            }

            entVeiculo = null;
        }

        private void LimpaCampos() {
            txtPlaca.Text = "";
            txtCor.Text = "";
            hdnIdVeiculo.Value = "";
            Session["VeiculoEditar"] = null;
            cboMarca.SelectedIndex = 0;
            cboModelo.SelectedIndex = 0;
        }
    }
}