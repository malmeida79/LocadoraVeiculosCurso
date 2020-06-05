using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraVeiculos.Models
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCnpjCliente { get; set; }
        public DateTime DataCadastroCliente { get; set; }
        public DateTime DataAtualizacaoCliente { get; set; }
    }

}