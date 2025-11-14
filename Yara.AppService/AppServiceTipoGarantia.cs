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
    public class AppServiceTipoGarantia : IAppServiceTipoGarantia
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceTipoGarantia(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<TipoGarantiaDto> GetAsync(Expression<Func<TipoGarantiaDto, bool>> expression)
        {
            var contaclientecodigo = await _untUnitOfWork.TipoGarantiaRepository.GetAsync(
                 Mapper.Map<Expression<Func<TipoGarantia, bool>>>(expression));
            return contaclientecodigo.MapTo<TipoGarantiaDto>();
        }

        public async Task<IEnumerable<TipoGarantiaDto>> GetAllFilterAsync(Expression<Func<TipoGarantiaDto, bool>> expression)
        {
            var contaClientecodigo = await
                _untUnitOfWork.TipoGarantiaRepository.GetAllFilterAsync(
                    Mapper.Map<Expression<Func<TipoGarantia, bool>>>(expression));

            return Mapper.Map<IEnumerable<TipoGarantiaDto>>(contaClientecodigo);
        }

        public async Task<IEnumerable<TipoGarantiaDto>> GetAllAsync()
        {
            var contaClientecodigo = await _untUnitOfWork.TipoGarantiaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<TipoGarantiaDto>>(contaClientecodigo);
        }

        public bool Insert(TipoGarantiaDto obj)
        {
            var tipogarantia = obj.MapTo<TipoGarantia>();
            tipogarantia.ID = Guid.NewGuid();
            tipogarantia.DataCriacao = DateTime.Now;
            tipogarantia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.TipoGarantiaRepository.Insert(tipogarantia);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> Update(TipoGarantiaDto obj)
        {
            var tipogarantia = await _untUnitOfWork.TipoGarantiaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            tipogarantia.Nome = obj.Nome;
            tipogarantia.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            tipogarantia.Ativo = obj.Ativo;
            _untUnitOfWork.TipoGarantiaRepository.Update(tipogarantia);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(TipoClienteDto obj)
        {
            var tipogarantia = obj.MapTo<TipoGarantia>();
            var exist = await _untUnitOfWork.TipoGarantiaRepository.GetAsync(c => c.Nome.Equals(obj.Nome));

            if (exist != null)
                throw new Exception("Tipo de Garantia já está cadastrada.");
            
            tipogarantia.ID = Guid.NewGuid();
            tipogarantia.DataCriacao = DateTime.Now;
            tipogarantia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.TipoGarantiaRepository.Insert(tipogarantia);
            return _untUnitOfWork.Commit();
        }
    }
}