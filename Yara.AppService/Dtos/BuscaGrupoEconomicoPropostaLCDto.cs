using System;

namespace Yara.AppService.Dtos
{
    public class BuscaGrupoEconomicoPropostaLCDto
    {
        public Guid ContaClienteID { get; set; }
        public string Integrante { get; set; }
        public string Codigo { get; set; }
        public string Documento { get; set; }
        public string TipoCliente { get; set; }
        public decimal? Patrimonial { get; set; }
        public decimal? ReceitaTotal { get; set; }
        public decimal? Demonstrativo { get; set; }
        public decimal? PotencialCredito { get; set; }
        public decimal? PotencialReceita { get; set; }
        public decimal? PotencialPatrimonial { get; set; }
        public decimal? LimiteSugerido { get; set; }
        public DateTime? VigenciaSugerida { get; set; }
        public DateTime? VigenciaFimSugerida { get; set; }
        public Guid? DemonstrativoID { get; set; }
    }
}
