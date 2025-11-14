using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceFluxoAlcadaAnalise : IAppServiceFluxoAlcadaAnalise
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoAlcadaAnalise(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FluxoAlcadaAnaliseDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoAlcadaAnaliseDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoAlcadaAnaliseDto>> GetAllFilterAsync(Expression<Func<FluxoAlcadaAnaliseDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoAlcadaAnalise, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoAlcadaAnaliseDto>>(fluxo);
        }

        public async Task<FluxoAlcadaAnaliseDto> GetAsync(Expression<Func<FluxoAlcadaAnaliseDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAsync(Mapper.Map<Expression<Func<FluxoAlcadaAnalise, bool>>>(expression));
            return Mapper.Map<FluxoAlcadaAnaliseDto>(fluxo);
        }

        public async Task<string> GetPerfilAtivoByValor(decimal? valorProposto, Guid? segmentoID)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAllFilterAsync(c => c.SegmentoID == segmentoID.Value && c.ValorAte >= valorProposto && c.Ativo);

            return fluxo.OrderBy(c => c.Nivel).FirstOrDefault()?.Perfil.Descricao;
        }

        public async Task<bool> Inactive(Guid id, Guid userIdAlteracao)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAsync(c => c.ID.Equals(id));
            
            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = false;
            
            _unitOfWork.FluxoAlcadaAnaliseRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }

        public bool Insert(FluxoAlcadaAnaliseDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(FluxoAlcadaAnaliseDto obj)
        {
            var fluxo = obj.MapTo<FluxoAlcadaAnalise>();

            var existNivel = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAsync(c => c.SegmentoID.Equals(obj.SegmentoID) && c.Nivel.Equals(obj.Nivel) && c.Ativo && c.EmpresaID.Equals(obj.EmpresaID));
            if (existNivel != null)
                throw new ArgumentException("Nível já esta cadastrado nesse segmento.");

            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;

            _unitOfWork.FluxoAlcadaAnaliseRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(FluxoAlcadaAnaliseDto obj)
        {
            var fluxo = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.PerfilID = obj.PerfilID;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = obj.Ativo;
            fluxo.ValorDe = obj.ValorDe;
            fluxo.ValorAte = obj.ValorAte;

            _unitOfWork.FluxoAlcadaAnaliseRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }

    }
}
