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
    public class AppServiceTipoEmpresa : IAppServiceTipoEmpresa
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTipoEmpresa(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TipoEmpresaDto> GetAsync(Expression<Func<TipoEmpresaDto, bool>> expression)
        {
            var tipo = await _unitOfWork.TipoEmpresaRepository.GetAsync(Mapper.Map<Expression<Func<TipoEmpresa, bool>>>(expression));
            return Mapper.Map<TipoEmpresaDto>(tipo);
        }

        public async Task<IEnumerable<TipoEmpresaDto>> GetAllFilterAsync(Expression<Func<TipoEmpresaDto, bool>> expression)
        {
            var tipo = await _unitOfWork.TipoEmpresaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<TipoEmpresa, bool>>>(expression));
            return Mapper.Map<IEnumerable<TipoEmpresaDto>>(tipo);
        }

        public async Task<IEnumerable<TipoEmpresaDto>> GetAllAsync()
        {
            var tipo = await _unitOfWork.TipoEmpresaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<TipoEmpresaDto>>(tipo);
        }

        public bool Insert(TipoEmpresaDto obj)
        {
            var tipo = obj.MapTo<TipoEmpresa>();
            tipo.DataCriacao = DateTime.Now;
            tipo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.TipoEmpresaRepository.Insert(tipo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(TipoEmpresaDto obj)
        {
            var tipo = await _unitOfWork.TipoEmpresaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            tipo.Tipo = obj.Tipo;
            tipo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            tipo.Ativo = obj.Ativo;
            _unitOfWork.TipoEmpresaRepository.Update(tipo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(TipoEmpresaDto obj)
        {
            var tipo = obj.MapTo<TipoEmpresa>();
            var exist = await _unitOfWork.TipoEmpresaRepository.GetAsync(c => c.Tipo.Equals(obj.Tipo));

            if (exist != null)
                throw new Exception("Tipo de Empresa ja esta cadastrado.");

            tipo.DataCriacao = DateTime.Now;
            tipo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.TipoEmpresaRepository.Insert(tipo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var tipo = await _unitOfWork.TipoEmpresaRepository.GetAsync(c => c.ID.Equals(id));
            tipo.Ativo = false;
            _unitOfWork.TipoEmpresaRepository.Update(tipo);
            return _unitOfWork.Commit();
        }
    }
}
