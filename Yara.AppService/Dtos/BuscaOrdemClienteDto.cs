using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class BuscaOrdemClienteDto
    {
        public Guid ClienteId { get; set; }
        public string EmpresaId { get; set; }
        public List<BuscaOrdemVendaMultiDto> BuscaOrdemVendaPrazos { get; set; }
        public List<BuscaOrdemVendaMultiDto> BuscaOrdemVendaAVista { get; set; }
        public List<BuscaOrdemVendaMultiDto> BuscaOrdemPagaRetira { get; set; }
    }
}
