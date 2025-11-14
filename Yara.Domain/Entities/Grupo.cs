using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Grupo : Base
    {
        public String Nome { get; set; }
        public Boolean Ativo { get; set; }
        public bool IsProcesso { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }

        public virtual ICollection<Permissao> Permissoes { get; set; }

        public Grupo()
        {
            Permissoes = new List<Permissao>();
            Usuarios = new List<Usuario>();
        }
    }
}