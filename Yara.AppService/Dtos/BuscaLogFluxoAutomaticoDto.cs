using System;

namespace Yara.AppService.Dtos
{
    public class BuscaLogFluxoAutomaticoDto
    {
        public Guid LogId { get; set; }
        public Guid? ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public string CodigoCliente { get; set; }
        public int OrdemDivisao { get; set; }
        public int OrdemVendaItem { get; set; }
        public string OrdemVendaNumero { get; set; }
        public string Restricao { get; set; }
        public string Motivo { get; set; }
        public string Detalhes { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public Guid SolicitanteFluxoID { get; set; }
    }
}
