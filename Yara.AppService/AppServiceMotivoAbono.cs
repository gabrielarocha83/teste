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
    public class AppServiceMotivoAbono : IAppServiceMotivoAbono
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceMotivoAbono(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MotivoAbonoDto>> GetAllAsync()
        {
            var motivosabono = await _unitOfWork.MotivoAbonoRepository.GetAllAsync();
            return AutoMapper.Mapper.Map<IEnumerable<MotivoAbonoDto>>(motivosabono);
        }

        public async Task<IEnumerable<MotivoAbonoDto>> GetAllFilterAsync(Expression<Func<MotivoAbonoDto, bool>> expression)
        {
            var motivoabono = await _unitOfWork.MotivoAbonoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<MotivoAbono, bool>>>(expression));
            return AutoMapper.Mapper.Map<IEnumerable<MotivoAbonoDto>>(motivoabono);
        }

        public async Task<MotivoAbonoDto> GetAsync(Expression<Func<MotivoAbonoDto, bool>> expression)
        {
            var motivoabono = await _unitOfWork.MotivoAbonoRepository.GetAsync(Mapper.Map<Expression<Func<MotivoAbono, bool>>>(expression));
            return AutoMapper.Mapper.Map<MotivoAbonoDto>(motivoabono);
        }

        public bool Insert(MotivoAbonoDto obj)
        {
            var motivoabono = obj.MapTo<MotivoAbono>();
            motivoabono.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            motivoabono.DataCriacao = DateTime.Now;

            _unitOfWork.MotivoAbonoRepository.Insert(motivoabono);

            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(MotivoAbonoDto obj)
        {
            var motivoabono = obj.MapTo<MotivoAbono>();

            var exists = await _unitOfWork.MotivoAbonoRepository.GetAsync(c => c.Nome.Equals(obj.Nome));
            if (exists != null)
                throw new Exception("Motivo do Abono já cadastrado.");

            motivoabono.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            motivoabono.DataCriacao = DateTime.Now;

            _unitOfWork.MotivoAbonoRepository.Insert(motivoabono);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(MotivoAbonoDto obj)
        {
            var motivoabono = await _unitOfWork.MotivoAbonoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            motivoabono.Nome = obj.Nome;
            motivoabono.Ativo = obj.Ativo;
            motivoabono.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            motivoabono.DataAlteracao = DateTime.Now;

            _unitOfWork.MotivoAbonoRepository.Update(motivoabono);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var motivoabono = await _unitOfWork.MotivoAbonoRepository.GetAsync(c => c.ID.Equals(id));
            motivoabono.Ativo = false;

            _unitOfWork.MotivoAbonoRepository.Update(motivoabono);

            return _unitOfWork.Commit();
        }
    }
}