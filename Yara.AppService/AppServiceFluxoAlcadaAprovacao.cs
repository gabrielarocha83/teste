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
    public class AppServiceFluxoAlcadaAprovacao : IAppServiceFluxoAlcadaAprovacao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoAlcadaAprovacao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FluxoAlcadaAprovacaoDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAprovacaoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoAlcadaAprovacaoDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoAlcadaAprovacaoDto>> GetAllFilterAsync(Expression<Func<FluxoAlcadaAprovacaoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAprovacaoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoAlcadaAprovacao, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoAlcadaAprovacaoDto>>(fluxo);
        }

        public async Task<FluxoAlcadaAprovacaoDto> GetAsync(Expression<Func<FluxoAlcadaAprovacaoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAprovacaoRepository.GetAsync(Mapper.Map<Expression<Func<FluxoAlcadaAprovacao, bool>>>(expression));
            return Mapper.Map<FluxoAlcadaAprovacaoDto>(fluxo);
        }

        public async Task<bool> Inactive(Guid id, Guid userIdAlteracao)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAprovacaoRepository.GetAsync(c => c.ID.Equals(id));

            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = false;

            _unitOfWork.FluxoAlcadaAprovacaoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }

        public bool Insert(FluxoAlcadaAprovacaoDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(FluxoAlcadaAprovacaoDto obj)
        {
            var fluxo = obj.MapTo<FluxoAlcadaAprovacao>();

            var existNivel = await _unitOfWork.FluxoAlcadaAprovacaoRepository.GetAsync(c => c.SegmentoID.Equals(obj.SegmentoID) && c.Nivel.Equals(obj.Nivel) && c.Ativo && c.EmpresaID.Equals(obj.EmpresaID));
            if (existNivel != null)
                throw new ArgumentException("Nível já esta cadastrado nesse segmento.");

            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;

            _unitOfWork.FluxoAlcadaAprovacaoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(FluxoAlcadaAprovacaoDto obj)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAprovacaoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.PerfilID = obj.PerfilID;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = obj.Ativo;
            fluxo.ValorDe = obj.ValorDe;
            fluxo.ValorAte = obj.ValorAte;

            _unitOfWork.FluxoAlcadaAprovacaoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
