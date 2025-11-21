using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceGrupoEconomicoMembros : IAppServiceGrupoEconomicoMembros
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceGrupoEconomicoMembros(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GrupoEconomicoMembrosDto> GetAsync(Expression<Func<GrupoEconomicoMembrosDto, bool>> expression)
        {
            var grupo = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(Mapper.Map<Expression<Func<GrupoEconomicoMembros, bool>>>(expression));
            return Mapper.Map<GrupoEconomicoMembrosDto>(grupo);
        }

        public async Task<IEnumerable<GrupoEconomicoMembrosDto>> GetAllFilterAsync(Expression<Func<GrupoEconomicoMembrosDto, bool>> expression)
        {
            var grupo = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllFilterAsync(Mapper.Map<Expression<Func<GrupoEconomicoMembros, bool>>>(expression));
            return Mapper.Map<IEnumerable<GrupoEconomicoMembrosDto>>(grupo);
        }

        public async Task<IEnumerable<GrupoEconomicoMembrosDto>> GetAllAsync()
        {
            var grupos = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllAsync();
            return AutoMapper.Mapper.Map<IEnumerable<GrupoEconomicoMembrosDto>>(grupos);
        }

        public bool Insert(GrupoEconomicoMembrosDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(GrupoEconomicoMembrosDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(GrupoEconomicoMembrosDto obj)
        {
            var membros = obj.MapTo<GrupoEconomicoMembros>();
            var exist = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID.Equals(obj.ContaClienteID) && c.GrupoEconomicoID.Equals(obj.GrupoEconomicoID) && c.Ativo);
            if (exist != null)
                throw new Exception("Este cliente já pertence a este grupo.");

            membros.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            membros.DataCriacao = DateTime.Now;
            membros.StatusGrupoEconomicoFluxoID = "PI";
            membros.Ativo = false;

            var retSolicitante = InsereSolicitante(membros.UsuarioIDCriacao);
            if (retSolicitante == null)
                throw new Exception("Problemas ao inserir solicitante para aprovação.");

            var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(obj.GrupoEconomicoID));
            if (grupo == null)
                throw new Exception("Problemas ao buscar grupo econômico para associar o cliente.");

            var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAsync(c => c.ClassificacaoGrupoEconomicoId.Equals(grupo.ClassificacaoGrupoEconomicoID) && c.Ativo && c.Nivel == 1 && c.EmpresaID.Equals(obj.EmpresaID));
            if (fluxo == null)
                throw new Exception("Não existe fluxo cadastrado para envio de aprovação.");

            //var retFluxo = InserirLiberacaoFluxo(retSolicitante.ID, fluxo.ID, grupo.ID, obj.UsuarioIDCriacao);
            return _unitOfWork.Commit();
        }

        private bool InserirLiberacaoFluxo(Guid solicitanteId, Guid fluxoId, Guid grupoId, Guid userId, string codigoSap, string status)
        {
            try
            {
                var liberacao = new LiberacaoGrupoEconomicoFluxoDto()
                {
                    ID = Guid.NewGuid(),
                    UsuarioIDCriacao = userId,
                    DataCriacao = DateTime.Now,
                    FluxoGrupoEconomicoID = fluxoId,
                    SolicitanteGrupoEconomicoID = solicitanteId,
                    GrupoEconomicoID = grupoId,
                    CodigoSap = codigoSap,
                    StatusGrupoEconomicoFluxoID = status
                };

                _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.Insert(liberacao.MapTo<LiberacaoGrupoEconomicoFluxo>());

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<BuscaGrupoEconomicoDetalheDto> BuscaContaCliente(Guid grupoId, string EmpresaID)
        { // Busca a listagem da proc e quebra ela em membros.
            var listaGrupoMembros = await _unitOfWork.GrupoEconomicoMembroReporitory.BuscaContaCliente(grupoId, EmpresaID);
            var buscaGrupoEconomicoDetalhe = new BuscaGrupoEconomicoDetalheDto();

            if (listaGrupoMembros != null)
            {
                foreach (var item in listaGrupoMembros)
                {
                    buscaGrupoEconomicoDetalhe.GrupoId = item.GrupoId;
                    buscaGrupoEconomicoDetalhe.GrupoNome = item.GrupoNome;
                    buscaGrupoEconomicoDetalhe.LcGrupo = buscaGrupoEconomicoDetalhe.LcGrupo + item.LcIndividual;
                    buscaGrupoEconomicoDetalhe.ExpGrupo = buscaGrupoEconomicoDetalhe.ExpGrupo + item.ExpIndividual;
                    buscaGrupoEconomicoDetalhe.StatusGrupo = item.StatusGrupo;
                    buscaGrupoEconomicoDetalhe.ClassificacaoNome = item.ClassificacaoNome;
                    buscaGrupoEconomicoDetalhe.ExplodeGrupo = item.ExplodeGrupo;

                    var contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(item.ClienteId) && c.EmpresasID.Equals(EmpresaID)) ?? new ContaClienteFinanceiro { DividaAtiva = false, Empresas = new Empresas() { ID = "Y", Nome = "Yara" } };

                    var buscaGrupoEconomicoMembrosDetalhe = new BuscaGrupoEconomicoMembrosDetalheDto
                    {
                        ClienteId = item.ClienteId,
                        ClienteNome = item.ClienteNome,
                        ClienteCodigo = item.ClienteCodigo,
                        ClienteDocumento = item.ClienteDocumento,
                        ClienteTipoClienteID = item.ClienteTipoClienteID,
                        ClienteTipoSerasaDto = item.ClienteTipoSerasa.MapTo<TipoSerasaDto>(),
                        LcIndividual = item.LcIndividual,
                        ExpIndividual = item.ExpIndividual,
                        StatusMembro = item.StatusMembro,
                        MembroPrincipal = item.MembroPrincipal,
                        SolicitanteSerasaID = item.SolicitanteSerasaID,
                        RestricaoSerasa = item.RestricaoSerasa || item.BloqueioManual || contaClienteFinanceiro.DividaAtiva || (contaClienteFinanceiro.ConceitoCobranca != null) /* || contaClienteFinanceiro.GrupoEconomicoRestricao */, // NOTA: A flag se chama restrição serasa mas contempla outros tipos de restrição também.
                        PendenciaSerasa = item.PendenciaSerasa,
                        PossuiGarantia = item.PossuiGarantia,
                        ExplodeGrupo = item.ExplodeGrupo
                    };

                    buscaGrupoEconomicoDetalhe.MembrosDetalhes.Add(buscaGrupoEconomicoMembrosDetalhe);
                }
            }

            return buscaGrupoEconomicoDetalhe;
        }

        public async Task<KeyValuePair<Guid?, bool>> InsertAsyncList(List<GrupoEconomicoMembrosDto> obj, Guid usuarioId, string EmpresaID, string URL)
        {
            var idGrupo = obj.FirstOrDefault()?.GrupoEconomicoID;

            KeyValuePair<Guid?, bool> retorno = new KeyValuePair<Guid?, bool>(idGrupo, false);

            IList<string> errors = new List<string>();

            if (idGrupo != null && idGrupo.HasValue)
            {
                var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(idGrupo.Value));
                if (grupo != null)
                {

                    foreach (var itemMembros in obj)
                    {
                        var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(itemMembros.ContaClienteID));

                        var exist = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID.Equals(itemMembros.ContaClienteID) && c.GrupoEconomicoID.Equals(itemMembros.GrupoEconomicoID));
                        if (exist != null && exist.Ativo) // Já existe um registro do membro no grupo e o registro está ativo.
                        {
                            errors.Add($"O cliente {cliente.Nome} já pertence a este grupo."); // throw new ArgumentException("O cliente " + cliente.Nome + " já pertence a este grupo.");
                            continue;
                        }

                        var temCompartilhado = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllFilterAsync(c => c.ContaClienteID.Equals(itemMembros.ContaClienteID) && c.Ativo && c.GrupoEconomico.ClassificacaoGrupoEconomicoID.Equals(1) && c.GrupoEconomico.EmpresasID.Equals(EmpresaID));
                        if (temCompartilhado.Any() && grupo.ClassificacaoGrupoEconomicoID.Equals(1)) //Verifica se o membro já faz parte de um grupo compartilhado
                        {
                            errors.Add($"O cliente {cliente.Nome} já pertence a um grupo compartilhado."); // throw new ArgumentException("O cliente " + cliente.Nome + " já pertence a um grupo compartilhado.");
                            continue;
                        }

                        if (exist != null) // Já existe algum registro, ele É deste grupo E não está ativo.
                        {
                            exist.DataAlteracao = DateTime.Now;
                            exist.UsuarioIDAlteracao = usuarioId;
                            exist.StatusGrupoEconomicoFluxoID = "PI";
                            exist.Ativo = true;
                            exist.ExplodeGrupo = itemMembros.ExplodeGrupo;
                            _unitOfWork.GrupoEconomicoMembroReporitory.Update(exist);
                        }
                        else // Não existe nenhum registro, ele É inserido E está ativo.
                        {
                            exist = itemMembros.MapTo<GrupoEconomicoMembros>();
                            exist.DataCriacao = DateTime.Now;
                            exist.UsuarioIDCriacao = usuarioId;
                            exist.StatusGrupoEconomicoFluxoID = "PI";
                            exist.Ativo = true;
                            exist.ExplodeGrupo = itemMembros.ExplodeGrupo;
                            _unitOfWork.GrupoEconomicoMembroReporitory.Insert(exist);
                        }

                        var retSolicitante = InsereSolicitante(exist.UsuarioIDCriacao);
                        if (retSolicitante == null)
                        {
                            errors.Add($"Problemas ao inserir o solicitante {cliente.Nome} para aprovação de inclusão."); // throw new ArgumentException("Problemas ao inserir o solicitante " + cliente.Nome + " para aprovação de inclusão.");
                            continue;
                        }

                        var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAsync(c => c.ClassificacaoGrupoEconomicoId.Equals(grupo.ClassificacaoGrupoEconomicoID) && c.Ativo && c.Nivel == 1 && c.EmpresaID.Equals(EmpresaID));
                        if (fluxo == null)
                        {
                            errors.Add($"Não existe fluxo cadastrado para envio de aprovação de inclusão do cliente {cliente.Nome}."); // throw new ArgumentException("Não existe fluxo cadastrado para envio de aprovação de inclusão do cliente " + cliente.Nome + ".");
                            continue;
                        }

                        var estrutura = await _unitOfWork.ContaClienteEstruturaComercialRepository.GetAsync(c => c.ContaClienteId.Equals(itemMembros.ContaClienteIDAcesso) && c.EmpresasId.Equals(EmpresaID) && c.EstruturaComercial.EstruturaComercialPapelID.Equals("C"));
                        if (estrutura == null)
                        {
                            var clientePrincipal = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(itemMembros.ContaClienteIDAcesso));
                            errors.Add($"A conta cliente {clientePrincipal.Nome} não possuí uma estrutura comercial de CTC para aprovação de inclusão de Grupos Econômicos."); // throw new ArgumentException("A conta cliente " + cliente.Nome + " não possuí uma estrutura comercial de CTC para aprovação de inclusão de Grupos Econômicos.");
                            break;
                        }

                        InserirLiberacaoFluxo(retSolicitante.ID, fluxo.ID, grupo.ID, itemMembros.UsuarioIDCriacao, estrutura.EstruturaComercialId, "PI");

                        if (estrutura != null)
                        {
                            var responsavel = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(estrutura.EstruturaComercialId) && c.PerfilId.Equals(fluxo.PerfilId));
                            if (responsavel?.Usuario == null)
                            {
                                errors.Add($"O CTC da estrutura {estrutura.EstruturaComercial.Nome} não possui configuração de perfil para aprovação da inclusão do cliente {cliente.Nome}."); // throw new ArgumentException($"O CTC da estrutura {estrutura.EstruturaComercial.Nome} não possui configuração de perfil para aprovação da inclusão do cliente " + cliente.Nome + ".");
                                continue;
                            }

                            if (responsavel != null)
                            {
                                var grupoEconomico = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(itemMembros.GrupoEconomicoID));
                                grupoEconomico.DataAlteracao = DateTime.Now;
                                grupoEconomico.UsuarioIDAlteracao = usuarioId;
                                grupoEconomico.StatusGrupoEconomicoFluxoID = "PI";
                                _unitOfWork.GrupoEconomicoReporitory.Update(grupoEconomico);

                                try
                                {
                                    var email = new AppServiceEnvioEmail(_unitOfWork);
                                    await email.SendMailGrupoEconomico(grupoEconomico.ID, responsavel.Usuario.MapTo<UsuarioDto>(), grupoEconomico.Nome, URL);
                                }
                                catch
                                {

                                }

                                var commit = _unitOfWork.Commit();

                                retorno = new KeyValuePair<Guid?, bool>(idGrupo, commit);
                            }
                        }
                    }
                }
                else
                {
                    errors.Add($"Problemas ao buscar grupo econômico."); // throw new ArgumentException("Problemas ao buscar grupo econômico.");
                }
            }
            else
            {
                errors.Add($"Problemas ao buscar grupo econômico."); // throw new ArgumentException("Problemas ao buscar grupo econômico.");
            }

            if (errors.Any())
                throw new ArgumentException(String.Join(Environment.NewLine, errors));

            return retorno;
        }

        public async Task<KeyValuePair<Guid?, bool>> InactiveAsyncList(List<GrupoEconomicoMembrosDto> obj, Guid usuarioId, string EmpresaID, string URL)
        {
            var idGrupo = obj.FirstOrDefault()?.GrupoEconomicoID;

            KeyValuePair<Guid?, bool> retorno = new KeyValuePair<Guid?, bool>(idGrupo, false);

            IList<string> errors = new List<string>();

            if (idGrupo != null)
            {
                var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(idGrupo.Value));
                if (grupo != null)
                {
                    foreach (var itemMembros in obj)
                    {
                        var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(itemMembros.ContaClienteID));

                        var exist = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID.Equals(itemMembros.ContaClienteID) && c.GrupoEconomicoID.Equals(itemMembros.GrupoEconomicoID) && c.MembroPrincipal);
                        if (exist != null)
                        {
                            errors.Add("Não é possivel fazer a exclusão do participante principal. Somente por exclusão de grupo."); // throw new ArgumentException("Não é possivel fazer a exclusão do participante principal. Somente por exclusão de grupo.");
                            break;
                        }

                        var count = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllFilterAsync(c => c.GrupoEconomicoID.Equals(itemMembros.GrupoEconomicoID));
                        if ((count.Count() - obj.Count) < 2)
                        {
                            errors.Add("O grupo deve possuir ao menos dois participantes para prosseguir com esta ação."); // throw new ArgumentException("O grupo deve obter pelo menos dois participantes. Não foi possivel prosseguir com esta operação.");
                            break;
                        }

                        var membros = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID.Equals(itemMembros.ContaClienteID) && c.GrupoEconomicoID.Equals(itemMembros.GrupoEconomicoID));
                        membros.UsuarioIDAlteracao = usuarioId;
                        membros.DataAlteracao = DateTime.Now;
                        membros.StatusGrupoEconomicoFluxoID = "PE";
                        membros.Ativo = true;
                        _unitOfWork.GrupoEconomicoMembroReporitory.Update(membros);

                        var retSolicitante = InsereSolicitante(membros.UsuarioIDCriacao);
                        if (retSolicitante == null)
                        {
                            errors.Add($"Problemas ao inserir o solicitante {cliente.Nome} para aprovação de exclusão."); // throw new ArgumentException($"Problemas ao inserir solicitante { cliente.Nome } para aprovação de exclusão.");
                            continue;
                        }

                        var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAsync(c => c.ClassificacaoGrupoEconomicoId.Equals(grupo.ClassificacaoGrupoEconomicoID) && c.Ativo && c.Nivel == 1 && c.EmpresaID.Equals(EmpresaID));
                        if (fluxo == null)
                        {
                            errors.Add($"Não existe fluxo cadastrado para envio de aprovação de exclusão do cliente {cliente.Nome}."); // throw new ArgumentException($"Não existe fluxo cadastrado para envio de aprovação de exclusão do cliente { cliente.Nome }.");
                            continue;
                        }

                        var estrutura = await _unitOfWork.ContaClienteEstruturaComercialRepository.GetAsync(c => c.ContaClienteId.Equals(itemMembros.ContaClienteIDAcesso) && c.EmpresasId.Equals(EmpresaID) && c.EstruturaComercial.EstruturaComercialPapelID.Equals("C"));
                        if (estrutura == null)
                        {
                            var clientePrincipal = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(itemMembros.ContaClienteIDAcesso));
                            errors.Add($"A conta cliente {clientePrincipal.Nome} não possuí uma estrutura comercial de CTC para aprovação de exclusão de Grupos Econômicos."); // throw new ArgumentException($"A conta cliente { cliente.Nome } não possuí uma estrutura comercial de CTC para aprovação de exclusão de Grupos Econômicos.");
                            break;
                        }

                        InserirLiberacaoFluxo(retSolicitante.ID, fluxo.ID, grupo.ID, itemMembros.UsuarioIDCriacao, estrutura.EstruturaComercialId, "PE");

                        if (estrutura != null)
                        {
                            var responsavel = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(estrutura.EstruturaComercialId) && c.PerfilId.Equals(fluxo.PerfilId));
                            if (responsavel?.Usuario == null)
                            {
                                errors.Add($"O CTC da estrutura {estrutura.EstruturaComercial.Nome} não possui configuração de perfil para aprovação da exclusão do cliente {cliente.Nome}."); // throw new ArgumentException($"O CTC da estrutura {estrutura.EstruturaComercial.Nome} não possui configuração de perfil para aprovação da exclusão do cliente { cliente.Nome }.");
                                continue;
                            }

                            if (responsavel != null)
                            {
                                var grupoEconomico = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(itemMembros.GrupoEconomicoID));
                                grupoEconomico.DataAlteracao = DateTime.Now;
                                grupoEconomico.UsuarioIDAlteracao = usuarioId;
                                grupoEconomico.StatusGrupoEconomicoFluxoID = "PE";
                                _unitOfWork.GrupoEconomicoReporitory.Update(grupoEconomico);

                                try
                                {
                                    var email = new AppServiceEnvioEmail(_unitOfWork);
                                    await email.SendMailGrupoEconomico(grupoEconomico.ID, responsavel.Usuario.MapTo<UsuarioDto>(), grupoEconomico.Nome, URL);
                                }
                                catch
                                {

                                }

                                var commit = _unitOfWork.Commit();

                                retorno = new KeyValuePair<Guid?, bool>(idGrupo, commit);
                            }
                        }
                    }
                }
                else
                {
                    errors.Add($"Problemas ao buscar grupo econômico.");
                }
            }
            else
            {
                errors.Add($"Problemas ao buscar grupo econômico."); // throw new ArgumentException("Problemas ao buscar grupo econômico.");
            }

            if (errors.Any())
                throw new ArgumentException(String.Join(Environment.NewLine, errors));

            return retorno;
        }

        private SolicitanteGrupoEconomicoDto InsereSolicitante(Guid? usuarioId)
        {
            if (usuarioId != null)
            {
                var solicitante = new SolicitanteGrupoEconomicoDto
                {
                    ID = Guid.NewGuid(),
                    UsuarioIDCriacao = usuarioId.Value,
                    DataCriacao = DateTime.Now
                };

                try
                {
                    _unitOfWork.SolicitanteGrupoEconomicoRepository.Insert(solicitante.MapTo<SolicitanteGrupoEconomico>());
                    return solicitante;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return null;
        }

        public Task<bool> Inactive(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateExplosaoGrupoAsync(GrupoEconomicoMembrosDto grupoEconomicoMembro)
        {
            IList<string> errors = new List<string>();
            bool result;

            var entity = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(m => m.GrupoEconomicoID == grupoEconomicoMembro.GrupoEconomicoID && m.ContaClienteID == grupoEconomicoMembro.ContaClienteID);
            if (entity != null)
            {
                entity.ExplodeGrupo = !grupoEconomicoMembro.ExplodeGrupo;
                _unitOfWork.GrupoEconomicoMembroReporitory.Update(entity);

                result = _unitOfWork.Commit();
            }
            else
            {
                errors.Add($"Problemas ao atualizar a informação Explosão de Grupo. Membro ou Grupo não encontrado.");
                result = false;
            }

            if (errors.Any())
                throw new ArgumentException(String.Join(Environment.NewLine, errors));

            return result;
        }
    }
}