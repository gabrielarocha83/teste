using System;

namespace Yara.Domain.Entities
{
    public class FluxoLiberacaoManual : Base
    {

        public int Nivel { get; set; }
        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }
        public bool Ativo { get; set; }
        public string Estrutura { get; set; }
        public string Aprovador { get; set; }
        public Guid? SegmentoID { get; set; }
        public Guid? Usuario { get; set; }
        public Guid? Grupo { get; set; }

        // Nav Properties
        public virtual Segmento Segmento { get; set; }

    }
}
