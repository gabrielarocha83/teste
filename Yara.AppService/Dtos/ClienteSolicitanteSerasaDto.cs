using System;

namespace Yara.AppService.Dtos
{
    public class ClienteSolicitanteSerasaDto
    {
        public Guid Codigo { get; set; }
        public Guid ContaClienteID { get; set; }
        public string Documento { get; set; }
        public Guid TipoClienteID { get; set; }
        public DateTime? DataRegistro { get; set; }
        public bool TemPendenciaSerasa { get; set; }
        public string DescricaoRestricao { get; set; }
        public TipoSerasaDto TipoSerasa { get; set; }
        public OrigemDocumentoDto OrigemDocumento { get; set; }
    }

    public enum OrigemDocumentoDto
    {
        ContaCliente = 1,
        Proposta = 2,
        Conjugue = 3,
        Parceiro = 4,
        MembroGrupo = 5
    }
}
