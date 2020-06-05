using LocadoraVeiculos.ENT;
using System.Collections.Generic;

namespace LocadoraVeiculos.Class
{
    public sealed class Veiculos
    {
        private static readonly Veiculos _instance = new Veiculos();
        private string _erro = string.Empty;
        AcoesGenericas<ENTVeiculos> acoes = new AcoesGenericas<ENTVeiculos>();

        private Veiculos()
        {

        }

        public static Veiculos GetVeiculos()
        {
            return _instance;
        }

        public string GetRetornoErro()
        {
            return this._erro;
        }

        public bool SalvarDados(ENTVeiculos veiculo)
        {
            bool retorno = false;
            retorno = acoes.SalvarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool CadastrarDados(ENTVeiculos veiculo)
        {
            bool retorno = false;
            retorno = acoes.CadastrarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool ExcluirDados(ENTVeiculos veiculo)
        {
            bool retorno = false;
            retorno = acoes.ExcluirDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public List<ENTVeiculos> ListarDados(ENTVeiculos veiculo)
        {
            List<ENTVeiculos> lista = acoes.ListarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public List<ENTVeiculos> ConsultarDados(ENTVeiculos veiculo)
        {
            List<ENTVeiculos> lista = acoes.ConsultarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public int ContarDados(ENTVeiculos veiculo)
        {
            int retorno = 0;
            retorno = acoes.ContarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

    }
}