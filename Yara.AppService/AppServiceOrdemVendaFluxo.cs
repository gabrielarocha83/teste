using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceOrdemVendaFluxo : IAppServiceOrdemVendaFluxo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppServiceEnvioEmail _email;
        private readonly IAppServiceUsuario _usuario;

        public AppServiceOrdemVendaFluxo(IUnitOfWork unitOfWork, IAppServiceEnvioEmail email, IAppServiceUsuario usuario)
        {
            _unitOfWork = unitOfWork;
            _email = email;
            _usuario = usuario;
        }

        public async Task<OrdemVendaFluxoDto> GetAsync(Expression<Func<OrdemVendaFluxoDto, bool>> expression)
        {

            var fluxo = await _unitOfWork.OrdemVendaFluxoRepository.GetAsync(Mapper.Map<Expression<Func<OrdemVendaFluxo, bool>>>(expression));

            return Mapper.Map<OrdemVendaFluxoDto>(fluxo);

        }

        public Task<IEnumerable<OrdemVendaFluxoDto>> GetAllFilterAsync(Expression<Func<OrdemVendaFluxoDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrdemVendaFluxoDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(OrdemVendaFluxoDto obj)
        {
            var fluxo = obj.MapTo<OrdemVendaFluxo>();
            fluxo.ID = Guid.NewGuid();
            fluxo.SolicitanteFluxoID = obj.SolicitanteFluxoID;
            // fluxo.FluxoLiberacaoManualID = obj.FluxoLimiteCreditoID;
            fluxo.Divisao = obj.Divisao;
            fluxo.ItemOrdemVenda = obj.ItemOrdemVenda;
            fluxo.OrdemVendaNumero = obj.OrdemVendaNumero;
            //  fluxo.UsuarioID = obj.UsuarioID;
            //  fluxo.Status = obj.Status;

            fluxo.DataCriacao = DateTime.Now;
            fluxo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.OrdemVendaFluxoRepository.Insert(fluxo);
            return _unitOfWork.Commit();
        }

        public Task<bool> Update(OrdemVendaFluxoDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatusFluxo(OrdemVendaFluxoDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<LiberacaoOrdemVendaFluxoDto> FluxoAprovacaoReprovacaoOrdem(LiberacaoOrdemVendaFluxoDto solicitante, string URL)
        {
            var status = await _unitOfWork.StatusOrdemVendaRepository.GetAsync(c => c.Status.Equals(solicitante.StatusOrdemVendaNome));
            solicitante.StatusOrdemVendasID = status.ID;
            var solicitanteCliente = await _unitOfWork.SolicitanteFluxoRepository.GetAsync(c => c.ID == solicitante.SolicitanteFluxoID);

            var fluxoDto = await _unitOfWork.OrdemVendaFluxoRepository.FluxoAprovacaoOrdem(solicitante.MapTo<LiberacaoOrdemVendaFluxo>());

            var fluxo = fluxoDto.MapTo<LiberacaoOrdemVendaFluxoDto>();
            if (fluxoDto != null)
            {
                try
                {
                    var user = _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(fluxoDto.UsuarioID.Value));
                    var email = new AppServiceEnvioEmail(_unitOfWork);
                    await email.SendMailLiberacaoManual(solicitante.ID,user.MapTo<UsuarioDto>(), "", fluxoDto.EmpresasId, URL);
                }
                catch
                {
                }
            }
            else
            {
                fluxo = new LiberacaoOrdemVendaFluxoDto();
            }
            return fluxo;
        }
    }
}
