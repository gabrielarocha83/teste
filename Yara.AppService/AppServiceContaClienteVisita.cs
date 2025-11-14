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
    public class AppServiceContaClienteVisita : IAppServiceContaClienteVisita
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceContaClienteVisita(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContaClienteVisitaDto> GetAsync(Expression<Func<ContaClienteVisitaDto, bool>> expression)
        {
            var visita = await _unitOfWork.ContaClienteVisitaRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteVisita, bool>>>(expression));
            return Mapper.Map<ContaClienteVisitaDto>(visita);
        }

        public async Task<IEnumerable<ContaClienteVisitaDto>> GetAllFilterAsync(Expression<Func<ContaClienteVisitaDto, bool>> expression)
        {
            var visita = await _unitOfWork.ContaClienteVisitaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ContaClienteVisita, bool>>>(expression));
            return Mapper.Map<IEnumerable<ContaClienteVisitaDto>>(visita);
        }

        public async Task<IEnumerable<ContaClienteVisitaDto>> GetAllAsync()
        {
            var visita = await _unitOfWork.ContaClienteVisitaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteVisitaDto>>(visita);
        }

        public bool Insert(ContaClienteVisitaDto obj)
        {
            var visita = obj.MapTo<ContaClienteVisita>();
            visita.DataCriacao = DateTime.Now;
            visita.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ContaClienteVisitaRepository.Insert(visita);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ContaClienteVisitaDto obj)
        {
            var visita = await _unitOfWork.ContaClienteVisitaRepository.GetAsync(c => c.ID.Equals(obj.ID));
            visita.DataParecer = obj.DataParecer;
            visita.Parecer = visita.Parecer + " " + obj.Parecer;
            visita.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            visita.DataAlteracao = obj.DataAlteracao;

            _unitOfWork.ContaClienteVisitaRepository.Update(visita);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ContaClienteVisitaDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
