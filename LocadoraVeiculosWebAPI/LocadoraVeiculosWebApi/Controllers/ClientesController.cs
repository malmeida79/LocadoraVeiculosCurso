using LocadoraVeiculosWebApi.Models;
using LocadoraVeiculosWebApi.UTIL;
using System.Collections.Generic;
using System.Web.Http;

namespace LocadoraVeiculosWebApi.Controllers
{
    public class ClientesController : ApiController
    {
        AcoesGenericas<Clientes> acoes = new AcoesGenericas<Clientes>();
        private string _erro = string.Empty;

        public string GetErro
        {
            get
            {
                return this._erro;
            }
        }

        public IEnumerable<Clientes> GetClientes()
        {
            Clientes cliente = new Clientes();
            return acoes.ListarDados(cliente);
        }

        public IEnumerable<Clientes> GetCliente(int idCliente)
        {
            Clientes cliente = new Clientes();
            cliente.IdCliente = idCliente;
            return acoes.ListarDados(cliente);
        }

        public IEnumerable<Clientes> GetCliente(string nome)
        {
            Clientes cliente = new Clientes();
            cliente.IdCliente = 0;
            cliente.NomeCliente = nome;
            return acoes.ListarDados(cliente);
        }

        public bool Post(Clientes cliente)
        {
            bool retorno = false;

            try
            {
                if (acoes.CadastrarDados(cliente))
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

        public bool Put(Clientes cliente)
        {
            bool retorno = false;

            try
            {
                if (acoes.SalvarDados(cliente))
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

            Clientes cliente = new Clientes();
            cliente.IdCliente = id;

            try
            {
                if (acoes.ExcluirDados(cliente))
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
