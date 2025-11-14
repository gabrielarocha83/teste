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
    public class AppServiceFluxoLiberacaoProrrogacao : IAppServiceFluxoLiberacaoProrrogacao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoLiberacaoProrrogacao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoLiberacaoProrrogacaoDto> GetAsync(Expression<Func<FluxoLiberacaoProrrogacaoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoProrrogacaoRepository.GetAsync(Mapper.Map<Expression<Func<FluxoLiberacaoProrrogacao, bool>>>(expression));
            return Mapper.Map<FluxoLiberacaoProrrogacaoDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoProrrogacaoDto>> GetAllFilterAsync(Expression<Func<FluxoLiberacaoProrrogacaoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoProrrogacaoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoLiberacaoProrrogacao, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoLiberacaoProrrogacaoDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoLiberacaoProrrogacaoDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoProrrogacaoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoLiberacaoProrrogacaoDto>>(fluxo);
        }

        public bool Insert(FluxoLiberacaoProrrogacaoDto obj)
        {

            throw new NotImplementedException();

        }

        public async Task<bool> Update(FluxoLiberacaoProrrogacaoDto obj)
        {

            var fluxo = await _unitOfWork.FluxoLiberacaoProrrogacaoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            fluxo.PrimeiroPerfilID = obj.PrimeiroPerfilID;
            fluxo.SegundoPerfilID = obj.SegundoPerfilID;
            fluxo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            fluxo.Ativo = obj.Ativo;
            fluxo.ValorDe = obj.ValorDe;
            fluxo.ValorAte = obj.ValorAte;

            _unitOfWork.FluxoLiberacaoProrrogacaoRepository.Update(fluxo);
            return _unitOfWork.Commit();


        }

        public async Task<bool> InsertAsync(FluxoLiberacaoProrrogacaoDto obj)
        {
            var fluxo = obj.MapTo<FluxoLiberacaoProrrogacao>();

            //Validação de fluxo para o mesmo perfil no mesmo nivel
            var existNivel = await _unitOfWork.FluxoLiberacaoProrrogacaoRepository.GetAsync(c => c.SegmentoID.Equals(obj.SegmentoID) && c.Nivel.Equals(obj.Nivel) && c.Ativo && c.EmpresaID == obj.EmpresaID);
            if (existNivel != null)
                throw new ArgumentException("Nível já esta cadastrado nesse segmento.");
            
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.DataCriacao = DateTime.Now;

            _unitOfWork.FluxoLiberacaoProrrogacaoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id, Guid userIdAlteracao)
        {
            var fluxo = await _unitOfWork.FluxoLiberacaoProrrogacaoRepository.GetAsync(c => c.ID.Equals(id));
            fluxo.Ativo = false;
            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.DataAlteracao = DateTime.Now;
            _unitOfWork.FluxoLiberacaoProrrogacaoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
