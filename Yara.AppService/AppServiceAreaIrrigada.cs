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
    public class AppServiceAreaIrrigada : IAppServiceAreaIrrigada
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceAreaIrrigada(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AreaIrrigadaDto> GetAsync(Expression<Func<AreaIrrigadaDto, bool>> expression)
        {
            var area = await _unitOfWork.AreaIrrigadaRepository.GetAsync(Mapper.Map<Expression<Func<AreaIrrigada, bool>>>(expression));
            return Mapper.Map<AreaIrrigadaDto>(area);
        }

        public async Task<IEnumerable<AreaIrrigadaDto>> GetAllFilterAsync(Expression<Func<AreaIrrigadaDto, bool>> expression)
        {
            var area = await _unitOfWork.AreaIrrigadaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<AreaIrrigada, bool>>>(expression));
            return Mapper.Map<IEnumerable<AreaIrrigadaDto>>(area);
        }

        public async Task<IEnumerable<AreaIrrigadaDto>> GetAllAsync()
        {
            var area = await _unitOfWork.AreaIrrigadaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<AreaIrrigadaDto>>(area);
        }

        public bool Insert(AreaIrrigadaDto obj)
        {
            var area = obj.MapTo<AreaIrrigada>();
            area.DataCriacao = DateTime.Now;
            area.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.AreaIrrigadaRepository.Insert(area);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(AreaIrrigadaDto obj)
        {
            var area = await _unitOfWork.AreaIrrigadaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            area.Nome = obj.Nome;
            area.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            area.Ativo = obj.Ativo;
            _unitOfWork.AreaIrrigadaRepository.Update(area);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(AreaIrrigadaDto obj)
        {
            var area = obj.MapTo<AreaIrrigada>();
            var exist = await _unitOfWork.AreaIrrigadaRepository.GetAsync(c => c.Nome.Equals(obj.Nome));

            if (exist != null)
                throw new Exception("Area Irrigada já esta cadastrada.");

            area.DataCriacao = DateTime.Now;
            area.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.AreaIrrigadaRepository.Insert(area);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var area = await _unitOfWork.AreaIrrigadaRepository.GetAsync(c => c.ID.Equals(id));
            area.Ativo = false;
            _unitOfWork.AreaIrrigadaRepository.Update(area);
            return _unitOfWork.Commit();
        }
    }
}
