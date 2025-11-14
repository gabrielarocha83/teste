using System;
using Newtonsoft.Json;
using Yara.Domain.Entities;

// ReSharper disable InconsistentNaming

namespace Yara.AppService.Dtos
{
    public class ContaClienteTelefoneDto :BaseDto
    {
        public Guid ContaClienteID { get; set; }
        [JsonIgnore]
        public ContaCliente ContaCliente { get; set; }
        public TipoTelefoneDto Tipo { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }

    public enum TipoTelefoneDto
    {
        Fixo = 1,
        Celular=2
    }
}
