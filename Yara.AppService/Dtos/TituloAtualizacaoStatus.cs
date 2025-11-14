using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class TituloAtualizacaoStatus
    {
        public string Texto { get; set; }
        public DateTime? DataPrevisaoPagamento { get; set; }
        public Guid? StatusCobrancaID { get; set; }
        public DateTime? DataAceite { get; set; }
        public decimal? TaxaJuros { get; set; }
        public Guid UsuarioCriacao { get; set; }
        public List<TituloAtualizacaoStatusChave> TituloAtualizacaoStatusChaves { get; set; }

        public TituloAtualizacaoStatus()
        {
            TituloAtualizacaoStatusChaves = new List<TituloAtualizacaoStatusChave>();
        }
    }
}
