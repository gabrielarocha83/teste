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
    public class AppServiceTipoPecuaria : IAppServiceTipoPecuaria
    { 
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTipoPecuaria(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TipoPecuariaDto> GetAsync(Expression<Func<TipoPecuariaDto, bool>> expression)
        {
            var tipopecuaria = await _unitOfWork.TipoPecuariaRepository.GetAsync(Mapper.Map<Expression<Func<TipoPecuaria, bool>>>(expression));

            return AutoMapper.Mapper.Map<TipoPecuariaDto>(tipopecuaria);
        }

        public async Task<IEnumerable<TipoPecuariaDto>> GetAllFilterAsync(Expression<Func<TipoPecuariaDto, bool>> expression)
        {
            var tipopecuaria = await _unitOfWork.TipoPecuariaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<TipoPecuaria, bool>>>(expression));

            return AutoMapper.Mapper.Map <IEnumerable<TipoPecuariaDto> > (tipopecuaria);
        }

        public async Task<IEnumerable<TipoPecuariaDto>> GetAllAsync()
        {
            var tipopecuaria = await _unitOfWork.TipoPecuariaRepository.GetAllAsync();
           // var grupos = await _unitOfWork.GrupoRepository.GetAllPaginationAsync(null,page,skip,false);
            return AutoMapper.Mapper.Map<IEnumerable<TipoPecuariaDto>>(tipopecuaria);
        }

        public  bool Insert(TipoPecuariaDto obj)
        {
            var tipopecuaria = obj.MapTo<TipoPecuaria>();
            tipopecuaria.ID = Guid.NewGuid();
            tipopecuaria.DataCriacao = DateTime.Now;
            _unitOfWork.TipoPecuariaRepository.Insert(tipopecuaria);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Update(TipoPecuariaDto obj)
        {
            var tipopecuaria = await _unitOfWork.TipoPecuariaRepository.GetAsync(c => c.ID.Equals(obj.ID));
            tipopecuaria.Tipo = obj.Tipo;
            tipopecuaria.UnidadeMedida = obj.UnidadeMedida;
            tipopecuaria.Ativo = obj.Ativo;
            tipopecuaria.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            tipopecuaria.DataAlteracao = DateTime.Now;
            _unitOfWork.TipoPecuariaRepository.Update(tipopecuaria);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
           var tipopecuaria= await _unitOfWork.TipoPecuariaRepository.GetAsync(c => c.ID.Equals(id));
            tipopecuaria.Ativo = false;
            _unitOfWork.TipoPecuariaRepository.Update(tipopecuaria);
            return  _unitOfWork.Commit();
        }
    }
}