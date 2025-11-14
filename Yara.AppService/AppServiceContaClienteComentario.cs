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

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaClienteComentario : IAppServiceContaClienteComentario
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceContaClienteComentario(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContaClienteComentarioDto> GetIdAsync(Guid id)
        {
            var contaClienteComentario = await _unitOfWork.ContaClienteComentarioRepository.GetAsync(c => c.ID.Equals(id));
            return Mapper.Map<ContaClienteComentarioDto>(contaClienteComentario);
        }

        public async Task<ContaClienteComentarioDto> GetAsync(Expression<Func<ContaClienteComentarioDto, bool>> expression)
        {
            var contaclientecomentario = await _unitOfWork.ContaClienteComentarioRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteComentario, bool>>>(expression));

            return AutoMapper.Mapper.Map<ContaClienteComentarioDto>(contaclientecomentario);
        }

        public async Task<IEnumerable<ContaClienteComentarioDto>> GetAllFilterAsync(Expression<Func<ContaClienteComentarioDto, bool>> expression)
        {
            var contaclientecomentario = await _unitOfWork.ContaClienteComentarioRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ContaClienteComentario, bool>>>(expression));

            return AutoMapper.Mapper.Map<IEnumerable<ContaClienteComentarioDto>>(contaclientecomentario);
        }

        public async Task<IEnumerable<ContaClienteComentarioDto>> GetAllAsync()
        {
            var contaClienteComentarioDto = await _unitOfWork.ContaClienteComentarioRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteComentarioDto>>(contaClienteComentarioDto);
        }

        public bool Insert(ContaClienteComentarioDto obj)
        {
            var contaCliente = obj.MapTo<ContaClienteComentario>();
            contaCliente.ID = Guid.NewGuid();
            contaCliente.DataCriacao = DateTime.Now;
            contaCliente.UsuarioID = contaCliente.UsuarioID;
            contaCliente.Usuario = null;
            _unitOfWork.ContaClienteComentarioRepository.Insert(contaCliente);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ContaClienteComentarioDto obj)
        {
            var contaCliente = obj.MapTo<ContaClienteComentario>();
            _unitOfWork.ContaClienteComentarioRepository.Update(contaCliente);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var contaCliente = await _unitOfWork.ContaClienteComentarioRepository.GetAsync(c => c.ID.Equals(id));
            contaCliente.Ativo = false;
            _unitOfWork.ContaClienteComentarioRepository.Update(contaCliente);
            return _unitOfWork.Commit();
        }
    }
}
