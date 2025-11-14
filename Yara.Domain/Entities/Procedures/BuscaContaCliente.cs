using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaContaCliente
    {
        public Guid ID { get; set; }
        public string CodigoPrincipal { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Apelido { get; set; }
        public string GrupoEconomico { get; set; }
        public string NumeroOrdem { get; set; }
        public bool Simplificado { get; set; }
        public string EmpresaId { get; set; }

    }
}
