using System;


namespace Yara.AppService.Dtos
{
    public class BuscaUsuariosDto
    {
        public bool? Ativo { get; set; }
        public Guid? GrupoID { get; set; }
        public string EmpresaID { get; set; }
        public int? TipoAcesso { get; set; }
        public string Usuario { get; set; }
        public string Login { get; set; }

    }
}
