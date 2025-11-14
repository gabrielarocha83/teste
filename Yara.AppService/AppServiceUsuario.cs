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
using System.Web;
using System.Linq;

namespace Yara.AppService
{
    public class AppServiceUsuario : IAppServiceUsuario
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceUsuario(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioDto> GetAsync(Expression<Func<UsuarioDto, bool>> expression)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(Mapper.Map<Expression<Func<Usuario, bool>>>(expression));

            return AutoMapper.Mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllFilterAsync(Expression<Func<UsuarioDto, bool>> expression)
        {
            var usuarios = await _unitOfWork.UsuarioRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Usuario, bool>>>(expression));

            return AutoMapper.Mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuario = await _unitOfWork.UsuarioRepository.ListaAllUserNotTracking();
            return Mapper.Map<IEnumerable<UsuarioDto>>(usuario);
        }

        public async Task<UsuarioDto> SetToken(Guid id)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(id));
            usuario.TokenID = Guid.NewGuid();
            _unitOfWork.UsuarioRepository.Update(usuario);
            _unitOfWork.Commit();
            return Mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<IEnumerable<UsuarioDto>> GetSimpleUserList()
        {

            var list = await _unitOfWork.UsuarioRepository.GetSimpleUserList();
            return Mapper.Map<IEnumerable<UsuarioDto>>(list);

        }

        public async Task<IEnumerable<UsuarioDto>> GetListUsers(BuscaUsuariosDto filtros)
        {
            var list = await _unitOfWork.UsuarioRepository.GetListUsers(filtros.Ativo,filtros.EmpresaID,filtros.TipoAcesso,filtros.GrupoID, filtros.Usuario,filtros.Login);
            return Mapper.Map<IEnumerable<UsuarioDto>>(list);
        }

        public bool Insert(UsuarioDto usuarioDto)
        {
            var usuario = usuarioDto.MapTo<Usuario>();
            usuario.DataCriacao = DateTime.Now;
            usuario.UsuarioIDCriacao = usuarioDto.UsuarioIDCriacao;
            _unitOfWork.UsuarioRepository.Insert(usuario);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(UsuarioDto usuarioDto)
        {
            var usuario = usuarioDto.MapTo<Usuario>();
            var exist = await _unitOfWork.UsuarioRepository.GetAsync(c => c.Login.Equals(usuarioDto.Login));

            if (exist != null)
                throw new Exception("Já existe um usuário cadastrado com este login.");

            usuario.Grupos.Clear();
            foreach (var item in usuarioDto.Grupos)
            {
                var group = await _unitOfWork.GrupoRepository.GetAsync(c => c.ID.Equals(item.ID));
                usuario.Grupos.Add(group);
            }

            usuario.EstruturasComerciais.Clear();
            foreach (var estruturaComercial in usuarioDto.EstruturasComerciais)
            {
                var estrutura = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(estruturaComercial.CodigoSap));
                usuario.EstruturasComerciais.Add(estrutura);
            }

            usuario.ID = Guid.NewGuid();
            usuario.EmpresasID = usuarioDto.EmpresasID;
            usuario.DataCriacao = DateTime.Now;
            usuario.UsuarioIDCriacao = usuarioDto.UsuarioIDCriacao;
            _unitOfWork.UsuarioRepository.Insert(usuario);
            return _unitOfWork.Commit();
        }

        public async Task<IEnumerable<UsuarioDto>> ListaUsuariosPorGrupo(string grupoId)
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ListaUsuariosPorGrupo(grupoId);
            var usuarioDtos = new List<UsuarioDto>();
            foreach (var item in usuarios)
            {
                var dto = new UsuarioDto();
                dto.ID = item.ID;
                dto.Nome = item.Nome;
                dto.Login = item.Login;
                dto.Email = item.Email;
                dto.Ativo = item.Ativo;
                dto.UsuarioIDCriacao = item.UsuarioIDCriacao;
                dto.UsuarioIDAlteracao = item.UsuarioIDAlteracao;
                dto.DataCriacao = item.DataCriacao;
                dto.DataAlteracao = item.DataAlteracao;
                usuarioDtos.Add(dto);
            }
            return usuarioDtos;
        }

        public async Task<IEnumerable<UsuarioDto>> ListaUsuariosPorEstruturaPapel(string papel, string ordemVendaNumero)
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ListaUsuariosPorEstruturaPapel(papel, ordemVendaNumero);
            var usuarioDtos = new List<UsuarioDto>();
            foreach (var item in usuarios)
            {
                var dto = new UsuarioDto();
                dto.ID = item.ID;
                dto.Nome = item.Nome;
                dto.Login = item.Login;
                dto.Email = item.Email;
                dto.Ativo = item.Ativo;
                dto.UsuarioIDCriacao = item.UsuarioIDCriacao;
                dto.UsuarioIDAlteracao = item.UsuarioIDAlteracao;
                dto.DataCriacao = item.DataCriacao;
                dto.DataAlteracao = item.DataAlteracao;
                usuarioDtos.Add(dto);
            }
            return usuarioDtos;
        }

        public async Task<bool> UpdateStructs(UsuarioDto usuarioDto)
        {
            var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(usuarioDto.ID));

            user.DataAlteracao = DateTime.Now;

            user.Grupos.Clear();
            foreach (var item in usuarioDto.Grupos)
            {
                var group = await _unitOfWork.GrupoRepository.GetAsync(c => c.ID.Equals(item.ID));
                user.Grupos.Add(group);
            }

            user.EstruturasComerciais.Clear();
            foreach (var estruturaComercial in usuarioDto.EstruturasComerciais)
            {
                var estrutura = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(estruturaComercial.CodigoSap));
                user.EstruturasComerciais.Add(estrutura);
            }

            user.Representantes.Clear();
            foreach (var representante in usuarioDto.Representantes)
            {
                var representanteUsuario = await _unitOfWork.RepresentanteRepository.GetAsync(c => c.CodigoSap.Equals(representante.CodigoSap));
                user.Representantes.Add(representanteUsuario);
            }

            _unitOfWork.UsuarioRepository.Update(user);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(UsuarioDto usuarioDto)
        {
            var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(usuarioDto.ID));

            var exist = await _unitOfWork.UsuarioRepository.GetAsync(c => c.Login.Equals(usuarioDto.Login));
            if (exist != null && exist.ID != user.ID)
                throw new Exception("Já existe um usuário cadastrado com este login.");

            user.DataAlteracao = DateTime.Now;
            user.Email = usuarioDto.Email;
            user.Nome = usuarioDto.Nome;
            user.TipoAcesso = usuarioDto.TipoAcesso.MapTo<TipoAcesso>();
            user.Login = usuarioDto.Login;
            user.UsuarioIDAlteracao = usuarioDto.UsuarioIDAlteracao;
            user.Ativo = usuarioDto.Ativo;
            user.EmpresasID = usuarioDto.EmpresasID;

            user.Grupos.Clear();
            foreach (var item in usuarioDto.Grupos)
            {
                var group = await _unitOfWork.GrupoRepository.GetAsync(c => c.ID.Equals(item.ID));
                user.Grupos.Add(group);
            }

            user.EstruturasComerciais.Clear();
            foreach (var estruturaComercial in usuarioDto.EstruturasComerciais)
            {
                var estrutura = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(estruturaComercial.CodigoSap));
                user.EstruturasComerciais.Add(estrutura);
            }

            user.Representantes.Clear();
            foreach (var representante in usuarioDto.Representantes)
            {
                var representanteUsuario = await _unitOfWork.RepresentanteRepository.GetAsync(c => c.CodigoSap.Equals(representante.CodigoSap));
                user.Representantes.Add(representanteUsuario);
            }

            _unitOfWork.UsuarioRepository.Update(user);

            return _unitOfWork.Commit();
        }

        public async Task<bool> UpdatePreferences(UsuarioDto usuarioDto)
        {
            var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(usuarioDto.ID));

            user.DataAlteracao = DateTime.Now;
            user.EmpresaLogada = usuarioDto.EmpresaLogada;

            _unitOfWork.UsuarioRepository.Update(user);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(id));
            usuario.Ativo = false;
            _unitOfWork.UsuarioRepository.Update(usuario);
            return _unitOfWork.Commit();
        }
    }
}
