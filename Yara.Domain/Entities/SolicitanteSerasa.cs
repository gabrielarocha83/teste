using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yara.Domain.Entities
{
    public class SolicitanteSerasa : Base
    {
        public TipoCliente TipoCliente { get; set; }
        public Guid TipoClienteID { get; set; }
        public ContaCliente ContaCliente { get; set; }
        public Guid ContaClienteID { get; set; }
        public Guid UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }
        public TipoSerasa TipoSerasa { get; set; }
        public string Json { get; set; }
        public decimal? Total { get; set; }
        public string MotivoConsulta { get; set; }
        public string Documento { get; set; }
        public bool TemPendenciaSerasa { get; set; }
    }

    public class PendenciasSerasa
    {
        public Guid ID { get; set; }
        public SolicitanteSerasa SolicitanteSerasa { get; set; }
        public Guid SolicitanteSerasaID { get; set; }
        public DateTime? Data { get; set; }
        public string Modalidade { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public string Empresa { get; set; }
        public string CNPJ { get; set; }
        public bool Falencia { get; set; }
    }
}