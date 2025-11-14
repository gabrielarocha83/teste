using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceEstado : IAppServiceEstado
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceEstado(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<EstadoDto>> GetAllStates()
        {
            var estados = await  _unitOfWork.EstadoRepository.GetAllAsync();
            return estados.MapTo<IEnumerable<EstadoDto>>();
        }


        public async Task<EstadoDto> GetById(Guid ID)
        {
            var estados = await _unitOfWork.EstadoRepository.GetAsync(c=>c.ID.Equals(ID));
            return estados.MapTo<EstadoDto>();
        }
        public async Task<IEnumerable<EstadoDto>> GetAllStatesByRegion(Guid RegionID)
        {
            var estados = await _unitOfWork.EstadoRepository.GetAllFilterAsync(c=>c.RegiaoID.Equals(RegionID));
            return estados.MapTo<IEnumerable<EstadoDto>>();
        }
    }
}
