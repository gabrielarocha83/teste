using System;

namespace Yara.AppService.Dtos
{
    public class PropostaProrrogacaoParcelamentoDto
    {
        public Guid ID { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public virtual PropostaProrrogacaoDto PropostaProrrogacao { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }
}