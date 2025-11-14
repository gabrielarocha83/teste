using System;
using Newtonsoft.Json;
using Yara.AppService.Dtos;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaLCGarantiaDto
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
        public PropostaLCDemonstrativoDto PropostaLCDemonstrativo { get; set; }
        public Guid? PropostaLCDemonstrativoID { get; set; }
        public decimal? PotencialPatrimonial { get; set; }
        // Navigation Properties
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }

        public virtual TipoGarantiaDto TipoGarantia { get; set; }

        [JsonIgnore]
        public ICollection<PropostaLCBemRuralDto> PropostaLCBensRurais;

        [JsonIgnore]
        public ICollection<PropostaLCBemUrbanoDto> PropostaLCBensUrbanos;

        [JsonIgnore]
        public ICollection<PropostaLCMaquinaEquipamentoDto> PropostaLCMaquinasEquipamentos;
    }
}