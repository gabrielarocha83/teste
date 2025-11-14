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
    public class AppServiceDivisaoRemessa : IAppServiceDivisaoRemessa
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceDivisaoRemessa(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DivisaoRemessaDto> GetDadosAsync(int Divisao, int Item, string Numero)
        {
            var remessa = await _unitOfWork.DivisaoRemessaRepository.GetDivisaoAsync(Divisao, Item, Numero);
           

            return remessa.MapTo<DivisaoRemessaDto>();
        }

        public async Task<IEnumerable<DivisaoRemessaLogFluxoDto>> GetAllLogByDivisao(int Divisao, int Item, string Numero, Guid SolicitanteID)
        {
            var remessa = await _unitOfWork.DivisaoRemessaRepository.GetAllLogByDivisao(Divisao, Item, Numero, SolicitanteID);
            return remessa.MapTo<IEnumerable<DivisaoRemessaLogFluxoDto>>();
        }

        public async Task<DivisaoRemessaDto> GetAsync(Expression<Func<DivisaoRemessaDto, bool>> expression)
        {
            var remessa = await _unitOfWork.DivisaoRemessaRepository.GetAsync(Mapper.Map<Expression<Func<DivisaoRemessa, bool>>>(expression));
            return Mapper.Map<DivisaoRemessaDto>(remessa);
        }

        public async Task<IEnumerable<DivisaoRemessaDto>> GetAllFilterAsync(Expression<Func<DivisaoRemessaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DivisaoRemessaDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(DivisaoRemessaDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(DivisaoRemessaDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DivisaoRemessaCockPitDto>> GetAllPendencyByUser(Guid UserID, string EmpresaID)
        {
            var divisao = await _unitOfWork.DivisaoRemessaRepository.GetAllPendencyByUser(UserID, EmpresaID);

            return divisao.MapTo<IEnumerable<DivisaoRemessaCockPitDto>>();
        }
    }
}
