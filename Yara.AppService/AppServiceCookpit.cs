using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceCookpit : IAppServiceCookpit
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceCookpit(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BuscaGrupoEconomicoDto>> BuscaGrupoEconomico(Guid usuarioID, string EmpresaID)
        {
            var grupos = await _unitOfWork.BuscaGrupoCookpitRepository.BuscaGrupoEconomico(usuarioID, EmpresaID);
            return Mapper.Map<IEnumerable<BuscaGrupoEconomicoDto>>(grupos);
        }

        public async Task<IEnumerable<BuscaCockpitPropostaLCDto>> BuscaPropostaLC(Guid usuarioID, string EmpresaID)
        {
            var propostas = await _unitOfWork.PropostaLCRepository.BuscaPropostaLC(usuarioID, EmpresaID);

            return propostas.MapTo<IEnumerable<BuscaCockpitPropostaLCDto>>();
        }

        public async Task<IEnumerable<BuscaPropostaAbonoDto>> BuscaPropostaAbono(Guid usuarioID, string EmpresaID, bool Acompanhar)
        {
            var propostas = await _unitOfWork.PropostaAbonoRepository.BuscaPendenciasAbono(usuarioID, EmpresaID, Acompanhar);

            return propostas.MapTo<IEnumerable<BuscaPropostaAbonoDto>>();
        }

        public async Task<IEnumerable<BuscaCockpitPropostaLCDto>> BuscaPropostaLCAcompanhamento(Guid usuarioID, string EmpresaID)
        {
            var propostas = await _unitOfWork.PropostaLCRepository.BuscaPropostaLCAcompanhamento(usuarioID, EmpresaID);

            return propostas.MapTo<IEnumerable<BuscaCockpitPropostaLCDto>>();
        }
        public async Task<IEnumerable<DivisaoRemessaCockPitDto>> BuscaOrdemVenda(Guid usuarioID, string EmpresaID)
        {
            var propostas = await _unitOfWork.DivisaoRemessaRepository.GetAllPendencyByUser(usuarioID, EmpresaID);

            return propostas.MapTo<IEnumerable<DivisaoRemessaCockPitDto>>();
        }

        public async Task<IEnumerable<BuscaPropostaProrrogacaoDto>> BuscaPropostaProrrogacao(Guid usuarioID, string EmpresaID, bool Acompanhar)
        {
            var propostas = await _unitOfWork.PropostaProrrogacao.BuscaPendenciasProrrogacao(usuarioID, EmpresaID, Acompanhar);

            return propostas.MapTo<IEnumerable<BuscaPropostaProrrogacaoDto>>();
        }

        public async Task<IEnumerable<BuscaCockpitPropostaAlcadaDto>> BuscaPropostaCockpitAlcada(Guid usuarioId, string empresaId, bool acompanhar)
        {
            var cockpit = await _unitOfWork.PropostaAlcadaComercial.BuscaPropostaCockpit(usuarioId, empresaId, acompanhar);
            return Mapper.Map<IEnumerable<BuscaCockpitPropostaAlcadaDto>>(cockpit);
        }

        public async Task<IEnumerable<DivisaoRemessaCockPitDto>> GetAllPendencyByUser(Guid UserID, string EmpresaID)
        {
            var divisao = await _unitOfWork.DivisaoRemessaRepository.GetAllPendencyByUser(UserID, EmpresaID);

            return divisao.MapTo<IEnumerable<DivisaoRemessaCockPitDto>>();
        }

        public async Task<IEnumerable<BuscaCockpitPropostaRenovacaoVigenciaLCDto>> BuscaPropostaRenovacaoVigenciaLC(Guid UserID, string EmpresaID)
        {
            var propostas = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetCockpit(UserID, EmpresaID);

            return propostas.MapTo<IEnumerable<BuscaCockpitPropostaRenovacaoVigenciaLCDto>>();
        }

        public async Task<IEnumerable<BuscaPropostaLCAdicionalDto>> BuscaPropostaLCAdicional(Guid usuarioID, string EmpresaID, bool Acompanhar)
        {
            var propostas = await _unitOfWork.PropostaLCAdicionalRepository.BuscaPendenciasLCAdicional(usuarioID, EmpresaID, Acompanhar);

            return propostas.MapTo<IEnumerable<BuscaPropostaLCAdicionalDto>>();
        }
    }
}