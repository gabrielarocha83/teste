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

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaClienteTelefone : IAppServiceContaClienteTelefone
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceContaClienteTelefone(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<ContaClienteTelefoneDto> GetAsync(Expression<Func<ContaClienteTelefoneDto, bool>> expression)
        {
            var contaclientecodigo = await _untUnitOfWork.ContaClienteTelefoneRepository.GetAsync(
                 Mapper.Map<Expression<Func<ContaClienteTelefone, bool>>>(expression));
            return contaclientecodigo.MapTo<ContaClienteTelefoneDto>();
        }

        public async Task<IEnumerable<ContaClienteTelefoneDto>> GetAllFilterAsync(Expression<Func<ContaClienteTelefoneDto, bool>> expression)
        {
            var contaClientecodigo = await
                _untUnitOfWork.ContaClienteTelefoneRepository.GetAllFilterAsync(
                    Mapper.Map<Expression<Func<ContaClienteTelefone, bool>>>(expression));

            return Mapper.Map<IEnumerable<ContaClienteTelefoneDto>>(contaClientecodigo);
        }

        public async Task<IEnumerable<ContaClienteTelefoneDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

       
        public bool Insert(ContaClienteTelefoneDto obj)
        {
            var telefonesCliente = obj.MapTo<ContaClienteTelefone>();
            telefonesCliente.ID = Guid.NewGuid();
            telefonesCliente.DataCriacao = DateTime.Now;

            _untUnitOfWork.ContaClienteTelefoneRepository.Insert(telefonesCliente);
            return _untUnitOfWork.Commit();
        }

        public Task<bool> Update(ContaClienteTelefoneDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Inactive(ContaClienteTelefoneDto obj)
        {
            var telefone = await _untUnitOfWork.ContaClienteTelefoneRepository.GetAsync(c => c.ID.Equals(obj.ID));
            if (telefone != null)
            {
                telefone.Ativo = false;
                telefone.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
                telefone.DataAlteracao = DateTime.Now;
                _untUnitOfWork.ContaClienteTelefoneRepository.Update(telefone);
                return _untUnitOfWork.Commit();
            }
            return false;
        }

        public async Task<bool> InsertOrUpdateManyAsync(IEnumerable<ContaClienteTelefoneDto> contaClienteTelefones)
        {
            foreach (var contaclientetelefone in contaClienteTelefones)
            {
                if (!contaclientetelefone.ID.Equals(Guid.Empty) ||  contaclientetelefone.ID==null)
                {
                    var contaclientetelefone01 = await _untUnitOfWork.ContaClienteTelefoneRepository.GetAsync(c => c.ID.Equals(contaclientetelefone.ID));
                    contaclientetelefone01.DataAlteracao = DateTime.Now;
                    contaclientetelefone01.UsuarioIDAlteracao = contaclientetelefone.UsuarioIDAlteracao;
                    contaclientetelefone01.Ativo = contaclientetelefone.Ativo;
                  
                    
                  _untUnitOfWork.ContaClienteTelefoneRepository.Update(contaclientetelefone01.MapTo<ContaClienteTelefone>());
                }
                else
                {
                    contaclientetelefone.ID = Guid.NewGuid();
                    contaclientetelefone.DataCriacao = DateTime.Now;
                    contaclientetelefone.UsuarioIDAlteracao = null;
                    _untUnitOfWork.ContaClienteTelefoneRepository.Insert(contaclientetelefone.MapTo<ContaClienteTelefone>());
                }
            }
            return _untUnitOfWork.Commit();

        }
    }
}