using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Yara.AppService.Dtos
{
    public class FeriasDto:BaseDto
    {

        public Guid UsuarioID { get; set; }
        public DateTime FeriasInicio { get; set; }
        public DateTime FeriasFim { get; set; }
        public Guid SubstitutoID { get; set; }
        public bool Ativo { get; set; }

        // Nav Properties
        public virtual UsuarioDto Usuario { get; set; }
        public virtual UsuarioDto Substituto { get; set; }

    }
    
}