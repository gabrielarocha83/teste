using System;
// ReSharper disable InconsistentNaming

namespace Yara.AppService.Dtos
{
    public class ContaClienteCodigoDto : BaseDto
    {
        public Guid ContaClienteID { get; set; }
        public bool CodigoPrincipal { get; set; }
        public string Codigo { get; set; }
        public string Documento { get; set; }

        public ContaClienteDto ContaCliente { get; set; }
    }
}

