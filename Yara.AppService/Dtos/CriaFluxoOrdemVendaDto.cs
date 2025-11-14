using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class CriaFluxoOrdemVendaDto
    {
        public List<OrdemVendaFluxoDto> Ordens { get; set; }
        public string EmpresaID { get; set; }
        public Guid UsuarioID { get; set; }
        public bool AcompanharProposta { get; set; }
        public string Comentario { get; set; }
        public string CodigoSap { get; set; }
        public decimal Total { get; set; }
        public Guid SegmentoID { get; set; }
        public Guid ContaClienteID { get; set; }
    }
}