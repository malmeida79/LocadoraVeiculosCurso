using LocadoraVeiculosWebApi.Models;
using LocadoraVeiculosWebApi.UTIL;
using System.Collections.Generic;
using System.Web.Http;

namespace LocadoraVeiculosWebApi.Controllers
{
    public class LocacoesController : ApiController
    {
        AcoesGenericas<Locacoes> acoes = new AcoesGenericas<Locacoes>();
        private string _erro = string.Empty;

        public string GetErro
        {
            get
            {
                return this._erro;
            }
        }

        public IEnumerable<Locacoes> GetLocacoes()
        {
            Locacoes locacoes = new Locacoes();
            return acoes.ListarDados(locacoes);
        }

        public IEnumerable<Locacoes> GetLocacao(int idLocacao)
        {
            Locacoes locacoes = new Locacoes();
            locacoes.IdLocacao = idLocacao;
            return acoes.ListarDados(locacoes);
        }

        public IEnumerable<Locacoes> GetLocacao(string nome, string placa)
        {
            Locacoes locacoes = new Locacoes();

            if (string.IsNullOrEmpty(nome))
            {
                locacoes.NomeCliente = nome;
            }

            if (string.IsNullOrEmpty(placa))
            {
                locacoes.PlacaVeiculo = placa;
            }

            return acoes.ListarDados(locacoes);
        }

        public bool Post(Locacoes locacao)
        {
            bool retorno = false;

            try
            {
                if (acoes.CadastrarDados(locacao))
                {
                    retorno = true;
                }
                else
                {
                    _erro = acoes.GetRetornoErro;
                    retorno = false;
                }
            }
            catch (System.Exception ex)
            {
                _erro = ex.Message.ToString();
                retorno = false;
            }

            return retorno;
        }

        public bool Put(Locacoes locacao)
        {
            bool retorno = false;

            try
            {
                if (acoes.SalvarDados(locacao))
                {
                    retorno = true;
                }
                else
                {
                    _erro = acoes.GetRetornoErro;
                    retorno = false;
                }
            }
            catch (System.Exception ex)
            {
                _erro = ex.Message.ToString();
                retorno = false;
            }

            return retorno;
        }

        public bool Delete(int id)
        {
            bool retorno = false;

            Locacoes locacao = new Locacoes();
            locacao.IdLocacao = id;

            try
            {
                if (acoes.ExcluirDados(locacao))
                {
                    retorno = true;
                }
                else
                {
                    _erro = acoes.GetRetornoErro;
                    retorno = false;
                }
            }
            catch (System.Exception ex)
            {
                _erro = ex.Message.ToString();
                retorno = false;
            }

            return retorno;
        }
    }
}
