using System;

namespace Yara.Domain.Entities
{
    public class FluxoAlcadaAnalise : Base
    {

        public int Nivel { get; set; }
        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }
        public Guid SegmentoID { get; set; }
        public Guid PerfilID { get; set; }

        // Nav Properties
        public virtual Segmento Segmento { get; set; }
        public virtual Perfil Perfil { get; set; }

    }
}
