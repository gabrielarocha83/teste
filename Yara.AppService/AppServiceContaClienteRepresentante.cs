using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaClienteRepresentante : IAppServiceContaClienteRepresentante
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceContaClienteRepresentante(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContaClienteRepresentanteDto> GetAsync(Expression<Func<ContaClienteRepresentanteDto, bool>> expression)
        {
            var conta = await _unitOfWork.ContaClienteRepresentanteRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteRepresentante, bool>>>(expression));
            return Mapper.Map<ContaClienteRepresentanteDto>(conta);
        }

        public async Task<IEnumerable<ContaClienteRepresentanteDto>> GetAllFilterAsync(Expression<Func<ContaClienteRepresentanteDto, bool>> expression)
        {
            var conta = await _unitOfWork.ContaClienteRepresentanteRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ContaClienteRepresentante, bool>>>(expression));
            return Mapper.Map<IEnumerable<ContaClienteRepresentanteDto>>(conta);
        }

        public async Task<IEnumerable<ContaClienteRepresentanteDto>> GetAllAsync()
        {
            var conta = await _unitOfWork.ContaClienteRepresentanteRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteRepresentanteDto>>(conta);
        }

        public bool Insert(ContaClienteRepresentanteDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ContaClienteRepresentanteDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
