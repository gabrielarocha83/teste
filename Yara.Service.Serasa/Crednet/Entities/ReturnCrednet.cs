using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Crednet.Entities
{
    public class ReturnCrednet
    {
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataSituacao { get; set; }
        public string NomeMae { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string DocumentoCliente { get; set; }
        public List<CrednetResumo> CrednetResumos { get; set; }
        public List<PendenciasInternasDetalhe> PendenciasInternasDetalhes { get; set; }
        public List<ProtestoEstadual> ProtestoEstaduais { get; set; }
        public List<ChequeBacen> ChequeBacen { get; set; }
        public List<PendenciasFinanceiras> PendenciasFinanceiras { get; set; }
        public List<DocumentosRoubados> DocumentosRoubados { get; set; }
        
        public ReturnCrednet()
        {
            CrednetResumos = new List<CrednetResumo>();
            PendenciasInternasDetalhes = new List<PendenciasInternasDetalhe>();
            ProtestoEstaduais = new List<ProtestoEstadual>();
            ChequeBacen = new List<ChequeBacen>();
            PendenciasFinanceiras = new List<PendenciasFinanceiras>();
            DocumentosRoubados = new List<DocumentosRoubados>();
        }
    }
}
