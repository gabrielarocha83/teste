using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class CobrancaListaClienteResultadoDto
    {
        public string NomeDiretoria { get; set; }
        public IEnumerable<CobrancaListaClienteDto> Clientes { get; set; }
        public decimal ValorTotal { get; set; }
        public int QuantidadeTotal { get; set; }
        public decimal ValorHoje { get; set; }
        public int QuantidadeHoje { get; set; }
    }

    public class CobrancaListaClienteDto
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
