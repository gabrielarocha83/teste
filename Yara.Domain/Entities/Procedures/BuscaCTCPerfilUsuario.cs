using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaCTCPerfilUsuario
    {
        public string CodCTC { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string DI { get; set; }
        public Guid PerfilID { get; set; }
        public string Perfil { get; set; }
        public Guid? UsuarioID { get; set; }
    }
}