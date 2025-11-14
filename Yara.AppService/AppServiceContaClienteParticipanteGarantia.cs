using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaClienteParticipanteGarantia : IAppServiceContaClienteParticipanteGarantia
    {
        public async Task<ContaClienteParticipanteGarantiaDto> GetAsync(Expression<Func<ContaClienteParticipanteGarantiaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContaClienteParticipanteGarantiaDto>> GetAllFilterAsync(Expression<Func<ContaClienteParticipanteGarantiaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContaClienteParticipanteGarantiaDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(ContaClienteParticipanteGarantiaDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ContaClienteParticipanteGarantiaDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
