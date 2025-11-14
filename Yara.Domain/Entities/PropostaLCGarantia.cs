using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class PropostaLCGarantia
    {

        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid TipoGarantiaID { get; set; }
        public string Descricao { get; set; }
        public bool GarantiaRecebida { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Documento { get; set; }
        public string Nome { get; set; }
        public string Comentario { get; set; }
        public PropostaLCDemonstrativo PropostaLCDemonstrativo { get; set; }
        public Guid? PropostaLCDemonstrativoID { get; set; }

        public decimal? PotencialPatrimonial { get; set; }
        // Navigation Properties
        public PropostaLC PropostaLC { get; set; }
        public virtual TipoGarantia TipoGarantia { get; set; }

        public ICollection<PropostaLCBemRural> PropostaLCBensRurais;
        public ICollection<PropostaLCBemUrbano> PropostaLCBensUrbanos;
        public ICollection<PropostaLCMaquinaEquipamento> PropostaLCMaquinasEquipamentos;
    }
}