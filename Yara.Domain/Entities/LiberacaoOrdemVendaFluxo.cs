using System;

namespace Yara.Domain.Entities
{
    public class LiberacaoOrdemVendaFluxo : Base
    {
        public Guid SolicitanteFluxoID { get; set; }
        public virtual SolicitanteFluxo SolicitanteFluxo { get; set; }

        public Guid FluxoLiberacaoOrdemVendaID { get; set; }
        public virtual FluxoLiberacaoOrdemVenda FluxoLiberacaoOrdemVenda { get; set; }

        public Guid? UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }

        public Guid StatusOrdemVendasID { get; set; }
        public virtual StatusOrdemVendas StatusOrdemVendas { get; set; }

        public string CodigoSap { get; set; }

        public string EmpresasId { get; set; }
        public virtual Empresas Empresas { get; set; }

        public string Comentario { get; set; }
    }
}