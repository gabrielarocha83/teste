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
    public class AppServiceCulturaEstado : IAppServiceCulturaEstado
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceCulturaEstado(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<CulturaEstadoDto> GetAsync(Expression<Func<CulturaEstadoDto, bool>> expression)
        {
            var cult = await _unitOfWork.CulturaEstadoReporitory.GetAsync(Mapper.Map<Expression<Func<CulturaEstado, bool>>>(expression));
            return Mapper.Map<CulturaEstadoDto>(cult);
        }

        public async Task<IEnumerable<CulturaEstadoDto>> GetAllFilterAsync(Expression<Func<CulturaEstadoDto, bool>> expression)
        {
            var cult = await _unitOfWork.CulturaEstadoReporitory.GetAllFilterAsync(Mapper.Map<Expression<Func<CulturaEstado, bool>>>(expression));
            return Mapper.Map<IEnumerable<CulturaEstadoDto>>(cult);
        }

        public async Task<IEnumerable<CulturaEstadoDto>> GetAllAsync()
        {
            var cult = await _unitOfWork.CulturaEstadoReporitory.GetAllAsync();
            return Mapper.Map<IEnumerable<CulturaEstadoDto>>(cult);
        }

        public bool Insert(CulturaEstadoDto obj)
        {
            var cult = obj.MapTo<CulturaEstado>();
            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.CulturaEstadoReporitory.Insert(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(CulturaEstadoDto obj)
        {
            var cult = await _unitOfWork.CulturaEstadoReporitory.GetAsync(c => c.ID.Equals(obj.ID));

            cult.CulturaID = obj.CulturaID;
            cult.EstadoID = obj.EstadoID;
            cult.MediaFertilizante = obj.MediaFertilizante;
            cult.PorcentagemFertilizanteCusto = obj.PorcentagemFertilizanteCusto;
            cult.Preco = obj.Preco;
            cult.ProdutividadeMedia = obj.ProdutividadeMedia;
            cult.Custo = obj.Custo;
            
            cult.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            cult.Ativo = obj.Ativo;
            _unitOfWork.CulturaEstadoReporitory.Update(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(CulturaEstadoDto obj)
        {
            var cult = obj.MapTo<CulturaEstado>();
            var exist = await _unitOfWork.CulturaEstadoReporitory.GetAsync(c => c.EstadoID.Equals(obj.EstadoID) && c.CulturaID.Equals(obj.CulturaID));

            if (exist != null)
                throw new Exception("Está cultura já esta cadastrada para este Estado.");
             
            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.CulturaEstadoReporitory.Insert(cult);
            return _unitOfWork.Commit();
        }

      
    }
}
