using System;

namespace Yara.AppService.Dtos
{
    public class PropostaAlcadaComercialRestricoesDto
    {
        public Guid ID { get; set; }
        public Guid ContaClienteID { get; set; }
        public string CodigoSap { get; set; }
        public string Mensagem { get; set; }

        public ContaClienteDto ContaCliente { get; set; }
    }
}
