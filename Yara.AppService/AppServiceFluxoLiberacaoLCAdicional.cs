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
    public class AppServiceFluxoLiberacaoLCAdicional : IAppServiceFluxoLiberacaoLCAdicional
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoLiberacaoLCAdicional(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoLiberacaoLCAdicionalDto> GetAsync(Expression<Func<FluxoLiberacaoLCAdicionalDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAsync(Mapper.Map<Expression<Func<FluxoLiberacaoLCAdicional, bool>>>(expression));
            return Mapper.Map<FluxoLiberacaoLCAdicionalDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoLCAdicionalDto>> GetAllFilterAsync(Expression<Func<FluxoLiberacaoLCAdicionalDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoLiberacaoLCAdicional, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoLiberacaoLCAdicionalDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoLCAdicionalDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoLiberacaoLCAdicionalDto>>(fluxo);
        }

        public bool Insert(FluxoLiberacaoLCAdicionalDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(FluxoLiberacaoLCAdicionalDto obj)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.PrimeiroPerfilID = obj.PrimeiroPerfilID;
            fluxo.SegundoPerfilID = obj.SegundoPerfilID;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = obj.Ativo;
            fluxo.ValorDe = obj.ValorDe;
            fluxo.ValorAte = obj.ValorAte;

            _unitOfWork.FluxoLiberacaoLCAdicionalRepository.Update(fluxo);
            
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(FluxoLiberacaoLCAdicionalDto obj)
        {
            var fluxo = obj.MapTo<FluxoLiberacaoLCAdicional>();

            //Validação de fluxo para o mesmo perfil no mesmo nivel
            var existNivel = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAsync(c => c.SegmentoID.Equals(obj.SegmentoID) && c.Nivel.Equals(obj.Nivel) && c.Ativo && c.EmpresaID.Equals(obj.EmpresaID));
            if (existNivel != null)
                throw new ArgumentException("Nível já esta cadastrado nesse segmento.");
            
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;
            
            _unitOfWork.FluxoLiberacaoLCAdicionalRepository.Insert(fluxo);
            
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id, Guid userIdAlteracao)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAsync(c => c.ID.Equals(id));
            
            fluxo.Ativo = false;
            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            
            _unitOfWork.FluxoLiberacaoLCAdicionalRepository.Update(fluxo);
            
            return _unitOfWork.Commit();
        }
    }
}
