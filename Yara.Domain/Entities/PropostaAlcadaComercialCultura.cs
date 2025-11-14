using System;

namespace Yara.Domain.Entities
{
    public class PropostaAlcadaComercialCultura
    {
        public Guid ID { get; set; }
        public Guid PropostaAlcadaComercialID { get; set; }
        public Guid? CulturaID { get; set; }
        public Guid? EstadoID { get; set; }
        public Guid? CidadeID { get; set; }
        public string Documento { get; set; }
        public decimal? AreaPropria { get; set; }
        public decimal? AreaArrendada { get; set; }

        // Navigation Properties
        public PropostaAlcadaComercial PropostaAlcadaComercial { get; set; }
        public virtual Cultura Cultura { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }
    }
}