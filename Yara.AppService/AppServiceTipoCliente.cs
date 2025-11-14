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
    public class AppServiceTipoCliente : IAppServiceTipoCliente
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTipoCliente(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TipoClienteDto> GetAsync(Expression<Func<TipoClienteDto, bool>> expression)
        {
            var tipocliente = await _unitOfWork.TipoClienteRepository.GetAsync(Mapper.Map<Expression<Func<TipoCliente, bool>>>(expression));

            return AutoMapper.Mapper.Map<TipoClienteDto>(tipocliente);
        }

        public async Task<IEnumerable<TipoClienteDto>> GetAllFilterAsync(Expression<Func<TipoClienteDto, bool>> expression)
        {
            var tipocliente = await _unitOfWork.TipoClienteRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<TipoCliente, bool>>>(expression));

            return AutoMapper.Mapper.Map <IEnumerable<TipoClienteDto> > (tipocliente);
        }

        public async Task<IEnumerable<TipoClienteDto>> GetAllAsync()
        {
            var tipoCliente = await _unitOfWork.TipoClienteRepository.GetAllAsync();
           // var grupos = await _unitOfWork.GrupoRepository.GetAllPaginationAsync(null,page,skip,false);
            return AutoMapper.Mapper.Map<IEnumerable<TipoClienteDto>>(tipoCliente);
        }

        public  bool Insert(TipoClienteDto obj)
        {
            var tipoCliente = obj.MapTo<TipoCliente>();
            tipoCliente.ID = Guid.NewGuid();
            tipoCliente.DataCriacao = DateTime.Now;
            _unitOfWork.TipoClienteRepository.Insert(tipoCliente);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Update(TipoClienteDto obj)
        {
            var tipocliente = await _unitOfWork.TipoClienteRepository.GetAsync(c => c.ID.Equals(obj.ID));
            tipocliente.Nome = obj.Nome;
            tipocliente.LayoutProposta = obj.LayoutProposta;
            tipocliente.Ativo = obj.Ativo;
            tipocliente.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            tipocliente.TipoSerasa = obj.TipoSerasa.MapTo<TipoSerasa>();
            tipocliente.Valor = obj.Valor;
            tipocliente.DataAlteracao = DateTime.Now;
            _unitOfWork.TipoClienteRepository.Update(tipocliente);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
           var tipoCliente= await _unitOfWork.TipoClienteRepository.GetAsync(c => c.ID.Equals(id));
            tipoCliente.Ativo = false;
            _unitOfWork.TipoClienteRepository.Update(tipoCliente);
            return  _unitOfWork.Commit();
        }
    }
}