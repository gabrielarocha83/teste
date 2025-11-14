using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaAbonoInserirDto:BaseDto
    {
        public Guid ContaClienteID { get; set; }
        public IEnumerable<AbonoTitulos> Titulos { get; set; }
        public Guid Motivo { get; set; }
        public string EmpresaID { get; set; }

    }

    public class AbonoTitulos
    {
        public string NumeroDocumento { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public decimal ValorDocumento { get; set; }

    }
}