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
    public class AppServiceEstruturaComercialPapel : IAppServiceEstruturaComercialPapel
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppServiceEstruturaComercialPapel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
        public async Task<IEnumerable<EstruturaComercialPapelDto>> GetAllFilterAsync(Expression<Func<EstruturaComercialPapelDto, bool>> expression)
        {
            var EstruturaComercial = await _unitOfWork.EstruturaComercialPapelRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<EstruturaComercialPapel, bool>>>(expression));

            return AutoMapper.Mapper.Map<IEnumerable<EstruturaComercialPapelDto>>(EstruturaComercial);
        }

        public async Task<EstruturaComercialPapelDto> GetByPaper(string sigla)
        {
            var EstruturaComercial = await _unitOfWork.EstruturaComercialPapelRepository.GetAsync(c=>c.ID.Equals(sigla));

            return AutoMapper.Mapper.Map<EstruturaComercialPapelDto>(EstruturaComercial);
        }

   }
}
