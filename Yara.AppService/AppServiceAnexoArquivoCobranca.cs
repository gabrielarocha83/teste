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
    public class AppServiceAnexoArquivoCobranca : IAppServiceAnexoArquivoCobranca
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceAnexoArquivoCobranca(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AnexoArquivoCobrancaDto> GetAsync(Expression<Func<AnexoArquivoCobrancaDto, bool>> expression)
        {
            var anexo = await _unitOfWork.AnexoArquivoCobrancaRepository.GetAsync(Mapper.Map<Expression<Func<AnexoArquivoCobranca, bool>>>(expression));
            return Mapper.Map<AnexoArquivoCobrancaDto>(anexo);
        }

        public async Task<IEnumerable<AnexoArquivoCobrancaDto>> GetAllFilterAsync(Expression<Func<AnexoArquivoCobrancaDto, bool>> expression)
        {
            var anexo = await _unitOfWork.AnexoArquivoCobrancaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<AnexoArquivoCobranca, bool>>>(expression));
            anexo.OrderBy(c => c.NomeArquivo);
            return Mapper.Map<IEnumerable<AnexoArquivoCobrancaDto>>(anexo);
        }

        public async Task<IEnumerable<AnexoArquivoCobrancaDto>> GetAllAsync()
        {
            var anexo = await _unitOfWork.AnexoArquivoCobrancaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<AnexoArquivoCobrancaDto>>(anexo);
        }

        public bool Insert(AnexoArquivoCobrancaDto obj)
        {
            var anexo = obj.MapTo<AnexoArquivoCobranca>();
            anexo.ID = Guid.NewGuid();
            anexo.DataCriacao = DateTime.Now;
            anexo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.AnexoArquivoCobrancaRepository.Insert(anexo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(AnexoArquivoCobrancaDto obj)
        {
            var anexo = await _unitOfWork.AnexoArquivoCobrancaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            anexo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            anexo.DataAlteracao = obj.DataAlteracao;
            anexo.Ativo = obj.Ativo;

            _unitOfWork.AnexoArquivoCobrancaRepository.Update(anexo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(AnexoArquivoCobrancaDto obj, string empresaId)
        {
            var anexoArquivoCobranca = obj.MapTo<AnexoArquivoCobranca>();
            var exist = await _unitOfWork.AnexoArquivoCobrancaRepository.GetAsync(c => c.PropostaCobranca == obj.PropostaCobranca && c.Ativo && c.ContaClienteID == obj.ContaClienteID && c.TipoProposta == obj.TipoProposta && c.NomeArquivo == obj.NomeArquivo);
            var extensoes = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("anexo") && c.EmpresasID == empresaId);
            var tamanho = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Tipo.Equals("tamanho") && c.EmpresasID == empresaId);
            var arquivotamanho = ConvertBytesToMegabytes(obj.Arquivo.Length);

            if (!extensoes.Any(c => c.Valor.Split(',').Contains(obj.ExtensaoArquivo)))
                //throw new ArgumentException("Extensão não permitida.");

            if (exist != null)
                throw new ArgumentException("Arquivo ja existe para esta proposta.");

            if (arquivotamanho > Convert.ToDouble(tamanho.Valor))
                throw new ArgumentException($"O Arquivo é maior que {tamanho.Valor} MB.");


            anexoArquivoCobranca.ID = Guid.NewGuid();
            anexoArquivoCobranca.PropostaCobranca = obj.PropostaCobranca;
            anexoArquivoCobranca.TipoProposta = obj.TipoProposta;
            anexoArquivoCobranca.Arquivo = obj.Arquivo;
            anexoArquivoCobranca.NomeArquivo = obj.NomeArquivo;
            anexoArquivoCobranca.ExtensaoArquivo = obj.ExtensaoArquivo;
            anexoArquivoCobranca.Ativo = true;
            anexoArquivoCobranca.DataCriacao = DateTime.Now;
            anexoArquivoCobranca.UsuarioIDCriacao = obj.UsuarioIDCriacao;

            _unitOfWork.AnexoArquivoCobrancaRepository.Insert(anexoArquivoCobranca);

            try
            {
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<AnexoArquivoCobrancaWithOutFileDto>> GetAllFilterWithOutFileAsync(Guid propostaid, int tipo)
        {
            var anexo = await _unitOfWork.AnexoArquivoCobrancaRepository.GetAllFilterAsync(c => c.PropostaCobranca == propostaid && c.TipoProposta == tipo && c.Ativo);
            anexo.OrderBy(c => c.NomeArquivo);




            return Mapper.Map<IEnumerable<AnexoArquivoCobrancaWithOutFileDto>>(anexo);
        }

        private double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
