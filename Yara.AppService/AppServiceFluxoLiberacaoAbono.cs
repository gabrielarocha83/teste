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
    public class AppServiceFluxoLiberacaoAbono : IAppServiceFluxoLiberacaoAbono
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoLiberacaoAbono(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoLiberacaoAbonoDto> GetAsync(Expression<Func<FluxoLiberacaoAbonoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAsync(Mapper.Map<Expression<Func<FluxoLiberacaoAbono, bool>>>(expression));
            return Mapper.Map<FluxoLiberacaoAbonoDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoAbonoDto>> GetAllFilterAsync(Expression<Func<FluxoLiberacaoAbonoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoLiberacaoAbono, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoLiberacaoAbonoDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoAbonoDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoLiberacaoAbonoDto>>(fluxo);
        }

        public bool Insert(FluxoLiberacaoAbonoDto obj)
        {

            throw new NotImplementedException();

        }

        public async Task<bool> Update(FluxoLiberacaoAbonoDto obj)
        {

            var fluxo = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.PrimeiroPerfilID = obj.PrimeiroPerfilID;
            fluxo.SegundoPerfilID = obj.SegundoPerfilID;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = obj.Ativo;
            fluxo.ValorDe = obj.ValorDe;
            fluxo.ValorAte = obj.ValorAte;

            _unitOfWork.FluxoLiberacaoAbonoRepository.Update(fluxo);
            return _unitOfWork.Commit();


        }

        public async Task<bool> InsertAsync(FluxoLiberacaoAbonoDto obj)
        {
            var fluxo = obj.MapTo<FluxoLiberacaoAbono>();

            //Validação de fluxo para o mesmo perfil no mesmo nivel
            var existNivel = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAsync(c => c.SegmentoID.Equals(obj.SegmentoID) && c.Nivel.Equals(obj.Nivel) && c.Ativo && c.EmpresaID == obj.EmpresaID);
            if (existNivel != null)
                throw new ArgumentException("Nível já esta cadastrado nesse segmento.");
            
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;

            _unitOfWork.FluxoLiberacaoAbonoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id, Guid userIdAlteracao)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAsync(c => c.ID.Equals(id));
            fluxo.Ativo = false;
            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            _unitOfWork.FluxoLiberacaoAbonoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
