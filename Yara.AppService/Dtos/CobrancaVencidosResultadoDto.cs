using System;

namespace Yara.AppService.Dtos
{
    public class CobrancaVencidosResultadoDto
    {

        public Guid ContaClienteId { get; set; }
        public string Nome { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }

    }
}