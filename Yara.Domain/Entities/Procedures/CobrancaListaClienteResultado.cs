using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities.Procedures
{
    public class CobrancaListaClienteResultado
    {
        public IEnumerable<CobrancaListaCliente> Clientes { get; set; }
        public decimal ValorTotal { get; set; }
        public int QuantidadeTotal { get; set; }
        public decimal ValorHoje { get; set; }
        public int QuantidadeHoje { get; set; }
    }

    public class CobrancaListaCliente
    {
        public Guid ContaClienteID { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Aging { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        public decimal Valor { get; set; }
        public decimal Percentual { get; set; }
    }
}
