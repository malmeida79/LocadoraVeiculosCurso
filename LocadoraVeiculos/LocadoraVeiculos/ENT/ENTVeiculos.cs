using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocadoraVeiculos.ENT
{
    public class ENTVeiculos
    {
        public int IdVeiculo { get; set; }
        public string PlacaVeiculo { get; set; }
        public int IdMarcaVeiculo { get; set; }
        public string MarcaVeiculo { get; set; }
        public int IdModeloVeiculo { get; set; }
        public string ModeloVeiculo { get; set; }
        public string CorVeiculo { get; set; }
        public DateTime DataCadastroVeiculo { get; set; }
        public DateTime DataAtualizacaoVeiculo { get; set; }
    }
}