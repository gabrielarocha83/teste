using System;
using System.Collections.Generic;
using System.Globalization;
using Yara.Service.Serasa.Interface;
using Yara.Service.Serasa.Relato.Entities;

namespace Yara.Service.Serasa.Relato.Return
{
    public class ReturnRelato : ISerializar<Entities.ReturnRelato>
    {
        private readonly Entities.ReturnRelato _relato = new Entities.ReturnRelato();
        private Participacao _participacao = new Participacao();

        public Entities.ReturnRelato Serasa(string retorno)
        {
            if (!retorno.Contains("#L010000") || !retorno.Contains("#L010102"))
            {
                throw new SerasaException(string.Format("Retorno do Relato Inválido: {0}", retorno));
            }

            retorno = retorno.Replace("#", Environment.NewLine + "#");

            try
            {
                using (var reader = new System.IO.StringReader(retorno))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        if (line.Substring(0, 2) != "#L") continue;

                        if (line.Substring(0, 8) == "#L010000")
                            DadosInfo(line);

                        if (line.Substring(0, 8) == "#L010101")
                            Contabilizacao(line);

                        if (line.Substring(0, 8) == "#L010102")
                            Identificacao(line);

                        if (line.Substring(0, 8) == "#L010103")
                            Endereco(line);

                        if (line.Substring(0, 8) == "#L010104")
                            Localizacao(line);

                        if (line.Substring(0, 8) == "#L010105")
                            Atividade(line);

                        if (line.Substring(0, 8) == "#L010106")
                            Filiais(line);

                        if (line.Substring(0, 8) == "#L010108")
                            ControleSocietario(line);

                        if (line.Substring(0, 8) == "#L010109")
                            ControleSocietarioDetalhes(line);

                        if (line.Substring(0, 8) == "#L010110")
                            QuadroAdministrativo(line);

                        if (line.Substring(0, 8) == "#L010111")
                            QuadroAdministrativoDetalhes(line);

                        if (line.Substring(0, 8) == "#L010112")
                            Participacoes(line);

                        if (line.Substring(0, 8) == "#L010113")
                            Participada(line);

                        if (line.Substring(0, 8) == "#L010114")
                            ParticipacoesDetalhes(line);

                        if (line.Substring(0, 8) == "#L010116")
                            Antecessora(line);

                        if (line.Substring(0, 8) == "#L020102")
                            FontesPagamentosHistorico(line);

                        if (line.Substring(0, 8) == "#L020108")
                            PagamentosHistorico(line);

                        if (line.Substring(0, 8) == "#L040202")
                            ConcentreResumo(line);

                        if (line.Substring(0, 8) == "#L040101")
                            PendenciasFinanceirasPefin(line);

                        if (line.Substring(0, 8) == "#L040102")
                            PendenciasFinanceirasRefin(line);

                        if (line.Substring(0, 8) == "#L040201")
                            ConcentreGrafias(line);

                        if (line.Substring(0, 8) == "#L040301")
                            Protestos(line);

                        if (line.Substring(0, 8) == "#L040401")
                            AcaoJudicial(line);

                        if (line.Substring(0, 8) == "#L040601")
                            FalenciaConcordata(line);

                        if (line.Substring(0, 8) == "#L040701")
                            DividaVencida(line);

                        if (line.Substring(0, 8) == "#L040801")
                            AcheiCheque(line);

                        if (line.Substring(0, 8) == "#L040901")
                            CCFCheque(line);

                        if (line.Substring(0, 8) == "#L040199")
                            MensagemPendenciaFinanceira(line);

                        if (line.Substring(0, 8) == "#L040299")
                            MensagemConcentre(line);

                        if (line.Substring(0, 8) == "#L041099")
                            MensagemReCheque(line);

                        if (line.Substring(0, 8) == "#L041101")
                            ParticipantesAnotacoes(line);
                    }

                    if (!_relato.Participacao.Contains(_participacao))
                        _relato.Participacao.Add(_participacao);
                }
            }
            catch (Exception)
            {
                throw new SerasaException(string.Format("Retorno do Relato Inválido: {0}", retorno));
            }

            return _relato;
        }

        #region Dados Cadastrais

        public void DadosInfo(string linha)
        {
            linha = linha.Substring(8);
            var descricaosituacao = linha.Substring(2, 79);
            var codigocnpj = linha.Substring(81, 9);
            var indficha = linha.Substring(90, 1);
            var cont = linha.Substring(91, 4);
            var reservada = linha.Substring(95, 11);
            var cont02 = linha.Substring(106, 4);
            var cont03 = linha.Substring(110, 4);
            var cont04 = linha.Substring(114, 4);
            var cont05 = linha.Substring(118, 4);
            var cont06 = linha.Substring(122, 4);
            var cont07 = linha.Substring(126, 4);
            var cont08 = linha.Substring(130, 4);
            var cont09 = linha.Substring(134, 4);
            var cont10 = linha.Substring(138, 4);
            var tiporelato = linha.Substring(142, 1);
            var temrecipr = linha.Substring(143, 1);
            var tiprelcob = linha.Substring(144, 1);
            var diasrest = linha.Substring(145, 2);
            // var dssitunov = linha.Substring(148, 80);
            var dssitunov = linha.Substring(147, 80);
            _relato.Situacao = dssitunov.Trim();
        }

        public void Identificacao(string linha)
        {
            linha = linha.Substring(8);
            var razaosocial = linha.Substring(0, 70);
            var cnpj = linha.Substring(70, 9);
            var nomefantasia = linha.Substring(79, 60);
            var nire = linha.Substring(139, 11);
            var tiposociedade = linha.Substring(150, 60);
            var opcaotributaria = linha.Substring(210, 30);

            _relato.NomeFantasia = nomefantasia.Trim();
            _relato.RazaoSocial = razaosocial.Trim();
            //_relato.CNPJ = cnpj.Trim();
            _relato.NIRE = nire.Trim();
            _relato.TipoSociedade = tiposociedade.Trim();
            _relato.OpcaoTributaria = opcaotributaria.Trim();
        }

        public void Endereco(string linha)
        {
            linha = linha.Substring(8);
            var endcompleto = linha.Substring(0, 70);
            var bairro = linha.Substring(70, 30);
            var end = linha.Substring(100, 80);

            _relato.Endereco = end.Trim();
            _relato.Bairro = bairro.Trim();
        }

        public void Localizacao(string linha)
        {
            linha = linha.Substring(8);
            var cidade = linha.Substring(0, 30);
            var uf = linha.Substring(30, 2);
            var cep = linha.Substring(32, 9);
            var ddd = linha.Substring(41, 4);
            var tel1 = linha.Substring(45, 9);
            var fax1 = linha.Substring(54, 9);
            var area = linha.Substring(63, 4);
            var site = linha.Substring(67, 70);

            _relato.Cidade = cidade.Trim();
            _relato.CEP = cep.Trim();
            _relato.UF = uf.Trim();
            _relato.Telefone = $"{ddd.Trim()} {tel1.Trim()}";
            _relato.Fax = $"{fax1.Trim()}";
            _relato.Site = site.Trim();
        }

        public void Atividade(string linha)
        {
            linha = linha.Substring(8);
            var datafundacao = linha.Substring(0, 8);
            var datacnpj = linha.Substring(8, 8);
            var ramo = linha.Substring(16, 54);
            var codeserasa = linha.Substring(70, 7);
            var totalempregados = linha.Substring(77, 5);
            var percentualcompras = linha.Substring(82, 3);
            var percentualvendas = linha.Substring(85, 3);
            var numerofiliais = linha.Substring(88, 3);
            var qtdfiliais = linha.Substring(91, 6);
            var cnae = linha.Substring(97, 7);
            var datamanutencao = linha.Substring(104, 8);

            _relato.DataRegistro = DataFormatada(datacnpj);
            _relato.QuantidadeFiliais = qtdfiliais.Trim();
            _relato.QuantidadeFuncionarios = totalempregados;
            _relato.Fundacao = DataFormatada(datafundacao.Trim());
            _relato.Ramo = ramo.Trim();
            _relato.CodSerasa = codeserasa.Trim();
            _relato.CNAE = cnae.Trim();
        }

        public void Filiais(string linha)
        {
            linha = linha.Substring(8);
            var nome = linha.Substring(0, 30);

            _relato.Filiais = string.IsNullOrEmpty(_relato.Filiais) ? nome.Trim() : $"{_relato.Filiais},{nome.Trim()}";
        }

        public void Contabilizacao(string linha)
        {
            linha = linha.Substring(8);
            var cicuser = linha.Substring(0, 60);
            var dataemissao = linha.Substring(60, 8);
            var horaemissao = linha.Substring(68, 8);
            var reservado = linha.Substring(76, 2);
            var cnpj = linha.Substring(78, 24);
            var atualizacao = linha.Substring(102, 8);
            var origem = linha.Substring(110, 1);
            var registro = linha.Substring(111, 11);
            var dataregistro = linha.Substring(122, 8);

            _relato.CNPJ = cnpj.Replace("CNPJ: ", "");
            _relato.DataCriacao = DataFormatada(dataemissao);
            _relato.HoraCriacao = horaemissao;
            _relato.DataRegistro = DataFormatada(dataregistro);
            _relato.Registro = registro;
        }

        public void Antecessora(string linha)
        {
            linha = linha.Substring(8);
            var razaosocial = linha.Substring(0, 70);
            var reservado = linha.Substring(70, 1);
            var dataemissao = linha.Substring(71, 8);

            _relato.Antecessora = razaosocial.Trim();
            _relato.DataFimAntecessora = DataFormatada(dataemissao);
        }

        #endregion

        #region Sócios

        public void ControleSocietario(string linha)
        {
            linha = linha.Substring(8);
            var data = linha.Substring(0, 8);
            var valorcapitalsocial = linha.Substring(8, 13);
            var valorcapitalrealizado = linha.Substring(21, 13);
            var valorcapitalautorizado = linha.Substring(34, 13);
            var descricaonacionalidade = linha.Substring(47, 12);
            var descricaoorigem = linha.Substring(59, 12);
            var descricaonatureza = linha.Substring(71, 12);
            var controlesocietario = linha.Substring(83, 1) == " " ? "NÃO" : "SIM";

            _relato.DataControleSocietario = DataFormatada(data);
            _relato.ControleSocietario.CapitalSocial = decimal.Parse(valorcapitalsocial, NumberStyles.Currency);
            _relato.ControleSocietario.Realizado = decimal.Parse(valorcapitalrealizado, NumberStyles.Currency);
            _relato.ControleSocietario.Natureza = descricaonatureza.Trim();
            _relato.ControleSocietario.Controle = descricaoorigem.Trim();
            _relato.ControleSocietario.Origem = descricaonacionalidade.Trim();
        }

        public void ControleSocietarioDetalhes(string linha)
        {
            linha = linha.Substring(8);
            var pessoa = linha.Substring(0, 1);
            var documento = linha.Substring(1, 9);
            var sequencia = linha.Substring(10, 4);
            var digcpf = linha.Substring(14, 2);
            var nomesocio = linha.Substring(16, 65);
            var pais = linha.Substring(81, 12);
            var percentual = linha.Substring(93, 4);
            var dataentrada = linha.Substring(97, 8);
            var restricaosocio = linha.Substring(105, 1);
            var percentualvotante = linha.Substring(106, 4);
            var situacaoempresa = linha.Substring(110, 2);
            var codserasa = linha.Substring(112, 7);

            if (pessoa.Equals("F"))
                documento = documento + digcpf;
            else if (documento != "099999999" && !String.IsNullOrEmpty(documento.Trim()) && !String.IsNullOrEmpty(digcpf.Trim()))
                documento = (documento.StartsWith("000") ? "00" : (documento.StartsWith("00") ? "0" : "")) + int.Parse(documento).ToString() + "0001" + digcpf.Trim();
            else
                documento = String.Empty;

            _relato.Socios.Add(new Socios()
            {
                Documento = documento.Trim(),
                Entrada = DataFormatada(dataentrada),
                Nacionalidade = pais.Trim(),
                Votante = percentualvotante == "000" ? 0 : float.Parse(percentualvotante) / 10,
                Total = percentual == "000" ? 0 : float.Parse(percentual) / 10,
                SocioAcionista = nomesocio.Trim()
            });
        }

        #endregion

        #region Participantes

        public void Participacoes(string linha)
        {
            linha = linha.Substring(8);
            var atualizacao = linha.Substring(0, 8);

            _relato.AtualizacaoParticipacoes = DataFormatada(atualizacao);
        }

        public void Participada(string linha)
        {
            linha = linha.Substring(8);
            var cnpj = linha.Substring(0, 9);
            var digito = linha.Substring(9, 2);
            var empresa = linha.Substring(11, 67);
            var restricao = linha.Substring(78, 1);
            var sequencia = linha.Substring(79, 4);
            var identificacao = linha.Substring(83, 1);
            var documento = cnpj;

            if (identificacao.Equals("F"))
                documento = cnpj + digito;
            else if (documento != "099999999" && !String.IsNullOrEmpty(documento.Trim()) && !String.IsNullOrEmpty(digito.Trim()))
                documento = (documento.StartsWith("000") ? "00" : (documento.StartsWith("00") ? "0" : "")) + int.Parse(cnpj).ToString() + "0001" + digito.Trim();
            else
                documento = String.Empty;

            if (_participacao.CNPJ != null)
            {
                if (_participacao.CNPJ == documento)
                    return;
                else
                    _relato.Participacao.Add(_participacao);
            }

            _participacao = new Participacao
            {
                Nome = empresa.Trim(),
                CNPJ = documento
            };
        }

        public void ParticipacoesDetalhes(string linha)
        {
            linha = linha.Substring(8);
            var nome = linha.Substring(0, 67);
            var vinculo = linha.Substring(67, 9);
            var codigo = linha.Substring(76, 4);
            var municipio = linha.Substring(80, 30);
            var uf = linha.Substring(110, 2);
            var percentual = linha.Substring(112, 5);
            var restricao = linha.Substring(117, 1);
            var documento = linha.Substring(118, 9);
            var documentosequencia = linha.Substring(127, 4);
            var documentodigito = linha.Substring(131, 2);
            var identificacao = linha.Substring(133, 1);

            if (identificacao.Equals("F"))
                documento = documento + documentodigito;
            else if (documento != "099999999" && !String.IsNullOrEmpty(documento.Trim()) && !String.IsNullOrEmpty(documentodigito.Trim()))
                documento = (documento.StartsWith("000") ? "00" : (documento.StartsWith("00") ? "0" : "")) + int.Parse(documento).ToString() + "0001" + documentodigito.Trim();
            else
                documento = String.Empty;

            _participacao.Membros.Add(new Membro()
            {
                Nome = nome.Trim(),
                Capital = float.Parse(percentual) / 100,
                Documento = documento.Trim(),
                Vinculo = vinculo.Trim()
            });

            _participacao.Cidade = municipio.Trim();
            _participacao.UF = uf.Trim();
        }

        #endregion

        #region Grafias

        public void ConcentreGrafias(string linha)
        {
            linha = linha.Substring(8);
            var grafia = linha.Substring(0, 70);

            _relato.ConcentreGrafias.Add(new Grafia()
            {
                Descricao = grafia.Trim()
            });
        }

        #endregion

        #region Administrativo

        public void QuadroAdministrativo(string linha)
        {
            linha = linha.Substring(8);
            var atualizacao = linha.Substring(0, 8);

            _relato.AtualizacaoAdministracao = DataFormatada(atualizacao);
        }

        public void QuadroAdministrativoDetalhes(string linha)
        {
            linha = linha.Substring(8);
            var pessoa = linha.Substring(0, 1);
            var cnpj = linha.Substring(1, 9);
            var sequencia = linha.Substring(10, 4);
            var digcpf = linha.Substring(14, 2);
            var nome = linha.Substring(16, 58);
            var cargo = linha.Substring(74, 12);
            var pais = linha.Substring(86, 12);
            var estadocivil = linha.Substring(98, 9);
            var datainicio = linha.Substring(107, 8);
            var datafim = linha.Substring(115, 8);
            var restricao = linha.Substring(123, 1);
            var codcargo = linha.Substring(124, 3);
            var situacao = linha.Substring(127, 2);
            var dataentrada = linha.Substring(129, 8);
            var dtInicio = DataFormatada(datainicio);
            var dtFim = DataFormatada(datafim);

            var documento = cnpj;
            if (pessoa.Equals("F"))
                documento = cnpj + digcpf;
            else if (documento != "099999999" && !String.IsNullOrEmpty(documento.Trim()) && !String.IsNullOrEmpty(digcpf.Trim()))
                documento = (documento.StartsWith("000") ? "00" : (documento.StartsWith("00") ? "0" : "")) + int.Parse(cnpj).ToString() + "0001" + digcpf.Trim();
            else
                documento = String.Empty;

            var mandato = String.Empty;
            if (dtInicio == null || dtInicio == DateTime.MinValue)
                mandato = "IDETERMINADO";
            else
                mandato = dtInicio.Value.ToShortDateString().Substring(3, 7);

            if (dtFim == null || dtFim == DateTime.MinValue)
                mandato += "- INDETERMINADO";
            else
                mandato += " - " + dtFim.Value.ToShortDateString().Substring(3, 7);

            _relato.Administracao.Add(new Administracao()
            {
                Nome = nome.Trim(),
                Cargo = cargo.Trim(),
                Documento = documento.Trim(),
                Nacionalidade = pais.Trim(), 
                EstadoCivil = estadocivil.Trim(),
                Mandato = mandato,
                Entrada = DataFormatada(dataentrada)
            });
        }

        #endregion

        #region Historico Pagamentos

        public void FontesPagamentosHistorico(string linha)
        {
            linha = linha.Substring(8);
            var restrito = linha.Substring(0, 23);
            var qtdefontes = linha.Substring(23, 4);

            if (_relato.HistoricoPagamento != null)
                _relato.HistoricoPagamento.Fontes = int.Parse(qtdefontes);
        }

        public void PagamentosHistorico(string linha)
        {
            linha = linha.Substring(8);
            var descricao = linha.Substring(0, 14);
            var qtde = linha.Substring(14, 6);
            var percentual = linha.Substring(20, 4);

            if (_relato.HistoricoPagamento == null)
            {
                _relato.HistoricoPagamento = new Historico() {
                    Titulos = new List<Titulos>() {
                        new Titulos() {
                            Quantidade = int.Parse(qtde),
                            Descricao = descricao.Trim(),
                            Percentual = int.Parse(percentual)
                        }
                    }
                };
            }
            else
            {
                _relato.HistoricoPagamento.Titulos.Add(new Titulos() {
                    Quantidade = int.Parse(qtde),
                    Descricao = descricao,
                    Percentual = int.Parse(percentual)
                });
            }
        }

        #endregion

        #region Pendências Serasa

        public void PendenciasFinanceirasPefin(string linha)
        {
            linha = linha.Substring(8);
            var qtdependencias = linha.Substring(0, 9);
            var qtdeultimaocorrencia = linha.Substring(9, 9);
            var dataocorrencia = linha.Substring(18, 8);
            var tituloocorrencia = linha.Substring(26, 12);
            var avalista = linha.Substring(38, 1);
            var valor = linha.Substring(39, 13);
            var contrato = linha.Substring(52, 16);
            var origem = linha.Substring(68, 20);
            var filial = linha.Substring(88, 4);
            var msg = linha.Substring(92, 32);
            var praca = linha.Substring(124, 4);
            var distrito = linha.Substring(128, 2);
            var vara = linha.Substring(130, 2);
            var data = linha.Substring(132, 8);
            var processo = linha.Substring(140, 16);
            var codigonatureza = linha.Substring(156, 3);
            var reservado = linha.Substring(159, 24);
            var mensage = linha.Substring(183, 44);
            // var total = linha.Substring(228, 13);
            var total = linha.Substring(227, 13);

            if (_relato.Pefin != null)
            {
                _relato.Pefin.PendenciaPefin.Add(new PendenciaFinanceira()
                {
                    Valor = decimal.Parse(valor),
                    Mensagem = msg.Trim(),
                    Avalista = avalista.Trim(),
                    Origem = origem.Trim(),
                    Contrato = contrato.Trim(),
                    Filial = filial.Trim(),
                    Quantidade = int.Parse(qtdependencias),
                    Quantidadeultimaocorrencia = int.Parse(qtdeultimaocorrencia),
                    Titulo = tituloocorrencia.Trim(),
                    DataUltimaOcorrencia = DataFormatada(dataocorrencia)
                });
            }
            else
            {
                _relato.Pefin = new Pefin()
                {
                    Total = decimal.Parse(total),
                    Quantidade = int.Parse(qtdependencias),
                };

                _relato.Pefin.PendenciaPefin.Add(new PendenciaFinanceira()
                {
                    Valor = decimal.Parse(valor),
                    Mensagem = msg.Trim(),
                    Avalista = avalista.Trim(),
                    Origem = origem.Trim(),
                    Contrato = contrato.Trim(),
                    Filial = filial.Trim(),
                    Quantidade = int.Parse(qtdependencias),
                    Quantidadeultimaocorrencia = int.Parse(qtdeultimaocorrencia),
                    Titulo = tituloocorrencia.Trim(),
                    DataUltimaOcorrencia = DataFormatada(dataocorrencia)
                });
            }
        }

        public void PendenciasFinanceirasRefin(string linha)
        {
            linha = linha.Substring(8);
            var qtdependencias = linha.Substring(0, 9);
            var qtdeultimaocorrencia = linha.Substring(9, 9);
            var dataocorrencia = linha.Substring(18, 8);
            var tituloocorrencia = linha.Substring(26, 12);
            var avalista = linha.Substring(38, 1);
            var valor = linha.Substring(39, 13);
            var contrato = linha.Substring(52, 16);
            var origem = linha.Substring(68, 20);
            var filial = linha.Substring(88, 4);
            var msg = linha.Substring(92, 32);
            var praca = linha.Substring(124, 4);
            var distrito = linha.Substring(128, 2);
            var vara = linha.Substring(130, 2);
            var data = linha.Substring(132, 8);
            var processo = linha.Substring(140, 16);
            var codigonatureza = linha.Substring(156, 3);
            var reservado = linha.Substring(159, 24);
            var mensage = linha.Substring(183, 44);
            // var total = linha.Substring(228, 13);
            var total = linha.Substring(227, 13);

            if (_relato.Refin != null)
            {
                _relato.Refin.PendenciaRefin.Add(new PendenciaFinanceira()
                {
                    Valor = decimal.Parse(valor),
                    Quantidade = int.Parse(dataocorrencia),
                    Mensagem = msg.Trim(),
                    Avalista = avalista.Trim(),
                    Origem = origem.Trim(),
                    Contrato = contrato.Trim(),
                    Filial = filial.Trim(),
                    Quantidadeultimaocorrencia = int.Parse(qtdeultimaocorrencia),
                    Titulo = tituloocorrencia.Trim(),
                    DataUltimaOcorrencia = DataFormatada(dataocorrencia)
                });
            }
            else
            {
                _relato.Refin = new Refin()
                {
                    Total = decimal.Parse(total),
                    Quantidade = int.Parse(qtdependencias),
                };

                _relato.Refin.PendenciaRefin.Add(new PendenciaFinanceira()
                {
                    Valor = decimal.Parse(valor),
                    Quantidade = int.Parse(dataocorrencia),
                    Mensagem = msg.Trim(),
                    Avalista = avalista.Trim(),
                    Origem = origem.Trim(),
                    Contrato = contrato.Trim(),
                    Filial = filial.Trim(),
                    Quantidadeultimaocorrencia = int.Parse(qtdeultimaocorrencia),
                    Titulo = tituloocorrencia.Trim(),
                    DataUltimaOcorrencia = DataFormatada(dataocorrencia)
                });
            }
        }

        public void ConcentreResumo(string linha)
        {
            /*
            var natureza = new Dictionary<string, string>
            {
                {"01","pend.financeira"},
                {"03","protesto"},
                {"04","ação judicial"},
                {"05","partic.falencia"},
                {"06","falencia/concordata"},
                {"07","divida vencida"},
                {"09","cheque sem fundo achei"},
                {"10","refin"},
            };
            */

            linha = linha.Substring(8);
            var qtdeocorrencias = linha.Substring(0, 9);
            var grupo = linha.Substring(9, 27);
            var mesiniciodescricao = linha.Substring(36, 3);
            var mesinicio = linha.Substring(39, 2);
            var anoinicio = linha.Substring(41, 2);
            var mesfinaldescricao = linha.Substring(43, 3);
            var mesfinal = linha.Substring(46, 2);
            var anofinal = linha.Substring(48, 2);
            var moeda = linha.Substring(50, 3);
            var valor = linha.Substring(53, 13);
            var origem = linha.Substring(66, 20);
            var agencia = linha.Substring(86, 4);
            var total = linha.Substring(90, 13);
            var codigonatureza = linha.Substring(103, 3);

            _relato.ConcentreResumos.Add(new ConcentreResumo()
            {
                Valor = decimal.Parse(valor),
                Quantidade = int.Parse(qtdeocorrencias),
                Origem = origem.Trim(),
                Discriminacao = grupo.Trim(),
                Periodo = $"{mesiniciodescricao}/{anoinicio} - {mesfinaldescricao}/{anofinal}",
                Praca = agencia,
                Total = decimal.Parse(total)
            });
        }

        public void Protestos(string linha)
        {
            if (linha.Length < 99)
                return;

            linha = linha.Substring(8);
            var qtdeocorrencias = linha.Substring(0, 9);
            var dataprotesto = linha.Substring(9, 8);
            var moeda = linha.Substring(17, 3);
            var valor = linha.Substring(20, 13);
            var cartorio = linha.Substring(33, 2);
            var cidade = linha.Substring(35, 30);
            var uf = linha.Substring(65, 2);
            var msg = linha.Substring(67, 32);

            _relato.ProtestosConcentre.Add(new ProtestoConcentre()
            {
                Quantidade = int.Parse(qtdeocorrencias),
                Moeda = moeda.Trim(),
                Mensagem = msg.Trim(),
                Cidade = cidade.Trim(),
                Valor = decimal.Parse(valor),
                UF = uf.Trim(),
                Cartorio = cartorio.Trim(),
                Data = DataFormatada(dataprotesto),
                Total = _relato.ConcentreResumos.Find(c => c.Discriminacao.Contains("PROTESTO")).Total
            });
        }

        public void AcaoJudicial(string linha)
        {
            linha = linha.Substring(8);
            var qtdeocorrencias = linha.Substring(0, 9);
            var dataacao = linha.Substring(9, 8);
            var natureza = linha.Substring(17, 20);
            var avalista = linha.Substring(37, 1);
            var moeda = linha.Substring(38, 3);
            var valor = linha.Substring(41, 13);
            var distrito = linha.Substring(54, 2);
            var vara = linha.Substring(56, 4);
            var cidade = linha.Substring(60, 30);
            var uf = linha.Substring(90, 2);
            var msg = linha.Substring(92, 32);

            _relato.AcoesJudiciais.Add(new AcaoJudicial()
            {
                Cidade = cidade.Trim(),
                Valor = decimal.Parse(valor),
                UF = uf.Trim(),
                Quantidade = int.Parse(qtdeocorrencias),
                Data = DataFormatada(dataacao),
                Avalista = avalista.Trim(),
                Mensagem = msg.Trim(),
                Moeda = moeda.Trim(),
                Natureza = natureza.Trim(),
                Distrito = distrito.Trim(),
                Vara = vara.Trim(),
                Total = _relato.ConcentreResumos.Find(c => c.Discriminacao.Contains("ACAO JUDICIAL")).Total
            });
        }

        public void MensagemPendenciaFinanceira(string linha)
        {
            linha = linha.Substring(8);
            var mensagem = linha.Substring(0, 79);

            _relato.PendenciasFinanceiras = mensagem.Trim();
        }

        public void MensagemConcentre(string linha)
        {
            linha = linha.Substring(8);
            var mensagem = linha.Substring(0, 79);

            _relato.Concentre = mensagem.Trim();
        }

        public void MensagemReCheque(string linha)
        {
            linha = linha.Substring(8);
            var mensagem = linha.Substring(0, 79);

            _relato.ReCheque = mensagem.Trim();
        }

        public void ParticipacaoFalencia(string linha)
        {
            linha = linha.Substring(8);
            var quantidade = linha.Substring(0, 9);
            var data = linha.Substring(9, 8);
            var tipo = linha.Substring(17, 20);
            var cnpj = linha.Substring(37, 9);
            var empresa = linha.Substring(46, 69);
            var codnatureza = linha.Substring(115, 3);

            _relato.ParticipacoesFalencia.Add(new ParticipacaoFalencia()
            {
                Tipo = tipo.Trim(),
                Quantidade = int.Parse(quantidade),
                Empresa = empresa.Trim(),
                CNPJ = cnpj.Trim(),
                Data = data.Trim(),
                Natureza = codnatureza.Trim()
            });
        }

        public void FalenciaConcordata(string linha)
        {
            linha = linha.Substring(8);
            var quantidade = linha.Substring(0, 9);
            var data = linha.Substring(9, 8);
            var tipo = linha.Substring(17, 20);
            var origem = linha.Substring(37, 5);
            var vara = linha.Substring(42, 4);
            var cidade = linha.Substring(46, 30);
            // var uf = linha.Substring(66, 2);
            var uf = linha.Substring(76, 2);
            // var natureza = linha.Substring(68, 3);
            var natureza = linha.Substring(78, 3);

            _relato.FalenciaConcordatas.Add(new FalenciaConcordata()
            {
                Tipo = tipo.Trim(),
                Quantidade = int.Parse(quantidade),
                Cidade = cidade.Trim(),
                Natureza = natureza.Trim(),
                Data = DataFormatada(data),
                UF = uf.Trim(),
                Origem = origem.Trim(),
                Vara = vara.Trim()
            });
        }

        public void DividaVencida(string linha)
        {
            linha = linha.Substring(8);
            var quantidade = linha.Substring(0, 9);
            var data = linha.Substring(9, 8);
            var modalidade = linha.Substring(17, 15);
            var moeda = linha.Substring(32, 3);
            var valor = linha.Substring(35, 13);
            var titulo = linha.Substring(48, 15);
            var instituicao = linha.Substring(63, 15);
            var local = linha.Substring(78, 3);
            var codnatureza = linha.Substring(81, 3);

            _relato.DividasVencidas.Add(new DividaVencida()
            {
                Quantidade = int.Parse(quantidade),
                Data = DataFormatada(data),
                Natureza = codnatureza.Trim(),
                Valor = decimal.Parse(valor),
                Moeda = moeda.Trim(),
                Titulo = titulo.Trim(),
                Instituicao = instituicao.Trim(),
                Local = local.Trim(),
                Modalidade = modalidade.Trim(),
                Total = _relato.ConcentreResumos.Find(c => c.Discriminacao.Contains("DIVIDA VENCIDA")).Total
            });
        }

        public void AcheiCheque(string linha)
        {
            linha = linha.Substring(8);
            var quantidade = linha.Substring(0, 9);
            var dataocorrencia = linha.Substring(9, 8);
            var numerocheque = linha.Substring(17, 6);
            var alinea = linha.Substring(23, 2);
            var qtdebanco = linha.Substring(25, 5);
            var moeda = linha.Substring(30, 3);
            var valor = linha.Substring(33, 13);
            var banco = linha.Substring(46, 12);
            var agencia = linha.Substring(58, 4);
            var cidade = linha.Substring(62, 30);
            var uf = linha.Substring(92, 2);
            var codnatureza = linha.Substring(94, 3);

            _relato.ChequesSemFundo.Add(new ChequeSemFundo()
            {
                Cidade = cidade.Trim(),
                Valor = decimal.Parse(valor),
                Numero = numerocheque.Trim(),
                Data = DataFormatada(dataocorrencia),
                UF = uf.Trim(),
                Quantidade = int.Parse(quantidade),
                Natureza = codnatureza.Trim(),
                Agencia = agencia.Trim(),
                Aliena = alinea.Trim(),
                Banco = banco.Trim(),
                Moeda = moeda.Trim(),
                QuantidadeBanco = int.Parse(qtdebanco)
            });
        }

        public void CCFCheque(string linha)
        {
            linha = linha.Substring(8);
            var quantidade = linha.Substring(0, 9);
            var dataocorrencia = linha.Substring(9, 8);
            var numerocheque = linha.Substring(17, 6);
            var qtdebanco = linha.Substring(23, 5);
            var banco = linha.Substring(28, 16);
            var agencia = linha.Substring(44, 4);
            var cidade = linha.Substring(48, 30);
            // var uf = linha.Substring(68, 2);
            var uf = linha.Substring(78, 2);
            // var codnatureza = linha.Substring(70, 3);
            var codnatureza = linha.Substring(80, 3);

            _relato.ChequesCCF.Add(new ChequeCCF()
            {
                Cidade = cidade.Trim(),
                Quantidade = int.Parse(quantidade),
                Numero = numerocheque.Trim(),
                Data = DataFormatada(dataocorrencia),
                UF = uf.Trim(),
                Natureza = codnatureza.Trim(),
                QuantidadeBanco = int.Parse(qtdebanco),
                Agencia = agencia.Trim(),
                Banco = banco.Trim()
            });
        }

        public void ParticipantesAnotacoes(string linha)
        {
            linha = linha.Substring(8);
            var nome = linha.Substring(0, 65);
            var documento = linha.Substring(65, 11); // O retorno do documento não traz o dígito.
            var tipo = linha.Substring(76, 1);
            
            //Gambiarra pra pegar o numero do documento correto...
            //string documento = null;
            //bool breakchain = false;
            //foreach(var participacao in _relato.Participacao)
            //{
            //    if (breakchain)
            //        break;
            //
            //    if (participacao.Nome.Trim().ToLower().Contains(nome.Trim().ToLower()))
            //    {
            //        documento = participacao.CNPJ;
            //        breakchain = true;
            //    }
            //    else
            //    {
            //        foreach (var membro in participacao.Membros)
            //        {
            //            if (membro.Nome.ToLower().Equals(nome.Trim().ToLower()))
            //            {
            //                documento = membro.Documento;
            //                breakchain = true;
            //            }
            //            else
            //            {
            //                foreach(var socioacionista in _relato.Socios)
            //                {
            //                    if (socioacionista.SocioAcionista.Contains(nome.Trim()))
            //                    {
            //                        documento = socioacionista.Documento;
            //                        breakchain = true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            
            _relato.ParticipantesAnotacoes.Add(new ParticipanteAnotacao()
            {
                Nome = nome.Trim(),
                Documento = (documento.StartsWith("000") ? "00" : (documento.StartsWith("00") ? "0" : "")) + int.Parse(documento.Trim()),
                Tipo = tipo.Trim()
            });
        }

        #endregion

        private DateTime? DataFormatada(string datastring)
        {
            if (datastring.Length != 8 || datastring == "99999999" || datastring == "00000000")
                return null;
            else
            {
                var ano = int.Parse(datastring.Substring(0, 4));
                if (ano < 1900)
                    ano = 1900;

                var mes = int.Parse(datastring.Substring(4, 2));
                if (mes < 1)
                    mes = 1;
                else if (mes > 12)
                    mes = 12;

                var dia = int.Parse(datastring.Substring(6, 2));
                if (dia < 1)
                    dia = 1;
                else if (DateTime.IsLeapYear(ano) && mes == 2 && dia > 29)
                    dia = 29;
                else if (!DateTime.IsLeapYear(ano) && mes == 2 && dia > 28)
                    dia = 28;
                else if (dia > 31)
                    dia = 31;

                try
                {
                    return new DateTime(ano, mes, dia);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}