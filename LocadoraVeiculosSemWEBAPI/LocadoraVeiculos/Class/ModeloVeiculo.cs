using LocadoraVeiculos.ENT;
using System.Collections.Generic;

namespace LocadoraVeiculos.Class
{
    public sealed class ModeloVeiculo
    {
        private static readonly ModeloVeiculo _instance = new ModeloVeiculo();
        private string _erro = string.Empty;
        AcoesGenericas<ENTModeloVeiculo> acoes = new AcoesGenericas<ENTModeloVeiculo>();

        private ModeloVeiculo()
        {

        }

        public string GetRetornoErro()
        {
            return this._erro;
        }


        public static ModeloVeiculo GetModeloVeiculo()
        {
            return _instance;
        }

        public bool CadastrarDados(ENTModeloVeiculo veiculo)
        {
            bool retorno = false;
            retorno = acoes.CadastrarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool SalvarDados(ENTModeloVeiculo veiculo) {
            bool retorno = false;
            retorno = acoes.SalvarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool ExcluirDados(ENTModeloVeiculo veiculo)
        {
            bool retorno = false;
            retorno = acoes.ExcluirDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public List<ENTModeloVeiculo> ListarDados(ENTModeloVeiculo veiculo)
        {
            List<ENTModeloVeiculo> lista = acoes.ListarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public List<ENTModeloVeiculo> ConsultarDados(ENTModeloVeiculo veiculo)
        {
            List<ENTModeloVeiculo> lista = acoes.ConsultarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public int ContarDados(ENTModeloVeiculo veiculo)
        {
            int retorno = 0;
            retorno = acoes.ContarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

    }
}