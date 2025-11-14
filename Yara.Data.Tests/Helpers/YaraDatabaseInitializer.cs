using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using Yara.Data.Entity.Context;
using Yara.Domain.Entities;

namespace Yara.Data.Tests
{
    public class YaraDatabaseInitializer : DbMigrationsConfiguration<YaraDataContext>
    {
        public YaraDatabaseInitializer()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(YaraDataContext context)
        {
            var permissaoLista = Guid.NewGuid();
            var permissaoInsert = Guid.NewGuid();
            var permissaoUpdate = Guid.NewGuid();
            var permissaoView = Guid.NewGuid();
            var grupoRepresent = Guid.NewGuid();
            var grupoCtc = Guid.NewGuid();

            var userId01 = Guid.NewGuid();
            var userId02 = Guid.NewGuid();
            var userId03 = Guid.NewGuid();
            var userId04 = Guid.NewGuid();
            var userId05 = Guid.NewGuid();
            var userId06 = Guid.NewGuid();

            var idTipoCliente = Guid.NewGuid();

            var contaClienteId = Guid.NewGuid();

            var regiaoId01 = Guid.NewGuid();
            var regiaoId02 = Guid.NewGuid();
            var regiaoId03 = Guid.NewGuid();

            #region Log
            context.LogLevels.Add(new LogLevel()
            {
                ID = 1,
                Status = "Info"
            });
            context.LogLevels.Add(new LogLevel()
            {
                ID = 2,
                Status = "Error"
            });
            context.LogLevels.Add(new LogLevel()
            {
                ID = 3,
                Status = "Warn"
            });
            context.LogLevels.Add(new LogLevel()
            {
                ID = 4,
                Status = "ContaCliente"
            });
            #endregion

            #region Permissao
            var permissao01 = new Permissao()
            {
                Nome = "ContaCliente_Acesso",
                Descricao = "Acesso ao conta cliente",
                Ativo = true,
            };
            context.Permissao.Add(permissao01);
            var permissao02 = new Permissao()
            {
                Nome = "ContaCliente_Inserir",
                Descricao = "Inserção ao conta cliente",
                Ativo = true,
            };

            context.Permissao.Add(permissao02);
            var permissao03 = new Permissao()
            {
                Nome = "ContaCliente_Alterar",
                Descricao = "Alteração ao conta cliente",
                Ativo = true,
            };

            context.Permissao.Add(permissao03);

            var permissao04 = new Permissao()
            {
                Nome = "ContaCliente_Visualizar",
                Descricao = "Visualizar a conta cliente",
                Ativo = true,
            };

            context.Permissao.Add(permissao04);
            #endregion

            #region Grupos
            var group01 = new Grupo()
            {
                ID = grupoRepresent,
                Nome = "Representante",
                Ativo = true,
                DataCriacao = DateTime.Now

            };

            group01.Permissoes.Add(permissao01);
            group01.Permissoes.Add(permissao02);
            group01.Permissoes.Add(permissao03);
            group01.Permissoes.Add(permissao04);
            context.Grupo.Add(group01);

            var group02 = new Grupo()
            {
                ID = grupoCtc,
                Nome = "CTC",
                Ativo = true,
                DataCriacao = DateTime.Now
            };
            group02.Permissoes.Add(permissao01);
            group02.Permissoes.Add(permissao02);
            group02.Permissoes.Add(permissao03);
            group02.Permissoes.Add(permissao04);
            context.Grupo.Add(group02);

            var group03 = new Grupo()
            {
                ID = Guid.NewGuid(),
                Nome = "Financeiro",
                Ativo = true,
                DataCriacao = DateTime.Now
            };

            group03.Permissoes.Add(permissao01);
            group03.Permissoes.Add(permissao02);
            group03.Permissoes.Add(permissao03);
            group03.Permissoes.Add(permissao04);
            context.Grupo.Add(group03);

            var group04 = new Grupo()
            {
                ID = Guid.NewGuid(),
                Nome = "Supervisor",
                Ativo = true,
                DataCriacao = DateTime.Now
            };

            group04.Permissoes.Add(permissao01);
            group04.Permissoes.Add(permissao02);
            group04.Permissoes.Add(permissao03);
            group04.Permissoes.Add(permissao04);
            context.Grupo.Add(group04);


            #endregion

            #region Usuarios

            var user01 = new Usuario()
            {
                ID = userId01,
                Nome = "Junior Porfirio",
                Login = "cpjunior",
                Email = "claudemir.junior@performait.com",
                Ativo = true,
                TipoAcesso = TipoAcesso.AD,
                DataCriacao = DateTime.Now

            };

            user01.Grupos.Add(group01);
            context.Usuarios.Add(user01);

            var user02 = new Usuario()
            {
                ID = userId02,
                Nome = "Luciano Carlos de Jesus",
                Login = "lcjesus",
                Email = "luciano.jesus@performait.com",
                Ativo = true,
                TipoAcesso = TipoAcesso.AD,
                DataCriacao = DateTime.Now
            };

            user02.Grupos.Add(group01);
            context.Usuarios.Add(user02);

            var user03 = new Usuario()
            {
                ID = userId03,
                Nome = "Felipe Trova",
                Login = "frtrova",
                Email = "felipe.trova@performait.com",
                Ativo = true,
                TipoAcesso = TipoAcesso.AD,
                DataCriacao = DateTime.Now
            };

            user03.Grupos.Add(group01);
            context.Usuarios.Add(user03);

            var user04 = new Usuario()
            {
                ID = userId04,
                Nome = "SF Login",
                Login = "juniorporfirio@gmail.com",
                Email = "juniorporfirio@gmail.com",
                Ativo = true,
                TipoAcesso = TipoAcesso.SF,
                DataCriacao = DateTime.Now
            };

            user04.Grupos.Add(group01);
            context.Usuarios.Add(user04);

            var user05 = new Usuario()
            {
                ID = userId05,
                Nome = "Carlos Silvestre",
                Login = "crsilvestre",
                Email = "carlos.silvestre@performait.com",
                Ativo = true,
                TipoAcesso = TipoAcesso.AD,
                DataCriacao = DateTime.Now
            };

            user05.Grupos.Add(group01);
            context.Usuarios.Add(user05);

            var user06 = new Usuario()
            {
                ID = userId06,
                Nome = "Andre Paganuchi",
                Login = "apaganuchi",
                Email = "andre.paganuchi@performait.com",
                Ativo = true,
                TipoAcesso = TipoAcesso.AD,
                DataCriacao = DateTime.Now
            };

            user06.Grupos.Add(group01);
            context.Usuarios.Add(user06);

            #endregion

            #region Segmentos

            var segmentoID = Guid.NewGuid();
            var segmento01 = new Segmento()
            {
                ID = segmentoID,
                Descricao = "Crop Nutrition",
                UsuarioIDCriacao = userId01,
                UsuarioIDAlteracao = userId01,
                Ativo = true,
                DataCriacao = DateTime.Now
            };
            context.Segmento.Add(segmento01);
            var segmento02 = new Segmento()
            {
                ID = Guid.NewGuid(),
                Descricao = "Industrial",
                UsuarioIDCriacao = userId01,
                UsuarioIDAlteracao = userId01,
                Ativo = true,
                DataCriacao = DateTime.Now
            };
            context.Segmento.Add(segmento02);
            var segmento03 = new Segmento()
            {
                ID = Guid.NewGuid(),
                Descricao = "Misturadoras",
                UsuarioIDCriacao = userId01,
                UsuarioIDAlteracao = userId01,
                Ativo = true,
                DataCriacao = DateTime.Now
            };
            context.Segmento.Add(segmento03);

            var segmento04 = new Segmento()
            {
                ID = Guid.NewGuid(),
                Descricao = "Permutas",
                UsuarioIDCriacao = userId01,
                UsuarioIDAlteracao = userId01,
                Ativo = true,
                DataCriacao = DateTime.Now
            };
            context.Segmento.Add(segmento04);


            #endregion

            #region TipoCliente

            
            context.TipoCliente.Add(new TipoCliente()
            {
                ID = idTipoCliente,
                Nome = "Usina",
                UsuarioIDCriacao = userId01,
                Ativo = true,
                DataCriacao = DateTime.Now
            });


            #endregion

            #region "Grupo Economico"

            var grupoeconomico01 = new GrupoEconomico
            {
                ID = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                //CodigoPrincipal = "1234567890",
                Nome = "Sim – Irrelevante",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                //Tipo = "Tipo 01",
                Descricao = "Sim"

            };
            context.GrupoEconomico.Add(grupoeconomico01);
            #endregion 

            #region Representantes

            var representante01 = new Representante
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                CodigoSap = "1234567890",
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now,
                Nome = "João Barbosa Corrêa",

            };
            context.Representante.Add(representante01);

            var representante02 = new Representante
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                CodigoSap = "0987654321",
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now,
                Nome = "Lucas Feliz"

            };
            context.Representante.Add(representante02);

            #endregion

         
            #region ConceitoCobranca

            var ConceitoCobrancaID = Guid.NewGuid();
            context.ConceitoCobranca.Add(new ConceitoCobranca
            {
                ID = ConceitoCobrancaID,
                Nome = "H",
                Descricao = "Informação vem da proposta de abono",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            var ConceitoCobrancaID2 = Guid.NewGuid();
            context.ConceitoCobranca.Add(new ConceitoCobranca
            {
                ID = ConceitoCobrancaID2,
                Nome = "J",
                Descricao = "Informação vem da proposta de envio ao jurídico",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            var ConceitoCobrancaID3 = Guid.NewGuid();
            context.ConceitoCobranca.Add(new ConceitoCobranca
            {
                ID = ConceitoCobrancaID3,
                Nome = "II",
                Descricao = "Informação enviada a cobrança",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });
            #endregion

            #region ContaCliente
            
            var contacliente = new ContaCliente
            {
                ID = contaClienteId,
                Documento = "31691301884",
                CodigoPrincipal = "1234567890",
                Nome = "Claudemir Porfirio Junior",
                Apelido = "Junior Porfirio",
                TipoClienteID = idTipoCliente,
                DataNascimentoFundacao = DateTime.Now,
                Contato = "Mesmo",
                CEP = "13044-503",
                Endereco = "Rua Maria Mercedes Etter Von Zuben,151 BL C AP 204",
                Cidade = "Campinas",
                UF = "SP",
                Telefone = "19-991041566",
                Pais = "Brazil",
                Email = "claudemir.junior@performait.com",
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now,
                BloqueioManual = false
            };



            //contacliente.GrupoEconomicos.Add(grupoeconomico01);

            context.ContaCliente.Add(contacliente);

            #endregion

            #region ContaClienteFinanceiro

            context.ContaClienteFinanceiro.Add(new ContaClienteFinanceiro
            {
                ContaClienteID = contaClienteId,
                ConceitoCobrancaID = ConceitoCobrancaID,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Exposicao = 100,
                LC = 100,
                Rating = "Baixo",
                Recebiveis = 10000,
                OperacaoFinanciamento = 10000,
                Vigencia = DateTime.Now
            });


            #endregion

            #region "Conta Cliente Código"
            context.ContaClienteCodigo.Add(new ContaClienteCodigo
            {
                ID = Guid.NewGuid(),
                Codigo = "1234567890",
                CodigoPrincipal = true,
                Documento = "31691301884",
                ContaClienteID = contaClienteId,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            context.ContaClienteCodigo.Add(new ContaClienteCodigo
            {
                ID = Guid.NewGuid(),
                Codigo = "0987654321",
                CodigoPrincipal = false,
                Documento = "12032608875",
                ContaClienteID = contaClienteId,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });


            context.ContaClienteCodigo.Add(new ContaClienteCodigo
            {
                ID = Guid.NewGuid(),
                Codigo = "1122334455",
                CodigoPrincipal = false,
                Documento = "5554851526",
                ContaClienteID = contaClienteId,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });
            #endregion

            #region "Conta Cliente Telefones
            context.ContaClienteTelefone.Add(new ContaClienteTelefone()
            {
                ID = Guid.NewGuid(),
                ContaClienteID = contaClienteId,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Telefone = "1932245255",
                Tipo = TipoTelefone.Fixo,
                Ativo = true
            });

            context.ContaClienteTelefone.Add(new ContaClienteTelefone()
            {
                ID = Guid.NewGuid(),
                ContaClienteID = contaClienteId,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Telefone = "1932245222",
                Tipo = TipoTelefone.Fixo,
                Ativo = true
            });
            context.ContaClienteTelefone.Add(new ContaClienteTelefone()
            {
                ID = Guid.NewGuid(),
                ContaClienteID = contaClienteId,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Telefone = "19991041566",
                Tipo = TipoTelefone.Celular,
                Ativo = true
            });



            #endregion

            #region Cadastro de Experiencia

            context.Experiencias.Add(new Experiencia()
            {
                ID = Guid.NewGuid(),
                Descricao = "Sem experiência",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            context.Experiencias.Add(new Experiencia()
            {
                ID = Guid.NewGuid(),
                Descricao = "Até 5 anos",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            context.Experiencias.Add(new Experiencia()
            {
                ID = Guid.NewGuid(),
                Descricao = "De 5 a 10 anos",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            context.Experiencias.Add(new Experiencia()
            {
                ID = Guid.NewGuid(),
                Descricao = "De 10 a 15 anos",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            context.Experiencias.Add(new Experiencia()
            {
                ID = Guid.NewGuid(),
                Descricao = "Mais de 15 anos",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            #endregion

            #region Cadastro de Culturas

            context.Culturas.Add(new Cultura()
            {
                ID = Guid.NewGuid(),
                Descricao = "Avicultura",
                UnidadeMedida = "Cabeças",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });

            context.Culturas.Add(new Cultura()
            {
                ID = Guid.NewGuid(),
                Descricao = "Suinocultura",
                UnidadeMedida = "Cabeças",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });

            #endregion

            #region Receita

            context.Receitas.Add(new Receita()
            {
                ID = Guid.NewGuid(),
                Descricao = "Estoque",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });
            context.Receitas.Add(new Receita()
            {
                ID = Guid.NewGuid(),
                Descricao = "Arrendamento",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });
            context.Receitas.Add(new Receita()
            {
                ID = Guid.NewGuid(),
                Descricao = "Vendas de Bens",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });
            context.Receitas.Add(new Receita()
            {
                ID = Guid.NewGuid(),
                Descricao = "Fazenda",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            #endregion

            #region Tipo Empresa

            context.TipoEmpresas.Add(new TipoEmpresa()
            {
                ID = Guid.NewGuid(),
                Tipo = "Defensivos",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });
            context.TipoEmpresas.Add(new TipoEmpresa()
            {
                ID = Guid.NewGuid(),
                Tipo = "Sementes",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });
            context.TipoEmpresas.Add(new TipoEmpresa()
            {
                ID = Guid.NewGuid(),
                Tipo = "Trading",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01
            });

            #endregion

            #region Area Irrigada

            context.AreaIrrigadas.Add(new AreaIrrigada()
            {
                ID = Guid.NewGuid(),
                Nome = "Não possui irrigação",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.AreaIrrigadas.Add(new AreaIrrigada()
            {
                ID = Guid.NewGuid(),
                Nome = "Menos de 30%",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.AreaIrrigadas.Add(new AreaIrrigada()
            {
                ID = Guid.NewGuid(),
                Nome = "Entre 30% e 50%",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.AreaIrrigadas.Add(new AreaIrrigada()
            {
                ID = Guid.NewGuid(),
                Nome = "Entre 50% e 75%",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.AreaIrrigadas.Add(new AreaIrrigada()
            {
                ID = Guid.NewGuid(),
                Nome = "Mais de 75%",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });

            #endregion

            #region TipoGarantia

            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "Carta Fiança"

            });

            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "Hipotéca"

            });

            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "Alienação de Bens Imóveis"

            });

            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "Alienação de Bens Móveis"

            });


            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "Penhor"

            });

            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "Cessão de crédito"

            });

            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "CPR"

            });

            context.TipoGarantias.Add(new TipoGarantia()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = userId01,
                Nome = "Outros"

            });

            #endregion

            #region IdadeMediaCanavial

            context.IdadeMediaCanvial.Add(new IdadeMediaCanavial()
            {
                ID = Guid.NewGuid(),
                Nome = "Abaixo do mercado – Ruim",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });

            context.IdadeMediaCanvial.Add(new IdadeMediaCanavial()
            {
                ID = Guid.NewGuid(),
                Nome = "Em linha com o mercado – Bom",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });

            context.IdadeMediaCanvial.Add(new IdadeMediaCanavial()
            {
                ID = Guid.NewGuid(),
                Nome = "Acima do mercado – Ótimo",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });

            #endregion

            #region Procedures

            var sqlfiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory.Contains("Test") ? $"{AppDomain.CurrentDomain.BaseDirectory}/../../../SQL/" : $"{AppDomain.CurrentDomain.BaseDirectory}/../SQL/", "*.sql");

            foreach (var file in sqlfiles)
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file));
            }

            #endregion

            #region Produtos e Serviços

            context.ProdutoServicos.Add(new ProdutoServico()
            {
                ID = Guid.NewGuid(),
                Nome = "Defensivos",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.ProdutoServicos.Add(new ProdutoServico()
            {
                ID = Guid.NewGuid(),
                Nome = "Sementes",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.ProdutoServicos.Add(new ProdutoServico()
            {
                ID = Guid.NewGuid(),
                Nome = "Serviços",
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });


            #endregion

            #region Produtividade Media

            context.ProdutividadesMedia.Add(new ProdutividadeMedia()
            {
                ID = Guid.NewGuid(),
                Nome = "Volume de moagem propria",
                RegiaoID = regiaoId01,
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.ProdutividadesMedia.Add(new ProdutividadeMedia()
            {
                ID = Guid.NewGuid(),
                Nome = "Idade media",
                RegiaoID = regiaoId02,
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });
            context.ProdutividadesMedia.Add(new ProdutividadeMedia()
            {
                ID = Guid.NewGuid(),
                Nome = "Sem ideia",
                RegiaoID = regiaoId03,
                Ativo = true,
                UsuarioIDCriacao = userId01,
                DataCriacao = DateTime.Now
            });

            #endregion

            #region Média por Saca

            context.MediaSacas.Add(new MediaSaca()
            {
                ID = Guid.NewGuid(),
                Nome = "Descrição da saca de milho",
                Peso = 60,
                Valor = Convert.ToDecimal(string.Format("{0:N}", "25,50")),
                Ativo = true,
                UsuarioIDCriacao = userId02,
                DataCriacao = DateTime.Now
            });
            context.MediaSacas.Add(new MediaSaca()
            {
                ID = Guid.NewGuid(),
                Nome = "Descrição da saca de arroz",
                Peso = 60,
                Valor = Convert.ToDecimal(string.Format("{0:N}", "30,50")),
                Ativo = true,
                UsuarioIDCriacao = userId02,
                DataCriacao = DateTime.Now
            });
            context.MediaSacas.Add(new MediaSaca()
            {
                ID = Guid.NewGuid(),
                Nome = "Descrição da saca de feijão",
                Peso = 60,
                Valor = Convert.ToDecimal(string.Format("{0:N}", "40,50")),
                Ativo = true,
                UsuarioIDCriacao = userId02,
                DataCriacao = DateTime.Now
            });

            #endregion

            #region Regiões

            context.Regioes.Add(new Regiao(regiaoId01, "REGIÃO SUL"));
            context.Regioes.Add(new Regiao(regiaoId02, "REGIÃO NORDESTE"));
            context.Regioes.Add(new Regiao(regiaoId03, "REGIÃO SUDESTE"));
            context.Regioes.Add(new Regiao(Guid.NewGuid(), "REGIÃO NORTE"));
            context.Regioes.Add(new Regiao(Guid.NewGuid(), "REGIÃO CENTRO OESTE"));
            #endregion

            context.SaveChanges();
            base.Seed(context);
        }
    }
}

