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
    public class AppServiceReceita : IAppServiceReceita
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceReceita(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReceitaDto> GetAsync(Expression<Func<ReceitaDto, bool>> expression)
        {
            var receita = await _unitOfWork.ReceitaRepository.GetAsync(Mapper.Map<Expression<Func<Receita, bool>>>(expression));
            return Mapper.Map<ReceitaDto>(receita);
        }

        public async Task<IEnumerable<ReceitaDto>> GetAllFilterAsync(Expression<Func<ReceitaDto, bool>> expression)
        {
            var receita = await _unitOfWork.ReceitaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Receita, bool>>>(expression));
            return Mapper.Map<IEnumerable<ReceitaDto>>(receita);
        }

        public async Task<IEnumerable<ReceitaDto>> GetAllAsync()
        {
            var receita = await _unitOfWork.ReceitaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ReceitaDto>>(receita);
        }

        public bool Insert(ReceitaDto obj)
        {
            var receita = obj.MapTo<Receita>();
            receita.DataCriacao = DateTime.Now;
            receita.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ReceitaRepository.Insert(receita);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ReceitaDto obj)
        {
            var receita = await _unitOfWork.ReceitaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            receita.Descricao = obj.Descricao;
            receita.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            receita.Ativo = obj.Ativo;
            _unitOfWork.ReceitaRepository.Update(receita);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ReceitaDto obj)
        {
            var receita = obj.MapTo<Receita>();
            var exist = await _unitOfWork.ReceitaRepository.GetAsync(c => c.Descricao.Equals(obj.Descricao));

            if (exist != null)
                throw new Exception("Receita já cadastrada");

            receita.DataCriacao = DateTime.Now;
            receita.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ReceitaRepository.Insert(receita);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var receita = await _unitOfWork.ReceitaRepository.GetAsync(c => c.ID.Equals(id));
            receita.Ativo = false;
            _unitOfWork.ReceitaRepository.Update(receita);
            return _unitOfWork.Commit();
        }
    }
}
