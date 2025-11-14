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
    public class AppServiceContaClienteGarantia : IAppServiceContaClienteGarantia
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceContaClienteGarantia(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContaClienteGarantiaDto> GetAsync(Expression<Func<ContaClienteGarantiaDto, bool>> expression)
        {
            var garantia = await _unitOfWork.ContaClienteGarantiaRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteGarantia, bool>>>(expression));
            return Mapper.Map<ContaClienteGarantiaDto>(garantia);
        }

        public async Task<IEnumerable<ContaClienteGarantiaDto>> GetAllFilterAsync(Expression<Func<ContaClienteGarantiaDto, bool>> expression)
        {
            var garantias = await _unitOfWork.ContaClienteGarantiaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ContaClienteGarantia, bool>>>(expression));
            return Mapper.Map<IEnumerable<ContaClienteGarantiaDto>>(garantias);
        }

        public async Task<IEnumerable<ContaClienteGarantiaDto>> GetAllAsync()
        {
            var garantias = await _unitOfWork.ContaClienteGarantiaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteGarantiaDto>>(garantias);
        }

        public bool Insert(ContaClienteGarantiaDto obj)
        {
            var garantia = obj.MapTo<ContaClienteGarantia>();
            garantia.DataCriacao = DateTime.Now;
            garantia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ContaClienteGarantiaRepository.Insert(garantia);
            return _unitOfWork.Commit();
        }

        public Task<bool> Update(ContaClienteGarantiaDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Inactive(Guid id)
        { // NOT USED ANYWHERE... IS THIS DEPRECATED? HM MAYBE...
            var garantia = await _unitOfWork.ContaClienteGarantiaRepository.GetAsync(c => c.ID.Equals(id));
            garantia.Ativo = false;
            _unitOfWork.ContaClienteGarantiaRepository.Update(garantia);
            return _unitOfWork.Commit();
        }

        public async Task<ContaClienteGarantiaDto> InsertAsync(ContaClienteGarantiaDto obj)
        {
            try
            {
                var exist = await _unitOfWork.ContaClienteGarantiaRepository.GetAsync(c => c.Codigo.Equals(obj.Codigo) && c.EmpresasID.Equals(obj.EmpresasID));
                if (exist != null)
                    throw new ArgumentException("Garantia já esta cadastrada.");

                var garantia = obj.MapTo<ContaClienteGarantia>();
                garantia.Codigo = _unitOfWork.ContaClienteGarantiaRepository.GetMaxNumeroInterno();
                garantia.EmpresasID = obj.EmpresasID;

                //Cria Participantes de Garantia
                garantia.ParticipanteGarantia = obj.ParticipanteGarantia.Select(itemParticipanteGarantia => new ContaClienteParticipanteGarantia
                {
                    ID = Guid.NewGuid(),
                    Documento = itemParticipanteGarantia.Documento,
                    Nome = itemParticipanteGarantia.Nome,
                    Ativo = itemParticipanteGarantia.Ativo,
                    ContaClienteGarantiaID = obj.ID,
                    Garantido = itemParticipanteGarantia.Garantido,
                    DataCriacao = obj.DataCriacao,
                    UsuarioIDCriacao = obj.UsuarioIDCriacao
                }).ToList();

                //Cria Responsaveis de Garantia
                garantia.ResponsavelGarantia = obj.ResponsavelGarantia.Select(item => new ContaClienteResponsavelGarantia()
                {
                    ID = Guid.NewGuid(),
                    Documento = item.Documento,
                    Nome = item.Nome,
                    Ativo = item.Ativo,
                    ContaClienteGarantiaID = obj.ID,
                    TipoResponsabilidade = item.TipoResponsabilidade,
                    DataCriacao = obj.DataCriacao,
                    UsuarioIDCriacao = obj.ContaClienteID
                }).ToList();

                _unitOfWork.ContaClienteGarantiaRepository.Insert(garantia);
                _unitOfWork.Commit();

                return garantia.MapTo<ContaClienteGarantiaDto>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ContaClienteGarantiaDto> UpdateAsync(ContaClienteGarantiaDto obj)
        {
            try
            {
                var garantia = await _unitOfWork.ContaClienteGarantiaRepository.GetAsync(c => c.ID.Equals(obj.ID));
                if (!String.IsNullOrEmpty(garantia.EmpresasID) && garantia.EmpresasID != obj.EmpresasID)
                    throw new ArgumentException("Não é possível atualizar a garantia de outra empresa.");

                garantia.ValorGarantia = obj.ValorGarantia;
                garantia.ValorGarantido = obj.ValorGarantido;
                garantia.Vigencia = obj.Vigencia;
                garantia.VigenciaFim = obj.VigenciaFim;
                garantia.DataPagamento = obj.DataPagamento;
                garantia.Motivo = obj.Motivo;
                garantia.Observacao = obj.Observacao;
                garantia.Grau = obj.Grau;
                garantia.Matricula = obj.Matricula;
                garantia.TipoImovel = obj.TipoImovel;
                garantia.Cidade = obj.Cidade;
                garantia.Uf = obj.Uf;
                garantia.Registro = obj.Registro;
                garantia.Laudo = obj.Laudo;
                garantia.ValorForcada = obj.ValorForcada;
                garantia.ValorMercado = obj.ValorMercado;
                garantia.Produto = obj.Produto;
                garantia.Quantidade = obj.Quantidade;
                garantia.Empresa = obj.Empresa;
                garantia.Area = obj.Area;
                garantia.Monitoramento = obj.Monitoramento;
                garantia.EmpresaMonitoramento = obj.EmpresaMonitoramento;
                garantia.OutrasGarantias = obj.OutrasGarantias;
                garantia.TipoGarantia = obj.TipoGarantia;
                garantia.Status = obj.Status;
                garantia.Ativo = obj.Ativo;
                garantia.ContaClienteID = obj.ContaClienteID;
                garantia.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
                garantia.DescricaoOutros = obj.DescricaoOutros;
                garantia.DataAlteracao = DateTime.Now;
                garantia.EmpresasID = obj.EmpresasID;

                //Atualiza Participantes de Garantia
                var idsParticipantes = obj.ParticipanteGarantia.Where(pg => pg.ID != Guid.Empty).Select(pg => pg.ID).ToList();
                var removeParticipantes = garantia.ParticipanteGarantia.Where(pa => !idsParticipantes.Contains(pa.ID)).ToList();

                ContaClienteParticipanteGarantia ccpAtual = null;

                foreach (var rpa in removeParticipantes)
                {
                    _unitOfWork.ContaClienteParticipanteGarantiaRepository.Delete(rpa);
                }

                foreach (var pa in obj.ParticipanteGarantia)
                {
                    ccpAtual = garantia.ParticipanteGarantia.FirstOrDefault(p => p.ID.Equals(pa.ID));

                    if (ccpAtual == null)
                    {
                        pa.ContaClienteGarantiaID = garantia.ID;
                        pa.ID = Guid.NewGuid();
                        pa.UsuarioIDCriacao = obj.UsuarioIDAlteracao.Value;
                        pa.DataCriacao = DateTime.Now;
                        pa.Ativo = obj.Ativo;
                        garantia.ParticipanteGarantia.Add(pa.MapTo<ContaClienteParticipanteGarantia>());
                    }
                    else
                    {
                        ccpAtual.Documento = pa.Documento;
                        ccpAtual.Nome = pa.Nome;
                        ccpAtual.Garantido = pa.Garantido;
                        ccpAtual.Ativo = pa.Ativo;
                        ccpAtual.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
                        ccpAtual.DataAlteracao = DateTime.Now;
                        _unitOfWork.ContaClienteParticipanteGarantiaRepository.Update(ccpAtual);
                    }
                }

                //Atualiza Responsaveis de Garantia
                var idsResponsaveis = obj.ResponsavelGarantia.Where(pg => pg.ID != Guid.Empty).Select(pg => pg.ID).ToList();
                var removeResponsaveis = garantia.ResponsavelGarantia.Where(pa => !idsResponsaveis.Contains(pa.ID)).ToList();

                ContaClienteResponsavelGarantia rcpAtual = null;

                foreach (var rpr in removeResponsaveis)
                {
                    _unitOfWork.ContaClienteResponsavelGarantiaRepository.Delete(rpr);
                }

                foreach (var rg in obj.ResponsavelGarantia)
                {
                    rcpAtual = garantia.ResponsavelGarantia.FirstOrDefault(p => p.ID.Equals(rg.ID));

                    if (rcpAtual == null)
                    {
                        rg.ContaClienteGarantiaID = garantia.ID;
                        rg.ID = Guid.NewGuid();
                        rg.UsuarioIDCriacao = obj.UsuarioIDAlteracao.Value;
                        rg.DataCriacao = DateTime.Now;
                        rg.Ativo = obj.Ativo;
                        garantia.ResponsavelGarantia.Add(rg.MapTo<ContaClienteResponsavelGarantia>());
                    }
                    else
                    {
                        rcpAtual.Documento = rg.Documento;
                        rcpAtual.Nome = rg.Nome;
                        rcpAtual.TipoResponsabilidade = rg.TipoResponsabilidade;
                        rcpAtual.Ativo = rg.Ativo;
                        rcpAtual.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
                        rcpAtual.DataAlteracao = DateTime.Now;
                        _unitOfWork.ContaClienteResponsavelGarantiaRepository.Update(rcpAtual);
                    }
                }

                _unitOfWork.ContaClienteGarantiaRepository.Update(garantia);
                _unitOfWork.Commit();

                return garantia.MapTo<ContaClienteGarantiaDto>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ContaClienteGarantiaDto>> GetAllFilterAsyncAll(string documento)
        {
            var participante = await _unitOfWork.ContaClienteParticipanteGarantiaRepository.GetAllFilterAsync(c => c.Documento.Equals(documento));

            var garantiaLst = new List<ContaClienteGarantiaDto>();

            foreach (var item in participante)
            {
                var ret = await _unitOfWork.ContaClienteGarantiaRepository.GetAsync(c => c.ID.Equals(item.ContaClienteGarantiaID));
                var garantia = ret.MapTo<ContaClienteGarantiaDto>();
                garantiaLst.Add(garantia);
            }

            return garantiaLst.GroupBy(c => c.Codigo).Select(g => g.FirstOrDefault());
        }
    }
}
