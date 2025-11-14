using System;

namespace Yara.Domain.Entities.Procedures
{
    public class CobrancaVencidosResultado
    {

        public Guid ContaClienteId { get; set; }
        public string Nome { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }

    }
}