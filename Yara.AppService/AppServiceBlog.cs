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
    public class AppServiceBlog : IAppServiceBlog
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceBlog(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogDto> GetAsync(Expression<Func<BlogDto, bool>> expression)
        {
            var blog = await _unitOfWork.BlogRepository.GetAsync(Mapper.Map<Expression<Func<Blog, bool>>>(expression));
            return Mapper.Map<BlogDto>(blog);
        }

        public async Task<IEnumerable<BlogDto>> GetAllFilterAsync(Expression<Func<BlogDto, bool>> expression)
        {
            var blog = await _unitOfWork.BlogRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Blog, bool>>>(expression));
            return Mapper.Map<IEnumerable<BlogDto>>(blog);
        }

        public async Task<IEnumerable<BlogDto>> GetAllAsync()
        {
            var feriado = await _unitOfWork.BlogRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<BlogDto>>(feriado);
        }

        public bool Insert(BlogDto obj)
        {
            var blog = obj.MapTo<Blog>();
            blog.DataCriacao = DateTime.Now;
            blog.UsuarioCriacaoID = obj.UsuarioCriacaoID;
            _unitOfWork.BlogRepository.Insert(blog);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(BlogDto obj)
        {
            var blog = await _unitOfWork.BlogRepository.GetAsync(c => c.ID.Equals(obj.ID));
            blog.DataCriacao = DateTime.Now;
            blog.ParaID = obj.ParaID;
            blog.Mensagem = obj.Mensagem;
            blog.UsuarioCriacaoID = obj.UsuarioCriacaoID;
            _unitOfWork.BlogRepository.Update(blog);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(BlogDto obj, string URL)
        {
            var blog = obj.MapTo<Blog>();
            blog.ID = Guid.NewGuid();
            blog.DataCriacao = DateTime.Now;
            blog.UsuarioCriacaoID = obj.UsuarioCriacaoID;
            _unitOfWork.BlogRepository.Insert(blog);

            if (!obj.ParaID.HasValue)
                return _unitOfWork.Commit();

            try
            {
                var email = new AppServiceEnvioEmail(_unitOfWork);
                await email.SendMailBlogProposta(obj, obj.ContaClienteID, URL);
            }
            catch
            {

            }

            return _unitOfWork.Commit();
        }

        public async Task<IEnumerable<BlogDto>> GetByArea(Guid Area, string EmpresaID, string URL)
        {
            var consulta = await _unitOfWork.BlogRepository.GetAllFilterAsync(c => c.Area.Equals(Area) && c.EmpresaID == EmpresaID);
            return consulta.MapTo<IEnumerable<BlogDto>>();
        }
    }
}
