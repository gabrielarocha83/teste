using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceRepresentante : IAppServiceRepresentante
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceRepresentante(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RepresentanteDto>> GetAllRepresentation()
        {
            var representante = await _unitOfWork.RepresentanteRepository.GetAllFilterAsync(c=>c.Ativo);
            return Mapper.Map<IEnumerable<RepresentanteDto>>(representante);
        }

        public async Task<IEnumerable<RepresentanteDto>> GetAllRepresentationByAccountClient(Guid contaID, string EmpresaID)
        {
            var representantes = await _unitOfWork.ContaClienteRepresentanteRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(contaID) && c.EmpresasID.Equals(EmpresaID));
            var retorno = representantes.Select(c => c.Representante);
            return Mapper.Map<IEnumerable<RepresentanteDto>>(retorno);
        }
    }
}
