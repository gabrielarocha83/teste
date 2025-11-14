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
    public class AppServiceFeriado : IAppServiceFeriado
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceFeriado(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FeriadoDto> GetAsync(Expression<Func<FeriadoDto, bool>> expression)
        {
            var feriado = await _unitOfWork.FeriadoRepository.GetAsync(Mapper.Map<Expression<Func<Feriado, bool>>>(expression));
            return Mapper.Map<FeriadoDto>(feriado);
        }

        public async Task<IEnumerable<FeriadoDto>> GetAllFilterAsync(Expression<Func<FeriadoDto, bool>> expression)
        {
            var feriado = await _unitOfWork.FeriadoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Feriado, bool>>>(expression));
            return Mapper.Map<IEnumerable<FeriadoDto>>(feriado);
        }

        public async Task<IEnumerable<FeriadoDto>> GetAllAsync()
        {
            var feriado = await _unitOfWork.FeriadoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<FeriadoDto>>(feriado);
        }

        public bool Insert(FeriadoDto obj)
        {
            var feriado = obj.MapTo<Feriado>();
            feriado.DataCriacao = DateTime.Now;
            feriado.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.FeriadoRepository.Insert(feriado);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(FeriadoDto obj)
        {
            var feriado = await _unitOfWork.FeriadoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            feriado.DataFeriado = obj.DataFeriado;
            feriado.Descricao = obj.Descricao;
            feriado.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            feriado.DataAlteracao = obj.DataAlteracao;
            _unitOfWork.FeriadoRepository.Update(feriado);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(FeriadoDto obj)
        {
            var feriado = obj.MapTo<Feriado>();
            var exist = await _unitOfWork.FeriadoRepository.GetAsync(c => c.DataFeriado.Equals(obj.DataFeriado));

            if (exist != null)
            {
                exist.DataFeriado = obj.DataFeriado;
                exist.Descricao = obj.Descricao;
                exist.Ativo = true;
                exist.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
                exist.DataAlteracao = obj.DataAlteracao;
                _unitOfWork.FeriadoRepository.Update(exist);

            }
            else
            {
                feriado.ID = Guid.NewGuid();
                feriado.DataCriacao = DateTime.Now;
                feriado.UsuarioIDCriacao = obj.UsuarioIDCriacao;
                _unitOfWork.FeriadoRepository.Insert(feriado);
            }
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var feriado = await _unitOfWork.FeriadoRepository.GetAsync(c => c.ID.Equals(id));
            feriado.Ativo = false;
            _unitOfWork.FeriadoRepository.Update(feriado);
            return _unitOfWork.Commit();
        }
    }
}
