using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class EstruturaComercial
    {
        public string CodigoSap { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Nome { get; set; }
        public virtual EstruturaComercial Superior { get; set; }

        public bool Ativo { get; set; }
        public string EstruturaComercialPapelID { get; set; }

        public virtual EstruturaComercialPapel EstruturaComercialPapel { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<ContaClienteEstruturaComercial> ContaClienteEstruturaComercial { get; set; }

        public EstruturaComercial()
        {
            Usuarios = new List<Usuario>();
            ContaClienteEstruturaComercial = new List<ContaClienteEstruturaComercial>();
        }
    }
}
