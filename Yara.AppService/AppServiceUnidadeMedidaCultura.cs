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
    public class AppServiceUnidadeMedidaCultura : IAppServiceUnidadeMedidaCultura
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceUnidadeMedidaCultura(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<UnidadeMedidaCulturaDto> GetAsync(Expression<Func<UnidadeMedidaCulturaDto, bool>> expression)
        {
            var contaclientecodigo = await _untUnitOfWork.UnidadeMedidaCulturaRepository.GetAsync(
                 Mapper.Map<Expression<Func<UnidadeMedidaCultura, bool>>>(expression));
            return contaclientecodigo.MapTo<UnidadeMedidaCulturaDto>();
        }

        public async Task<IEnumerable<UnidadeMedidaCulturaDto>> GetAllFilterAsync(Expression<Func<UnidadeMedidaCulturaDto, bool>> expression)
        {
            var contaClientecodigo = await
                _untUnitOfWork.UnidadeMedidaCulturaRepository.GetAllFilterAsync(
                    Mapper.Map<Expression<Func<UnidadeMedidaCultura, bool>>>(expression));

            return Mapper.Map<IEnumerable<UnidadeMedidaCulturaDto>>(contaClientecodigo);
        }

        public async Task<IEnumerable<UnidadeMedidaCulturaDto>> GetAllAsync()
        {
            var contaClientecodigo = await _untUnitOfWork.UnidadeMedidaCulturaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<UnidadeMedidaCulturaDto>>(contaClientecodigo);
        }

       
        public bool Insert(UnidadeMedidaCulturaDto obj)
        {
            var tipogaratia = obj.MapTo<UnidadeMedidaCultura>();
            tipogaratia.ID = Guid.NewGuid();
            tipogaratia.DataCriacao = DateTime.Now;
            tipogaratia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.UnidadeMedidaCulturaRepository.Insert(tipogaratia);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> Update(UnidadeMedidaCulturaDto obj)
        {
            var tipogarantia = await _untUnitOfWork.UnidadeMedidaCulturaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            tipogarantia.Nome = obj.Nome;
            tipogarantia.Sigla = obj.Sigla;
            tipogarantia.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            tipogarantia.Ativo = obj.Ativo;
            _untUnitOfWork.UnidadeMedidaCulturaRepository.Update(tipogarantia);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(UnidadeMedidaCulturaDto obj)
        {
            var tipogarantia = obj.MapTo<UnidadeMedidaCultura>();
            var exist = await _untUnitOfWork.UnidadeMedidaCulturaRepository.GetAsync(c => c.Nome.Equals(obj.Nome));

            if (exist != null)
                throw new Exception("Unidade de Medida de cultura já cadastrada");

            tipogarantia.DataCriacao = DateTime.Now;
            tipogarantia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.UnidadeMedidaCulturaRepository.Insert(tipogarantia);
            return _untUnitOfWork.Commit();
        }
    }
}