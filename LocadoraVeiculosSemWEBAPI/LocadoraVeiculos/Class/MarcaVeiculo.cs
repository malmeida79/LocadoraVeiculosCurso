using LocadoraVeiculos.ENT;
using System.Collections.Generic;

namespace LocadoraVeiculos.Class
{
    public sealed class MarcaVeiculo
    {
        private static readonly MarcaVeiculo _instance = new MarcaVeiculo();
        private string _erro = string.Empty;
        AcoesGenericas<ENTMarcaVeiculo> acoes = new AcoesGenericas<ENTMarcaVeiculo>();

        private MarcaVeiculo()
        {

        }


        public string GetRetornoErro()
        {
            return this._erro;
        }


        public static MarcaVeiculo GetMarcaVeiculo()
        {
            return _instance;
        }

        public bool CadastrarDados(ENTMarcaVeiculo veiculo)
        {
            bool retorno = false;
            retorno = acoes.CadastrarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool SalvarDados(ENTMarcaVeiculo veiculo) {
            bool retorno = false;
            retorno = acoes.SalvarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool ExcluirDados(ENTMarcaVeiculo veiculo)
        {
            bool retorno = false;
            retorno = acoes.ExcluirDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public List<ENTMarcaVeiculo> ListarDados(ENTMarcaVeiculo veiculo)
        {
            List<ENTMarcaVeiculo> lista = acoes.ListarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public List<ENTMarcaVeiculo> ConsultarDados(ENTMarcaVeiculo veiculo)
        {
            List<ENTMarcaVeiculo> lista = acoes.ConsultarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public int ContarDados(ENTMarcaVeiculo veiculo)
        {
            int retorno = 0;
            retorno = acoes.ContarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

    }
}