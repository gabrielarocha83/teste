using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteResponsavelGarantiaDto : BaseDto
    {
        public string Documento { get; set; }
        public string Nome { get; set; }

        //FD = Fiel Depositário || PI = Proprietário do Imovel || CO = Cessionário
        public string TipoResponsabilidade { get; set; }
        public bool Ativo { get; set; }
        public Guid ContaClienteGarantiaID { get; set; }
        public ContaClienteGarantiaDto ContaClienteGarantia { get; set; }
    }
}
