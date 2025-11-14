using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFerias : RepositoryBase<Ferias>, IRepositoryFerias
    {

        public RepositoryFerias(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ferias>> GetFeriasByIDUser(Guid user)
        {
            return await _context.Set<Ferias>().Include("Substituto").Where(c => c.UsuarioID.Equals(user)).ToListAsync();
        }

        public async Task<IEnumerable<Ferias>> GetStatus(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var strSql = "SELECT * FROM dbo.Ferias f";
                strSql = strSql + " WHERE f.UsuarioID = '" + usuarioId + "'";
                strSql = strSql + " AND f.Ativo = 1";
                strSql = strSql + " AND CONVERT(DATETIME, '" + dataInicio + "', 103) < f.FeriasInicio";
                strSql = strSql + " AND CONVERT(DATETIME, '" + dataFim + "', 103) < f.FeriasInicio";
                strSql = strSql + " UNION";
                strSql = strSql + " SELECT* FROM dbo.Ferias f";
                strSql = strSql + " WHERE f.UsuarioID = '" + usuarioId + "'";
                strSql = strSql + " AND f.Ativo = 1";
                strSql = strSql + " AND CONVERT(DATETIME, '" + dataInicio + "', 103) > f.FeriasFim";
                strSql = strSql + " AND CONVERT(DATETIME, '" + dataFim + "', 103) > f.FeriasFim;";
                
                IEnumerable<Ferias> list = await _context.Database.SqlQuery<Ferias>(strSql).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
