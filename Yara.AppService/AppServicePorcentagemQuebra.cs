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
    public class AppServicePorcentagemQuebra : IAppServicePorcentagemQuebra
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServicePorcentagemQuebra(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<PorcentagemQuebraDto> GetAsync(Expression<Func<PorcentagemQuebraDto, bool>> expression)
        {
            var contaclientecodigo = await _untUnitOfWork.PorcentagemQuebraRepository.GetAsync(
                 Mapper.Map<Expression<Func<PorcentagemQuebra, bool>>>(expression));
            return contaclientecodigo.MapTo<PorcentagemQuebraDto>();
        }

        public async Task<IEnumerable<PorcentagemQuebraDto>> GetAllFilterAsync(Expression<Func<PorcentagemQuebraDto, bool>> expression)
        {
            var contaClientecodigo = await
                _untUnitOfWork.PorcentagemQuebraRepository.GetAllFilterAsync(
                    Mapper.Map<Expression<Func<PorcentagemQuebra, bool>>>(expression));

            return Mapper.Map<IEnumerable<PorcentagemQuebraDto>>(contaClientecodigo);
        }

        public async Task<IEnumerable<PorcentagemQuebraDto>> GetAllAsync()
        {
            var contaClientecodigo = await _untUnitOfWork.PorcentagemQuebraRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<PorcentagemQuebraDto>>(contaClientecodigo);
        }
        
        public bool Insert(PorcentagemQuebraDto obj)
        {
            var tipogaratia = obj.MapTo<PorcentagemQuebra>();
            tipogaratia.DataCriacao = DateTime.Now;
            tipogaratia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.PorcentagemQuebraRepository.Insert(tipogaratia);
            return _untUnitOfWork.Commit();

        }

        public async Task<bool> Update(PorcentagemQuebraDto obj)
        {
            var tipogarantia = await _untUnitOfWork.PorcentagemQuebraRepository.GetAsync(c => c.ID.Equals(obj.ID));

            tipogarantia.Porcentagem = obj.Porcentagem;
            tipogarantia.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            tipogarantia.Ativo = obj.Ativo;
            _untUnitOfWork.PorcentagemQuebraRepository.Update(tipogarantia);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(PorcentagemQuebraDto obj)
        {
            var tipogarantia = obj.MapTo<PorcentagemQuebra>();
            var exist = await _untUnitOfWork.PorcentagemQuebraRepository.GetAsync(c => c.Porcentagem.Equals(obj.Porcentagem));

            if (exist != null)
                throw new Exception("Porcentagem de quebra já cadastrada");

            tipogarantia.DataCriacao = DateTime.Now;
            tipogarantia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _untUnitOfWork.PorcentagemQuebraRepository.Insert(tipogarantia);
            return _untUnitOfWork.Commit();
        }
    }
}