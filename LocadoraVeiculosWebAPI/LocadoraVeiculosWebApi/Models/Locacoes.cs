using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraVeiculosWebApi.Models
{
    public class Locacoes
    {
        public int IdLocacao { get; set; }
        public int IdVeiculo { get; set; }
        public string PlacaVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public string NomeModeloVeiculo { get; set; }
        public string NomeMarcaVeiculo { get; set; }
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Observacoes { get; set; }
    }
}