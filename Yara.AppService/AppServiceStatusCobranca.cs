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
    public class AppServiceStatusCobranca : IAppServiceStatusCobranca
    { 
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceStatusCobranca(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StatusCobrancaDto> GetAsync(Expression<Func<StatusCobrancaDto, bool>> expression)
        {
            var statusCobranca = await _unitOfWork.StatusCobrancaRepository.GetAsync(Mapper.Map<Expression<Func<StatusCobranca, bool>>>(expression));
            return Mapper.Map<StatusCobrancaDto>(statusCobranca);
        }

        public async Task<IEnumerable<StatusCobrancaDto>> GetAllFilterAsync(Expression<Func<StatusCobrancaDto, bool>> expression)
        {
            var statusCobranca = await _unitOfWork.StatusCobrancaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<StatusCobranca, bool>>>(expression));
            return Mapper.Map <IEnumerable<StatusCobrancaDto> > (statusCobranca);
        }

        public async Task<IEnumerable<StatusCobrancaDto>> GetAllAsync()
        {
            var statusCobranca = await _unitOfWork.StatusCobrancaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<StatusCobrancaDto>>(statusCobranca);
        }

        public bool Insert(StatusCobrancaDto obj)
        {
            var statusCobranca = obj.MapTo<StatusCobranca>();

            //var exists = await _unitOfWork.StatusCobrancaRepository.GetAsync(c => c.Nome.Equals(obj.Descricao));
            //if (exists != null)
                //throw new Exception("Status de Cobrança já cadastrado.");

            statusCobranca.ID = Guid.NewGuid();
            statusCobranca.DataCriacao = DateTime.Now;

            _unitOfWork.StatusCobrancaRepository.Insert(statusCobranca);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(StatusCobrancaDto obj)
        {
            var statusCobranca = await _unitOfWork.StatusCobrancaRepository.GetAsync(c => c.ID.Equals(obj.ID));
            statusCobranca.Ativo = obj.Ativo;
            statusCobranca.Descricao = obj.Descricao;
            statusCobranca.CobrancaEfetiva = obj.CobrancaEfetiva;
            statusCobranca.Padrao = obj.Padrao;
            statusCobranca.NaoCobranca = obj.NaoCobranca;
            statusCobranca.ContaExposicao = obj.ContaExposicao;
            statusCobranca.BloqueioOrdem = obj.BloqueioOrdem;
            statusCobranca.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            statusCobranca.DataAlteracao = DateTime.Now;

            _unitOfWork.StatusCobrancaRepository.Update(statusCobranca);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var statusCobranca = await _unitOfWork.StatusCobrancaRepository.GetAsync(c => c.ID.Equals(id));
            statusCobranca.Ativo = false;

            _unitOfWork.StatusCobrancaRepository.Update(statusCobranca);

            return _unitOfWork.Commit();
        }
    }
}