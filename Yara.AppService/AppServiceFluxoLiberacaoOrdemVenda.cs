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
    public class AppServiceFluxoLiberacaoOrdemVenda : IAppServiceFluxoLiberacaoOrdemVenda
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoLiberacaoOrdemVenda(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoLiberacaoOrdemVendaDto> GetAsync(Expression<Func<FluxoLiberacaoOrdemVendaDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoOrdemVendaRepository.GetAsync(Mapper.Map<Expression<Func<FluxoLiberacaoOrdemVenda, bool>>>(expression));
            return Mapper.Map<FluxoLiberacaoOrdemVendaDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoOrdemVendaDto>> GetAllFilterAsync(Expression<Func<FluxoLiberacaoOrdemVendaDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoOrdemVendaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoLiberacaoOrdemVenda, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoLiberacaoOrdemVendaDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoOrdemVendaDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoOrdemVendaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoLiberacaoOrdemVendaDto>>(fluxo);
        }

        public bool Insert(FluxoLiberacaoOrdemVendaDto obj)
        {

            throw new NotImplementedException();

        }

        public async Task<bool> Update(FluxoLiberacaoOrdemVendaDto obj)
        {

            var fluxo = await _unitOfWork.FluxoLiberacaoOrdemVendaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.PerfilID = obj.PerfilID;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = obj.Ativo;
            fluxo.ValorDe = obj.ValorDe;
            fluxo.ValorAte = obj.ValorAte;

            _unitOfWork.FluxoLiberacaoOrdemVendaRepository.Update(fluxo);
            return _unitOfWork.Commit();


        }

        public async Task<bool> InsertAsync(FluxoLiberacaoOrdemVendaDto obj)
        {
            var fluxo = obj.MapTo<FluxoLiberacaoOrdemVenda>();

            //Validação de fluxo para o mesmo perfil no mesmo nivel
            var existNivel = await _unitOfWork.FluxoLiberacaoOrdemVendaRepository.GetAsync(c => c.SegmentoID.Equals(obj.SegmentoID) && c.Nivel.Equals(obj.Nivel) && c.Ativo && c.EmpresaID == obj.EmpresaID);
            if (existNivel != null)
                throw new ArgumentException("Nível já esta cadastrado nesse segmento.");
            
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;

            _unitOfWork.FluxoLiberacaoOrdemVendaRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id, Guid userIdAlteracao)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoOrdemVendaRepository.GetAsync(c => c.ID.Equals(id));
            fluxo.Ativo = false;
            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            _unitOfWork.FluxoLiberacaoOrdemVendaRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
