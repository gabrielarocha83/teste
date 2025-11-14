using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class ContaClienteGarantia : Base
    {
        public int? Codigo { get; set; }
        public decimal? ValorGarantia { get; set; }
        public decimal? ValorGarantido { get; set; }
        public DateTime? Vigencia { get; set; }
        public DateTime? VigenciaFim { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string Motivo { get; set; }
        public string Observacao { get; set; }

        public string Grau { get; set; }
        public string Matricula { get; set; }
        public string TipoImovel { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Registro { get; set; }
        public string Laudo { get; set; }
        public decimal? ValorForcada { get; set; }
        public decimal? ValorMercado { get; set; }

        public string Produto { get; set; }
        public string Quantidade { get; set; }

        public string Empresa { get; set; }
        public string Area { get; set; }

        public string Monitoramento { get; set; }
        public string EmpresaMonitoramento { get; set; }
        public string OutrasGarantias { get; set; }

        //0 = N/D;
        //1 = Carta Fiança;
        //2 = Hipoteca;
        //3 = Alienação de Bens Móveis;
        //4 = Alienação de Bens Imóveis;
        //5 = Penhor;
        //6 = Cessão de Crédito;
        //7 = CPR;
        //8 = Outros;
        public string TipoGarantia { get; set; }
        
        public string Status { get; set; }
        public bool Ativo { get; set; }
        public string DescricaoOutros { get; set; }

        public Guid ContaClienteID { get; set; }
        public ContaCliente ContaCliente { get; set; }

        public string EmpresasID { get; set; }
        public virtual Empresas Empresas { get; set; }

        public virtual ICollection<ContaClienteParticipanteGarantia> ParticipanteGarantia { get; set; }
        public virtual ICollection<ContaClienteResponsavelGarantia> ResponsavelGarantia { get; set; }

        public ContaClienteGarantia()
        {
            ParticipanteGarantia = new List<ContaClienteParticipanteGarantia>();
            ResponsavelGarantia = new List<ContaClienteResponsavelGarantia>();
        }
    }
}
