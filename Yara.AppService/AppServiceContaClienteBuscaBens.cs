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
    public class AppServiceContaClienteBuscaBens : IAppServiceContaClienteBuscaBens
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceContaClienteBuscaBens(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContaClienteBuscaBensDto> GetAsync(Expression<Func<ContaClienteBuscaBensDto, bool>> expression)
        {
            var visita = await _unitOfWork.ContaClienteBuscaBensRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteBuscaBens, bool>>>(expression));
            return Mapper.Map<ContaClienteBuscaBensDto>(visita);
        }

        public async Task<IEnumerable<ContaClienteBuscaBensDto>> GetAllFilterAsync(Expression<Func<ContaClienteBuscaBensDto, bool>> expression)
        {
            var visita = await _unitOfWork.ContaClienteBuscaBensRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ContaClienteBuscaBens, bool>>>(expression));
            return Mapper.Map<IEnumerable<ContaClienteBuscaBensDto>>(visita);
        }

        public async Task<IEnumerable<ContaClienteBuscaBensDto>> GetAllAsync()
        {
            var visita = await _unitOfWork.ContaClienteBuscaBensRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteBuscaBensDto>>(visita);
        }

        public bool Insert(ContaClienteBuscaBensDto obj)
        {
            var buscabens = obj.MapTo<ContaClienteBuscaBens>();
            buscabens.DataCriacao = DateTime.Now;
            buscabens.UsuarioIDCriacao = obj.UsuarioIDCriacao;

            _unitOfWork.ContaClienteBuscaBensRepository.Insert(buscabens);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ContaClienteBuscaBensDto obj)
        {
            var buscabens = await _unitOfWork.ContaClienteBuscaBensRepository.GetAsync(c => c.ID.Equals(obj.ID));
            buscabens.DataPatrimonio = obj.DataPatrimonio;
            buscabens.Patrimonio = buscabens.Patrimonio + " " + obj.Patrimonio;
            buscabens.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            buscabens.DataAlteracao = obj.DataAlteracao;

            _unitOfWork.ContaClienteBuscaBensRepository.Update(buscabens);

            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ContaClienteBuscaBensDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
