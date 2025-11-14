using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaContaClienteEstComl
    {
        public Guid ID { get; set; }
        public string CodigoPrincipal { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Apelido { get; set; }
        public string Representante { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string DC { get; set; }
        public string EmpresaId { get; set; }


    }
}
