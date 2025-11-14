using System;

namespace Yara.AppService.Dtos
{
    public class FluxoLiberacaoLCAdicionalDto : BaseDto
    {
        public int Nivel { get; set; }
        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }

        public Guid SegmentoID { get; set; }
        public Guid PrimeiroPerfilID { get; set; }
        public Guid? SegundoPerfilID { get; set; }

        public virtual SegmentoDto Segmento { get; set; }
        public virtual PerfilDto PrimeiroPerfil { get; set; }
        public virtual PerfilDto SegundoPerfil { get; set; }
    }
}
