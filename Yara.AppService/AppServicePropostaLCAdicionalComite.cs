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

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServicePropostaLCAdicionalComite : IAppServicePropostaLCAdicionalComite
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaLCAdicionalComite(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropostaLCAdicionalComiteDto> GetAsync(Expression<Func<PropostaLCAdicionalComiteDto, bool>> expression)
        {
            var comite = await _unitOfWork.PropostaLCAdicionalComiteRepository.GetAsync(Mapper.Map<Expression<Func<PropostaLCAdicionalComite, bool>>>(expression));
            return comite.MapTo<PropostaLCAdicionalComiteDto>();
        }

        public async Task<IEnumerable<PropostaLCAdicionalComiteDto>> GetAllFilterAsync(Expression<Func<PropostaLCAdicionalComiteDto, bool>> expression)
        {
            var comite = await _unitOfWork.PropostaLCAdicionalComiteRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<PropostaLCAdicionalComite, bool>>>(expression));
            return comite.MapTo<IEnumerable<PropostaLCAdicionalComiteDto>>();
        }

        public async Task<IEnumerable<PropostaLCAdicionalComiteDto>> GetAllAsync()
        {
            var comite = await _unitOfWork.PropostaLCAdicionalComiteRepository.GetAllAsync();
            return comite.MapTo<IEnumerable<PropostaLCAdicionalComiteDto>>();
        }

        public bool Insert(PropostaLCAdicionalComiteDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaLCAdicionalComiteDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<KeyValuePair<bool, string>> UpdateValuePair(PropostaLCAdicionalComiteDto comite, string URL)
        {
            try
            {
                var retorno = new KeyValuePair<bool, string>();

                var fluxo = await _unitOfWork.PropostaLCAdicionalComiteRepository.InsertFluxo(comite.MapTo<PropostaLCAdicionalComite>());
                if (fluxo != null)
                {
                    try
                    {
                        var email = new AppServiceEnvioEmail(_unitOfWork);
                        if (comite.PropostaLCAdicionalStatusComiteID.Equals("RF"))
                        {
                            // A ação de Rejeitar deve enviar e-mail para o criador da proposta.
                            var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(p => p.ID.Equals(comite.PropostaLCAdicionalID));
                            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(u => u.ID.Equals(proposta.UsuarioIDCriacao));

                            retorno = await email.SendMailFeedBackPropostas(usuario.MapTo<UsuarioDto>(), proposta.ID, "Proposta de limite de crédito adicional rejeitada no fluxo de aprovação.", proposta.ContaClienteID, URL);
                        }
                        else
                        {
                            // A ação de Aprovar ou Rejeitar e Seguir Fluxo deve enviar e-mail para o próximo aprovador.
                            var sendemail = fluxo.MapTo<PropostaLCAdicionalComiteDto>();
                            sendemail.EmpresaID = comite.EmpresaID;

                            retorno = await email.SendMailComiteLCAdicional(sendemail, URL);
                        }
                    }
                    catch
                    {

                    }
                }
                else
                    retorno = new KeyValuePair<bool, string>(false, null);

                return new KeyValuePair<bool, string>(retorno.Key, retorno.Value);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> InsertAsync(PropostaLCAdicionalComiteDto comite, string URL)
        {
            var retorno = await UpdateComite(comite, URL);
            _unitOfWork.Commit();

            return retorno;
        }

        public async Task<bool> InsertNivel(PropostaLCAdicionalComiteDto comite)
        {
            var comites = await _unitOfWork.PropostaLCAdicionalComiteRepository.GetAllFilterAsync(c => c.Ativo && c.PropostaLCAdicionalID.Equals(comite.PropostaLCAdicionalID));
            var ultimo = comites?.OrderByDescending(c => c.Nivel).FirstOrDefault();

            var fluxo = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAsync(c => c.ID.Equals(ultimo.FluxoLiberacaoLCAdicionalID));
            var proximo = await _unitOfWork.FluxoLiberacaoLCAdicionalRepository.GetAsync(c => c.SegmentoID.Equals(fluxo.SegmentoID) && c.Nivel.Equals(ultimo.Nivel + 1) && c.Ativo && c.EmpresaID.Equals(fluxo.EmpresaID));

            var usuario = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.PerfilId.Equals(fluxo.PrimeiroPerfilID) && c.CodigoSap.Equals(comite.CodigoSAP));

            var comitee = new PropostaLCAdicionalComite
            {
                ID = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Nivel = fluxo.Nivel,
                Adicionado = true,
                PropostaLCAdicionalID = comite.PropostaLCAdicionalID,
                FluxoLiberacaoLCAdicionalID = fluxo.ID,
                Ativo = true,
                DataAcao = null,
                PropostaLCAdicionalStatusComiteID = "PE",
                PerfilID = fluxo.PrimeiroPerfilID,
                Round = 1,
                UsuarioID = usuario.UsuarioId.Value,
                ValorDe = fluxo.ValorDe,
                ValorAte = fluxo.ValorAte,
                ValorEstipulado = null
            };

            _unitOfWork.PropostaLCAdicionalComiteRepository.Insert(comitee);

            if (fluxo.SegundoPerfilID.HasValue)
            {
                usuario = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.PerfilId.Equals(fluxo.SegundoPerfilID) && c.CodigoSap.Equals(comite.CodigoSAP));
                comite.UsuarioID = usuario.UsuarioId.Value;
                comite.Round = 2;

                _unitOfWork.PropostaLCAdicionalComiteRepository.Insert(comitee);
            }

            var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.ID.Equals(comite.PropostaLCAdicionalID));
            var descricao = $"A Proposta LA{proposta.NumeroInternoProposta:00000}/{proposta.DataCriacao:yyyy} adicionou o responsável {usuario.Usuario.Nome} Nivel {fluxo.Nivel} do comite de aprovação.";

            SaveHistorico(proposta, descricao);

            return _unitOfWork.Commit();
        }

        private void SaveHistorico(PropostaLCAdicional proposta, string descricao)
        {
            var historico = new PropostaLCAdicionalHistorico
            {
                ID = Guid.NewGuid(),
                UsuarioID = proposta.UsuarioIDAlteracao ?? proposta.UsuarioIDCriacao,
                PropostaLCAdicionalID = proposta.ID,
                PropostaLCStatusID = proposta.PropostaLCStatusID,
                DataCriacao = DateTime.Now,
                Descricao = descricao
            };

            _unitOfWork.PropostaLCAdicionalHistoricoRepository.Insert(historico);
        }

        private async Task<bool> UpdateComite(PropostaLCAdicionalComiteDto comite, string URL)
        {
            bool retorno = false;

            var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.ID.Equals(comite.PropostaLCAdicionalID) && c.EmpresaID == comite.EmpresaID);
            if (proposta != null)
            {
                var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
                if (conta.SegmentoID.HasValue)
                {
                    var dadoscomite = await _unitOfWork.PropostaLCAdicionalComiteRepository.InsertPropostaLCAdicionalComite(comite.PropostaLCAdicionalID, conta.SegmentoID.Value, comite.CodigoSAP, comite.UsuarioID, comite.EmpresaID);
                    if (dadoscomite.Any())
                    {
                        var responsavel = dadoscomite.FirstOrDefault(c => c.Nivel == 1 && c.Round == 1 && c.Ativo);

                        proposta.ResponsavelID = responsavel.UsuarioID;
                        proposta.UsuarioIDAlteracao = comite.UsuarioID;
                        proposta.CodigoSap = comite.CodigoSAP;

                        await UpdatePropostaAprovacao(proposta, URL);

                        var email = responsavel.MapTo<PropostaLCAdicionalComiteDto>();
                        email.EmpresaID = comite.EmpresaID;

                        try
                        {
                            retorno = await EnvioEmailComite(email, URL);
                        }
                        catch
                        {
                            retorno = false;
                        }

                        SaveHistorico(proposta, $"A Proposta LA{proposta.NumeroInternoProposta:00000}/{proposta.DataCriacao:yyyy} foi enviada para o comite com o status Em Aprovação com o CTC: {comite.CodigoSAP}");
                    }
                    else
                        throw new ArgumentException("Não é possível enviar ao comite, pois, não possuí configurações de valores e responsáveis para perfis deste CTC.");
                }
                else
                    throw new ArgumentException("Este cliente não possuí um segmento configurado, verifique as configurações.");
            }
            else
                throw new ArgumentException("Esta proposta não existe, verifique as configurações.");

            _unitOfWork.Commit();

            return retorno;
        }

        private async Task UpdatePropostaAprovacao(PropostaLCAdicional proposta, string URL)
        {
            proposta.PropostaLCStatusID = "FC";
            proposta.DataAlteracao = DateTime.Now;

            _unitOfWork.PropostaLCAdicionalRepository.Update(proposta);
        }

        private async Task<bool> EnvioEmailComite(PropostaLCAdicionalComiteDto responsavel, string URL)
        {
            var update = await _unitOfWork.PropostaLCAdicionalComiteRepository.GetAsync(c => c.ID.Equals(responsavel.ID));
            update.PropostaLCAdicionalStatusComiteID = "AA";
            update.DataAcao = DateTime.Now;

            _unitOfWork.PropostaLCAdicionalComiteRepository.Update(update);

            var retorno = true;

            try
            {
                var email = new AppServiceEnvioEmail(_unitOfWork);
                retorno = (await email.SendMailComiteLCAdicional(responsavel, URL)).Key;
            }
            catch
            {

            }

            return retorno;
        }
    }
}