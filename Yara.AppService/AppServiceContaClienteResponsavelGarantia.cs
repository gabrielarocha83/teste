using System;
using System.Collections.Generic;
using System.Linq.Expressions;  
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaClienteResponsavelGarantia : IAppServiceContaClienteResponsavelGarantia
    {
        public async Task<ContaClienteResponsavelGarantiaDto> GetAsync(Expression<Func<ContaClienteResponsavelGarantiaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContaClienteResponsavelGarantiaDto>> GetAllFilterAsync(Expression<Func<ContaClienteResponsavelGarantiaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContaClienteResponsavelGarantiaDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(ContaClienteResponsavelGarantiaDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ContaClienteResponsavelGarantiaDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
