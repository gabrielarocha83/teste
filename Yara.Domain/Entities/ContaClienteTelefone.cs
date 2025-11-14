using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteTelefone :Base
    {
        public Guid ContaClienteID { get; set; }
        public ContaCliente ContaCliente { get; set; }
        public  TipoTelefone Tipo { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }

    public enum TipoTelefone
    {
        Fixo = 1,
        Celular=2
    }
}
