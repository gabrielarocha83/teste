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
    public class AppServiceCustoHaRegiao : IAppServiceCustoHaRegiao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceCustoHaRegiao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<CustoHaRegiaoDto> GetAsync(Expression<Func<CustoHaRegiaoDto, bool>> expression)
        {
            var cult = await _unitOfWork.CustoHaRegiaoRepository.GetAsync(Mapper.Map<Expression<Func<CustoHaRegiao, bool>>>(expression));
            return Mapper.Map<CustoHaRegiaoDto>(cult);
        }

        public async Task<IEnumerable<CustoHaRegiaoDto>> GetAllFilterAsync(Expression<Func<CustoHaRegiaoDto, bool>> expression)
        {
            var cult = await _unitOfWork.CustoHaRegiaoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<CustoHaRegiao, bool>>>(expression));
            return Mapper.Map<IEnumerable<CustoHaRegiaoDto>>(cult);
        }

        public async Task<IEnumerable<CustoHaRegiaoDto>> GetAllAsync()
        {
            var cult = await _unitOfWork.CustoHaRegiaoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<CustoHaRegiaoDto>>(cult);
        }

        public bool Insert(CustoHaRegiaoDto obj)
        {
            var cult = obj.MapTo<CustoHaRegiao>();
            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.CustoHaRegiaoRepository.Insert(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(CustoHaRegiaoDto obj)
        {
            var cult = await _unitOfWork.CustoHaRegiaoRepository.GetAsync(c => c.ID.Equals(obj.ID));


            cult.ValorHaCultivavel = obj.ValorHaCultivavel;
            cult.ValorHaNaoCultivavel = obj.ValorHaNaoCultivavel;
            cult.ModuloRural = obj.ModuloRural;

            cult.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            cult.Ativo = obj.Ativo;
            _unitOfWork.CustoHaRegiaoRepository.Update(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(CustoHaRegiaoDto obj)
        {
            var cult = obj.MapTo<CustoHaRegiao>();
            var exist = await _unitOfWork.CustoHaRegiaoRepository.GetAsync(c => c.CidadeID == obj.CidadeID);

            if (exist != null)
                throw new Exception("Custo do Ha por Região já cadastrado.");

            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.CustoHaRegiaoRepository.Insert(cult);
            return _unitOfWork.Commit();
        }

    }
}
