using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceTipoRelacaoGrupoEconomico : IAppServiceTipoRelacaoGrupoEconomico
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTipoRelacaoGrupoEconomico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<TipoRelacaoGrupoEconomicoDto> GetAsync(Expression<Func<TipoRelacaoGrupoEconomicoDto, bool>> expression)
        {
            var relacao = await _unitOfWork.TipoRelacaoGrupoEconomicoRepository.GetAsync(Mapper.Map<Expression<Func<TipoRelacaoGrupoEconomico, bool>>>(expression));
            return Mapper.Map<TipoRelacaoGrupoEconomicoDto>(relacao);
        }

        public async Task<IEnumerable<TipoRelacaoGrupoEconomicoDto>> GetAllFilterAsync(Expression<Func<TipoRelacaoGrupoEconomicoDto, bool>> expression)
        {
            var cult = await _unitOfWork.TipoRelacaoGrupoEconomicoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<TipoRelacaoGrupoEconomico, bool>>>(expression));
            return Mapper.Map<IEnumerable<TipoRelacaoGrupoEconomicoDto>>(cult);
        }

        public async Task<IEnumerable<TipoRelacaoGrupoEconomicoDto>> GetAllAsync()
        {
            var cult = await _unitOfWork.TipoRelacaoGrupoEconomicoRepository.GetAllNoTracking();
            return Mapper.Map<IEnumerable<TipoRelacaoGrupoEconomicoDto>>(cult);
        }

        public bool Insert(TipoRelacaoGrupoEconomicoDto obj)
        {
            var relacao = obj.MapTo<TipoRelacaoGrupoEconomico>();
            relacao.DataCriacao = DateTime.Now;
            relacao.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.TipoRelacaoGrupoEconomicoRepository.Insert(relacao);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(TipoRelacaoGrupoEconomicoDto obj)
        {
            var relacao = await _unitOfWork.TipoRelacaoGrupoEconomicoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            relacao.Nome = obj.Nome;
            relacao.ClassificacaoGrupoEconomicoID = obj.ClassificacaoGrupoEconomicoID;
            relacao.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            relacao.Ativo = obj.Ativo;
            relacao.DataAlteracao = DateTime.Now;
            
         
            _unitOfWork.TipoRelacaoGrupoEconomicoRepository.Update(relacao);
            return _unitOfWork.Commit();
        }

      
    }
}
