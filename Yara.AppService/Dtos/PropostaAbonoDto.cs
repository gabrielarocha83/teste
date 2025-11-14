using System;
using System.Collections.Generic;
using Yara.Domain.Entities;

namespace Yara.AppService.Dtos
{
    public class PropostaAbonoDto : BaseDto
    {
        public Guid? MotivoAbonoID { get; set; }
        public Guid ContaClienteID { get; set; }
        public Guid ResponsavelID { get; set; }
        public bool Acompanhar { get; set; }
        public int NumeroInternoProposta { get; set; }
        public bool ConceitoH { get; set; }
        public string ParecerComercial { get; set; }
        public string ParecerCobranca { get; set; }
        public string PropostaCobrancaStatusID { get; set; }
        public string Motivo { get; set; }
        public decimal ValorTotalDocumento { get; set; }
        public string EmpresaID { get; set; }
        public decimal TotalDebito { get; set; }
        public bool Sinistro { get; set; }
        public string CodigoSap { get; set; }
        public virtual MotivoAbonoDto MotivoAbono { get; set; }
        public virtual PropostaCobrancaStatusDto PropostaCobrancaStatus { get; set; }
        public virtual IEnumerable<PropostaAbonoTituloDto> Titulos { get; set; }

        public Usuario Responsavel { get; set; }
        public virtual ContaClienteDto ContaCliente { get; set; }
        public virtual EmpresasDto Empresa { get; set; }

        public new DateTime DataCriacao { get; set; }

        public string NumeroProposta
        {
            get
            {
                return string.Format("AB{0:00000}/{1:yyyy}", this.NumeroInternoProposta, this.DataCriacao);
            }
        }
    }

    public class AprovaReprovaAbonoDto
    {
        public Guid FluxoID { get; set; }
        public Guid PropostaAbonoID { get; set; }
        public string EmpresaID { get; set; }
        public Guid UsuarioID { get; set; }
        public bool ConceitoH { get; set; }
        public bool Aprovado { get; set; }
        public string Comentario { get; set; }
    }
}
