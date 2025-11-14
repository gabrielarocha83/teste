using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServicePropostaRenovacaoVigenciaLC : IAppServicePropostaRenovacaoVigenciaLC
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaRenovacaoVigenciaLC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropostaRenovacaoVigenciaLCDto> GetAsync(Expression<Func<PropostaRenovacaoVigenciaLCDto, bool>> expression)
        {
            var proposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(Mapper.Map<Expression<Func<PropostaRenovacaoVigenciaLC, bool>>>(expression));
            var propostaDto = Mapper.Map<PropostaRenovacaoVigenciaLCDto>(proposta);

            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(propostaDto.UsuarioIDCriacao));
            propostaDto.UsuarioNomeCriacao = Mapper.Map<UsuarioDto>(usuario).Nome;

            return propostaDto;
        }

        public async Task<IEnumerable<PropostaRenovacaoVigenciaLCDto>> GetAllFilterAsync(Expression<Func<PropostaRenovacaoVigenciaLCDto, bool>> expression)
        {
            var propostas = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<PropostaRenovacaoVigenciaLC, bool>>>(expression));
            var propostasDto = Mapper.Map<IEnumerable<PropostaRenovacaoVigenciaLCDto>>(propostas);

            foreach(var propostaDto in propostasDto)
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(propostaDto.UsuarioIDCriacao));
                propostaDto.UsuarioNomeCriacao = Mapper.Map<UsuarioDto>(usuario).Nome;
            }

            return propostasDto;
        }

        public async Task<IEnumerable<PropostaRenovacaoVigenciaLCDto>> GetAllAsync()
        {
            var propostas = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAllAsync();
            var propostasDto = Mapper.Map<IEnumerable<PropostaRenovacaoVigenciaLCDto>>(propostas);

            foreach (var propostaDto in propostasDto)
            {
                var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(propostaDto.UsuarioIDCriacao));
                propostaDto.UsuarioNomeCriacao = Mapper.Map<UsuarioDto>(usuario).Nome;
            }

            return propostasDto;
        }

        public bool Insert(PropostaRenovacaoVigenciaLCDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaRenovacaoVigenciaLCDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BuscaContaClientePropostaRenovacaoLCDto>> GetClientListByFilterAsync(FiltroContaClientePropostaRenovacaoVigenciaLCDto filter)
        {
            try
            {
                var filtros = filter.MapTo<FiltroContaClientePropostaRenovacaoVigenciaLC>();
                var consulta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetClientListByFilterAsync(filtros);

                return consulta.MapTo<IEnumerable<BuscaContaClientePropostaRenovacaoLCDto>>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BuscaContaClientePropostaRenovacaoLCDto>> GetClientListByFilterAsync(ListaClientePropostaRenovacaoVigenciaLCDto clientes, string empresaID)
        {
            try
            {
                var filtros = new FiltroContaClientePropostaRenovacaoVigenciaLCDto { EmpresasID = empresaID, XMLGuidList = this.ParseGuidListXML(clientes.ContaClienteID.ToList()) };
                var consulta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetClientListByFilterAsync(filtros.MapTo<FiltroContaClientePropostaRenovacaoVigenciaLC>());

                return consulta.MapTo<IEnumerable<BuscaContaClientePropostaRenovacaoLCDto>>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<byte[]> GetClientListExcelByFilterAsync(FiltroContaClientePropostaRenovacaoVigenciaLCDto filter)
        {
            try
            {
                var filtros = filter.MapTo<FiltroContaClientePropostaRenovacaoVigenciaLC>();
                var consulta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetClientListByFilterAsync(filtros);

                // Regex para remover pontos, dígitos e barras do número do documento.
                var rgx = new Regex("[\\.\\-\\/]+");

                // Criar Excel
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var sheet = excel.Workbook.Worksheets.Add("Clientes");

                    // Linha de Titulo
                    sheet.Cells[1, 1].Value = "Nome do Cliente";
                    sheet.Cells[1, 2].Value = "Código Cliente";
                    sheet.Cells[1, 3].Value = "Apelido";
                    sheet.Cells[1, 4].Value = "CPF/CNPJ";
                    sheet.Cells[1, 5].Value = "Tipo de Cliente";
                    sheet.Cells[1, 6].Value = "Nome do grupo";
                    sheet.Cells[1, 7].Value = "Classificação Grupo";
                    sheet.Cells[1, 8].Value = "Vigência do LC";
                    sheet.Cells[1, 9].Value = "Rating";
                    sheet.Cells[1, 10].Value = "Valor do LC";
                    sheet.Cells[1, 11].Value = "Top 10";
                    sheet.Cells[1, 12].Value = "Data consulta Serasa";
                    sheet.Cells[1, 13].Value = "Restrição Serasa";
                    sheet.Cells[1, 14].Value = "Restrição Yara";
                    sheet.Cells[1, 15].Value = "Rest. Serasa Grupo";
                    sheet.Cells[1, 16].Value = "Rest. Yara Grupo";
                    sheet.Cells[1, 17].Value = "ID Proposta LC";
                    sheet.Cells[1, 18].Value = "Valor Proposta LC";
                    sheet.Cells[1, 19].Value = "Status Proposta LC";
                    sheet.Cells[1, 20].Value = "ID Proposta Alçada";
                    sheet.Cells[1, 21].Value = "Valor Proposta Alçada";
                    sheet.Cells[1, 22].Value = "Status Proposta Alçada";
                    sheet.Cells[1, 23].Value = "Data última compra";
                    sheet.Cells[1, 24].Value = "Data validade garantia";
                    sheet.Cells[1, 25].Value = "Representante";
                    sheet.Cells[1, 26].Value = "CTC";
                    sheet.Cells[1, 27].Value = "GC";
                    sheet.Cells[1, 28].Value = "Diretoria";
                    sheet.Cells[1, 29].Value = "Analista";

                    // Linha de Titulo - Negrito
                    sheet.Cells[1, 1, 1, 29].Style.Font.Bold = true;
                    sheet.Cells[1, 1, 1, 29].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // NOT READY YET
                    //sheet.Cells[1, 25].Value = "Quantidade de Renovações";
                    //sheet.Cells[1, 26].Value = "Data última Renovação";

                    var startRow = 1;

                    var consultaDto = consulta.MapTo<IEnumerable<BuscaContaClientePropostaRenovacaoLCDto>>();

                    foreach (var cliente in consulta)
                    {
                        // Parse das informações do documento
                        string documentoFormatado = rgx.Replace(cliente.Documento.Trim(), "");
                        bool converted = UInt64.TryParse(documentoFormatado, out ulong documentoOut);

                        startRow++;

                        sheet.Cells[startRow, 1].Value = cliente.NomeCliente;
                        sheet.Cells[startRow, 2].Value = cliente.CodigoCliente;
                        sheet.Cells[startRow, 3].Value = cliente.Apelido;
                        sheet.Cells[startRow, 4].Value = converted ? (documentoFormatado.Length > 11 ? documentoOut.ToString(@"00\.000\.000\/0000\-00") : documentoOut.ToString(@"000\.000\.000\-00")) : "";
                        sheet.Cells[startRow, 5].Value = cliente.TipoCliente;
                        sheet.Cells[startRow, 6].Value = cliente.NomeGrupo;
                        sheet.Cells[startRow, 7].Value = cliente.ClassificacaoGrupo;
                        sheet.Cells[startRow, 8].Value = cliente.DataVigenciaLC;
                        sheet.Cells[startRow, 8].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 9].Value = cliente.Rating;
                        sheet.Cells[startRow, 10].Value = cliente.ValorLC;
                        sheet.Cells[startRow, 10].Style.Numberformat.Format = "#,##0.00";
                        sheet.Cells[startRow, 11].Value = (cliente.Top10.HasValue && cliente.Top10.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 12].Value = cliente.DataConsultaSerasa;
                        sheet.Cells[startRow, 12].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 13].Value = (cliente.RestricaoSerasa.HasValue && cliente.RestricaoSerasa.Value > 0 ? "Sim" : "Não");
                        sheet.Cells[startRow, 14].Value = (cliente.RestricaoYara.HasValue && cliente.RestricaoYara.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 15].Value = (cliente.RestricaoSerasaGrupo.HasValue && cliente.RestricaoSerasaGrupo.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 16].Value = (cliente.RestricaoYaraGrupo.HasValue && cliente.RestricaoYaraGrupo.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 17].Value = cliente.CodigoPropostaLC;
                        sheet.Cells[startRow, 18].Value = cliente.ValorPropostaLC;
                        sheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                        sheet.Cells[startRow, 19].Value = cliente.PropostaLCStatus;
                        sheet.Cells[startRow, 20].Value = cliente.CodigoPropostaAC;
                        sheet.Cells[startRow, 21].Value = cliente.ValorPropostaAC;
                        sheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                        sheet.Cells[startRow, 22].Value = cliente.PropostaACStatus;
                        sheet.Cells[startRow, 23].Value = cliente.DataUltimaCompra;
                        sheet.Cells[startRow, 23].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 24].Value = cliente.DataValidadeGarantia;
                        sheet.Cells[startRow, 24].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 25].Value = cliente.Representante;
                        sheet.Cells[startRow, 26].Value = cliente.CTC;
                        sheet.Cells[startRow, 27].Value = cliente.GC;
                        sheet.Cells[startRow, 28].Value = cliente.Diretoria;
                        sheet.Cells[startRow, 29].Value = cliente.Analista;

                        // NOT READY YET
                        //sheet.Cells[startRow, 25].Value = 999; 
                        //sheet.Cells[startRow, 26].Value = DateTime.Now;
                        //sheet.Cells[startRow, 26].Style.Numberformat.Format = "dd/MM/yyyy";
                    }

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();

                    var excelFileContent = excel.GetAsByteArray();

                    return excelFileContent;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> InsertPropostalAsync(ListaClientePropostaRenovacaoVigenciaLCDto clientes, Guid usuarioID, Guid propostaRenovacaoVigenciaLCID, string empresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa)
        {
            try
            {
                ICollection<PropostaRenovacaoVigenciaLCClienteDto> clientesProposta = new List<PropostaRenovacaoVigenciaLCClienteDto>();

                var serasa = new AppServiceSerasa(_unitOfWork);

                foreach (var contaClienteID in clientes.ContaClienteID)
                {
                    // 0) Validação de proposta em andamento
                    var existeProposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetProposalByAccountClient(contaClienteID);

                    if (existeProposta != null && existeProposta != Guid.Empty)
                    {
                        var clienteProposta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == contaClienteID);
                        throw new ArgumentException($"O Cliente {clienteProposta.Nome} já possui uma proposta em andamento.");
                    }

                    // 1) Serasa
                    var existeSerasa = await serasa.ExistSerasa(contaClienteID, empresaID);

                    if (existeSerasa.ContaClienteID == null || existeSerasa.ContaClienteID == Guid.Empty)
                    {
                        var solicitante = new SolicitanteSerasaDto
                        {
                            UsuarioIDCriacao = usuarioID,
                            ContaClienteID = contaClienteID
                        };

                        await serasa.ConsultarSerasa(solicitante, empresaID, urlSerasa, usuarioSerasa, senhaSerasa);
                    }

                    // 2) Buscar o cliente na lista de clientesProposta já preenchendo se é apto ou não
                    var clienteAtualizado = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetClientListByFilterAsync(new FiltroContaClientePropostaRenovacaoVigenciaLC { ContaClienteID = contaClienteID, EmpresasID = empresaID });
                    var cliente = clienteAtualizado.FirstOrDefault();

                    if (cliente != null)
                    {
                        var clienteInserir = new PropostaRenovacaoVigenciaLCClienteDto
                        {
                            ID = Guid.NewGuid(),
                            ContaClienteID = cliente.ContaClienteID,
                            NomeCliente = cliente.NomeCliente,
                            CodigoCliente = cliente.CodigoCliente,
                            Apelido = cliente.Apelido,
                            Documento = cliente.Documento,
                            TipoCliente = cliente.TipoCliente,
                            NomeGrupo = cliente.NomeGrupo,
                            ClassificacaoGrupo = cliente.ClassificacaoGrupo,
                            DataVigenciaLC = cliente.DataVigenciaLC,
                            Rating = cliente.Rating,
                            ValorLC = cliente.ValorLC,
                            Top10 = cliente.Top10,
                            DataConsultaSerasa = cliente.DataConsultaSerasa,
                            RestricaoSerasa = cliente.RestricaoSerasa,
                            RestricaoYara = cliente.RestricaoYara,
                            RestricaoSerasaGrupo = cliente.RestricaoSerasaGrupo,
                            RestricaoYaraGrupo = cliente.RestricaoYaraGrupo,
                            CodigoPropostaLC = cliente.CodigoPropostaLC,
                            ValorPropostaLC = cliente.ValorPropostaLC,
                            PropostaLCStatus = cliente.PropostaLCStatus,
                            CodigoPropostaAC = cliente.CodigoPropostaAC,
                            ValorPropostaAC = cliente.ValorPropostaAC,
                            PropostaACStatus = cliente.PropostaACStatus,
                            DataUltimaCompra = cliente.DataUltimaCompra,
                            DataValidadeGarantia = cliente.DataValidadeGarantia,
                            Representante = cliente.Representante,
                            CTC = cliente.CTC,
                            GC = cliente.GC,
                            Diretoria = cliente.Diretoria,
                            Analista = cliente.Analista,
                            Apto = !((cliente.RestricaoSerasa.HasValue && cliente.RestricaoSerasa.Value > 2) || (cliente.RestricaoYara.HasValue && cliente.RestricaoYara.Value == true) || (cliente.RestricaoSerasaGrupo.HasValue && cliente.RestricaoSerasaGrupo.Value == true) || (cliente.RestricaoYaraGrupo.HasValue && cliente.RestricaoYaraGrupo.Value == true)),
                            PropostaRenovacaoVigenciaLCID = propostaRenovacaoVigenciaLCID
                        };

                        clientesProposta.Add(clienteInserir);
                    }
                }

                // 3) Buscar o fluxo para identificar o primeiro responsavelID, que é o primeiro membro do comitê a atuar na proposta após o usuário de criação (esta validação foi movida para a ação de enviar para o comitê)

                // 4) Criar a proposta e relacionar os clientes
                var proposta = new PropostaRenovacaoVigenciaLC
                {
                    ID = propostaRenovacaoVigenciaLCID,
                    EmpresaID = empresaID,
                    NumeroInternoProposta = _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetMaxNumeroInterno(),
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = usuarioID,
                    PropostaLCStatusID = "XC",
                    Montante = clientesProposta.Where(c => c.Apto && c.ValorLC.HasValue).Sum(c => c.ValorLC.Value),
                    DataNovaVigencia = clientes.DataNovaVigencia,
                    ResponsavelID = usuarioID,
                    Clientes = Mapper.Map<ICollection<PropostaRenovacaoVigenciaLCCliente>>(clientesProposta)
                };

                _unitOfWork.PropostaRenovacaoVigenciaLCRepository.Insert(proposta);
            }
            catch(Exception e)
            {
                throw e;
            }

            return _unitOfWork.Commit();
        }

        public async Task<byte[]> GetProposalClientListExcelAsync(Guid propostaRenovacaoVigenciaLCID)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(c => c.ID == propostaRenovacaoVigenciaLCID);

                // Regex para remover pontos, dígitos e barras do número do documento.
                var rgx = new Regex("[\\.\\-\\/]+");

                // Criar Excel
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var sheet = excel.Workbook.Worksheets.Add("Clientes");

                    // Linha de Titulo
                    sheet.Cells[1, 1].Value = "Nome do Cliente";
                    sheet.Cells[1, 2].Value = "Código Cliente";
                    sheet.Cells[1, 3].Value = "Apelido";
                    sheet.Cells[1, 4].Value = "CPF/CNPJ";
                    sheet.Cells[1, 5].Value = "Tipo de Cliente";
                    sheet.Cells[1, 6].Value = "Nome do grupo";
                    sheet.Cells[1, 7].Value = "Classificação Grupo";
                    sheet.Cells[1, 8].Value = "Vigência do LC";
                    sheet.Cells[1, 9].Value = "Rating";
                    sheet.Cells[1, 10].Value = "Valor do LC";
                    sheet.Cells[1, 11].Value = "Top 10";
                    sheet.Cells[1, 12].Value = "Data consulta Serasa";
                    sheet.Cells[1, 13].Value = "Restrição Serasa";
                    sheet.Cells[1, 14].Value = "Restrição Yara";
                    sheet.Cells[1, 15].Value = "Rest. Serasa Grupo";
                    sheet.Cells[1, 16].Value = "Rest. Yara Grupo";
                    sheet.Cells[1, 17].Value = "ID Proposta LC";
                    sheet.Cells[1, 18].Value = "Valor Proposta LC";
                    sheet.Cells[1, 19].Value = "Status Proposta LC";
                    sheet.Cells[1, 20].Value = "ID Proposta Alçada";
                    sheet.Cells[1, 21].Value = "Valor Proposta Alçada";
                    sheet.Cells[1, 22].Value = "Status Proposta Alçada";
                    sheet.Cells[1, 23].Value = "Data última compra";
                    sheet.Cells[1, 24].Value = "Data validade garantia";
                    sheet.Cells[1, 25].Value = "Representante";
                    sheet.Cells[1, 26].Value = "CTC";
                    sheet.Cells[1, 27].Value = "GC";
                    sheet.Cells[1, 28].Value = "Diretoria";
                    sheet.Cells[1, 29].Value = "Analista";
                    sheet.Cells[1, 30].Value = "Apto/Com restrição";

                    // Linha de Titulo - Negrito
                    sheet.Cells[1, 1, 1, 30].Style.Font.Bold = true;
                    sheet.Cells[1, 1, 1, 30].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    var startRow = 1;

                    var clientesDto = Mapper.Map<IEnumerable<PropostaRenovacaoVigenciaLCClienteDto>>(proposta.Clientes);

                    foreach (var cliente in clientesDto)
                    {
                        // Parse das informações do documento
                        string documentoFormatado = rgx.Replace(cliente.Documento.Trim(), "");
                        bool converted = UInt64.TryParse(documentoFormatado, out ulong documentoOut);

                        startRow++;

                        sheet.Cells[startRow, 1].Value = cliente.NomeCliente;
                        sheet.Cells[startRow, 2].Value = cliente.CodigoCliente;
                        sheet.Cells[startRow, 3].Value = cliente.Apelido;
                        sheet.Cells[startRow, 4].Value = converted ? (documentoFormatado.Length > 11 ? documentoOut.ToString(@"00\.000\.000\/0000\-00") : documentoOut.ToString(@"000\.000\.000\-00")) : "";
                        sheet.Cells[startRow, 5].Value = cliente.TipoCliente;
                        sheet.Cells[startRow, 6].Value = cliente.NomeGrupo;
                        sheet.Cells[startRow, 7].Value = cliente.ClassificacaoGrupo;
                        sheet.Cells[startRow, 8].Value = cliente.DataVigenciaLC;
                        sheet.Cells[startRow, 8].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 9].Value = cliente.Rating;
                        sheet.Cells[startRow, 10].Value = cliente.ValorLC;
                        sheet.Cells[startRow, 10].Style.Numberformat.Format = "#,##0.00";
                        sheet.Cells[startRow, 11].Value = (cliente.Top10.HasValue && cliente.Top10.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 12].Value = cliente.DataConsultaSerasa;
                        sheet.Cells[startRow, 12].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 13].Value = (cliente.RestricaoSerasa.HasValue && cliente.RestricaoSerasa.Value > 0 ? "Sim" : "Não");
                        sheet.Cells[startRow, 14].Value = (cliente.RestricaoYara.HasValue && cliente.RestricaoYara.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 15].Value = (cliente.RestricaoSerasaGrupo.HasValue && cliente.RestricaoSerasaGrupo.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 16].Value = (cliente.RestricaoYaraGrupo.HasValue && cliente.RestricaoYaraGrupo.Value == true ? "Sim" : "Não");
                        sheet.Cells[startRow, 17].Value = cliente.CodigoPropostaLC;
                        sheet.Cells[startRow, 18].Value = cliente.ValorPropostaLC;
                        sheet.Cells[startRow, 18].Style.Numberformat.Format = "#,##0.00";
                        sheet.Cells[startRow, 19].Value = cliente.PropostaLCStatus;
                        sheet.Cells[startRow, 20].Value = cliente.CodigoPropostaAC;
                        sheet.Cells[startRow, 21].Value = cliente.ValorPropostaAC;
                        sheet.Cells[startRow, 21].Style.Numberformat.Format = "#,##0.00";
                        sheet.Cells[startRow, 22].Value = cliente.PropostaACStatus;
                        sheet.Cells[startRow, 23].Value = cliente.DataUltimaCompra;
                        sheet.Cells[startRow, 23].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 24].Value = cliente.DataValidadeGarantia;
                        sheet.Cells[startRow, 24].Style.Numberformat.Format = "dd/MM/yyyy";
                        sheet.Cells[startRow, 25].Value = cliente.Representante;
                        sheet.Cells[startRow, 26].Value = cliente.CTC;
                        sheet.Cells[startRow, 27].Value = cliente.GC;
                        sheet.Cells[startRow, 28].Value = cliente.Diretoria;
                        sheet.Cells[startRow, 29].Value = cliente.Analista;
                        sheet.Cells[startRow, 30].Value = cliente.Apto ? "Apto" : "Com restrição";
                    }

                    // Auto Ajustar Colunas
                    sheet.Cells.AutoFitColumns();

                    var excelFileContent = excel.GetAsByteArray();

                    return excelFileContent;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CancelProposalAsync(Guid propostaRenovacaoVigenciaLCID, Guid usuarioIDAlteracao)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(c => c.ID == propostaRenovacaoVigenciaLCID);

                proposta.PropostaLCStatusID = "XE";
                proposta.UsuarioIDAlteracao = usuarioIDAlteracao;
                proposta.DataAlteracao = DateTime.Now;

                _unitOfWork.PropostaRenovacaoVigenciaLCRepository.Update(proposta);
            }
            catch(Exception e)
            {
                throw e;
            }

            return _unitOfWork.Commit();
        }

        private string ParseGuidListXML(List<Guid> ContaClienteID)
        {
            var builder = new StringBuilder();

            foreach (Guid contaClienteID in ContaClienteID)
            {
                builder.Append($"<guid><value>{contaClienteID}</value></guid>");
            }

            return builder.ToString();
        }
    }
}
