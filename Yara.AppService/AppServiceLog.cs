using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceLog : IAppServiceLog
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceLog(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(LogDto log)
        {
            var objLog = log.MapTo<Log>();

            objLog.DataCriacao = DateTime.Now;
            objLog.UsuarioIDCriacao = log.UsuarioIDCriacao;
            objLog.UsuarioID = log.UsuarioID;
            _unitOfWork.LogRepository.Insert(objLog);

            return _unitOfWork.Commit();
        }

        public async Task<IEnumerable<LogDto>> GetAllFilter<TKey>(Expression<Func<LogDto, bool>> expression)
        {
            var listLog = await _unitOfWork.LogRepository.GetAllFilterAsync(expression.MapTo<Expression<Func<Log, bool>>>());
            return Mapper.Map<IEnumerable<LogDto>>(listLog);
        }

        public async Task<IEnumerable<LogDto>> GetAllAsync()
        {
            var logs = await _unitOfWork.LogRepository.GetAllAsync();
            // var grupos = await _unitOfWork.GrupoRepository.GetAllPaginationAsync(null,page,skip,false);
            return Mapper.Map<IEnumerable<LogDto>>(logs);
        }

        public async Task<IEnumerable<LogDto>> BuscaLog(BuscaLogsDto busca)
        {
            var buscalogs = Mapper.Map<BuscaLogs>(busca);
            var listadelogs = await _unitOfWork.LogRepository.BuscaLog(buscalogs);
            return Mapper.Map<IEnumerable<LogRetorno>,IEnumerable<LogDto>>(listadelogs);
        }

        public async Task<IEnumerable<LogContaClienteDto>> BuscaLogContaCliente(BuscaLogsDto busca)
        {
            var buscalogs = Mapper.Map<BuscaLogs>(busca);
            var listadelogs = await _unitOfWork.LogRepository.BuscaLog(buscalogs);
            return Mapper.Map<IEnumerable<LogRetorno>,IEnumerable<LogContaClienteDto>>(listadelogs);
        }

        public async Task<IEnumerable<BuscaLogFluxoAutomaticoDto>> BuscaLogFluxoAutomatico(BuscaLogFluxoAutomaticoDto busca)
        {
            var buscalogs = Mapper.Map<BuscaLogFluxoAutomatico>(busca);
            var listadelogs = await _unitOfWork.LogRepository.BuscaLogFluxoAutomatico(buscalogs);
            return Mapper.Map<IEnumerable<BuscaLogFluxoAutomaticoDto>>(listadelogs);
        }

        public async Task<IEnumerable<LogWithUserDto>> BuscaLogGrupoEconomico(Guid ContaClienteID, string EmpresaID)
        {
            var listadelogs = await _unitOfWork.LogRepository.BuscaLogGrupoEconomico(ContaClienteID, EmpresaID);
            return Mapper.Map<IEnumerable<LogWithUserDto>>(listadelogs);
        }

        public async Task<IEnumerable<LogWithUserDto>> BuscaLogProposta(Guid propostaId)
        {
            var listadelogs = await _unitOfWork.LogRepository.BuscaLogProposta(propostaId);
            return Mapper.Map<IEnumerable<LogWithUserDto>>(listadelogs);
        }
    }
}