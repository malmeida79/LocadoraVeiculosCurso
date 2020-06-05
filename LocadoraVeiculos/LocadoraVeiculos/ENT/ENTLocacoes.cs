using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraVeiculos.ENT
{
    public class ENTLocacoes
    {
        public int IdLocacao { get; set; }
        public int IdVeiculo { get; set; }
        public string PlacaVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public string ModeloVeiculo { get; set; }
        public string MarcaVeiculo { get; set; }
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Observacoes { get; set; }
    }
}