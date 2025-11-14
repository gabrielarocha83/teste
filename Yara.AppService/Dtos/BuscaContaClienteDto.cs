using System;

namespace Yara.AppService.Dtos
{
    public class BuscaContaClienteDto
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
