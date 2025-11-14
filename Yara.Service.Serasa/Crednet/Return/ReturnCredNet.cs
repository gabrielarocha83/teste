using System;
using System.IO;
using Yara.Service.Serasa.Common;
using Yara.Service.Serasa.Crednet.Entities;

namespace Yara.Service.Serasa.Crednet.Return
{
    public class ReturnCredNet
    {
        private readonly ReturnCrednet _returnCrednet = new ReturnCrednet();
        private string _tipoPessoa;
        private string _descricao;
        private string _documentoCliente;

        public ReturnCrednet ReturnCrednet(string retorno)
        {

            if (!retorno.Contains("B49C") || !retorno.Contains("T999"))
            {
                throw new SerasaException(string.Format("Retorno do Crednet Inválido: {0}", retorno));
            }

            // Quebra de linhas do arquivo de retorno
            retorno = retorno.Replace("B49C", Environment.NewLine + "B49C");
            retorno = retorno.Replace("N001", Environment.NewLine + "N001");
            retorno = retorno.Replace("N003", Environment.NewLine + "N003");
            retorno = retorno.Replace("N200", Environment.NewLine + "N200");
            retorno = retorno.Replace("N210", Environment.NewLine + "N210");
            retorno = retorno.Replace("N230", Environment.NewLine + "N230");
            retorno = retorno.Replace("N240", Environment.NewLine + "N240");
            retorno = retorno.Replace("N250", Environment.NewLine + "N250");
            retorno = retorno.Replace("N270", Environment.NewLine + "N270");
            retorno = retorno.Replace("N440", Environment.NewLine + "N440");
            retorno = retorno.Replace("N690", Environment.NewLine + "N690");
            retorno = retorno.Replace("N700", Environment.NewLine + "N700");
            retorno = retorno.Replace("N705", Environment.NewLine + "N705");
            retorno = retorno.Replace("T999", Environment.NewLine + "T999");

            var detalhe = new PendenciasFinanceiras();
            var documento = new DocumentosRoubados();

            using (var reader = new StringReader(retorno))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    switch (line.Substring(0, 4))
                    {

                        case "B49C":
                            _tipoPessoa = line.Substring(25, 1);
                            _documentoCliente = line.Substring(25,1) == "F" ? line.Substring(14, 11).Trim() : line.Substring(11, 14).Trim(); // Pega documento do cliente que retorna no cabeçalho do arquivo.
                            break;
                        case "N200": // Cabeçalho do Retorno
                            if (line.Substring(4, 2) == "00")
                            {
                                ConfirmeiSubTipo00(line);
                            }
                            else if (line.Substring(4, 2) == "01")
                            {
                                ConfirmeiSubTipo01(line);
                            }
                            break;
                        case "N210": // Alerta de Documentos Roubados
                            if (documento == null) documento = new DocumentosRoubados();

                            if (line.Substring(4, 2) == "00") // Mensagens de documentos Roubados
                            {
                                documento.NumeroMensagem = Convert.ToInt32(line.Substring(6, 2));
                                documento.TotalMensagem = Convert.ToInt32(line.Substring(8, 2));
                                documento.TipoDocumento = line.Substring(10, 6);
                                documento.NumeroDocumento = line.Substring(16, 20).Trim();
                                documento.Motivo = line.Substring(36, 4);
                                documento.DataOcorrencia = line.Substring(40, 10).Trim() != "" ? line.Substring(40, 10) != "0000000000" ? (DateTime?)Convert.ToDateTime(line.Substring(40, 10).Substring(0, 2) + "/" + line.Substring(40, 10).Substring(2, 2) + "/" + line.Substring(40, 10).Substring(4, 4)) : null : null;
                            }
                            else if (line.Substring(4, 2) == "01")
                            {
                                documento.Ddd1 = Convert.ToInt32(line.Substring(6, 4));
                                documento.Telefone1 = Convert.ToInt64(line.Substring(10, 10));
                                documento.Ddd2 = Convert.ToInt32(line.Substring(20, 4));
                                documento.Telefone2 = Convert.ToInt64(line.Substring(24, 10));
                                documento.Ddd3 = Convert.ToInt32(line.Substring(34, 4));
                                documento.Telefone3 = Convert.ToInt64(line.Substring(38, 10));

                                _returnCrednet.DocumentosRoubados.Add(documento);

                                documento = null;
                            }
                            else if (line.Substring(4, 2) == "99")
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Documentos Roubados, Furtados ou Extraviados",
                                    DescricaoQuantidade = line.Substring(6, 40).Trim()
                                });
                            }
                            break;
                        case "N230": // Pendências Internas
                            if (line.Substring(4, 2) == "00") // Detalhe da Pendencia
                            {
                                PendenciasInternasSubTipo00(line);
                            }
                            else if (line.Substring(4, 2) == "90") // Total de Pendências Internas
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Pendências Internas",
                                    Quantidade = Convert.ToInt32(line.Substring(6, 5)),
                                    Valor = Convert.ToDecimal(line.Substring(23, 15)) / 100,
                                    Periodo = line.Substring(17, 6).Substring(0, 2) + "/" + line.Substring(17, 6).Substring(2, 4)
                                });
                            }
                            else if (line.Substring(4, 2) == "99") // Pendências Internas - Caso não existam registros de consultas
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Pendências Internas",
                                    DescricaoQuantidade = line.Substring(6, 40).Trim()
                                });
                            }
                            break;
                        case "N240": // Pendências Financeiras
                            if (detalhe == null) detalhe = new PendenciasFinanceiras();

                            if (line.Substring(4, 2) == "00") // Detalhe das Pendências Financeiras separadas em Pefin, Refin e Dívidas vencidas
                            {
                                detalhe.DataOcorrencia = line.Substring(6, 8).Trim() != "" ? line.Substring(6, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(6, 8).Substring(0, 2) + "/" + line.Substring(6, 8).Substring(2, 2) + "/" + line.Substring(6, 8).Substring(4, 4)) : null : null;
                                detalhe.Modalidade = line.Substring(14, 30).Trim();
                                detalhe.Avalista = line.Substring(44, 1);
                                detalhe.TipoMoeda = line.Substring(45, 3);
                                detalhe.Valor = Convert.ToDecimal(line.Substring(48, 15)) / 100;
                                detalhe.Contrato = line.Substring(63, 16).Trim();
                                detalhe.Origem = line.Substring(79, 30).Trim();
                                detalhe.Sigla = line.Substring(109, 4).Trim();
                            }
                            else if (line.Substring(4, 2) == "01") // Detalhe de Pendências Financeiras – complemento
                            {
                                detalhe.SubJudice = line.Substring(6, 1);
                                detalhe.MensagemSubJudice = line.Substring(7, 76).Trim();
                                detalhe.TipoAnotacao = line.Substring(83, 1);
                                detalhe.CodigoCadus = line.Substring(84, 10).Trim();
                                detalhe.DataInclusao = line.Substring(94, 8).Trim() != "" ? line.Substring(94, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(94, 8).Substring(0, 2) + "/" + line.Substring(94, 8).Substring(2, 2) + "/" + line.Substring(94, 8).Substring(4, 4)) : null : null;
                                detalhe.DataDisponivel = line.Substring(94, 8).Trim() != "" ? line.Substring(102, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(102, 8).Substring(0, 2) + "/" + line.Substring(102, 8).Substring(2, 2) + "/" + line.Substring(102, 8).Substring(4, 4)) : null : null;
                                detalhe.SerieCadus = line.Substring(110, 1);

                                _returnCrednet.PendenciasFinanceiras.Add(detalhe);

                                detalhe = null;
                            }
                            else if (line.Substring(4, 2) == "90") // Total de Pendências Financeiras para cada tipo de anotação ("V" = Pefin / "I" = Refin / "5" = Dívida Vencida)
                            {
                                var tipo = line.Substring(38, 1);

                                switch (tipo)
                                {
                                    case "V":
                                        _descricao = "Pendências Financeiras Pefin";
                                        break;
                                    case "I":
                                        _descricao = "Pendências Financeiras Refin";
                                        break;
                                    default:
                                        _descricao = "Pendências Financeiras Dívida Vencida";
                                        break;
                                }

                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = _descricao,
                                    Quantidade = Convert.ToInt32(line.Substring(6, 5)),
                                    Valor = Convert.ToDecimal(line.Substring(23, 15)) / 100,
                                    Periodo = line.Substring(17, 6).Substring(0,2) + "/" + line.Substring(17, 6).Substring(2, 4)
                                });
                            }
                            else if (line.Substring(4, 2) == "99") // Pendências Financeiras - Caso não existam registros de consultas
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Pendências Financeiras",
                                    DescricaoQuantidade = line.Substring(6, 40).Trim()
                                });
                            }
                            break;
                        case "N250": //Protestos do Estado
                            if (line.Substring(4, 2) == "00") //Detalhe dos Protestos do Estado
                            {
                                ProtestoEstadualSubTipo00(line);
                            }
                            else if (line.Substring(4, 2) == "90") //Total de Protestos do Estado
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Protesto Estadual",
                                    Quantidade = Convert.ToInt32(line.Substring(6, 5)),
                                    Valor = Convert.ToDecimal(line.Substring(26, 15)) / 100,
                                    Periodo = line.Substring(17, 6).Substring(0, 2) + "/" + line.Substring(17, 6).Substring(2, 4)
                                });
                            }
                            else if (line.Substring(4, 2) == "99") //Protestos do Estado - Caso não existam registros de consultas
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Protesto Estadual",
                                    DescricaoQuantidade = line.Substring(6, 40).Trim()
                                });
                            }
                            break;
                        case "N270": //Cheques sem fundos - Bacen
                            if (line.Substring(4, 2) == "00") //Detalhe de Cheques sem fundos Bacen
                            {
                                ChequeBacenSubTipo00(line);
                            }
                            else if (line.Substring(4, 2) == "90") //Total de Cheques sem fundos Bacen
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Cheques Sem Fundo BACEN",
                                    Quantidade = Convert.ToInt32(line.Substring(6, 5)),
                                    Valor = Convert.ToDecimal(line.Substring(26, 15)) / 100,
                                    Periodo = line.Substring(17, 6).Substring(0, 2) + "/" + line.Substring(17, 6).Substring(2, 4)
                                });
                            }
                            else if (line.Substring(4, 2) == "99") //Cheques sem fundos Bacen - Caso não existam registros de consultas
                            {
                                _returnCrednet.CrednetResumos.Add(new CrednetResumo()
                                {
                                    Discriminacao = "Cheques Sem Fundo BACEN",
                                    DescricaoQuantidade = line.Substring(6, 40).Trim()
                                });
                            }
                            break;
                    }
                }
            }
            return _returnCrednet;
        }

        private void ChequeBacenSubTipo00(string line)
        {
            var bacen = new ChequeBacen();
            bacen.DataOcorrencia = line.Substring(6, 8).Trim() != "" ? line.Substring(6, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(6, 8).Substring(0, 2) + "/" + line.Substring(6, 8).Substring(2, 2) + "/" + line.Substring(6, 8).Substring(4, 4)) : null : null;
            bacen.NumeroCheque = line.Substring(14, 10).Trim();
            bacen.Alinea = line.Substring(24, 5).Trim();
            bacen.QuantidadeCheque = line.Substring(29, 5).Trim();
            bacen.Valor = Convert.ToDecimal(line.Substring(34, 15)) / 100;
            bacen.NumeroBanco = line.Substring(49, 3);
            bacen.NomeBanco = line.Substring(52, 14).Trim();
            bacen.Agencia = line.Substring(66, 4);
            bacen.Cidade = line.Substring(70, 30).Trim();
            bacen.Uf = line.Substring(100, 2);

            _returnCrednet.ChequeBacen.Add(bacen);
        }

        private void ProtestoEstadualSubTipo00(string line)
        {
            var estadual = new ProtestoEstadual();
            estadual.DataOcorrencia = line.Substring(6, 8).Trim() != "" ? line.Substring(6, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(6, 8).Substring(0, 2) + "/" + line.Substring(6, 8).Substring(2, 2) + "/" + line.Substring(6, 8).Substring(4, 4)) : null : null;
            estadual.TipoMoeda = line.Substring(14, 3);
            estadual.Valor = Convert.ToDecimal(line.Substring(17, 15)) / 100;
            estadual.Cartorio = line.Substring(32, 2);
            estadual.Origem = line.Substring(34, 30).Trim();
            estadual.Sigla = line.Substring(64, 2);

            _returnCrednet.ProtestoEstaduais.Add(estadual);
        }

        private void PendenciasInternasSubTipo00(string line)
        {
            var detalhe = new PendenciasInternasDetalhe();
            detalhe.DataOcorrencia = line.Substring(6, 8).Trim() != "" ? line.Substring(6, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(6, 8).Substring(0, 2) + "/" + line.Substring(6, 8).Substring(2, 2) + "/" + line.Substring(6, 8).Substring(4, 4)) : null : null;
            detalhe.Modalidade = line.Substring(14, 30).Trim();
            detalhe.Avalista = line.Substring(44, 1);
            detalhe.TipoMoeda = line.Substring(45, 3);
            detalhe.Valor = Convert.ToDecimal(line.Substring(48, 15)) / 100;
            detalhe.Contrato = line.Substring(63, 16).Trim();
            detalhe.Origem = line.Substring(79, 30).Trim();
            detalhe.Sigla = line.Substring(109, 4).Trim();

            _returnCrednet.PendenciasInternasDetalhes.Add(detalhe);
        }

        private void ConfirmeiSubTipo00(string line)
        {
            _returnCrednet.Nome = line.Substring(6, 70).Trim();
            _returnCrednet.Data = line.Substring(76, 8).Trim() != "" ? line.Substring(76, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(76, 8).Substring(0, 2) + "/" + line.Substring(76, 8).Substring(2, 2) + "/" + line.Substring(76, 8).Substring(4, 4)) : null : null;
            try
            {
                _returnCrednet.Situacao = _tipoPessoa == "F" ? SituacaoCadastral.CPF[line.Substring(84, 2).Trim()] : SituacaoCadastral.CNPJ[line.Substring(84, 2).Trim()];
            }
            catch (Exception) { }
            _returnCrednet.DataSituacao = line.Substring(86, 8).Trim() != "" ? line.Substring(86, 8) != "00000000" ? (DateTime?)Convert.ToDateTime(line.Substring(86, 8).Substring(0, 2) + "/" + line.Substring(86, 8).Substring(2, 2) + "/" + line.Substring(86, 8).Substring(4, 4)) : null : null;
            _returnCrednet.DataCriacao = DateTime.Now;
            _returnCrednet.DocumentoCliente = _documentoCliente.ToString();
        }

        private void ConfirmeiSubTipo01(string line)
        {
            _returnCrednet.NomeMae = line.Substring(6, 40).Trim();
        }
    }
}
