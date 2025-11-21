using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteEstruturaComercial
    {
        public Guid ContaClienteId { get; set; }
        public string EstruturaComercialId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public string EmpresasId { get; set; }
        public bool Ativo { get; set; }
        public ContaCliente ContaCliente { get; set; }
        public virtual EstruturaComercial EstruturaComercial { get; set; }
        public virtual Empresas Empresas { get; set; }
    }
}
