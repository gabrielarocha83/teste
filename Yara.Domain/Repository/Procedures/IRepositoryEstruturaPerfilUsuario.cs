using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository.Procedures
{
    public interface IRepositoryEstruturaPerfilUsuario : IRepositoryBase<EstruturaPerfilUsuario>
    {
        Task<IEnumerable<BuscaCTCPerfilUsuario>> BuscaContaCliente(string usuario, string ctc, string gc);
        Task<bool> SubstituicaoUsuario(Guid? usuarioOldId, Guid? usuarioNewId, string codigoSap, Guid? usuarioId);
    }
}
