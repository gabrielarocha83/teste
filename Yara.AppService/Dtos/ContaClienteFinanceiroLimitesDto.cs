using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteFinanceiroLimitesDto
    {
        public decimal? LC { get; set; }
        public decimal? LCAdicional { get; set; }
        public decimal? Exposicao { get; set; }

        public DateTime? Vigencia { get; set; }
        public DateTime? VigenciaFim { get; set; }

        public DateTime? VigenciaAdicional { get; set; }
        public DateTime? VigenciaAdicionalFim { get; set; }
    }
}