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
    public class AppServiceFluxoLiberacaoManual : IAppServiceFluxoLiberacaoManual
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoLiberacaoManual(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoLiberacaoManualDto> GetAsync(Expression<Func<FluxoLiberacaoManualDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLimiteCreditoRepository.GetAsync(Mapper.Map<Expression<Func<FluxoLiberacaoManual, bool>>>(expression));
            return Mapper.Map<FluxoLiberacaoManualDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoManualDto>> GetAllFilterAsync(Expression<Func<FluxoLiberacaoManualDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLimiteCreditoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoLiberacaoManual, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoLiberacaoManualDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoManualDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoLimiteCreditoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoLiberacaoManualDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoManualDto>> GetAllListFluxoAsync()
        {
            var fluxo = await _unitOfWork.FluxoLimiteCreditoRepository.GetAllListFluxoAsync();
            return Mapper.Map<IEnumerable<FluxoLiberacaoManualDto>>(fluxo);
        }

        public bool Insert(FluxoLiberacaoManualDto obj)
        {
            var fluxo = obj.MapTo<FluxoLiberacaoManual>();
            fluxo.DataCriacao = DateTime.Now;
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.FluxoLimiteCreditoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(FluxoLiberacaoManualDto obj)
        {
            var fluxo = await _unitOfWork.FluxoLimiteCreditoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            try
            {
                fluxo.ValorDe = obj.ValorDe;
                fluxo.ValorAte = obj.ValorAte;

                fluxo.Usuario = obj.Usuario;
                fluxo.Grupo = obj.Grupo;
                fluxo.Estrutura = obj.Estrutura;
                fluxo.Ativo = obj.Ativo;
                fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
                fluxo.DataAlteracao = obj.DataAlteracao;
            }
            catch (Exception e)
            {
                throw e;
            }

            _unitOfWork.FluxoLimiteCreditoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
        
        public async Task<bool> InsertAsync(FluxoLiberacaoManualDto obj)
        {
            var fluxo = obj.MapTo<FluxoLiberacaoManual>();
            var exist = await _unitOfWork.FluxoLimiteCreditoRepository.GetAsync(c => c.Nivel.Equals(obj.Nivel) && c.Ativo && c.SegmentoID == obj.SegmentoID);

            if (exist != null)
                throw new Exception("Este Nivel de Fluxo de aprovação de limite credito já esta cadastrado.");
            
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;

            _unitOfWork.FluxoLimiteCreditoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var fluxo = await _unitOfWork.FluxoLimiteCreditoRepository.GetAsync(c => c.ID.Equals(id));
            fluxo.Ativo = false;
            _unitOfWork.FluxoLimiteCreditoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
