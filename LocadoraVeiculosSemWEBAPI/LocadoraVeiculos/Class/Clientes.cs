using LocadoraVeiculos.ENT;
using System.Collections.Generic;

namespace LocadoraVeiculos.Class
{
    public sealed class Clientes
    {
        private static readonly Clientes _instance = new Clientes();
        private string _erro = string.Empty;
        AcoesGenericas<ENTClientes> acoes = new AcoesGenericas<ENTClientes>();

        private Clientes()
        {

        }

        public static Clientes GetClientes()
        {
            return _instance;
        }


        public string GetRetornoErro()
        {
            return this._erro;
        }

        public bool CadastrarDados(ENTClientes veiculo)
        {
            bool retorno = false;
            retorno = acoes.CadastrarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool SalvarDados(ENTClientes veiculo) {
            bool retorno = false;
            retorno = acoes.SalvarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public bool ExcluirDados(ENTClientes veiculo)
        {
            bool retorno = false;
            retorno = acoes.ExcluirDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

        public List<ENTClientes> ListarDados(ENTClientes veiculo)
        {
            List<ENTClientes> lista = acoes.ListarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public List<ENTClientes> ConsultarDados(ENTClientes veiculo)
        {
            List<ENTClientes> lista = acoes.ConsultarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return lista;
        }

        public int ContarDados(ENTClientes veiculo)
        {
            int retorno = 0;
            retorno = acoes.ContarDados(veiculo);
            _erro = acoes.GetRetornoErro;
            return retorno;
        }

    }
}