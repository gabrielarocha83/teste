using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryAnexoArquivo : RepositoryBase<AnexoArquivo>, IRepositoryAnexoArquivo
    {
        public RepositoryAnexoArquivo(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AnexoArquivo>> CustomGetAllFilterAsync(Expression<Func<AnexoArquivo, bool>> expression)
        {
            try
            {
                var list = await _context.Set<AnexoArquivo>().Where(expression).Select(c => new
                {
                    c.ID,
                    c.UsuarioIDCriacao,
                    c.UsuarioIDAlteracao,
                    c.DataCriacao,
                    c.DataAlteracao,
                    c.PropostaLCID,
                    c.AnexoID,
                    c.NomeArquivo,
                    c.ExtensaoArquivo,
                    c.Ativo,
                    c.ContaClienteID,
                    c.Status,
                    c.DataValidade,
                    c.Comentario,
                    c.Complemento,
                    c.Anexo
                }).ToListAsync();

                var ret = new List<AnexoArquivo>();

                list.ForEach(i => ret.Add(new AnexoArquivo
                {
                    ID = i.ID,
                    UsuarioIDCriacao = i.UsuarioIDCriacao,
                    UsuarioIDAlteracao = i.UsuarioIDAlteracao,
                    DataCriacao = i.DataCriacao,
                    DataAlteracao = i.DataAlteracao,
                    PropostaLCID = i.PropostaLCID,
                    AnexoID = i.AnexoID,
                    NomeArquivo = i.NomeArquivo,
                    ExtensaoArquivo = i.ExtensaoArquivo,
                    Ativo = i.Ativo,
                    ContaClienteID = i.ContaClienteID,
                    Status = i.Status,
                    DataValidade = i.DataValidade,
                    Comentario = i.Comentario,
                    Complemento = i.Complemento,
                    Anexo = i.Anexo
                }));

                return ret;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
