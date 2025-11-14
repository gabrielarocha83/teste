using System;

namespace Yara.AppService.Dtos
{
    public class HistoricoContaClienteDto
    {
        public Guid ID { get; set; }
        public Guid ContaClienteID { get; set; }
        public string EmpresasID { get; set; }
        
        public int Ano { get; set; }
        public decimal Montante { get; set; }
        public decimal MontantePrazo { get; set; }
        public decimal MontanteAVista { get; set; }

        public float DiasAtraso { get; set; }
        public float PesoAtraso { get; set; }
        public bool PRDias { get; set; }

        public float DiasMaiorAtraso { get; set; }
        public float PesoMaiorAtraso { get; set; }

        //public float PRPeso { get; set; }
        //public int REPRDias { get; set; }
        //public float REPRPeso { get; set; }

        public bool Pefin { get; set; }
        public bool OpFinanciamento { get; set; }

        public virtual ContaClienteDto ContaCliente { get; set; }
        public virtual EmpresasDto Empresas { get; set; }
    }
}