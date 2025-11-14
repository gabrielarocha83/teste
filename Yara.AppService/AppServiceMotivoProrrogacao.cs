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
    public class AppServiceMotivoProrrogacao : IAppServiceMotivoProrrogacao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceMotivoProrrogacao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MotivoProrrogacaoDto>> GetAllAsync()
        {
            var motivosprorrogacao = await _unitOfWork.MotivoProrrogacaoRepository.GetAllAsync();
            return AutoMapper.Mapper.Map<IEnumerable<MotivoProrrogacaoDto>>(motivosprorrogacao);
        }

        public async Task<IEnumerable<MotivoProrrogacaoDto>> GetAllFilterAsync(Expression<Func<MotivoProrrogacaoDto, bool>> expression)
        {
            var motivoprorrogacao = await _unitOfWork.MotivoProrrogacaoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<MotivoProrrogacao, bool>>>(expression));
            return AutoMapper.Mapper.Map<IEnumerable<MotivoProrrogacaoDto>>(motivoprorrogacao);
        }

        public async Task<MotivoProrrogacaoDto> GetAsync(Expression<Func<MotivoProrrogacaoDto, bool>> expression)
        {
            var motivoprorrogacao = await _unitOfWork.MotivoProrrogacaoRepository.GetAsync(Mapper.Map<Expression<Func<MotivoProrrogacao, bool>>>(expression));
            return AutoMapper.Mapper.Map<MotivoProrrogacaoDto>(motivoprorrogacao);
        }

        public bool Insert(MotivoProrrogacaoDto obj)
        {
            var motivoprorrogacao = obj.MapTo<MotivoProrrogacao>();
            motivoprorrogacao.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            motivoprorrogacao.DataCriacao = DateTime.Now;

            _unitOfWork.MotivoProrrogacaoRepository.Insert(motivoprorrogacao);

            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(MotivoProrrogacaoDto obj)
        {
            var motivoprorrogacao = obj.MapTo<MotivoProrrogacao>();

            var exists = await _unitOfWork.MotivoProrrogacaoRepository.GetAsync(c => c.Nome.Equals(obj.Nome));
            if (exists != null)
                throw new Exception("Motivo da Prorrogação já cadastrado.");

            motivoprorrogacao.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            motivoprorrogacao.DataCriacao = DateTime.Now;

            _unitOfWork.MotivoProrrogacaoRepository.Insert(motivoprorrogacao);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(MotivoProrrogacaoDto obj)
        {
            var motivoprorrogacao = await _unitOfWork.MotivoProrrogacaoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            motivoprorrogacao.Nome = obj.Nome;
            motivoprorrogacao.Ativo = obj.Ativo;
            motivoprorrogacao.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            motivoprorrogacao.DataAlteracao = DateTime.Now;

            _unitOfWork.MotivoProrrogacaoRepository.Update(motivoprorrogacao);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var motivoprorrogacao = await _unitOfWork.MotivoProrrogacaoRepository.GetAsync(c => c.ID.Equals(id));
            motivoprorrogacao.Ativo = false;

            _unitOfWork.MotivoProrrogacaoRepository.Update(motivoprorrogacao);

            return _unitOfWork.Commit();
        }
    }
}