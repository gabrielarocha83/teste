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

namespace Yara.AppService
{
    public class AppServiceFluxoRenovacaoVigenciaLC : IAppServiceFluxoRenovacaoVigenciaLC
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoRenovacaoVigenciaLC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoRenovacaoVigenciaLCDto> GetAsync(Expression<Func<FluxoRenovacaoVigenciaLCDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoRenovacaoVigenciaLCRepository.GetAsync(Mapper.Map<Expression<Func<FluxoRenovacaoVigenciaLC, bool>>>(expression));
            return Mapper.Map<FluxoRenovacaoVigenciaLCDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoRenovacaoVigenciaLCDto>> GetAllFilterAsync(Expression<Func<FluxoRenovacaoVigenciaLCDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoRenovacaoVigenciaLCRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoRenovacaoVigenciaLC, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoRenovacaoVigenciaLCDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoRenovacaoVigenciaLCDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoRenovacaoVigenciaLCRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoRenovacaoVigenciaLCDto>>(fluxo);
        }

        public bool Insert(FluxoRenovacaoVigenciaLCDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(FluxoRenovacaoVigenciaLCDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(FluxoRenovacaoVigenciaLCDto obj)
        {
            var exist = await _unitOfWork.FluxoRenovacaoVigenciaLCRepository.GetAsync(c => c.Nivel.Equals(obj.Nivel) && c.Ativo && c.UsuarioId.Equals(obj.UsuarioId) && c.EmpresaID.Equals(obj.EmpresaID));
            if (exist != null)
                throw new ArgumentException("Este nível de fluxo para este usuário já foi cadastrado.");

            var fluxo = obj.MapTo<FluxoRenovacaoVigenciaLC>();

            _unitOfWork.FluxoRenovacaoVigenciaLCRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> RemoveAsync(FluxoRenovacaoVigenciaLCDto obj)
        {
            // Essa validação deve ser adicionada após a criação das estruturas de comitê de proposta de renovação de lc
            //var exist = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.GetAllFilterAsync(c => c.FluxoRenovacaoVigenciaLCID.Equals(id) && (c.StatusGrupoEconomicoFluxoID.Equals("PE") || c.StatusGrupoEconomicoFluxoID.Equals("PI")));
            //if (exist != null && exist.Count() > 0)
            //    throw new ArgumentException("Existem aprovações pendentes para este nivel de fluxo para a esta classificação de grupo.");

            var fluxo = await _unitOfWork.FluxoRenovacaoVigenciaLCRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.DataAlteracao = obj.DataAlteracao;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.Ativo = obj.Ativo;

            _unitOfWork.FluxoRenovacaoVigenciaLCRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
