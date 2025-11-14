using System.Collections.Generic;
// ReSharper disable VirtualMemberCallInConstructor

namespace Yara.Domain.Entities
{
    public class Representante : Base
    {
        public string CodigoSap { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        //public virtual ICollection<ContaCliente> ContaClientes { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public ICollection<ContaClienteRepresentante> ContaClienteRepresentante { get; set; }

        public Representante()
        {
          
        }
    }
}
