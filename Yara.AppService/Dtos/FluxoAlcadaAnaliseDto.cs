using System;

namespace Yara.AppService.Dtos
{
    public class FluxoAlcadaAnaliseDto : BaseDto
    {
        public int Nivel { get; set; }
        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }
        public Guid SegmentoID { get; set; }
        public Guid PerfilID { get; set; }

        // Nav Properties
        public virtual SegmentoDto Segmento { get; set; }
        public virtual PerfilDto Perfil { get; set; }
    }
}
