using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceEstruturaComercial : IAppServiceEstruturaComercial
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceEstruturaComercial(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public async Task<IEnumerable<EstruturaComercialDto>> GetAllFilterAsync(Expression<Func<EstruturaComercialDto, bool>> expression)
        {
            var EstruturaComercial = await _unitOfWork.EstruturaComercialRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<EstruturaComercial, bool>>>(expression));

            return AutoMapper.Mapper.Map<IEnumerable<EstruturaComercialDto>>(EstruturaComercial);
        }

        public async Task<bool> Insert(EstruturaComercialDiretoriaDto EstruturaComercialDto)
        {
            var EstruturaComercial = new EstruturaComercial
            {
                CodigoSap =  Guid.NewGuid().ToString().Substring(0,7),
                Nome = EstruturaComercialDto.Nome,
                UsuarioIDCriacao = EstruturaComercialDto.UsuarioIDCriacao,
                Ativo = EstruturaComercialDto.Ativo,
                EstruturaComercialPapelID = "D",
                DataCriacao = DateTime.Now
            };
            
            _unitOfWork.EstruturaComercialRepository.Insert(EstruturaComercial);

            foreach (var gerente in EstruturaComercialDto.Gerentes)
            {
               var objetoGerente = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(gerente.CodigoSap));
                objetoGerente.Superior = EstruturaComercial;
                objetoGerente.Ativo = gerente.Ativo;
                _unitOfWork.EstruturaComercialRepository.Update(objetoGerente);
            }
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(EstruturaComercialDiretoriaDto EstruturaComercialDto)
        {
            var EstruturaComercial = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(EstruturaComercialDto.CodigoSap));
            EstruturaComercial.Nome = EstruturaComercialDto.Nome;
            EstruturaComercial.Ativo = EstruturaComercialDto.Ativo;
            EstruturaComercial.DataAlteracao = DateTime.Now;
            EstruturaComercial.UsuarioIDAlteracao = EstruturaComercialDto.UsuarioIDAlteracao;


            _unitOfWork.EstruturaComercialRepository.Update(EstruturaComercial);
            foreach (var gerente in EstruturaComercialDto.Gerentes)
            {
                var objetoGerente = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(gerente.CodigoSap));
                objetoGerente.Superior = gerente.Ativo ? EstruturaComercial : null;
                
               objetoGerente.DataAlteracao = DateTime.Now;
                objetoGerente.UsuarioIDAlteracao = EstruturaComercialDto.UsuarioIDAlteracao;
                _unitOfWork.EstruturaComercialRepository.Update(objetoGerente);
            }

            return _unitOfWork.Commit();
        }

        public async Task<IEnumerable<EstruturaComercialDto>> GetEstruturaComercialByPaper(string sigla)
        {
            var EstruturaComercial = await _unitOfWork.EstruturaComercialRepository.GetAllFilterAsync(c=>c.EstruturaComercialPapelID.Equals(sigla) && c.Ativo);

            return Mapper.Map<IEnumerable<EstruturaComercialDto>>(EstruturaComercial);
        }

        public async Task<EstruturaComercialDto> GetEstruturaComercialByID(string codSap)
        {
            var EstruturaComercial = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(codSap));

            return Mapper.Map<EstruturaComercialDto>(EstruturaComercial);
        }
    }
}
