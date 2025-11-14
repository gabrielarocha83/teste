using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaOrdemVendaSumarizado
    {
         public Guid ClienteId { get; set; }
        public string EmpresaId { get; set; }
        public OrdemVendaSumarizado BuscaOrdemVendaPrazosLiberadas { get; set; }
        public OrdemVendaSumarizado BuscaOrdemVendaPrazosBloqueadas { get; set; }
        public OrdemVendaSumarizado BuscaOrdemVendaAVista { get; set; }
        public OrdemVendaSumarizado BuscaOrdemPagaRetira { get; set; }
    }
}