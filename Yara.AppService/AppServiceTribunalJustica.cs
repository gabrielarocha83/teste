using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    class AppServiceTribunalJustica : IAppServiceTribunalJustica
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTribunalJustica(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TribunalJusticaDto>> GetAllAsync()
        {
            var tjs = await _unitOfWork.TribunalJusticaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<TribunalJusticaDto>>(tjs);
        }

        public async Task<IEnumerable<TribunalJusticaDto>> GetAllFilterAsync(Expression<Func<TribunalJusticaDto, bool>> expression)
        {
            var tjs = await _unitOfWork.TribunalJusticaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<TribunalJustica, bool>>>(expression));
            return Mapper.Map<IEnumerable<TribunalJusticaDto>>(tjs);
        }

        public async Task<TribunalJusticaDto> GetAsync(Expression<Func<TribunalJusticaDto, bool>> expression)
        {
            var tj = await _unitOfWork.TribunalJusticaRepository.GetAsync(Mapper.Map<Expression<Func<TribunalJustica, bool>>>(expression));
            return Mapper.Map<TribunalJusticaDto>(tj);
        }

        public bool Insert(TribunalJusticaDto obj)
        {
            var tj = obj.MapTo<TribunalJustica>();
            tj.DataCriacao = DateTime.Now;
            _unitOfWork.TribunalJusticaRepository.Insert(tj);
            return _unitOfWork.Commit();
        }

        public Task<bool> Update(TribunalJusticaDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckAsync(Guid contaClienteID)
        {
            bool retorno = false;

            var documento = (await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == contaClienteID))?.Documento;
            if (documento != null)
            {
                retorno = (await _unitOfWork.TribunalJusticaRepository.GetAsync(c => c.Documento.Equals(documento))) != null;
            }

            return retorno;
        }

        public async Task<bool> InsertAsync(TribunalJusticaDto obj)
        {
            bool retorno = false;

            var documento = (await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == obj.ContaClienteID))?.Documento;
            if (documento != null)
            {
                var tj = obj.MapTo<TribunalJustica>();
                tj.Documento = documento;
                tj.DataCriacao = DateTime.Now;
                _unitOfWork.TribunalJusticaRepository.Insert(tj);
                retorno = _unitOfWork.Commit();
            }

            return retorno;
        }
    }
}