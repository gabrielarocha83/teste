using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AppServiceEstruturaPerfilUsuario : IAppServiceEstruturaPerfilUsuario
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceEstruturaPerfilUsuario(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<EstruturaPerfilUsuarioDto> GetAsync(Expression<Func<EstruturaPerfilUsuarioDto, bool>> expression)
        {
            var estruturPerfil = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(expression.MapTo<Expression<Func<EstruturaPerfilUsuario, bool>>>());
            return estruturPerfil.MapTo<EstruturaPerfilUsuarioDto>();
        }

        public async Task<IEnumerable<EstruturaPerfilUsuarioDto>> GetAllFilterAsync(Expression<Func<EstruturaPerfilUsuarioDto, bool>> expression)
        {
            var estruturaPerfil = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<EstruturaPerfilUsuario, bool>>>(expression));
            return Mapper.Map<IEnumerable<EstruturaPerfilUsuarioDto>>(estruturaPerfil);
        }

        public async Task<IEnumerable<EstruturaPerfilUsuarioDto>> GetAllAsync()
        {
            //Adiciona Dados de CTC e Perfil na tabela de EstruturaPerfilUsuario Automatico com UsuarioId Null.
            var perfil = await _untUnitOfWork.PerfilRepository.GetAllAsync();
            var estruturaCtc = await _untUnitOfWork.EstruturaComercialRepository.GetAllFilterAsync(c => c.EstruturaComercialPapelID.Equals("C"));
            foreach (var itemPerfil in perfil)
            {
                foreach (var itemCtc in estruturaCtc)
                {
                    //Verifica se existe estrutura perfil
                    var exist = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(itemCtc.CodigoSap) && c.PerfilId.Equals(itemPerfil.ID));
                    if (exist == null)
                    {
                        var obj = new EstruturaPerfilUsuarioDto();
                        obj.ID = Guid.NewGuid();
                        obj.CodigoSap = itemCtc.CodigoSap;
                        obj.PerfilId = itemPerfil.ID;
                        obj.UsuarioId = null;
                        obj.UsuarioIDCriacao = Guid.Empty;
                        obj.DataCriacao = DateTime.Now;
                        Insert(obj);
                    }
                }
            }
            _untUnitOfWork.Commit();
            var estruturaPerfil = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<EstruturaPerfilUsuarioDto>>(estruturaPerfil);
        }

        public bool Insert(EstruturaPerfilUsuarioDto obj)
        {
            var estruturaPerfil = obj.MapTo<EstruturaPerfilUsuario>();
            try
            {
                estruturaPerfil.ID = Guid.NewGuid();
                estruturaPerfil.DataCriacao = DateTime.Now;
                _untUnitOfWork.EstruturaPerfilUsuarioRepository.Insert(estruturaPerfil);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(EstruturaPerfilUsuarioDto obj)
        {
            try
            {
                var estruturaPerfil = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.ID.Equals(obj.ID));
                estruturaPerfil.UsuarioId = obj.UsuarioId;
                estruturaPerfil.DataAlteracao = DateTime.Now;
                _untUnitOfWork.EstruturaPerfilUsuarioRepository.Update(estruturaPerfil);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InsertAsync(EstruturaPerfilUsuarioDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrUpdateAsync(EstruturaPerfilUsuarioDto obj, Guid userId)
        {
            bool commit;
            var exist = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(obj.CodigoSap) && c.PerfilId.Equals(obj.PerfilId));
            if (exist != null)
            {
                var usuarioIdOld = exist.UsuarioId;
                obj.ID = exist.ID;
                obj.UsuarioIDAlteracao = userId;
                await Update(obj);
                commit = _untUnitOfWork.Commit();

                //Chama procedure para fazer a substituição do usuario novo nas propostas, comitês, fluxo de grupo economico e ordem de venda
                if (commit)
                    await _untUnitOfWork.EstruturaPerfilUsuarioRepository.SubstituicaoUsuario(usuarioIdOld, obj.UsuarioId, obj.CodigoSap, userId);      
            }
            else
            {
                obj.UsuarioIDCriacao = userId;
                Insert(obj);
                commit = _untUnitOfWork.Commit();
            }

            return commit;
        }

        public async Task<bool> InsertListAsync(List<EstruturaPerfilUsuarioDto> obj, Guid userId)
        {
            foreach (var perfilUsuarioDto in obj)
            {
                var estruturaPerfil = perfilUsuarioDto.MapTo<EstruturaPerfilUsuario>();
                estruturaPerfil.ID = Guid.NewGuid();
                estruturaPerfil.UsuarioIDCriacao = userId;
                estruturaPerfil.DataCriacao = DateTime.Now;
                _untUnitOfWork.EstruturaPerfilUsuarioRepository.Insert(estruturaPerfil);
            }

            return _untUnitOfWork.Commit();
        }

        public async Task<bool> UpdateListAsync(List<EstruturaPerfilUsuarioDto> obj, Guid userId)
        {
            foreach (var perfilUsuarioDto in obj)
            {
                var estruturaPerfil = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.ID.Equals(perfilUsuarioDto.ID));
                estruturaPerfil.PerfilId = perfilUsuarioDto.PerfilId;
                estruturaPerfil.UsuarioId = perfilUsuarioDto.UsuarioId;
                estruturaPerfil.UsuarioIDAlteracao = userId;
                estruturaPerfil.DataAlteracao = DateTime.Now;
                _untUnitOfWork.EstruturaPerfilUsuarioRepository.Update(estruturaPerfil);
            }

            return _untUnitOfWork.Commit();
        }

        public async Task<IEnumerable<BuscaCTCPerfilUsuarioDto>> BuscaContaCliente(string Usuario, string CTC)
        {
            List<BuscaCTCPerfilUsuarioDto> retorno = new List<BuscaCTCPerfilUsuarioDto>();
            var perfis = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.BuscaContaCliente(Usuario, CTC);
            foreach (var perfil in perfis)
            {
                if (!retorno.Any(c => c.CodCTC.Equals(perfil.CodCTC)))
                {
                    var estrutura = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAllFilterAsync(c => c.CodigoSap.Equals(perfil.CodCTC));
                    var sql = from r in estrutura where r.CodigoSap.Equals(perfil.CodCTC) orderby r.Perfil.Ordem ascending select new EstruturaPerfilUsuarioDto { CodigoSap = r.CodigoSap, PerfilId = r.PerfilId, Usuario = r.Usuario.MapTo<UsuarioDto>(), UsuarioId = r.UsuarioId };

                    var busca = new BuscaCTCPerfilUsuarioDto()
                    {
                        CodCTC = perfil.CodCTC,
                        GC = perfil.GC,
                        CTC = perfil.CTC,
                        DI = perfil.DI,
                        Perfis = sql.ToList()
                    };

                    retorno.Add(busca);
                }
            }
            return retorno;
        }

        public async Task<string> GetActiveProfileByCustomer(Guid customerId, Guid userId, string empresaId)
        {
            var customerCTCs = await _untUnitOfWork.ContaClienteEstruturaComercialRepository.GetAllFilterAsync(ec => ec.ContaClienteId == customerId && ec.EstruturaComercial.EstruturaComercialPapelID == "C" && ec.EmpresasId == empresaId);
            var codigosCTCs = customerCTCs.Select(c => c.EstruturaComercial.CodigoSap).ToList();

            /*var ultProposta = await _untUnitOfWork.PropostaLCRepository.BuscaUltimaPropostaContaCliente(customerId, empresaId);
            if (!string.IsNullOrEmpty(ultProposta?.CodigoSap))
                codigosCTCs = new List<string>() { ultProposta.CodigoSap };*/

            var perfisUsuario = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAllFilterAsync(epu => epu.UsuarioId == userId && (codigosCTCs.Contains(epu.CodigoSap) || epu.Perfil.Ordem >= 5));
            return perfisUsuario.Any() ? perfisUsuario.OrderByDescending(p => p.Perfil.Ordem).First().Perfil.Descricao : "Representante";
        }

        public async Task<string> GetActiveProfileAlcadaByCustomer(Guid customerId, Guid userId, string empresaId)
        {
            var customerCTCs = await _untUnitOfWork.ContaClienteEstruturaComercialRepository.GetAllFilterAsync(ec => ec.ContaClienteId == customerId && ec.EstruturaComercial.EstruturaComercialPapelID == "C" && ec.EmpresasId == empresaId);
            var codigosCTCs = customerCTCs.Select(c => c.EstruturaComercial.CodigoSap).ToList();

            /*var ultProposta =  await _untUnitOfWork.PropostaAlcadaComercial.GetMaxProposal();
            if (!string.IsNullOrEmpty(ultProposta?.CodigoSap))
                codigosCTCs = new List<string>() { ultProposta.CodigoSap };*/

            var perfisUsuario = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAllFilterAsync(epu => epu.UsuarioId == userId && (codigosCTCs.Contains(epu.CodigoSap) || epu.Perfil.Ordem >= 5));
            return perfisUsuario.Any() ? perfisUsuario.OrderByDescending(p => p.Perfil.Ordem).First().Perfil.Descricao : "Representante";
        }


        public async Task<Guid> GetActiveProfileByUser(Guid customerId, Guid userId, string empresaId)
        {
            var customerCTCs = await _untUnitOfWork.ContaClienteEstruturaComercialRepository.GetAllFilterAsync(ec => ec.ContaClienteId == customerId && ec.EstruturaComercial.EstruturaComercialPapelID == "C" && ec.EmpresasId == empresaId);
            var codigosCTCs = customerCTCs.Select(c => c.EstruturaComercial.CodigoSap).ToList();

            /*var ultProposta = await _untUnitOfWork.PropostaLCRepository.BuscaUltimaPropostaContaCliente(customerId, empresaId);
            if (!string.IsNullOrEmpty(ultProposta?.CodigoSap))
                codigosCTCs = new List<string>() { ultProposta.CodigoSap };*/

            var perfisUsuario = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAllFilterAsync(epu => epu.UsuarioId == userId && (codigosCTCs.Contains(epu.CodigoSap) || epu.Perfil.Ordem >= 5));
            return perfisUsuario.Any() ? (perfisUsuario.OrderByDescending(p => p.Perfil.Ordem).First().UsuarioId ?? Guid.Empty) : Guid.Empty;
        }

        public async Task<Guid> GetUserPerfil(string codSAP, string Perfil)
        {
            Guid usuario = Guid.Empty;

            var perfil = await _untUnitOfWork.PerfilRepository.GetAsync(c => c.Descricao.Equals(Perfil));
            if (perfil == null)
                return usuario;

            var responsavel = await _untUnitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(codSAP) && c.PerfilId.Equals(perfil.ID));
            if (responsavel != null)
                usuario = responsavel.UsuarioId ?? Guid.Empty;

            return usuario;
        }
    }
}
