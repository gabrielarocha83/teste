using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class OrdemVenda
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Canal { get; set; }
        public string Setor { get; set; }
        public string OrgVendas { get; set; }
        public string CodigoCtc { get; set; }
        public string CodigoGc { get; set; }
        public string Emissor { get; set; }
        public string Pagador { get; set; }
        public string Representante { get; set; }
        public string CondPagto { get; set; }
        public string CondFrete { get; set; }
        public string PedidoCliente { get; set; }
        public string Moeda { get; set; }
        public decimal? TaxaCambio { get; set; }
        public string Cultura { get; set; }
        public string BloqueioRemessa { get; set; }
        public string BloqueioFaturamento { get; set; }
        public DateTime? DataEfetivaFixa { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public Guid UsuarioIdCriacao { get; set; }
        public Guid? UsuarioIdAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataValidade { get; set; }

        public ICollection<ItemOrdemVenda> Itens { get; set; }

        public ICollection<DivisaoRemessa> DivisaoRemessas { get; set; }
    }
}
