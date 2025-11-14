using System;

namespace Yara.Domain.Entities
{
    public class FluxoLiberacaoLCAdicional : Base
    {
        public int Nivel { get; set; }
        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }

        public Guid SegmentoID { get; set; }
        public Guid PrimeiroPerfilID { get; set; }
        public Guid? SegundoPerfilID { get; set; }

        public virtual Segmento Segmento { get; set; }
        public virtual Perfil PrimeiroPerfil { get; set; }
        public virtual Perfil SegundoPerfil { get; set; }
    }
}
