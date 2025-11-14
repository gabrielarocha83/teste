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
    public class AppServiceContaClienteEstruturaComercial : IAppServiceContaClienteEstruturaComercial
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceContaClienteEstruturaComercial(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContaClienteEstruturaComercialDto> GetAsync(Expression<Func<ContaClienteEstruturaComercialDto, bool>> expression)
        {
            var conta = await _unitOfWork.ContaClienteEstruturaComercialRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteEstruturaComercial, bool>>>(expression));
            return Mapper.Map<ContaClienteEstruturaComercialDto>(conta);
        }

        public async Task<IEnumerable<ContaClienteEstruturaComercialDto>> GetAllFilterAsync(Expression<Func<ContaClienteEstruturaComercialDto, bool>> expression)
        {
            var conta = await _unitOfWork.ContaClienteEstruturaComercialRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ContaClienteEstruturaComercial, bool>>>(expression));
            return Mapper.Map<IEnumerable<ContaClienteEstruturaComercialDto>>(conta);
        }

        public async Task<IEnumerable<ContaClienteEstruturaComercialDto>> GetAllAsync()
        {
            var conta = await _unitOfWork.ContaClienteEstruturaComercialRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteEstruturaComercialDto>>(conta);
        }

        public bool Insert(ContaClienteEstruturaComercialDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ContaClienteEstruturaComercialDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
