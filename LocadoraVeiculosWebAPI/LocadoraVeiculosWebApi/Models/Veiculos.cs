using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraVeiculosWebApi.Models
{
    public class Veiculos
    {
        public int IdVeiculo { get; set; }
        public string PlacaVeiculo { get; set; }
        public int IdMarcaVeiculo { get; set; }
        public string NomeMarcaVeiculo { get; set; }
        public int IdModeloVeiculo { get; set; }
        public string NomeModeloVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public string StatusVeiculo { get; set; }
        public DateTime DataCadastroVeiculo { get; set; }
        public DateTime DataAtualizacaoVeiculo { get; set; }
    }
}