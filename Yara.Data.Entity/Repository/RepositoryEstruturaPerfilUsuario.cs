using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository.Procedures;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryEstruturaPerfilUsuario : RepositoryBase<EstruturaPerfilUsuario>, IRepositoryEstruturaPerfilUsuario
    {

        public RepositoryEstruturaPerfilUsuario(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BuscaCTCPerfilUsuario>> BuscaContaCliente(string Usuario, string CTC, string GC)
        {
            IEnumerable<BuscaCTCPerfilUsuario> list = await _context.Database.SqlQuery<BuscaCTCPerfilUsuario>("EXEC spBuscaCTCPerfilUsuario @Usuario, @CTC, @GC",
                new SqlParameter("Usuario", string.IsNullOrEmpty(Usuario) ? DBNull.Value : (object)Usuario),
                new SqlParameter("CTC", string.IsNullOrEmpty(CTC) ? DBNull.Value : (object)CTC),
                new SqlParameter("GC", string.IsNullOrEmpty(GC) ? DBNull.Value : (object)GC)
            ).ToListAsync();

            return list;
        }

        public async Task<bool> SubstituicaoUsuario(Guid? usuarioOldId, Guid? usuarioNewId, string codigoSap, Guid? usuarioId)
        {
            try
            {

                if ((!usuarioOldId.HasValue) || (!usuarioNewId.HasValue))
                    return true;

                var retorno = await _context.Database.SqlQuery<bool>("EXEC spSubstituicaoPerfilUsuario @UsuarioOldID, @UsuarioNewID, @CodSap, @UsuarioID",
                    new SqlParameter("UsuarioOldID", usuarioOldId),
                    new SqlParameter("UsuarioNewID", usuarioNewId),
                    new SqlParameter("CodSap", codigoSap),
                    new SqlParameter("UsuarioID", usuarioId)
                ).FirstOrDefaultAsync();

                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
