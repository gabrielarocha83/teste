using System;

namespace Yara.Domain.Entities
{
    public class AnexoArquivo : Base
    {
        public Guid? ContaClienteID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? PropostaLCAdicionalID { get; set; }
        public Guid AnexoID { get; set; }
        public byte[] Arquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public bool Ativo { get; set; }

        //0 = Invalido, 1 = Não Valido, 2 = Valido
        public int? Status { get; set; }
        public DateTime? DataValidade { get; set; }
        public string Comentario { get; set; }
        public string Complemento { get; set; }

        public ContaCliente ContaCliente { get; set; }
        public PropostaLC PropostaLC { get; set; }
        public PropostaLCAdicional PropostaLCAdicional { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
