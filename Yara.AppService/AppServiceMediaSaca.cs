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
    public class AppServiceMediaSaca : IAppServiceMediaSaca
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceMediaSaca(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MediaSacaDto> GetAsync(Expression<Func<MediaSacaDto, bool>> expression)
        {
            var media = await _unitOfWork.MediaSacaRepository.GetAsync(Mapper.Map<Expression<Func<MediaSaca, bool>>>(expression));
            return Mapper.Map<MediaSacaDto>(media);
        }

        public async Task<IEnumerable<MediaSacaDto>> GetAllFilterAsync(Expression<Func<MediaSacaDto, bool>> expression)
        {
            var media = await _unitOfWork.MediaSacaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<MediaSaca, bool>>>(expression));
            return Mapper.Map<IEnumerable<MediaSacaDto>>(media);
        }

        public async Task<IEnumerable<MediaSacaDto>> GetAllAsync()
        {
            var media = await _unitOfWork.MediaSacaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<MediaSacaDto>>(media);
        }

        public bool Insert(MediaSacaDto obj)
        {
            var media = obj.MapTo<MediaSaca>();
            media.DataCriacao = DateTime.Now;
            media.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.MediaSacaRepository.Insert(media);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(MediaSacaDto obj)
        {
            var media = await _unitOfWork.MediaSacaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            media.Nome = obj.Nome;
            media.Peso = obj.Peso;
            media.Valor = obj.Valor;
            media.Ativo = obj.Ativo;
            media.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            _unitOfWork.MediaSacaRepository.Update(media);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(MediaSacaDto obj)
        {
            var media = obj.MapTo<MediaSaca>();
            var exist = await _unitOfWork.MediaSacaRepository.GetAsync(c => c.Nome.Equals(obj.Nome));

            if (exist != null)
                throw new Exception("Valor Média da Saca já esta cadastrada.");

            media.DataCriacao = DateTime.Now;
            media.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.MediaSacaRepository.Insert(media);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var media = await _unitOfWork.MediaSacaRepository.GetAsync(c => c.ID.Equals(id));
            media.Ativo = false;
            _unitOfWork.MediaSacaRepository.Update(media);
            return _unitOfWork.Commit();
        }
    }
}
