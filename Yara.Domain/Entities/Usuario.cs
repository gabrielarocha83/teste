using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Usuario : Base
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public TipoAcesso TipoAcesso { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaLogada { get; set; }
        public Guid? TokenID { get; set; }

        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<Representante> Representantes { get; set; }
        public virtual ICollection<EstruturaComercial> EstruturasComerciais { get; set; }

        public string EmpresasID { get; set; }
        public virtual Empresas Empresas { get; set; }

        public Usuario()
        {
            Grupos = new List<Grupo>();
            Representantes = new List<Representante>();
            EstruturasComerciais = new List<EstruturaComercial>();
        }
    }

    public enum TipoAcesso
    {
        AD = 1,
        SF = 2
    }
}
