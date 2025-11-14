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
    public class AppServiceFerias : IAppServiceFerias
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFerias(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FeriasDto> GetAsync(Expression<Func<FeriasDto, bool>> expression)
        {
            var Ferias = await _unitOfWork.FeriasRepository.GetAsync(Mapper.Map<Expression<Func<Ferias, bool>>>(expression));
            return Mapper.Map<FeriasDto>(Ferias);
        }

        public async Task<IEnumerable<FeriasDto>> GetAllFilterAsync(Expression<Func<FeriasDto, bool>> expression)
        {
            var Ferias = await _unitOfWork.FeriasRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Ferias, bool>>>(expression));
            return Mapper.Map<IEnumerable<FeriasDto>>(Ferias);
        }

        public async Task<IEnumerable<FeriasDto>> GetAllAsync()
        {
            var Ferias = await _unitOfWork.FeriasRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FeriasDto>>(Ferias.Where(c=>c.Ativo));
        }

        public bool Insert(FeriasDto obj)
        {
            var Ferias = obj.MapTo<Ferias>();
            Ferias.DataCriacao = DateTime.Now;
            Ferias.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.FeriasRepository.Insert(Ferias);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(FeriasDto obj)
        {
            var Ferias = await _unitOfWork.FeriasRepository.GetAsync(c => c.ID.Equals(obj.ID));
            Ferias.FeriasInicio = obj.FeriasInicio;
            Ferias.FeriasFim = obj.FeriasFim;
            Ferias.UsuarioID = obj.UsuarioID;
            Ferias.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            Ferias.DataAlteracao = DateTime.Now;
            Ferias.SubstitutoID = obj.SubstitutoID;

            _unitOfWork.FeriasRepository.Update(Ferias);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(FeriasDto obj)
        {
            //var verFerias = await _unitOfWork.FeriasRepository.GetStatus(obj.UsuarioID, obj.FeriasInicio, obj.FeriasFim);
            //if (verFerias != null)
            //    throw new ArgumentException("Problema ao cadastrar período de substituiçãode férias. As datas não podem sobrepor o(s) período(s) já cadastrado(s).");
            
            var Ferias = obj.MapTo<Ferias>();
            Ferias.ID= Guid.NewGuid();
            Ferias.FeriasInicio = obj.FeriasInicio;
            Ferias.FeriasFim = obj.FeriasFim;
            Ferias.Ativo = true;
            Ferias.UsuarioID = obj.UsuarioID;
            Ferias.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            Ferias.DataCriacao = DateTime.Now;
            _unitOfWork.FeriasRepository.Insert(Ferias);
            return _unitOfWork.Commit();
        }
        
        public async Task<bool> Inactive(Guid id)
        {
            var Ferias = await _unitOfWork.FeriasRepository.GetAsync(c => c.ID.Equals(id));
            Ferias.Ativo = false;
            _unitOfWork.FeriasRepository.Update(Ferias);
            return _unitOfWork.Commit();
        }

        public async Task<IEnumerable<FeriasDto>> GetFeriasByIDUser(Guid user)
        {
            var Ferias = await _unitOfWork.FeriasRepository.GetFeriasByIDUser(user);
            return Mapper.Map<IEnumerable<FeriasDto>>(Ferias);
        }
    }
}
