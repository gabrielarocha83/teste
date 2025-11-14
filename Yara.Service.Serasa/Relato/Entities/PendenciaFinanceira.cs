using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class PendenciaFinanceira
    {
        public int Quantidade { get; set; }
        public int Quantidadeultimaocorrencia { get; set; }
        public DateTime? DataUltimaOcorrencia { get; set; }
        public string Titulo { get; set; }
        public string Avalista { get; set; }
        public decimal Valor { get; set; }
        public string Contrato { get; set; }
        public string Origem { get; set; }
        public string Filial { get; set; }
        public string Mensagem { get; set; }

        public decimal Total => Valor * Quantidade;
    }
}