using System;

namespace Yara.Service.Serasa.Crednet.Entities
{
    public class ChequeBacen
    {
        public DateTime? DataOcorrencia { get; set; }
        public string NumeroCheque { get; set; }
        public string Alinea { get; set; }
        public string QuantidadeCheque { get; set; }
        public decimal Valor { get; set; }
        public string NumeroBanco { get; set; }
        public string NomeBanco { get; set; }
        public string Agencia { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string CodCadus { get; set; }

        public decimal Total => Valor;
    }
}
