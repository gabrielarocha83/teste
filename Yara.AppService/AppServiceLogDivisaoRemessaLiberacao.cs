using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceLogDivisaoRemessaLiberacao : IAppServiceLogDivisaoRemessaLiberacao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceLogDivisaoRemessaLiberacao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<LogDivisaoRemessaLiberacaoDto> GetAsync(Expression<Func<LogDivisaoRemessaLiberacaoDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LogDivisaoRemessaLiberacaoDto>> GetAllFilterAsync(Expression<Func<LogDivisaoRemessaLiberacaoDto, bool>> expression)
        {
            var log = await _unitOfWork.LogDivisaoRemessaLiberacaoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<LogDivisaoRemessaLiberacao, bool>>>(expression));
            return Mapper.Map<IEnumerable<LogDivisaoRemessaLiberacaoDto>>(log);
        }

        public async Task<IEnumerable<LogDivisaoRemessaLiberacaoDto>> GetAllAsync()
        {
            try
            {
                var log = await _unitOfWork.LogDivisaoRemessaLiberacaoRepository.GetAllAsync();
                return Mapper.Map<IEnumerable<LogDivisaoRemessaLiberacaoDto>>(log);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Insert(LogDivisaoRemessaLiberacaoDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(LogDivisaoRemessaLiberacaoDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
