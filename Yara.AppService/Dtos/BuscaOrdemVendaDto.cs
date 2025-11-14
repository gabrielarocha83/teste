using System;

namespace Yara.AppService.Dtos
{
    public class BuscaOrdemVendaDto
    {
        public int Divisao { get; set; }
        public int Item { get; set; }
        public string Numero { get; set; }
        public string Documento { get; set; }
        public string Rating { get; set; }
        public float? LC { get; set; }
        public float? LCDisponivel { get; set; }
        public float? Exposicao { get; set; }
        public DateTime? DataOrganizacao { get; set; }
        public decimal Preco { get; set; }
        public string BloqueioPortal { get; set; }
    }
}

