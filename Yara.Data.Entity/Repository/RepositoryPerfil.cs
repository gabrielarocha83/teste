using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    internal class RepositoryPerfil : RepositoryBase<Perfil>, IRepositoryPerfil
    {
        public RepositoryPerfil(DbContext context) : base(context)
        {
        }

        public async Task<Guid> GetPerfilID(string descricao)
        {
            return await _context.Set<Perfil>().Where(c => c.Descricao.Equals(descricao) && c.Ativo).Select(s => s.ID).FirstOrDefaultAsync();
        }
    }
}