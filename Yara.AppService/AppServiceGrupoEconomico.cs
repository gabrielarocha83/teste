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
    public class AppServiceGrupoEconomico : IAppServiceGrupoEconomico
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceGrupoEconomico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetCodeClassification(int ClassificacaoID)
        {
            var charinit = ClassificacaoID == 1 ? "C" : "I";
            var group = await _unitOfWork.GrupoEconomicoReporitory.GetAllFilterAsync(c => c.ClassificacaoGrupoEconomicoID.Equals(ClassificacaoID));
            var code = (group.Count() + 1).ToString();
            var padLeft = code.PadLeft(9, '0');
            return string.Concat(charinit, padLeft);
        }

        public async Task<GrupoEconomicoDto> GetAsync(Expression<Func<GrupoEconomicoDto, bool>> expression)
        {
            var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(Mapper.Map<Expression<Func<GrupoEconomico, bool>>>(expression));
            return Mapper.Map<GrupoEconomicoDto>(grupo);
        }

        public async Task<IEnumerable<GrupoEconomicoDto>> GetAllFilterAsync(Expression<Func<GrupoEconomicoDto, bool>> expression)
        {
            var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAllFilterAsync(Mapper.Map<Expression<Func<GrupoEconomico, bool>>>(expression));
            return Mapper.Map<IEnumerable<GrupoEconomicoDto>>(grupo);
        }

        public async Task<IEnumerable<GrupoEconomicoDto>> GetAllAsync()
        {
            var grupos = await _unitOfWork.GrupoEconomicoReporitory.GetAllAsync();
            return Mapper.Map<IEnumerable<GrupoEconomicoDto>>(grupos);
        }

        public bool Insert(GrupoEconomicoDto obj)
        {
            var grupo = obj.MapTo<GrupoEconomico>();
            grupo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.GrupoEconomicoReporitory.Insert(grupo);
            return _unitOfWork.Commit();
        }

        private async Task<bool> ExistCompartilhado(Guid participante, string empresaId)
        {
            var grupos = await _unitOfWork.GrupoEconomicoReporitory.GetAllFilterAsync(c => c.ClassificacaoGrupoEconomicoID.Equals(1) && c.EmpresasID.Equals(empresaId));
            var participantes = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllFilterAsync(c => c.Ativo && c.ContaClienteID.Equals(participante));
            return participantes.Any(c => grupos.Any(d => d.ID.Equals(c.GrupoEconomicoID)));
        }

        public async Task<bool> InsertAsync(NovoGrupoEconomicoDto obj)
        {
            var exist = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.Nome.Equals(obj.Nome) && c.Ativo);
            if (exist != null)
                throw new ArgumentException($"O nome {obj.Nome} já existe, tente outro nome para o grupo.");

            var grupo = new GrupoEconomico
            {
                ID = obj.ID,
                CodigoGrupo = await GetCodeClassification(obj.ClassificacaoGrupoEconomicoID),
                UsuarioIDCriacao = obj.UsuarioIDCriacao,
                DataCriacao = DateTime.Now,
                StatusGrupoEconomicoFluxoID = "AP",
                Nome = obj.Nome,
                Descricao = obj.Nome,
                TipoRelacaoGrupoEconomicoID = obj.TipoRelacaoGrupoEconomico,
                ClassificacaoGrupoEconomicoID = obj.ClassificacaoGrupoEconomicoID,
                EmpresasID = obj.EmpresaID,
                Ativo = true
            };
            _unitOfWork.GrupoEconomicoReporitory.Insert(grupo);

            ContaClienteFinanceiro contaClienteFinanceiro;

            foreach (var membro in obj.Membros)
            {
                var grupomembros = new GrupoEconomicoMembros();
                var conta = new ContaCliente();

                if (obj.ClassificacaoGrupoEconomicoID.Equals(1))
                { // LC Compartilhado
                    var existe = await ExistCompartilhado(membro.ID, grupo.EmpresasID);

                    if (existe)
                        throw new ArgumentException($"O cliente cujo CPF/CNPJ corresponde ao número {membro.Documento} já faz parte de outro grupo econômico de LC compartilhado.");
                }

                if (membro.ID == null || membro.ID.Equals(Guid.Empty))
                {
                    conta.ID = Guid.NewGuid();
                    conta.Nome = membro.Nome;
                    conta.Documento = membro.Documento;
                    conta.UsuarioIDCriacao = obj.UsuarioIDCriacao;
                    conta.DataCriacao = DateTime.Now;
                    conta.Simplificado = true;

                    _unitOfWork.ContaClienteRepository.Insert(conta);

                    membro.ID = conta.ID;
                }

                contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(membro.ID) && c.EmpresasID.Equals(grupo.EmpresasID));


                grupomembros.LCAntesGrupo = contaClienteFinanceiro?.LC ?? 0;
                grupomembros.Ativo = true;
                grupomembros.UsuarioIDCriacao = obj.UsuarioIDCriacao;
                grupomembros.ContaClienteID = membro.ID;
                grupomembros.GrupoEconomicoID = grupo.ID;
                grupomembros.StatusGrupoEconomicoFluxoID = "AP";
                grupomembros.MembroPrincipal = (membro.ID.Equals(obj.ContaClientePrincipalID)) ? true : false;
                grupomembros.DataCriacao = DateTime.Now;
                grupomembros.ExplodeGrupo = membro.ExplodeGrupo;

                _unitOfWork.GrupoEconomicoMembroReporitory.Insert(grupomembros);
            }

            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ContaClientePrincipalID));

            // Criar Processamento Carteira (Reavaliar Carteira Cliente)
            ProcessamentoCarteira pc = new ProcessamentoCarteira();
            pc.ID = Guid.NewGuid();
            pc.Cliente = contaCliente.CodigoPrincipal;
            pc.DataHora = DateTime.Now;
            pc.Status = 2;
            pc.Motivo = $"Criação do grupo econômico: {obj.Nome}";
            pc.Detalhes = pc.Motivo;
            pc.EmpresaID = obj.EmpresaID;
            _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

            return _unitOfWork.Commit();
        }

        public async Task<bool> DeleteAsync(GrupoEconomicoFluxoDto obj, string URL)
        {
            var retorno = false;

            try
            {
                var solicitante = InsereSolicitante(obj.UsuarioID);

                var fluxo = await GetFluxo(obj.ClassificacaoGrupoEconomicoID, obj.EmpresaID);
                if (fluxo == null)
                    throw new ArgumentException("Não existe fluxo de Grupo Econômico para esta classificação.");

                var propostaLC = await _unitOfWork.PropostaLCRepository.GetAsync(p => p.GrupoEconomicoID.Value.Equals(obj.GrupoID) && (p.PropostaLCStatusID != "AA" && p.PropostaLCStatusID != "XE" && p.PropostaLCStatusID != "XR"));
                if (propostaLC != null)
                    throw new ArgumentException($"Este grupo não pode ser excluído porque está vinculado à uma Análise de Grupo na proposta {propostaLC.MapTo<PropostaLCDto>().NumeroProposta}.");

                await AtualizaGrupoEconomico(obj.GrupoID, obj.UsuarioID);
                await AtualizaMembros(obj.GrupoID, obj.UsuarioID);

                var estrutura = await _unitOfWork.ContaClienteEstruturaComercialRepository.GetAsync(c => c.ContaClienteId.Equals(obj.ContaClienteID) && c.EmpresasId.Equals(obj.EmpresaID) && c.EstruturaComercial.EstruturaComercialPapelID.Equals("C"));
                if (estrutura == null)
                    throw new ArgumentException("Esta conta cliente não possuí uma estrutura comercial de CTC para aprovação de Grupos Econômicos.");

                InserirLiberacaoFluxo(solicitante.ID, fluxo.ID, obj.GrupoID, obj.UsuarioID, estrutura.EstruturaComercialId);

                if (estrutura != null)
                {
                    var responsavel = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(estrutura.EstruturaComercialId) && c.PerfilId.Equals(fluxo.PerfilId));
                    if (responsavel?.Usuario == null)
                        throw new ArgumentException($"O CTC da estrutura {estrutura.EstruturaComercial.Nome} não possui configuração de perfil para aprovação.");

                    if (responsavel != null)
                    {
                        try
                        {
                            var user = responsavel.Usuario.MapTo<UsuarioDto>();
                            var email = new AppServiceEnvioEmail(_unitOfWork);
                            await email.SendMailGrupoEconomico(obj.GrupoID, user, obj.Nome, URL);
                        }
                        catch
                        {

                        }

                        retorno = _unitOfWork.Commit();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        private void InserirLiberacaoFluxo(Guid SolicitanteID, Guid FluxoID, Guid GrupoID, Guid UserID, string CodigoSAP)
        {
            var liberacao = new LiberacaoGrupoEconomicoFluxoDto()
            {
                ID = Guid.NewGuid(),
                UsuarioIDCriacao = UserID,
                DataCriacao = DateTime.Now,
                FluxoGrupoEconomicoID = FluxoID,
                SolicitanteGrupoEconomicoID = SolicitanteID,
                GrupoEconomicoID = GrupoID,
                CodigoSap = CodigoSAP,
                StatusGrupoEconomicoFluxoID = "PE"
            };
            _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.Insert(liberacao.MapTo<LiberacaoGrupoEconomicoFluxo>());
        }

        private SolicitanteGrupoEconomicoDto InsereSolicitante(Guid? UsuarioID)
        {
            SolicitanteGrupoEconomicoDto retorno = null;
            if (UsuarioID != null)
            {
                retorno = new SolicitanteGrupoEconomicoDto
                {
                    ID = Guid.NewGuid(),
                    UsuarioIDCriacao = UsuarioID.Value,
                    DataCriacao = DateTime.Now
                };
                _unitOfWork.SolicitanteGrupoEconomicoRepository.Insert(retorno.MapTo<SolicitanteGrupoEconomico>());
            }
            return retorno;
        }

        private async Task AtualizaGrupoEconomico(Guid GrupoID, Guid? UsuarioID)
        {
            var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(GrupoID));
            grupo.StatusGrupoEconomicoFluxoID = "PE";
            grupo.UsuarioIDAlteracao = UsuarioID;
            grupo.DataAlteracao = DateTime.Now;
            _unitOfWork.GrupoEconomicoReporitory.Update(grupo);
        }

        private async Task AtualizaMembros(Guid GrupoID, Guid? UsuarioID)
        {
            var membros = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllFilterAsync(c => c.GrupoEconomicoID.Equals(GrupoID));
            foreach (var membro in membros)
            {
                membro.StatusGrupoEconomicoFluxoID = "PE";
                membro.UsuarioIDAlteracao = UsuarioID;
                membro.DataAlteracao = DateTime.Now;
                _unitOfWork.GrupoEconomicoMembroReporitory.Update(membro);
            }
        }

        private async Task<FluxoGrupoEconomicoDto> GetFluxo(int ClassificacaoID, string EmpresaID)
        {
            var fluxo = await _unitOfWork.FluxoGrupoEconomicoRepository.GetAsync(c => c.ClassificacaoGrupoEconomicoId == ClassificacaoID && c.Ativo && c.Nivel == 1 && c.EmpresaID == EmpresaID);
            return fluxo.MapTo<FluxoGrupoEconomicoDto>();
        }

        public async Task<IEnumerable<BuscaGrupoEconomicoDto>> BuscaGrupoEconomico(Guid clienteId, string EmpresaID)
        {
            var grupos = await _unitOfWork.GrupoEconomicoReporitory.BuscaGrupoEconomico(clienteId, EmpresaID);
            return Mapper.Map<IEnumerable<BuscaGrupoEconomicoDto>>(grupos);
        }

        public async Task<IEnumerable<BuscaGrupoEconomicoDto>> BuscaGrupoEconomicoPorGrupo(Guid grupoId, string empresaId)
        {
            var grupos = await _unitOfWork.GrupoEconomicoReporitory.BuscaGrupoEconomicoPorGrupo(grupoId, empresaId);
            return Mapper.Map<IEnumerable<BuscaGrupoEconomicoDto>>(grupos);
        }

        public async Task<bool> Update(GrupoEconomicoDto obj)
        {
            var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(obj.ID));
            grupo.Nome = obj.Nome;
            grupo.Ativo = obj.Ativo;
            grupo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            grupo.DataAlteracao = DateTime.Now;
            _unitOfWork.GrupoEconomicoReporitory.Update(grupo);
            return _unitOfWork.Commit();
        }

        public async Task<IEnumerable<BuscaHistoricoGrupoDto>> BuscaHistoricoPorGrupo(Guid codGrupo, string empresaId)
        {
            var retorno = await _unitOfWork.GrupoEconomicoReporitory.BuscaHistoricoPorGrupo(codGrupo, empresaId);
            return Mapper.Map<IEnumerable<BuscaHistoricoGrupoDto>>(retorno);
        }
    }
}