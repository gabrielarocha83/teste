using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class SolicitanteSerasaDto 
    {
        public Guid ID { get; set; }
        [JsonIgnore]
        public Guid UsuarioIDCriacao { get; set; }
        [JsonIgnore]
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; }
        public TipoClienteDto TipoCliente { get; set; }
        public Guid TipoClienteID { get; set; }
        public ContaClienteDto ContaCliente { get; set; }
        public Guid ContaClienteID { get; set; }
        public Guid UsuarioID { get; set; }
        public virtual UsuarioDto Usuario { get; set; }
        public TipoSerasaDto TipoSerasa { get; set; }
        [JsonIgnore]
        public string Json { get; set; }
        public decimal? Total { get; set; }
        public string MotivoConsulta { get; set; }
        public string Documento { get; set; }
        public bool TemPendenciaSerasa { get; set; }

        public SolicitanteSerasaDto()
        {
            ContaCliente = new ContaClienteDto();
            TipoCliente = new TipoClienteDto();
        }

    }

    public class ListaSolicitanteSerasaDto
    {
        public IEnumerable<Guid> ContaClienteID { get; set; }

        public ListaSolicitanteSerasaDto()
        {
            ContaClienteID = new List<Guid>();
        }
    }

    public class PendenciasSerasaDto
    {
        public Guid ID { get; set; }
        public SolicitanteSerasaDto SolicitanteSerasa { get; set; }
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