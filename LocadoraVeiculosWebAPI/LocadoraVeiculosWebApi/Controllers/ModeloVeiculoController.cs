using LocadoraVeiculosWebApi.Models;
using LocadoraVeiculosWebApi.UTIL;
using System.Collections.Generic;
using System.Web.Http;

namespace LocadoraVeiculosWebApi.Controllers
{
    public class ModeloVeiculoController : ApiController
    {
        AcoesGenericas<ModeloVeiculo> acoes = new AcoesGenericas<ModeloVeiculo>();
        private string _erro = string.Empty;

        public string GetErro
        {
            get
            {
                return this._erro;
            }
        }

        public IEnumerable<ModeloVeiculo> GetModeloVeiculos()
        {
            ModeloVeiculo modeloVeiculo = new ModeloVeiculo();
            return acoes.ListarDados(modeloVeiculo);
        }

        public IEnumerable<ModeloVeiculo> GetModeloVeiculos(int idModeloVeiculo)
        {
            ModeloVeiculo modeloVeiculo = new ModeloVeiculo();
            modeloVeiculo.IdModeloVeiculo = idModeloVeiculo;
            return acoes.ListarDados(modeloVeiculo);
        }

        public bool Post(ModeloVeiculo modeloVeiculo)
        {
            bool retorno = false;

            try
            {
                if (acoes.CadastrarDados(modeloVeiculo))
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

        public bool Put(ModeloVeiculo modeloVeiculo)
        {
            bool retorno = false;

            try
            {
                if (acoes.SalvarDados(modeloVeiculo))
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

            ModeloVeiculo modeloVeiculo = new ModeloVeiculo();
            modeloVeiculo.IdModeloVeiculo = id;

            try
            {
                if (acoes.ExcluirDados(modeloVeiculo))
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
