using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaOrdemCliente
    {
        public Guid ClienteId { get; set; }
        public string EmpresaId { get; set; }
        public List<BuscaOrdemVendaMulti> BuscaOrdemVendaPrazos { get; set; }
        public List<BuscaOrdemVendaMulti> BuscaOrdemVendaAVista { get; set; }
        public List<BuscaOrdemVendaMulti> BuscaOrdemPagaRetira { get; set; }
    }
}
