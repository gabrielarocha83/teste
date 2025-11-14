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

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaClienteCodigo : IAppServiceContaClienteCodigo
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceContaClienteCodigo(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<ContaClienteCodigoDto> GetAsync(Expression<Func<ContaClienteCodigoDto, bool>> expression)
        {
            var contaclientecodigo = await _untUnitOfWork.ContaClienteCodigoRepository.GetAsync(
                 Mapper.Map<Expression<Func<ContaClienteCodigo, bool>>>(expression));
            return contaclientecodigo.MapTo<ContaClienteCodigoDto>();
        }

        public async Task<IEnumerable<ContaClienteCodigoDto>> GetAllFilterAsync(Expression<Func<ContaClienteCodigoDto, bool>> expression)
        {
            var contaClientecodigo = await
                _untUnitOfWork.ContaClienteCodigoRepository.GetAllFilterAsync(
                    Mapper.Map<Expression<Func<ContaClienteCodigo, bool>>>(expression));

            return Mapper.Map<IEnumerable<ContaClienteCodigoDto>>(contaClientecodigo);
        }

        public async Task<IEnumerable<ContaClienteCodigoDto>> GetAllAsync()
        {
            var contaClientecodigo = await _untUnitOfWork.ContaClienteCodigoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteCodigoDto>>(contaClientecodigo);
        }

       
        public bool Insert(ContaClienteCodigoDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ContaClienteCodigoDto obj)
        {
            throw new NotImplementedException();
        }
    }
}