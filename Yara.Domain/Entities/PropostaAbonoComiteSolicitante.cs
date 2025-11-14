using System;
using Yara.Domain.Entities;

namespace Yara.Domain
{
    public class PropostaAbonoComiteSolicitante
    {
        public Guid ID { get; set; }
        public Guid UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}