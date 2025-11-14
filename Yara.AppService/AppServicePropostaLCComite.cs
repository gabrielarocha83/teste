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
    public class AppServicePropostaLCComite : IAppServicePropostaLCComite
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaLCComite(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropostaLCComiteDto> GetAsync(Expression<Func<PropostaLCComiteDto, bool>> expression)
        {
            var comite = await _unitOfWork.PropostaLcComiteRepository.GetAsync(Mapper.Map<Expression<Func<PropostaLCComite, bool>>>(expression));
            return comite.MapTo<PropostaLCComiteDto>();
        }

        public async Task<IEnumerable<PropostaLCComiteDto>> GetAllFilterAsync(Expression<Func<PropostaLCComiteDto, bool>> expression)
        {
            var comite = await _unitOfWork.PropostaLcComiteRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<PropostaLCComite, bool>>>(expression));
            return comite.MapTo<IEnumerable<PropostaLCComiteDto>>();
        }

        public async Task<IEnumerable<PropostaLCComiteDto>> GetAllAsync()
        {
            var comite = await _unitOfWork.PropostaLcComiteRepository.GetAllAsync();
            return comite.MapTo<IEnumerable<PropostaLCComiteDto>>();
        }

        public bool Insert(PropostaLCComiteDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaLCComiteDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<KeyValuePair<bool, string>> UpdateValuePair(PropostaLCComiteDto obj, string URL)
        {
            try
            {
                var retorno = new KeyValuePair<bool, string>();

                // A ação de Aprovar, Aprovar e Seguir Fluxo, Rejeitar e Rejeitar e Seguir Fluxo.
                var fluxo = await _unitOfWork.PropostaLcComiteRepository.InsertFluxo(obj.MapTo<PropostaLCComite>());
                if (fluxo != null)
                {
                    try
                    {
                        var email = new AppServiceEnvioEmail(_unitOfWork);
                        if (obj.StatusComiteID.Equals("RF"))
                        {
                            // A ação de Rejeitar deve enviar e-mail para o criador da proposta.
                            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(p => p.ID.Equals(obj.PropostaLCID));
                            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(u => u.ID.Equals(proposta.UsuarioIDCriacao));

                            retorno = await email.SendMailFeedBackPropostas(usuario.MapTo<UsuarioDto>(), proposta.ID, "Proposta de limite de crédito rejeitada no fluxo de aprovação.", proposta.ContaClienteID, URL);
                        }
                        else
                        {
                            // A ação de Aprovar, Aprovar e Seguir Fluxo e Rejeitar e Seguir Fluxo.
                            var sendemail = fluxo.MapTo<PropostaLCComiteDto>();
                            sendemail.EmpresaID = obj.EmpresaID;

                            retorno = await email.SendMailComiteLC(sendemail, URL);
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

        public async Task<bool> InsertAsync(PropostaLCComiteDto comite, string URL)
        {
            var retorno = await UpdateComite(comite, URL);
            _unitOfWork.Commit();

            return retorno;
        }

        public async Task<bool> InsertNivel(PropostaLCComiteDto comite)
        {
            var comites = await _unitOfWork.PropostaLcComiteRepository.GetAllFilterAsync(c => c.Ativo && c.PropostaLCID.Equals(comite.PropostaLCID));
            var ultimo = comites?.OrderByDescending(c => c.Nivel).FirstOrDefault();

            var fluxo = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAsync(c => c.ID.Equals(ultimo.FluxoLiberacaoLimiteCreditoID));
            var proximo = await _unitOfWork.FluxoLiberacaoLimiteCreditoRepository.GetAsync(c => c.SegmentoID.Equals(fluxo.SegmentoID) && c.Nivel.Equals(ultimo.Nivel + 1) && c.Ativo && c.EmpresaID.Equals(fluxo.EmpresaID));

            var usuario = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.PerfilId.Equals(fluxo.PrimeiroPerfilID) && c.CodigoSap.Equals(comite.CodigoSAP));

            var comitee = new PropostaLCComite
            {
                ID = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Nivel = fluxo.Nivel,
                Adicionado = true,
                PropostaLCID = comite.PropostaLCID,
                FluxoLiberacaoLimiteCreditoID = fluxo.ID,
                Ativo = true,
                DataAcao = null,
                StatusComiteID = "PE",
                PerfilID = fluxo.PrimeiroPerfilID,
                Round = 1,
                UsuarioID = usuario.UsuarioId.Value,
                ValorDe = fluxo.ValorDe,
                ValorAte = fluxo.ValorAte,
                ValorEstipulado = null
            };

            _unitOfWork.PropostaLcComiteRepository.Insert(comitee);

            if (fluxo.SegundoPerfilID.HasValue)
            {
                usuario = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.PerfilId.Equals(fluxo.SegundoPerfilID) && c.CodigoSap.Equals(comite.CodigoSAP));
                comite.UsuarioID = usuario.UsuarioId.Value;
                comite.Round = 2;

                _unitOfWork.PropostaLcComiteRepository.Insert(comitee);
            }

            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(comite.PropostaLCID));
            var descricao = $"A Proposta LC{proposta.NumeroInternoProposta:00000}/{proposta.DataCriacao:yyyy} adicionou o responsável {usuario.Usuario.Nome} Nivel {fluxo.Nivel} do comite de aprovação.";

            SaveHistorico(proposta, descricao);

            return _unitOfWork.Commit();
        }

        private void SaveHistorico(PropostaLC proposta, string descricao)
        {
            var status = proposta.PropostaLcStatus != null ? proposta.PropostaLcStatus.Nome : proposta.PropostaLCStatusID;

            var historico = new PropostaLCHistorico
            {
                ID = Guid.NewGuid(),
                UsuarioID = proposta.UsuarioIDAlteracao ?? proposta.UsuarioIDCriacao,
                PropostaLCID = proposta.ID,
                PropostaLCStatusID = proposta.PropostaLCStatusID,
                DataCriacao = DateTime.Now,
                Descricao = descricao
            };

            _unitOfWork.PropostaLCHistorico.Insert(historico);
        }

        private async Task<bool> UpdateComite(PropostaLCComiteDto comite, string URL)
        {
            bool retorno;

            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(comite.PropostaLCID) && c.EmpresaID == comite.EmpresaID);
            if (proposta != null)
            {
                var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(proposta.ContaClienteID));
                if (conta.SegmentoID.HasValue)
                {
                    var dadoscomite = await _unitOfWork.PropostaLcComiteRepository.InsertPropostaLCComite(comite.PropostaLCID, conta.SegmentoID.Value, comite.CodigoSAP, comite.UsuarioID, comite.EmpresaID);
                    if (dadoscomite.Any())
                    {
                        var responsavel = dadoscomite.FirstOrDefault(c => c.Nivel == 1 && c.Round == 1 && c.Ativo);

                        proposta.ResponsavelID = responsavel.UsuarioID;
                        proposta.UsuarioIDAlteracao = comite.UsuarioID;
                        proposta.CodigoSap = comite.CodigoSAP;

                        await UpdatePropostaAprovacao(proposta, URL);

                        var email = responsavel.MapTo<PropostaLCComiteDto>();
                        email.EmpresaID = comite.EmpresaID;

                        try
                        {
                            retorno = await EnvioEmailComite(email, URL);
                        }
                        catch
                        {
                            retorno = false;
                        }

                        SaveHistorico(proposta, $"A Proposta LC{proposta.NumeroInternoProposta:00000}/{proposta.DataCriacao:yyyy} foi enviada para o comite com o status Em Aprovação com o CTC: {comite.CodigoSAP}");
                    }
                    else
                        throw new ArgumentException("Não é possível enviar ao comite, pois, não possuí configurações de valores e responsáveis para perfis deste CTC.");
                }
                else
                    throw new ArgumentException("Este cliente não possuí um segmento configurado, verifique as configurações.");
            }
            else
                throw new ArgumentException("Esta proposta não existe, verifique as configurações.");

            return retorno;
        }

        private async Task UpdatePropostaAprovacao(PropostaLC proposta, string URL)
        {
            proposta.PropostaLCStatusID = "FC";
            proposta.DataAlteracao = DateTime.Now;

            _unitOfWork.PropostaLCRepository.Update(proposta);
        }

        private async Task<bool> EnvioEmailComite(PropostaLCComiteDto responsavel, string URL)
        {
            var update = await _unitOfWork.PropostaLcComiteRepository.GetAsync(c => c.ID.Equals(responsavel.ID));
            update.StatusComiteID = "AA";
            update.DataAcao = DateTime.Now;

            _unitOfWork.PropostaLcComiteRepository.Update(update);

            var retorno = true;

            try
            {
                var email = new AppServiceEnvioEmail(_unitOfWork);
                retorno = (await email.SendMailComiteLC(responsavel, URL)).Key;
            }
            catch
            {

            }

            return retorno;
        }
    }
}