using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class BuscaOrdemVendaSumarizadoDto
    {
         public Guid ClienteId { get; set; }
        public string EmpresaId { get; set; }
        public OrdemVendaSumarizadoDto BuscaOrdemVendaPrazosLiberadas { get; set; }
        public OrdemVendaSumarizadoDto BuscaOrdemVendaPrazosBloqueadas { get; set; }
        public OrdemVendaSumarizadoDto BuscaOrdemVendaAVista { get; set; }
        public OrdemVendaSumarizadoDto BuscaOrdemPagaRetira { get; set; }
    }
}