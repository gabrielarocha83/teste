using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceIdadeMediaCanavial : IAppServiceIdadeMediaCanavial
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceIdadeMediaCanavial(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<IdadeMediaCanavialDto> GetAsync(Expression<Func<IdadeMediaCanavialDto, bool>> expression)
        {
            var idadeMediaCanavialID = await _untUnitOfWork.IdateIdadeMediaCanavialRepository.GetAsync(
                 Mapper.Map<Expression<Func<IdadeMediaCanavial, bool>>>(expression));
            return idadeMediaCanavialID.MapTo<IdadeMediaCanavialDto>();
        }

        public async Task<IEnumerable<IdadeMediaCanavialDto>> GetAllFilterAsync(Expression<Func<IdadeMediaCanavialDto, bool>> expression)
        {
            var idadeMediaCanavialID = await
                _untUnitOfWork.IdateIdadeMediaCanavialRepository.GetAllFilterAsync(
                    Mapper.Map<Expression<Func<IdadeMediaCanavial, bool>>>(expression));

            return Mapper.Map<IEnumerable<IdadeMediaCanavialDto>>(idadeMediaCanavialID);
        }

        public async Task<IEnumerable<IdadeMediaCanavialDto>> GetAllAsync()
        {
            var contaClientecodigo = await _untUnitOfWork.IdateIdadeMediaCanavialRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<IdadeMediaCanavialDto>>(contaClientecodigo);
        }
       
        public bool Insert(IdadeMediaCanavialDto obj)
        {
            var idadeMediaCanavial = obj.MapTo<IdadeMediaCanavial>();
            idadeMediaCanavial.ID = Guid.NewGuid();
            idadeMediaCanavial.DataCriacao = DateTime.Now;
            idadeMediaCanavial.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.IdateIdadeMediaCanavialRepository.Insert(idadeMediaCanavial);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> Update(IdadeMediaCanavialDto obj)
        {
            var idadeMediaCanavial = await _untUnitOfWork.IdateIdadeMediaCanavialRepository.GetAsync(c => c.ID.Equals(obj.ID));

            idadeMediaCanavial.Nome = obj.Nome;
            idadeMediaCanavial.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            idadeMediaCanavial.Ativo = obj.Ativo;
            _untUnitOfWork.IdateIdadeMediaCanavialRepository.Update(idadeMediaCanavial);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(IdadeMediaCanavialDto obj)
        {
            var idadeMediaCanavial = obj.MapTo<IdadeMediaCanavial>();
            var exist = await _untUnitOfWork.IdateIdadeMediaCanavialRepository.GetAsync(c => c.Nome.Equals(obj.Nome));

            if (exist != null)
                throw new Exception("Idade Média de Canavial já está cadastrada.");

            idadeMediaCanavial.ID = Guid.NewGuid();
            idadeMediaCanavial.DataCriacao = DateTime.Now;
            idadeMediaCanavial.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.IdateIdadeMediaCanavialRepository.Insert(idadeMediaCanavial);
            return _untUnitOfWork.Commit();
        }
    }
}