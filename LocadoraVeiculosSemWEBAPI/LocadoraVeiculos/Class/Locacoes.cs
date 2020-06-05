using LocadoraVeiculos.ENT;
using System.Collections.Generic;

namespace LocadoraVeiculos.Class
{
    public sealed class Locacoes
    {
        private static readonly Locacoes _instance = new Locacoes();
        private string _erro = string.Empty;
        AcoesGenericas<ENTLocacoes> acoes = new AcoesGenericas<ENTLocacoes>();

        private Locacoes()
        {

        }


        public string GetRetornoErro()
        {
            return this._erro;
        }


        public static Locacoes GetLocacoes()
        {
            return _instance;
        }

        public bool SalvarDados(ENTLocacoes veiculo) {
            bool retorno = false;
            retorno = acoes.SalvarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool ExcluirDados(ENTLocacoes veiculo)
        {
            bool retorno = false;
            retorno = acoes.ExcluirDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public List<ENTLocacoes> ListarDados(ENTLocacoes veiculo)
        {
            List<ENTLocacoes> lista = acoes.ListarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public List<ENTLocacoes> ConsultarDados(ENTLocacoes veiculo)
        {
            List<ENTLocacoes> lista = acoes.ConsultarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public int ContarDados(ENTLocacoes veiculo)
        {
            int retorno = 0;
            retorno = acoes.ContarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

    }
}