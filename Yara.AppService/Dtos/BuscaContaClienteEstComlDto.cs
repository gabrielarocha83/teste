using System;

namespace Yara.AppService.Dtos
{
    public class BuscaContaClienteEstComlDto
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
        public string EstruturaComercialId { get; set; }
        public Guid? RepresentanteID { get; set; }
        public bool? Ativo { get; set; }
    }
}
