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
    public class AppServiceClassificacaoGrupoEconomico : IAppServiceClassificacaoGrupoEconomico
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceClassificacaoGrupoEconomico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


     
        public async Task<IEnumerable<ClassificacaoGrupoEconomicoDto>> GetAll()
        {
            var classificacao = await _unitOfWork.ClassificacaoGrupoEconomicoRepository.GetAllAsync();
            return classificacao.MapTo<IEnumerable<ClassificacaoGrupoEconomicoDto>>();
        }

        public async Task<ClassificacaoGrupoEconomicoDto> GetbyID(int ID)
        {
            var classificacao = await _unitOfWork.ClassificacaoGrupoEconomicoRepository.GetAsync(c=>c.ID.Equals(ID));
            return classificacao.MapTo<ClassificacaoGrupoEconomicoDto>();
        }
    }
}
