namespace Yara.Domain.Entities.Procedures
{
    public class CobrancaResultado
    {

        public string Tipo { get; set; }
        public string Chave { get; set; }
        public string Nome { get; set; }
        public string Complemento { get; set; }
        public decimal Dias30 { get; set; }
        public decimal Dias60 { get; set; }
        public decimal Dias90 { get; set; }
        public decimal Dias180 { get; set; }
        public decimal DiasMais { get; set; }
        public decimal Total { get; set; }
        public decimal Percentual { get; set; }

    }
}