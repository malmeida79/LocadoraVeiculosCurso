using LocadoraVeiculosWebApi.Models;
using LocadoraVeiculosWebApi.UTIL;
using System.Collections.Generic;
using System.Web.Http;

namespace LocadoraVeiculosWebApi.Controllers
{
    public class MarcaVeiculoController : ApiController
    {
        AcoesGenericas<MarcaVeiculo> acoes = new AcoesGenericas<MarcaVeiculo>();
        private string _erro = string.Empty;

        public string GetErro
        {
            get
            {
                return this._erro;
            }
        }

        public IEnumerable<MarcaVeiculo> GetMarcasVeiculos()
        {
            MarcaVeiculo marcaVeiculo = new MarcaVeiculo();
            return acoes.ListarDados(marcaVeiculo);
        }

        public IEnumerable<MarcaVeiculo> GetMarcasVeiculos(int idMarcaVeiculo)
        {
            MarcaVeiculo marcaVeiculo = new MarcaVeiculo();
            marcaVeiculo.IdMarcaVeiculo = idMarcaVeiculo;
            return acoes.ListarDados(marcaVeiculo);
        }

        public bool Post(MarcaVeiculo marcaVeiculo)
        {
            bool retorno = false;

            try
            {
                if (acoes.CadastrarDados(marcaVeiculo))
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

        public bool Put(MarcaVeiculo marcaVeiculo)
        {
            bool retorno = false;

            try
            {
                if (acoes.SalvarDados(marcaVeiculo))
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

            MarcaVeiculo marcaVeiculo = new MarcaVeiculo();
            marcaVeiculo.IdMarcaVeiculo = id;

            try
            {
                if (acoes.ExcluirDados(marcaVeiculo))
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
