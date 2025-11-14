using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceFluxoLiberacaoLimiteCredito : IAppServiceFluxoLiberacaoLimiteCredito
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoLiberacaoLimiteCredito(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoLiberacaoLimiteCreditoDto> GetAsync(Expression<Func<FluxoLiberacaoLimiteCreditoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAsync(Mapper.Map<Expression<Func<FluxoLiberacaoLimiteCredito, bool>>>(expression));
            return Mapper.Map<FluxoLiberacaoLimiteCreditoDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoLimiteCreditoDto>> GetAllFilterAsync(Expression<Func<FluxoLiberacaoLimiteCreditoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoLiberacaoLimiteCredito, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoLiberacaoLimiteCreditoDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoLimiteCreditoDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoLiberacaoLimiteCreditoDto>>(fluxo);
        }

        public bool Insert(FluxoLiberacaoLimiteCreditoDto obj)
        {

            throw new NotImplementedException();

        }

        public async Task<bool> Update(FluxoLiberacaoLimiteCreditoDto obj)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.PrimeiroPerfilID = obj.PrimeiroPerfilID;
            fluxo.SegundoPerfilID = obj.SegundoPerfilID;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = obj.Ativo;
            fluxo.ValorDe = obj.ValorDe;
            fluxo.ValorAte = obj.ValorAte;

            _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(FluxoLiberacaoLimiteCreditoDto obj)
        {
            var fluxo = obj.MapTo<FluxoLiberacaoLimiteCredito>();

            var existNivel = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAsync(c => c.SegmentoID.Equals(obj.SegmentoID) && c.Nivel.Equals(obj.Nivel) && c.Ativo && c.EmpresaID.Equals(obj.EmpresaID));
            if (existNivel != null)
                throw new ArgumentException("Nível já esta cadastrado nesse segmento.");
            
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;
            
            _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id, Guid userIdAlteracao)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAsync(c => c.ID.Equals(id));
            fluxo.Ativo = false;
            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
