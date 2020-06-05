using LocadoraVeiculosWebApi.Models;
using LocadoraVeiculosWebApi.UTIL;
using System.Collections.Generic;
using System.Web.Http;

namespace LocadoraVeiculosWebApi.Controllers
{
    public class VeiculosController : ApiController
    {
        AcoesGenericas<Veiculos> acoes = new AcoesGenericas<Veiculos>();
        private string _erro = string.Empty;

        public string GetErro
        {
            get
            {
                return this._erro;
            }
        }

        public IEnumerable<Veiculos> GetVeiculos()
        {
            Veiculos veiculo = new Veiculos();
            return acoes.ListarDados(veiculo);
        }

        public IEnumerable<Veiculos> GetVeiculos(string placa)
        {
            Veiculos veiculo = new Veiculos();
            veiculo.IdVeiculo = 0;
            veiculo.PlacaVeiculo = placa;
            return acoes.ListarDados(veiculo);
        }
                
        public IEnumerable<Veiculos> GetVeiculos(int idVeiculo)
        {
            Veiculos veiculo = new Veiculos();
            veiculo.IdVeiculo = idVeiculo;
            return acoes.ListarDados(veiculo);
        }

        public bool Post(Veiculos veiculo)
        {
            bool retorno = false;

            try
            {
                if (acoes.CadastrarDados(veiculo))
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

        public bool Put(Veiculos veiculo)
        {
            bool retorno = false;

            try
            {
                if (acoes.SalvarDados(veiculo))
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

            Veiculos veiculo = new Veiculos();
            veiculo.IdVeiculo = id;

            try
            {
                if (acoes.ExcluirDados(veiculo))
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
