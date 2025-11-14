using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteResponsavelGarantia : Base
    {
        public string Documento { get; set; }
        public string Nome { get; set; }

        //FD = Fiel Depositário || PI = Proprietário do Imovel || CO = Cessionário
        public string TipoResponsabilidade { get; set; }
        public bool Ativo { get; set; }
        public Guid ContaClienteGarantiaID { get; set; }
        public ContaClienteGarantia ContaClienteGarantia { get; set; }
    }
}
