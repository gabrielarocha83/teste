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
using System.IO;
using System.IO.Compression;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceAnexoArquivo : IAppServiceAnexoArquivo
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceAnexoArquivo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AnexoArquivoDto> GetAsync(Expression<Func<AnexoArquivoDto, bool>> expression)
        {
            var anexo = await _unitOfWork.AnexoArquivoRepository.GetAsync(Mapper.Map<Expression<Func<AnexoArquivo, bool>>>(expression));
            return Mapper.Map<AnexoArquivoDto>(anexo);
        }

        public async Task<IEnumerable<AnexoArquivoDto>> GetAllFilterAsync(Expression<Func<AnexoArquivoDto, bool>> expression)
        {
            var anexo = await _unitOfWork.AnexoArquivoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<AnexoArquivo, bool>>>(expression));
            anexo.OrderBy(c => c.Anexo.CategoriaDocumento);
            return Mapper.Map<IEnumerable<AnexoArquivoDto>>(anexo);
        }

        public async Task<IEnumerable<AnexoArquivoDto>> GetAllAsync()
        {
            var anexo = await _unitOfWork.AnexoArquivoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<AnexoArquivoDto>>(anexo);
        }

        public async Task<byte[]> GetAccountZip(Guid contaClienteId, string empresa)
        {

            var anexos = await _unitOfWork.AnexoArquivoRepository.GetAllFilterAsync(aa => aa.ContaClienteID == contaClienteId && aa.Ativo);

            if (anexos.Any())
            {

                List<string> entryNames = new List<string>();
                string entryName = "";
                int entryCount = 0;

                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {

                        foreach (var anexo in anexos)
                        {

                            entryName = anexo.NomeArquivo;
                            entryCount = 0;

                            try
                            {

                                while (entryNames.Contains(entryName))
                                {
                                    entryCount++;
                                    entryName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(anexo.NomeArquivo), entryCount, Path.GetExtension(anexo.NomeArquivo));
                                }
                            }
                            catch (Exception)
                            {
                                entryName = string.Format("Unnamed_{0}.{1}",Guid.NewGuid().ToString().Substring(0,8),Path.GetExtension(anexo.NomeArquivo));
                            }

                            entryNames.Add(entryName);
                            var zipEntry = archive.CreateEntry(entryName);

                            using (var entryStream = zipEntry.Open())
                            {
                                entryStream.Write(anexo.Arquivo, 0, anexo.Arquivo.Length);
                            }

                        }

                    }

                    return memoryStream.ToArray();

                }

            }
            else
            {
                return null;
            }

        }

        public bool Insert(AnexoArquivoDto obj)
        {
            var anexo = obj.MapTo<AnexoArquivo>();
            anexo.ID = Guid.NewGuid();
            anexo.DataCriacao = DateTime.Now;
            anexo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.AnexoArquivoRepository.Insert(anexo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(AnexoArquivoDto obj)
        {
            AnexoArquivo anexo;

            if (obj.ContaClienteID != null)
                anexo = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.Ativo && c.AnexoID == obj.ID && c.ContaClienteID == obj.ContaClienteID);
            else if (obj.PropostaLCID != null)         
                anexo = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.Ativo && c.AnexoID == obj.ID && c.PropostaLCID == obj.PropostaLCID);
            else if (obj.PropostaLCAdicionalID != null)
                anexo = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.Ativo && c.AnexoID == obj.ID && c.PropostaLCAdicionalID == obj.PropostaLCAdicionalID);
            else                                   
                anexo = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.Ativo && c.AnexoID == obj.ID);

            anexo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            anexo.DataAlteracao = obj.DataAlteracao;
            anexo.Ativo = obj.Ativo;

            anexo.Status = obj.Status;
            anexo.DataValidade = obj.DataValidade;
            anexo.Comentario = obj.Comentario;

            _unitOfWork.AnexoArquivoRepository.Update(anexo);
            return _unitOfWork.Commit();
        }

        private double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public async Task<bool> InsertAsync(AnexoArquivoDto obj)
        {
            var anexoArquivo = obj.MapTo<AnexoArquivo>();

            if (obj.PropostaLCID != null)
            {
                var exist = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.AnexoID == obj.AnexoID && c.Ativo && c.PropostaLCID == anexoArquivo.PropostaLCID);
                if (exist != null)
                {
                    throw new ArgumentException($"Este Arquivo já foi anexado para esta Proposta de LC!");
                }
            }

            if (obj.PropostaLCAdicionalID != null)
            {
                var exist = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.AnexoID == obj.AnexoID && c.Ativo && c.PropostaLCAdicionalID == anexoArquivo.PropostaLCAdicionalID);
                if (exist != null)
                {
                    throw new ArgumentException($"Este Arquivo já foi anexado para esta Proposta de LC Adicional!");
                }
            }

            var extensoes = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.Tipo.Equals("anexo") && c.EmpresasID == obj.EmpresaID);
            if (extensoes.Any(item => !item.Valor.Contains(obj.ExtensaoArquivo.Remove(0, 1).ToLower())))
            {
                throw new ArgumentException("Extensão não permitida!");
            }

            var tamanho = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Tipo.Equals("tamanho") && c.EmpresasID == obj.EmpresaID);
            if (ConvertBytesToMegabytes(obj.Arquivo.Length) > Convert.ToDouble(tamanho.Valor))
                throw new ArgumentException($"O Arquivo é maior que {tamanho.Valor} MB.");

            var anexo = await _unitOfWork.AnexoRepository.GetAsync(c => c.ID.Equals(obj.AnexoID));

            //Caso tenha Id da Proposta, adicionar na tabela.
            if (obj.PropostaLCID != null)
            {
                var lc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID == obj.PropostaLCID);
                anexoArquivo.PropostaLCID = lc.ID;
                anexoArquivo.PropostaLC = lc;
            }

            //Caso tenha Id da Proposta, adicionar na tabela.
            if (obj.PropostaLCAdicionalID != null)
            {
                var lca = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.ID == obj.PropostaLCAdicionalID);
                anexoArquivo.PropostaLCAdicionalID = lca.ID;
                anexoArquivo.PropostaLCAdicional = lca;
            }

            //Caso tenha Id do Cliente, adicinar na tabela.
            if (obj.ContaClienteID != null)
            {
                var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == obj.ContaClienteID);
                anexoArquivo.ContaClienteID = contaCliente.ID;
                anexoArquivo.ContaCliente = contaCliente;
            }

            anexoArquivo.ID = Guid.NewGuid();
            anexoArquivo.Anexo = anexo;
            anexoArquivo.DataCriacao = DateTime.Now;
            anexoArquivo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            anexoArquivo.Ativo = true;

            anexoArquivo.Status = obj.Status;
            anexoArquivo.DataValidade = obj.DataValidade;
            anexoArquivo.Comentario = obj.Comentario;

            _unitOfWork.AnexoArquivoRepository.Insert(anexoArquivo);

            try
            {
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateList(List<AnexoArquivoDto> obj, Guid userId)
        {
            foreach (var item in obj)
            {
                var anexo = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.ID.Equals(item.ID));
                anexo.UsuarioIDAlteracao = userId;
                anexo.DataAlteracao = DateTime.Now;
                anexo.Ativo = item.Ativo;
                anexo.NomeArquivo = item.NomeArquivo;
                anexo.Status = item.Status;
                anexo.DataValidade = item.DataValidade;
                anexo.Comentario = item.Comentario;

                if (item.ContaClienteID.HasValue)
                {
                    anexo.ContaClienteID = item.ContaClienteID;
                }
                else if (item.PropostaLCID.HasValue)
                {
                    anexo.PropostaLCID = item.PropostaLCID;
                }
                else if (item.PropostaLCAdicionalID.HasValue)
                {
                    anexo.PropostaLCAdicionalID = item.PropostaLCAdicionalID;
                }

                _unitOfWork.AnexoArquivoRepository.Update(anexo);

            }
            var retorno = _unitOfWork.Commit();
            return retorno;
        }

        public async Task<IEnumerable<AnexoArquivoDto>> CustomGetAllFilterAsync(Expression<Func<AnexoArquivoDto, bool>> expression)
        {
            var anexo = (await _unitOfWork.AnexoArquivoRepository.CustomGetAllFilterAsync(Mapper.Map<Expression<Func<AnexoArquivo, bool>>>(expression))).OrderBy(c => c.Anexo.CategoriaDocumento);
            return Mapper.Map<IEnumerable<AnexoArquivoDto>>(anexo);
        }
    }
}
