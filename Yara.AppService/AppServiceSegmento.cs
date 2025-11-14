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
    public class AppServiceSegmento : IAppServiceSegmento
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppServiceSegmento(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SegmentoDto> GetAsync(Expression<Func<SegmentoDto, bool>> expression)
        {
            var segmento = await _unitOfWork.SegmentoRepository.GetAsync(Mapper.Map<Expression<Func<Segmento, bool>>>(expression));

            return AutoMapper.Mapper.Map<SegmentoDto>(segmento);
        }

        public async Task<IEnumerable<SegmentoDto>> GetAllFilterAsync(Expression<Func<SegmentoDto, bool>> expression)
        {
            var segmento = await _unitOfWork.SegmentoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Segmento, bool>>>(expression));

            return AutoMapper.Mapper.Map<IEnumerable<SegmentoDto>>(segmento);
        }

        public async Task<IEnumerable<SegmentoDto>> GetAllAsync()
        {
            var segmento = await _unitOfWork.SegmentoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<SegmentoDto>>(segmento);
        }

        public bool Insert(SegmentoDto segmentoDto)
        {
            var segmento = segmentoDto.MapTo<Segmento>();
            segmento.ID = Guid.NewGuid();
            segmento.DataCriacao = DateTime.Now;
            segmento.UsuarioIDCriacao = segmentoDto.UsuarioIDCriacao;
            _unitOfWork.SegmentoRepository.Insert(segmento);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(SegmentoDto segmentoDto)
        {
            var segmento = await _unitOfWork.SegmentoRepository.GetAsync(c => c.ID.Equals(segmentoDto.ID));
            segmento.Descricao = segmentoDto.Descricao;
            segmento.Ativo = segmentoDto.Ativo;
            segmento.DataAlteracao = DateTime.Now;
            segmento.UsuarioIDAlteracao = segmentoDto.UsuarioIDAlteracao;

            _unitOfWork.SegmentoRepository.Update(segmento);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var segmento = await _unitOfWork.SegmentoRepository.GetAsync(c => c.ID.Equals(id));
            _unitOfWork.SegmentoRepository.Update(segmento);
            return _unitOfWork.Commit();
        }
    }
}
