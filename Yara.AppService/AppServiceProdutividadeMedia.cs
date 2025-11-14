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
    public class AppServiceProdutividadeMedia : IAppServiceProdutividadeMedia
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceProdutividadeMedia(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProdutividadeMediaDto> GetAsync(Expression<Func<ProdutividadeMediaDto, bool>> expression)
        {
            var prod = await _unitOfWork.ProdutividadeMediaRepository.GetAsync(Mapper.Map<Expression<Func<ProdutividadeMedia, bool>>>(expression));
            return Mapper.Map<ProdutividadeMediaDto>(prod);
        }

        public async Task<IEnumerable<ProdutividadeMediaDto>> GetAllFilterAsync(Expression<Func<ProdutividadeMediaDto, bool>> expression)
        {
            var prod = await _unitOfWork.ProdutividadeMediaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ProdutividadeMedia, bool>>>(expression));
            return Mapper.Map<IEnumerable<ProdutividadeMediaDto>>(prod);
        }

        public async Task<IEnumerable<ProdutividadeMediaDto>> GetAllAsync()
        {
            var prod = await _unitOfWork.ProdutividadeMediaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ProdutividadeMediaDto>>(prod);
        }

        public bool Insert(ProdutividadeMediaDto obj)
        {
            var prd = obj.MapTo<ProdutividadeMedia>();
            prd.DataCriacao = DateTime.Now;
            prd.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ProdutividadeMediaRepository.Insert(prd);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ProdutividadeMediaDto obj)
        {
            var prd = await _unitOfWork.ProdutividadeMediaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            prd.Nome = obj.Nome;
            prd.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            prd.RegiaoID = obj.RegiaoID;
            prd.Ativo = obj.Ativo;
            _unitOfWork.ProdutividadeMediaRepository.Update(prd);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ProdutividadeMediaDto obj)
        {
            var prd = obj.MapTo<ProdutividadeMedia>();
            var exist = await _unitOfWork.ProdutividadeMediaRepository.GetAsync(c => c.Nome.Equals(obj.Nome));

            if (exist != null)
                throw new Exception("Produtividade Media já esta cadastrado.");

            prd.DataCriacao = DateTime.Now;
            prd.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ProdutividadeMediaRepository.Insert(prd);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var prd = await _unitOfWork.ProdutividadeMediaRepository.GetAsync(c => c.ID.Equals(id));
            prd.Ativo = false;
            _unitOfWork.ProdutividadeMediaRepository.Update(prd);
            return _unitOfWork.Commit();
        }
    }
}
