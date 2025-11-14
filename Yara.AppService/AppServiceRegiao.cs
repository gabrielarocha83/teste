using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceRegiao : IAppServiceRegiao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceRegiao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RegiaoDto>> GetAllRegion()
        {
            var regiao = await _unitOfWork.RegiaoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<RegiaoDto>>(regiao);
        }
       
    }
}
