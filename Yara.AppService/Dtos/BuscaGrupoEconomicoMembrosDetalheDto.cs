using System;

namespace Yara.AppService.Dtos
{
    public class BuscaGrupoEconomicoMembrosDetalheDto
    {
        public Guid ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteCodigo { get; set; }
        public string ClienteDocumento { get; set; }
        public Guid ClienteTipoClienteID { get; set; }
        public TipoSerasaDto ClienteTipoSerasaDto { get; set; }
        public decimal LcIndividual { get; set; }
        public decimal ExpIndividual { get; set; }
        public string StatusMembro { get; set; }
        public bool MembroPrincipal { get; set; }
        public Guid SolicitanteSerasaID { get; set; }
        public bool RestricaoSerasa { get; set; }
        public int PendenciaSerasa { get; set; }
        public bool PossuiGarantia { get; set; }
        public bool BloqueioManual { get; set; }
    }
}
