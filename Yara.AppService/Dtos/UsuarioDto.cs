using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class UsuarioDto : BaseDto
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public TipoAcessoDto TipoAcesso { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaLogada { get; set; }
        public Guid? TokenID { get; set; }

        public string NomeLogin => Nome + " (" + Login + ")";

        public virtual ICollection<GrupoDto> Grupos { get; set; }
        public virtual ICollection<RepresentanteDto> Representantes { get; set; }
        public virtual ICollection<EstruturaComercialDto> EstruturasComerciais { get; set; }

        public string EmpresasID { get; set; }
        public virtual EmpresasDto Empresas { get; set; }

        public UsuarioDto()
        {
            Grupos = new List<GrupoDto>();
            Representantes = new List<RepresentanteDto>();
            EstruturasComerciais = new List<EstruturaComercialDto>();
        }
    }

    public enum TipoAcessoDto
    {
        AD = 1,
        SF = 2
    }
}
