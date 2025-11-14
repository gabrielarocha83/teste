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
    public class AppServiceProdutoServico : IAppServiceProdutoServico
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceProdutoServico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProdutoServicoDto> GetAsync(Expression<Func<ProdutoServicoDto, bool>> expression)
        {
            var serv = await _unitOfWork.ProdutoServicoRepository.GetAsync(Mapper.Map<Expression<Func<ProdutoServico, bool>>>(expression));
            return Mapper.Map<ProdutoServicoDto>(serv);
        }

        public async Task<IEnumerable<ProdutoServicoDto>> GetAllFilterAsync(Expression<Func<ProdutoServicoDto, bool>> expression)
        {
            var serv = await _unitOfWork.ProdutoServicoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ProdutoServico, bool>>>(expression));
            return Mapper.Map<IEnumerable<ProdutoServicoDto>>(serv);
        }

        public async Task<IEnumerable<ProdutoServicoDto>> GetAllAsync()
        {
            var serv = await _unitOfWork.ProdutoServicoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ProdutoServicoDto>>(serv);
        }

        public bool Insert(ProdutoServicoDto obj)
        {
            var serv = obj.MapTo<ProdutoServico>();
            serv.DataCriacao = DateTime.Now;
            serv.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ProdutoServicoRepository.Insert(serv);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ProdutoServicoDto obj)
        {
            var serv = await _unitOfWork.ProdutoServicoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            serv.Nome = obj.Nome;
            serv.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            serv.Ativo = obj.Ativo;
            _unitOfWork.ProdutoServicoRepository.Update(serv);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ProdutoServicoDto obj)
        {
            var serv = obj.MapTo<ProdutoServico>();
            var exist = await _unitOfWork.ProdutoServicoRepository.GetAsync(c => c.Nome.Equals(obj.Nome));

            if (exist != null)
                throw new Exception("Produto ou Serviço já esta cadastrado.");

            serv.DataCriacao = DateTime.Now;
            serv.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ProdutoServicoRepository.Insert(serv);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var serv = await _unitOfWork.ProdutoServicoRepository.GetAsync(c => c.ID.Equals(id));
            serv.Ativo = false;
            _unitOfWork.ProdutoServicoRepository.Update(serv);
            return _unitOfWork.Commit();
        }
    }
}
