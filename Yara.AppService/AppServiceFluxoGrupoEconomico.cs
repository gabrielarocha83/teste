using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AppServiceFluxoGrupoEconomico : IAppServiceFluxoGrupoEconomico
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFluxoGrupoEconomico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluxoGrupoEconomicoDto> GetAsync(Expression<Func<FluxoGrupoEconomicoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAsync(Mapper.Map<Expression<Func<FluxoGrupoEconomico, bool>>>(expression));
            return Mapper.Map<FluxoGrupoEconomicoDto>(fluxo);
        }

        public async Task<IEnumerable<FluxoGrupoEconomicoDto>> GetAllFilterAsync(Expression<Func<FluxoGrupoEconomicoDto, bool>> expression)
        {
            var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<FluxoGrupoEconomico, bool>>>(expression));
            return Mapper.Map<IEnumerable<FluxoGrupoEconomicoDto>>(fluxo);
        }

        public async Task<IEnumerable<FluxoGrupoEconomicoDto>> GetAllAsync()
        {
            var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FluxoGrupoEconomicoDto>>(fluxo);
        }

        public bool Insert(FluxoGrupoEconomicoDto obj)
        {
            var fluxo = obj.MapTo<FluxoGrupoEconomico>();

            fluxo.DataCriacao = DateTime.Now;
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.EmpresaID = obj.EmpresaID;

            _unitOfWork.FluxoGrupoEconomicoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public Task<bool> Update(FluxoGrupoEconomicoDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(FluxoGrupoEconomicoDto obj)
        {
            var exist = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAsync(c => c.Nivel.Equals(obj.Nivel) && c.Ativo && c.ClassificacaoGrupoEconomicoId == obj.ClassificacaoGrupoEconomicoId && c.EmpresaID == obj.EmpresaID);
            if (exist != null)
                throw new ArgumentException("Este nível de fluxo para a esta classificação de grupo já foi cadastrado.");

            var fluxo = obj.MapTo<FluxoGrupoEconomico>();

            fluxo.DataCriacao = DateTime.Now;
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            fluxo.EmpresaID = obj.EmpresaID;

            _unitOfWork.FluxoGrupoEconomicoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InactiveAsync(Guid id, Guid userIdAlteracao)
        {
            var exist = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.GetAllFilterAsync(c => c.FluxoGrupoEconomicoID.Equals(id) && (c.StatusGrupoEconomicoFluxoID.Equals("PE") || c.StatusGrupoEconomicoFluxoID.Equals("PI")));
            if (exist != null && exist.Count() > 0)
                throw new ArgumentException("Existem aprovações pendentes para este nivel de fluxo para a esta classificação de grupo.");

            var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAsync(c => c.ID.Equals(id));

            fluxo.DataAlteracao = DateTime.Now;
            fluxo.UsuarioIDAlteracao = userIdAlteracao;
            fluxo.Ativo = false;

            _unitOfWork.FluxoGrupoEconomicoRepository.Update(fluxo);
            return _unitOfWork.Commit();
        }
    }
}
