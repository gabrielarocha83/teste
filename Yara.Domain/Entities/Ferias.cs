using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Ferias:Base
    {
        public Guid UsuarioID { get; set; }
        public DateTime FeriasInicio { get; set; }
        public DateTime FeriasFim { get; set; }
        public Guid SubstitutoID { get; set; }
        public bool Ativo { get; set; }

        // Nav Properties
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Substituto { get; set; }


    }

}