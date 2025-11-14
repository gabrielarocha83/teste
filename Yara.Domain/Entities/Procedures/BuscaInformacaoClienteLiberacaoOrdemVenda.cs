using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaInformacaoClienteLiberacaoOrdemVenda
    {
        public Guid ContaClienteID { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Documento { get; set; }
        public string CodigoPrincipal { get; set; }
        public int PendenciaSerasa { get; set; }
        public bool Conceito { get; set; }
        public string DescricaoConceito { get; set; }
        public bool BloqueioManual { get; set; }
        public string NomeCtc { get; set; }
        public string NomeSupervisor { get; set; }
        public string NomeDiretoria { get; set; }
        public bool DividaAtiva { get; set; }
        public decimal LC { get; set; }
        public DateTime? Vigencia { get; set; }
        public DateTime? VigenciaFim { get; set; }
    }
}
