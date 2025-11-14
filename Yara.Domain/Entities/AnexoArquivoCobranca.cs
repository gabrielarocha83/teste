using System;

namespace Yara.Domain.Entities
{
    public class AnexoArquivoCobranca : Base
    {
        public Guid PropostaCobranca { get; set; }
        public Guid? ContaClienteID { get; set; }

        //0 = Proposta Prorrogação, 1 = Proposta Abono, 2 = Proposta Juridico
        public int TipoProposta { get; set; }
        public byte[] Arquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public bool Ativo { get; set; }

        public string Descricao { get; set; }
        public ContaCliente ContaCliente { get; set; }
    }
}
