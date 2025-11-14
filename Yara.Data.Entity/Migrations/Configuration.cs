using Yara.Domain.Entities;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Yara.Data.Entity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Yara.Data.Entity.Context.YaraDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Yara.Data.Entity.Context.YaraDataContext";
        }

        protected override void Seed(Yara.Data.Entity.Context.YaraDataContext context)
        {
            // TODO: Refazer as permissões...

            var permissaoLista = Guid.NewGuid();
            var permissaoUpdate = Guid.NewGuid();
            var permissaoView = Guid.NewGuid();

            #region Log
            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 1,
                Status = "Info"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 2,
                Status = "Error"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 3,
                Status = "Warn"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 4,
                Status = "ContaCliente"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 5,
                Status = "ConceitoCobranca"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 6,
                Status = "BloqueioManual"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 7,
                Status = "OrdemBloqueadoManual"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 8,
                Status = "OrdemLiberacaoManual"
            });

            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 9,
                Status = "GrupoEconomico"
            });
            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 10,
                Status = "Propostas"
            });
            context.LogLevels.AddOrUpdate(new LogLevel()
            {
                ID = 11,
                Status = "PerfilUsuario"
            });
            #endregion

            #region StatusPropostaAbono

            var EmCriacaoAbono = new PropostaCobrancaStatus()
            {
                ID = "EC",
                Status = "Em Criação"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("EC"))) context.PropostaAbonoStatus.AddOrUpdate(EmCriacaoAbono);


            var AprovadoAutoAbono = new PropostaCobrancaStatus()
            {
                ID = "AA",
                Status = "Aprovado Automático"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("AA"))) context.PropostaAbonoStatus.AddOrUpdate(AprovadoAutoAbono);

            var EmCobrancaAbono = new PropostaCobrancaStatus()
            {
                ID = "CC",
                Status = "Enviado Cobrança"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("CC"))) context.PropostaAbonoStatus.AddOrUpdate(EmCobrancaAbono);

            var EnviadoCTCAbono = new PropostaCobrancaStatus()
            {
                ID = "ET",
                Status = "Aguardando Parecer do CTC"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("ET"))) context.PropostaAbonoStatus.AddOrUpdate(EnviadoCTCAbono);

          

            var EmAprovacaoAbono = new PropostaCobrancaStatus()
            {
                ID = "EA",
                Status = "Em Aprovação"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("EA"))) context.PropostaAbonoStatus.AddOrUpdate(EmAprovacaoAbono);


            var CanceladaAbono = new PropostaCobrancaStatus()
            {
                ID = "CA",
                Status = "Cancelada"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("CA"))) context.PropostaAbonoStatus.AddOrUpdate(CanceladaAbono);

            var RejeitadaAbono = new PropostaCobrancaStatus()
            {
                ID = "RE",
                Status = "Rejeitada"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("RE"))) context.PropostaAbonoStatus.AddOrUpdate(RejeitadaAbono);


            var AprovadoAbono = new PropostaCobrancaStatus()
            {
                ID = "AP",
                Status = "Aprovado"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("AP"))) context.PropostaAbonoStatus.AddOrUpdate(AprovadoAbono);


            var AprovadoAbonoAC = new PropostaCobrancaStatus()
            {
                ID = "AC",
                Status = "Aguardando ação do analista de cobrança"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("AC"))) context.PropostaAbonoStatus.AddOrUpdate(AprovadoAbonoAC);


            var AprovadoAlacadaEncerrada = new PropostaCobrancaStatus()
            {
                ID = "EN",
                Status = "Encerrada"
            };
            if (!context.PropostaAbonoStatus.Any(c => c.ID.Equals("EN"))) context.PropostaAbonoStatus.AddOrUpdate(AprovadoAlacadaEncerrada);

            #endregion

            #region Permissao

            var proposta = new Permissao()
            {
                Nome = "Cookpit_PropostaLC",
                Descricao = "Acompanhamento de Propostas e Pendências",
                Processo = "Proposta",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cookpit_PropostaLC"))) context.Permissao.AddOrUpdate(proposta);

            #region Controle de Cobrança

            var juridicoAcesso = new Permissao()
            {
                Nome = "PropostaJuridico_Acesso",
                Descricao = "Solicitar proposta para enviar ao jurídico",
                Processo = "Parametro - Proposta Jurídico",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PropostaJuridico_Acesso"))) context.Permissao.AddOrUpdate(juridicoAcesso);

            var juridicoEditar = new Permissao()
            {
                Nome = "PropostaJuridico_Editar",
                Descricao = "Editar proposta jurídica",
                Processo = "Parametro - Proposta Jurídica",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PropostaJuridico_Editar"))) context.Permissao.AddOrUpdate(juridicoEditar);

            var relatoriopropostaview = new Permissao()
            {
                Nome = "RelatorioProposta_Visualizar",
                Descricao = "Visualizar Relatório de Propostas",
                Processo = "Parametros - Relatórios",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("RelatorioProposta_Visualizar"))) context.Permissao.AddOrUpdate(relatoriopropostaview);
        
            #endregion

            #region ContaClienteVisitas

            var acessoVisita = new Permissao()
            {
                Nome = "ContaClienteVisita_Acesso",
                Descricao = "Acesso Conta Cliente Visita",
                Processo = "Paramentro - Conta Cliente Visitas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteVisita_Acesso"))) context.Permissao.AddOrUpdate(acessoVisita);

            var inserirVisita = new Permissao()
            {
                Nome = "ContaClienteVisita_Inserir",
                Descricao = "Inserir Conta Cliente Visita",
                Processo = "Paramentro - Conta Cliente Visitas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteVisita_Inserir"))) context.Permissao.AddOrUpdate(inserirVisita);
            
            var editarVisita = new Permissao()
            {
                Nome = "ContaClienteVisita_Editar",
                Descricao = "Editar Conta Cliente Visita",
                Processo = "Paramentro - Conta Cliente Visitas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteVisita_Editar"))) context.Permissao.AddOrUpdate(editarVisita);

            #endregion

            #region ContaClienteBuscaBens

            var acessoBuscaBens = new Permissao()
            {
                Nome = "ContaClienteBuscaBens_Acesso",
                Descricao = "Acesso Conta Cliente Busca de Bens",
                Processo = "Paramentro - Conta Cliente Busca de Bens",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteBuscaBens_Acesso"))) context.Permissao.AddOrUpdate(acessoBuscaBens);

            var inserirBuscaBens = new Permissao()
            {
                Nome = "ContaClienteBuscaBens_Inserir",
                Descricao = "Inserir Conta Cliente Busca de Bens",
                Processo = "Paramentro - Conta Cliente Busca de Bens",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteBuscaBens_Inserir"))) context.Permissao.AddOrUpdate(inserirBuscaBens);

            var editarBuscaBens = new Permissao()
            {
                Nome = "ContaClienteBuscaBens_Editar",
                Descricao = "Editar Conta Cliente Busca de Bens",
                Processo = "Paramentro - Conta Cliente Busca de Bens",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteBuscaBens_Editar"))) context.Permissao.AddOrUpdate(editarBuscaBens);

            #endregion

            #region Serasa

            var serasa01 = new Permissao()
            {
                Nome = "Serasa_Consulta",
                Descricao = "Acesso a consultar serasa",
                Processo = "Serasa",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Serasa_Consulta"))) context.Permissao.AddOrUpdate(serasa01);


            var serasa02 = new Permissao()
            {
                Nome = "Serasa_Historico",
                Descricao = "Acesso ao Histórico de consultas serasa",
                Processo = "Serasa",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Serasa_Historico"))) context.Permissao.AddOrUpdate(serasa02);

            var serasa03 = new Permissao()
            {
                Nome = "Serasa_Alteracao",
                Descricao = "Editar Status serasa na conta cliente",
                Processo = "Serasa",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Serasa_Alteracao"))) context.Permissao.AddOrUpdate(serasa03);

            #endregion

            #region Conta Cliente

            var permissao01 = new Permissao()
            {
                Nome = "ContaCliente_Acesso",
                Descricao = "Acesso ao conta cliente",
                Processo = "Conta Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_Acesso"))) context.Permissao.AddOrUpdate(permissao01);

            var permissao02 = new Permissao()
            {
                Nome = "ContaCliente_Inserir",
                Descricao = "Inserir Conta Cliente Simplicada",
                Processo = "Conta Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_Inserir"))) context.Permissao.AddOrUpdate(permissao02);

            var contaclienteVisualizar = new Permissao()
            {
                Nome = "ContaCliente_Visualizar",
                Descricao = "Visualizar ao conta cliente",
                Processo = "Conta Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_Visualizar"))) context.Permissao.AddOrUpdate(contaclienteVisualizar);

            var permissao03 = new Permissao()
            {
                Nome = "ContaCliente_Editar",
                Descricao = "Alteração ao conta cliente",
                Processo = "Conta Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_Editar"))) context.Permissao.AddOrUpdate(permissao03);

            var permissaoMovimentarEstrutura = new Permissao()
            {
                Nome = "ContaClienteEstrutura_Editar",
                Descricao = "Alteração da estrutura ao conta cliente",
                Processo = "Conta Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteEstrutura_Editar"))) context.Permissao.AddOrUpdate(permissaoMovimentarEstrutura);

            var bloqueioManual = new Permissao()
            {
                Nome = "ContaCliente_BloqueioManual",
                Descricao = "Permite Bloqueio Manual",
                Processo = "Conta Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_BloqueioManual"))) context.Permissao.AddOrUpdate(bloqueioManual);

            var liberacaoManual = new Permissao()
            {
                Nome = "ContaCliente_LiberacaoManual",
                Descricao = "Permite Liberação Manual",
                Processo = "Conta Cliente",
                Ativo = true
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_LiberacaoManual"))) context.Permissao.AddOrUpdate(liberacaoManual);

            var permissaoReavaliarCarteira = new Permissao()
            {
                Nome = "ContaCliente_ReavaliarCarteira",
                Descricao = "Permite Reavaliar Carteira",
                Processo = "Conta Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_ReavaliarCarteira"))) context.Permissao.AddOrUpdate(permissaoReavaliarCarteira);

            #endregion

            #region Conta Cliente Financeiro

            var contaclientefinanceiroVisualizar = new Permissao()
            {
                Nome = "ContaClienteFinanceiro_Visualizar",
                Descricao = "Acesso aos dados da  conta cliente financeiro",
                Processo = "Conta Cliente - Financeiro",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteFinanceiro_Visualizar"))) context.Permissao.AddOrUpdate(contaclientefinanceiroVisualizar);

            var contaclientefinanceiroSF = new Permissao()
            {
                Nome = "ContaClienteFinanceiro_SalesForce",
                Descricao = "Acesso a integração com SF(Sales Force)",
                Processo = "Conta Cliente - Financeiro",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteFinanceiro_SalesForce"))) context.Permissao.AddOrUpdate(contaclientefinanceiroSF);

            var contaclientefinanceiroEditar = new Permissao()
            {
                Nome = "ContaClienteFinanceiro_Editar",
                Descricao = "Editar conceito de cobrança",
                Processo = "Conta Cliente - Financeiro",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteFinanceiro_Editar"))) context.Permissao.AddOrUpdate(contaclientefinanceiroEditar);

            var contaclientefinanceiroInserir = new Permissao()
            {
                Nome = "ContaClienteFinanceiro_Inserir",
                Descricao = "Inserir Dados Financeiros do Cliente",
                Processo = "Conta Cliente - Financeiro",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteFinanceiro_Inserir"))) context.Permissao.AddOrUpdate(contaclientefinanceiroInserir);
            
            var envioSeguradora = new Permissao()
            {
                Nome = "ContaCliente_EnvioSeguradora",
                Descricao = "Permite Enviar para Seguradora",
                Processo = "Conta Cliente - Financeiro",
                Ativo = true
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_EnvioSeguradora"))) context.Permissao.AddOrUpdate(envioSeguradora);

            #endregion

            #region Conta Cliente Comentários

            var parametroComentarioVisualizar = new Permissao()
            {
                Nome = "ContaClienteComentario_Visualizar",
                Descricao = "Visualiza comentário a conta cliente",
                Processo = "Conta Cliente - Comentários",
                Ativo = true,
            };

            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteComentario_Visualizar"))) context.Permissao.AddOrUpdate(parametroComentarioVisualizar);

            var parametroComentarioInserir = new Permissao()
            {
                Nome = "ContaClienteComentario_Inserir",
                Descricao = "Inserir comentário a conta cliente",
                Processo = "Conta Cliente - Comentários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaClienteComentario_Inserir"))) context.Permissao.AddOrUpdate(parametroComentarioInserir);

            #endregion

            #region Parametros

            var permissao05 = new Permissao()
            {
                Nome = "ParametroSistema_Visualizar",
                Descricao = "Visualizar Parametro Sistema",
                Processo = "Parâmetros Gerais",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ParametroSistema_Visualizar"))) context.Permissao.AddOrUpdate(permissao05);

            var permissao06 = new Permissao()
            {
                Nome = "ParametroSistema_Inserir",
                Descricao = "Visualizar Parametro Sistema",
                Processo = "Parâmetros Gerais",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ParametroSistema_Inserir"))) context.Permissao.AddOrUpdate(permissao06);

            var permissao07 = new Permissao()
            {
                Nome = "ParametroSistema_Editar",
                Descricao = "Visualizar Parametro Sistema",
                Processo = "Parâmetros Gerais",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ParametroSistema_Editar"))) context.Permissao.AddOrUpdate(permissao07);

            var permissao08 = new Permissao()
            {
                Nome = "ParametroSistema_Excluir",
                Descricao = "Visualizar Parametro Sistema",
                Processo = "Parâmetros Gerais",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ParametroSistema_Excluir"))) context.Permissao.AddOrUpdate(permissao08);

            #endregion

            #region Usuarios

            var usuarioVisualizar = new Permissao()
            {
                Nome = "Usuario_Acesso",
                Descricao = "Acesso Usuários",
                Processo = "Segurança - Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Usuario_Acesso"))) context.Permissao.AddOrUpdate(usuarioVisualizar);

            var usuarioEditar = new Permissao()
            {
                Nome = "Usuario_Editar",
                Descricao = "Editar Usuários",
                Processo = "Segurança - Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Usuario_Editar"))) context.Permissao.AddOrUpdate(usuarioEditar);

            var usuarioInserir = new Permissao()
            {
                Nome = "Usuario_Inserir",
                Descricao = "Inserir  Usuários",
                Processo = "Segurança - Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Usuario_Inserir"))) context.Permissao.AddOrUpdate(usuarioInserir);

            var usuarioExcluir = new Permissao()
            {
                Nome = "Usuario_Excluir",
                Descricao = "Excluir Usuários",
                Processo = "Segurança - Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Usuario_Excluir"))) context.Permissao.AddOrUpdate(usuarioExcluir);

            #endregion

            #region Grupo de Usuários

            var GrupousuarioVisualizar = new Permissao()
            {
                Nome = "GrupoUsuario_Acesso",
                Descricao = "Acesso Grupo de Usuários",
                Processo = "Segurança - Grupo de Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoUsuario_Acesso"))) context.Permissao.AddOrUpdate(GrupousuarioVisualizar);

            var GrupousuarioEditar = new Permissao()
            {
                Nome = "GrupoUsuario_Editar",
                Descricao = "Editar Grupo de Usuários",
                Processo = "Segurança - Grupo de Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoUsuario_Editar"))) context.Permissao.AddOrUpdate(GrupousuarioEditar);

            var GrupousuarioInserir = new Permissao()
            {
                Nome = "GrupoUsuario_Inserir",
                Descricao = "Inserir Grupo de Usuários",
                Processo = "Segurança - Grupo de Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoUsuario_Inserir"))) context.Permissao.AddOrUpdate(GrupousuarioInserir);

            var GrupousuarioExcluir = new Permissao()
            {
                Nome = "GrupoUsuario_Excluir",
                Descricao = "Excluir Grupo de Usuários",
                Processo = "Segurança - Grupo de Usuários",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoUsuario_Excluir"))) context.Permissao.AddOrUpdate(GrupousuarioExcluir);

            #endregion

            #region Tipo de Cliente

            var tipoclienteVisualizar = new Permissao()
            {
                Nome = "TipoCliente_Acesso",
                Descricao = "Acesso a tipo de clientes",
                Processo = "Parâmetros - Tipos de Clientes",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoCliente_Acesso"))) context.Permissao.AddOrUpdate(tipoclienteVisualizar);

            var tipoclienteEditar = new Permissao()
            {
                Nome = "TipoCliente_Editar",
                Descricao = "Editar um tipo de cliente",
                Processo = "Parâmetros - Tipos de Clientes",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoCliente_Editar"))) context.Permissao.AddOrUpdate(tipoclienteEditar);

            var tipoclienteInserir = new Permissao()
            {
                Nome = "TipoCliente_Inserir",
                Descricao = "Inserir um tipo de cliente",
                Processo = "Parâmetros - Tipos de Clientes",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoCliente_Inserir"))) context.Permissao.AddOrUpdate(tipoclienteInserir);

            var tipoclienteExcluir = new Permissao()
            {
                Nome = "TipoCliente_Excluir",
                Descricao = "Excluir um tipo de cliente",
                Processo = "Parâmetros - Tipos de Clientes",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoCliente_Excluir"))) context.Permissao.AddOrUpdate(tipoclienteExcluir);

            #endregion

            #region Tipo Empresa

            var tipoempresaVisualizar = new Permissao()
            {
                Nome = "TipoEmpresa_Acesso",
                Descricao = "Acesso a tipo empresa",
                Processo = "Parâmetros - Tipos de Empresas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoEmpresa_Acesso"))) context.Permissao.AddOrUpdate(tipoempresaVisualizar);

            var tipoempresaEditar = new Permissao()
            {
                Nome = "TipoEmpresa_Editar",
                Descricao = "Editar um tipo empresa",
                Processo = "Parâmetros - Tipos de Empresas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoEmpresa_Editar"))) context.Permissao.AddOrUpdate(tipoempresaEditar);

            var tipoempresaInserir = new Permissao()
            {
                Nome = "TipoEmpresa_Inserir",
                Descricao = "Inserir um tipo empresa",
                Processo = "Parâmetros - Tipos de Empresas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoEmpresa_Inserir"))) context.Permissao.AddOrUpdate(tipoempresaInserir);

            var tipoempresaExcluir = new Permissao()
            {
                Nome = "TipoEmpresa_Excluir",
                Descricao = "Exclui um tipo empresa",
                Processo = "Parâmetros - Tipos de Empresas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoEmpresa_Excluir"))) context.Permissao.AddOrUpdate(tipoempresaExcluir);

            #endregion

            #region Tipo Garantia

            var tipogarantiaVisualizar = new Permissao()
            {
                Nome = "TipoGarantia_Acesso",
                Descricao = "Acesso a tipo garantia",
                Processo = "Parâmetros - Tipos de Garantia",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoGarantia_Acesso"))) context.Permissao.AddOrUpdate(tipogarantiaVisualizar);

            var tipogarantiaEditar = new Permissao()
            {
                Nome = "TipoGarantia_Editar",
                Descricao = "Editar um tipo garantia",
                Processo = "Parâmetros - Tipos de Garantia",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoGarantia_Editar"))) context.Permissao.AddOrUpdate(tipogarantiaEditar);

            var tipogarantiaInserir = new Permissao()
            {
                Nome = "TipoGarantia_Inserir",
                Descricao = "Inserir um tipo garantia",
                Processo = "Parâmetros - Tipos de Garantia",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoGarantia_Inserir"))) context.Permissao.AddOrUpdate(tipogarantiaInserir);

            #endregion

            #region Tipo Receita

            var tiporeceitaVisualizar = new Permissao()
            {
                Nome = "TipoReceita_Acesso",
                Descricao = "Acesso a tipo receita",
                Processo = "Parâmetros - Tipos de Receita",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoReceita_Acesso"))) context.Permissao.AddOrUpdate(tiporeceitaVisualizar);

            var tiporeceitaEditar = new Permissao()
            {
                Nome = "TipoReceita_Editar",
                Descricao = "Editar um tipo receita",
                Processo = "Parâmetros - Tipos de Receita",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoReceita_Editar"))) context.Permissao.AddOrUpdate(tiporeceitaEditar);

            var tiporeceitaInserir = new Permissao()
            {
                Nome = "TipoReceita_Inserir",
                Descricao = "Inserir um tipo receita",
                Processo = "Parâmetros - Tipos de Receita",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoReceita_Inserir"))) context.Permissao.AddOrUpdate(tiporeceitaInserir);

            var tiporeceitaExcluir = new Permissao()
            {
                Nome = "TipoReceita_Excluir",
                Descricao = "Exclui um tipo receita",
                Processo = "Parâmetros - Tipos de Receita",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoReceita_Excluir"))) context.Permissao.AddOrUpdate(tiporeceitaExcluir);


            #endregion

            #region Tipo Pecuária

            var tipopecuariaVisualizar = new Permissao()
            {
                Nome = "TipoPecuaria_Acesso",
                Descricao = "Acesso a tipo pecuária",
                Processo = "Parâmetros - Tipos de Pecuária",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoPecuaria_Acesso"))) context.Permissao.AddOrUpdate(tipopecuariaVisualizar);

            var tipopecuariaEditar = new Permissao()
            {
                Nome = "TipoPecuaria_Editar",
                Descricao = "Editar um tipo pecuária",
                Processo = "Parâmetros - Tipos de Pecuária",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoPecuaria_Editar"))) context.Permissao.AddOrUpdate(tipopecuariaEditar);

            var tipopecuariaInserir = new Permissao()
            {
                Nome = "TipoPecuaria_Inserir",
                Descricao = "Inserir um tipo pecuária",
                Processo = "Parâmetros - Tipos de Pecuária",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoPecuaria_Inserir"))) context.Permissao.AddOrUpdate(tipopecuariaInserir);

            var tipopecuariaExcluir = new Permissao()
            {
                Nome = "TipoPecuaria_Excluir",
                Descricao = "Exclui um tipo pecuária",
                Processo = "Parâmetros - Tipos de Pecuária",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoPecuaria_Excluir"))) context.Permissao.AddOrUpdate(tipopecuariaExcluir);

            #endregion

            #region Área Irrigada

            var areairrigadaVisualizar = new Permissao()
            {
                Nome = "AreaIrrigada_Acesso",
                Descricao = "Acesso Área irrigada",
                Processo = "Parâmetros - Área irrigada",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AreaIrrigada_Acesso"))) context.Permissao.AddOrUpdate(areairrigadaVisualizar);

            var areairrigadaEditar = new Permissao()
            {
                Nome = "AreaIrrigada_Editar",
                Descricao = "Editar Área irrigada",
                Processo = "Parâmetros - Área irrigada",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AreaIrrigada_Editar"))) context.Permissao.AddOrUpdate(areairrigadaEditar);

            var areairrigadaInserir = new Permissao()
            {
                Nome = "AreaIrrigada_Inserir",
                Descricao = "Inserir Área irrigada",
                Processo = "Parâmetros - Área irrigada",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AreaIrrigada_Inserir"))) context.Permissao.AddOrUpdate(areairrigadaInserir);

            var areairrigadaExcluir = new Permissao()
            {
                Nome = "AreaIrrigada_Excluir",
                Descricao = "Excluir Área irrigada",
                Processo = "Parâmetros - Área irrigada",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AreaIrrigada_Excluir"))) context.Permissao.AddOrUpdate(areairrigadaExcluir);

            #endregion

            #region Conceito de Cobrança

            var conceitocobrancaVisualizar = new Permissao()
            {
                Nome = "ConceitoCobranca_Acesso",
                Descricao = "Acesso conceito cobrança",
                Processo = "Parâmetros - Conceitos Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ConceitoCobranca_Acesso"))) context.Permissao.AddOrUpdate(conceitocobrancaVisualizar);

            var conceitocobrancaEditar = new Permissao()
            {
                Nome = "ConceitoCobranca_Editar",
                Descricao = "Editar conceito cobrança",
                Processo = "Parâmetros - Conceitos Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ConceitoCobranca_Editar"))) context.Permissao.AddOrUpdate(conceitocobrancaEditar);

            var conceitocobrancaInserir = new Permissao()
            {
                Nome = "ConceitoCobranca_Inserir",
                Descricao = "Inserir conceito cobrança",
                Processo = "Parâmetros - Conceitos Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ConceitoCobranca_Inserir"))) context.Permissao.AddOrUpdate(conceitocobrancaInserir);

            var conceitocobrancaExcluir = new Permissao()
            {
                Nome = "ConceitoCobranca_Excluir",
                Descricao = "Excluir conceito cobrança",
                Processo = "Parâmetros - Conceitos Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ConceitoCobranca_Excluir"))) context.Permissao.AddOrUpdate(conceitocobrancaExcluir);

            #endregion

            #region Motivo do Abono

            var motivoAbonoVisualizar = new Permissao()
            {
                Nome = "MotivoAbono_Acesso",
                Descricao = "Acesso motivo do abono",
                Processo = "Parâmetros - Motivo do Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoAbono_Acesso"))) context.Permissao.AddOrUpdate(motivoAbonoVisualizar);

            var motivoAbonoEditar = new Permissao()
            {
                Nome = "MotivoAbono_Editar",
                Descricao = "Editar motivo do abono",
                Processo = "Parâmetros - Motivo do Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoAbono_Editar"))) context.Permissao.AddOrUpdate(motivoAbonoEditar);

            var motivoAbonoInserir = new Permissao()
            {
                Nome = "MotivoAbono_Inserir",
                Descricao = "Inserir motivo do abono",
                Processo = "Parâmetros - Motivo do Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoAbono_Inserir"))) context.Permissao.AddOrUpdate(motivoAbonoInserir);

            var motivoAbonoExcluir = new Permissao()
            {
                Nome = "MotivoAbono_Excluir",
                Descricao = "Excluir motivo do abono",
                Processo = "Parâmetros - Motivo do Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoAbono_Excluir"))) context.Permissao.AddOrUpdate(motivoAbonoExcluir);

            #endregion

            #region Motivo de Prorrogação

            var motivoProrrogacaoVisualizar = new Permissao()
            {
                Nome = "MotivoProrrogacao_Acesso",
                Descricao = "Acesso motivo de prorrogação",
                Processo = "Parâmetros - Motivo de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoProrrogacao_Acesso"))) context.Permissao.AddOrUpdate(motivoProrrogacaoVisualizar);

            var motivoProrrogacaoEditar = new Permissao()
            {
                Nome = "MotivoProrrogacao_Editar",
                Descricao = "Editar motivo de prorrogação",
                Processo = "Parâmetros - Motivo de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoProrrogacao_Editar"))) context.Permissao.AddOrUpdate(motivoProrrogacaoEditar);

            var motivoProrrogacaoInserir = new Permissao()
            {
                Nome = "MotivoProrrogacao_Inserir",
                Descricao = "Inserir motivo de prorrogação",
                Processo = "Parâmetros - Motivo de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoProrrogacao_Inserir"))) context.Permissao.AddOrUpdate(motivoProrrogacaoInserir);

            var motivoProrrogacaoExcluir = new Permissao()
            {
                Nome = "MotivoProrrogacao_Excluir",
                Descricao = "Excluir motivo de prorrogação",
                Processo = "Parâmetros - Motivo de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MotivoProrrogacao_Excluir"))) context.Permissao.AddOrUpdate(motivoProrrogacaoExcluir);

            #endregion

            #region Custo Ha Região

            var custoharegiaoVisualizar = new Permissao()
            {
                Nome = "CustoHaRegiao_Acesso",
                Descricao = "Acesso custo ha região",
                Processo = "Parâmetros - Custo Ha Região",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CustoHaRegiao_Acesso"))) context.Permissao.AddOrUpdate(custoharegiaoVisualizar);

            var custoharegiaoEditar = new Permissao()
            {
                Nome = "CustoHaRegiao_Editar",
                Descricao = "Editar custo ha região",
                Processo = "Parâmetros - Custo Ha Região",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CustoHaRegiao_Editar"))) context.Permissao.AddOrUpdate(custoharegiaoEditar);

            var custoharegiaoInserir = new Permissao()
            {
                Nome = "CustoHaRegiao_Inserir",
                Descricao = "Inserir custo ha região",
                Processo = "Parâmetros - Custo Ha Região",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CustoHaRegiao_Inserir"))) context.Permissao.AddOrUpdate(custoharegiaoInserir);

            #endregion

            #region Cultura

            var culturaVisualizar = new Permissao()
            {
                Nome = "Cultura_Acesso",
                Descricao = "Acesso cultura",
                Processo = "Parâmetros - Culturas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cultura_Acesso"))) context.Permissao.AddOrUpdate(culturaVisualizar);

            var culturaEditar = new Permissao()
            {
                Nome = "Cultura_Editar",
                Descricao = "Editar cultura",
                Processo = "Parâmetros - Culturas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cultura_Editar"))) context.Permissao.AddOrUpdate(culturaEditar);

            var culturaInserir = new Permissao()
            {
                Nome = "Cultura_Inserir",
                Descricao = "Inserir cultura",
                Processo = "Parâmetros - Culturas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cultura_Inserir"))) context.Permissao.AddOrUpdate(culturaInserir);

            var culturaExcluir = new Permissao()
            {
                Nome = "Cultura_Excluir",
                Descricao = "Excluir cultura",
                Processo = "Parâmetros - Culturas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cultura_Excluir"))) context.Permissao.AddOrUpdate(culturaExcluir);
            #endregion

            #region Cultura Estado

            var culturaEstadoVisualizar = new Permissao()
            {
                Nome = "CulturaEstado_Acesso",
                Descricao = "Acesso configurações de cultura",
                Processo = "Parâmetros - Culturas por Estado",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CulturaEstado_Acesso"))) context.Permissao.AddOrUpdate(culturaEstadoVisualizar);

            var culturaEstadoEditar = new Permissao()
            {
                Nome = "CulturaEstado_Editar",
                Descricao = "Editar configurações de cultura",
                Processo = "Parâmetros - Culturas por Estado",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CulturaEstado_Editar"))) context.Permissao.AddOrUpdate(culturaEstadoEditar);

            var culturaEstadoInserir = new Permissao()
            {
                Nome = "CulturaEstado_Inserir",
                Descricao = "Inserir configurações de cultura",
                Processo = "Parâmetros - Culturas por Estado",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CulturaEstado_Inserir"))) context.Permissao.AddOrUpdate(culturaEstadoInserir);

            #endregion

            #region Anexo

            var anexoVisualizar = new Permissao()
            {
                Nome = "Anexo_Acesso",
                Descricao = "Acesso Anexo",
                Processo = "Parâmetros - Anexos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Anexo_Acesso"))) context.Permissao.AddOrUpdate(anexoVisualizar);

            var anexoEditar = new Permissao()
            {
                Nome = "Anexo_Editar",
                Descricao = "Acesso Editar",
                Processo = "Parâmetros - Anexos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Anexo_Editar"))) context.Permissao.AddOrUpdate(anexoEditar);

            var anexoInserir = new Permissao()
            {
                Nome = "Anexo_Inserir",
                Descricao = "Acesso Inserir",
                Processo = "Parâmetros - Anexos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Anexo_Inserir"))) context.Permissao.AddOrUpdate(anexoInserir);

            var anexoExcluir = new Permissao()
            {
                Nome = "Anexo_Excluir",
                Descricao = "Acesso Excluir",
                Processo = "Parâmetros - Anexos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Anexo_Excluir"))) context.Permissao.AddOrUpdate(anexoExcluir);

            #endregion

            #region Experiência

            var experienciaVisualizar = new Permissao()
            {
                Nome = "Experiencia_Acesso",
                Descricao = "Acesso experiência",
                Processo = "Parâmetros - Experiência",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Experiencia_Acesso"))) context.Permissao.AddOrUpdate(experienciaVisualizar);

            var experienciaEditar = new Permissao()
            {
                Nome = "Experiencia_Editar",
                Descricao = "Editar experiência",
                Processo = "Parâmetros - Experiência",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Experiencia_Editar"))) context.Permissao.AddOrUpdate(experienciaEditar);

            var experienciaInserir = new Permissao()
            {
                Nome = "Experiencia_Inserir",
                Descricao = "Inserir experiência",
                Processo = "Parâmetros - Experiência",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Experiencia_Inserir"))) context.Permissao.AddOrUpdate(experienciaInserir);

            var experienciaExcluir = new Permissao()
            {
                Nome = "Experiencia_Excluir",
                Descricao = "Excluir experiência",
                Processo = "Parâmetros - Experiência",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Experiencia_Excluir"))) context.Permissao.AddOrUpdate(experienciaExcluir);

            #endregion

            #region Idade média canavial

            var idademediacanavialVisualizar = new Permissao()
            {
                Nome = "IdadeMediaCanavial_Acesso",
                Descricao = "Acesso idade média canavial",
                Processo = "Parâmetros - Idade Média Canavial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("IdadeMediaCanavial_Acesso"))) context.Permissao.AddOrUpdate(idademediacanavialVisualizar);

            var idademediacanavialEditar = new Permissao()
            {
                Nome = "IdadeMediaCanavial_Editar",
                Descricao = "Editar idade média canavial",
                Processo = "Parâmetros - Idade Média Canavial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("IdadeMediaCanavial_Editar"))) context.Permissao.AddOrUpdate(idademediacanavialEditar);

            var idademediacanavialInserir = new Permissao()
            {
                Nome = "IdadeMediaCanavial_Inserir",
                Descricao = "Inserir idade média canavial",
                Processo = "Parâmetros - Idade Média Canavial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("IdadeMediaCanavial_Inserir"))) context.Permissao.AddOrUpdate(idademediacanavialInserir);

            #endregion

            #region Conta Cliente - Cobrança
            
            var cobrancaVisualizar = new Permissao()
            {
                Nome = "Cobranca_Acesso",
                Descricao = "Acesso a Cobrança",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cobranca_Acesso"))) context.Permissao.AddOrUpdate(cobrancaVisualizar);

            var cobrancaEditar = new Permissao()
            {
                Nome = "Cobranca_Editar",
                Descricao = "Editar Cobrança",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cobranca_Editar"))) context.Permissao.AddOrUpdate(cobrancaEditar);

            var cobrancaInserir = new Permissao()
            {
                Nome = "Cobranca_Inserir",
                Descricao = "Inserir Cobrança",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cobranca_Inserir"))) context.Permissao.AddOrUpdate(cobrancaInserir);

            var cobrancaAlterarRestricaoSerasa = new Permissao()
            {
                Nome = "CobrancaAlterarRestricaoSerasa_Editar",
                Descricao = "Alterar Restrição Serasa",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CobrancaAlterarRestricaoSerasa_Editar"))) context.Permissao.AddOrUpdate(cobrancaAlterarRestricaoSerasa);

            var cobrancaIgnorarConceitoCobranca = new Permissao()
            {
                Nome = "CobrancaIgnorarConceitoCobranca_Editar",
                Descricao = "Ignorar Conceito Cobranca",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CobrancaIgnorarConceitoCobranca_Editar"))) context.Permissao.AddOrUpdate(cobrancaIgnorarConceitoCobranca);

            var cobrancaPercentualPdd = new Permissao()
            {
                Nome = "CobrancaPercentualPdd_Editar",
                Descricao = "Alterar Percentual de PDD",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CobrancaPercentualPdd_Editar"))) context.Permissao.AddOrUpdate(cobrancaPercentualPdd);

            var cobrancaAlteraSinistro = new Permissao()
            {
                Nome = "CobrancaAlteraSinistro_Editar",
                Descricao = "Alterar Sinistro",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("CobrancaAlteraSinistro_Editar"))) context.Permissao.AddOrUpdate(cobrancaAlteraSinistro);

            var acessoCompletoCobranca = new Permissao()
            {
                Nome = "Cobranca_Completo",
                Descricao = "Visualizar Dados Completos dos Títulos",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cobranca_Completo"))) context.Permissao.AddOrUpdate(acessoCompletoCobranca);

            var acessoCompletoBotoesCobranca = new Permissao()
            {
                Nome = "Cobranca_Botoes",
                Descricao = "Acesso a todos os botões do Controle de Cobrança, exceto Prorrogar e Abonar",
                Processo = "Conta Cliente - Cobrança",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cobranca_Botoes"))) context.Permissao.AddOrUpdate(acessoCompletoBotoesCobranca);

            #endregion

            #region Média saca
            /*

            var mediasacaVisualizar = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "MediaSaca_Acesso",
                Descricao = "Acesso  média saca",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MediaSaca_Acesso"))) context.Permissao.AddOrUpdate(mediasacaVisualizar);

            var mediasacaEditar = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "MediaSaca_Editar",
                Descricao = "Editar média saca",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MediaSaca_Editar"))) context.Permissao.AddOrUpdate(mediasacaEditar);

            var mediasacaInserir = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "MediaSaca_Inserir",
                Descricao = "Inserir média saca",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("MediaSaca_Inserir"))) context.Permissao.AddOrUpdate(mediasacaInserir);

            var mediasacaExcluir = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "Excluir_Inserir",
                Descricao = "Excluir média saca",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Excluir_Inserir"))) context.Permissao.AddOrUpdate(mediasacaExcluir);
            */

            #endregion

            #region Opções de produtos e serviços

            var opcoesdeprodutoseservicosVisualizar = new Permissao()
            {
                Nome = "OpcoesProdutoServico_Acesso",
                Descricao = "Acesso Opções de produtos e serviços",
                Processo = "Parâmetros - Produtos e Serviços",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OpcoesProdutoServico_Acesso"))) context.Permissao.AddOrUpdate(opcoesdeprodutoseservicosVisualizar);

            var opcoesdeprodutoseservicosEditar = new Permissao()
            {
                Nome = "OpcoesProdutoServico_Editar",
                Descricao = "Editar Opções de produtos e serviços",
                Processo = "Parâmetros - Produtos e Serviços",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OpcoesProdutoServico_Editar"))) context.Permissao.AddOrUpdate(opcoesdeprodutoseservicosEditar);

            var opcoesdeprodutoseservicosInserir = new Permissao()
            {
                Nome = "OpcoesProdutoServico_Inserir",
                Descricao = "Inserir Opções de produtos e serviços",
                Processo = "Parâmetros - Produtos e Serviços",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OpcoesProdutoServico_Inserir"))) context.Permissao.AddOrUpdate(opcoesdeprodutoseservicosInserir);

            var opcoesdeprodutoseservicosExcluir = new Permissao()
            {
                Nome = "OpcoesProdutoServico_Excluir",
                Descricao = "Excluir Opções de produtos e serviços",
                Processo = "Parâmetros - Produtos e Serviços",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OpcoesProdutoServico_Excluir"))) context.Permissao.AddOrUpdate(opcoesdeprodutoseservicosExcluir);

            #endregion

            #region Porcentagem de Quebra

            var porcentagemquebraVisualizar = new Permissao()
            {
                Nome = "PorcentagemQuebra_Acesso",
                Descricao = "Acesso Percentual de Quebra",
                Processo = "Parâmetros - Percentual de Quebra",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PorcentagemQuebra_Acesso"))) context.Permissao.AddOrUpdate(porcentagemquebraVisualizar);

            var porcentagemquebraEditar = new Permissao()
            {
                Nome = "PorcentagemQuebra_Editar",
                Descricao = "Editar Percentual de Quebra",
                Processo = "Parâmetros - Percentual de Quebra",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PorcentagemQuebra_Editar"))) context.Permissao.AddOrUpdate(porcentagemquebraEditar);

            var porcentagemquebraInserir = new Permissao()
            {
                Nome = "PorcentagemQuebra_Inserir",
                Descricao = "Inserir Percentual de Quebra",
                Processo = "Parâmetros - Percentual de Quebra",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PorcentagemQuebra_Inserir"))) context.Permissao.AddOrUpdate(porcentagemquebraInserir);

            #endregion

            #region Produtividade Média

            /*
            var produtividademediaVisualizar = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "ProdutividadeMedia_Acesso",
                Descricao = "Acesso Produtividade Média",
                Processo = "Parâmetros - Percentual de Quebra",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ProdutividadeMedia_Acesso"))) context.Permissao.AddOrUpdate(produtividademediaVisualizar);

            var produtividademediaEditar = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "ProdutividadeMedia_Editar",
                Descricao = "Editar Produtividade Média",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ProdutividadeMedia_Editar"))) context.Permissao.AddOrUpdate(produtividademediaEditar);

            var produtividademediaInserir = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "ProdutividadeMedia_Inserir",
                Descricao = "Inserir Produtividade Média",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ProdutividadeMedia_Inserir"))) context.Permissao.AddOrUpdate(produtividademediaInserir);

            var produtividademediaExcluir = new Permissao()
            {
                ID = Guid.NewGuid(),
                Nome = "ProdutividadeMedia_Excluir",
                Descricao = "Exclui Produtividade Média",
                Ativo = true,
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ProdutividadeMedia_Inserir"))) context.Permissao.AddOrUpdate(produtividademediaExcluir);
            */

            #endregion

            #region Segmento

            var segmentoVisualizar = new Permissao()
            {
                Nome = "Segmento_Acesso",
                Descricao = "Acesso Segmento",
                Processo = "Parâmetros - Segmentos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Segmento_Acesso"))) context.Permissao.AddOrUpdate(segmentoVisualizar);

            var segmentoEditar = new Permissao()
            {
                Nome = "Segmento_Editar",
                Descricao = "Editar Segmento",
                Processo = "Parâmetros - Segmentos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Segmento_Editar"))) context.Permissao.AddOrUpdate(segmentoEditar);

            var segmentoInserir = new Permissao()
            {
                Nome = "Segmento_Inserir",
                Descricao = "Inserir Segmento",
                Processo = "Parâmetros - Segmentos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Segmento_Inserir"))) context.Permissao.AddOrUpdate(segmentoInserir);

            var segmentoExcluir = new Permissao()
            {
                Nome = "Segmento_Excluir",
                Descricao = "Excluir Segmento",
                Processo = "Parâmetros - Segmentos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Segmento_Excluir"))) context.Permissao.AddOrUpdate(segmentoExcluir);

            #endregion

            #region Logs

            var logsVisualizar = new Permissao()
            {
                Nome = "Log_Acesso",
                Descricao = "Acesso Logs",
                Processo = "Logs Gerais",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Log_Acesso"))) context.Permissao.AddOrUpdate(logsVisualizar);



            #endregion

            #region Fluxo de Limite de Credito

            var fluxoLimiteVisualizar = new Permissao()
            {
                Nome = "FluxoLimite_Acesso",
                Descricao = "Acesso Fluxo Liberação Manual",
                Processo = "Parâmetros - Fluxo Liberação Manual",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLimite_Acesso"))) context.Permissao.AddOrUpdate(fluxoLimiteVisualizar);


            var fluxoLimiteVisualizarInserir = new Permissao()
            {
                Nome = "FluxoLimite_Inserir",
                Descricao = "Inserir Fluxo Liberação Manual",
                Processo = "Parâmetros - Fluxo Liberação Manual",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLimite_Inserir"))) context.Permissao.AddOrUpdate(fluxoLimiteVisualizarInserir);

            var fluxoLimiteVisualizarExcluir = new Permissao()
            {
                Nome = "FluxoLimite_Excluir",
                Descricao = "Excluir Fluxo Liberação Manual",
                Processo = "Parâmetros - Fluxo Liberação Manual",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLimite_Excluir"))) context.Permissao.AddOrUpdate(fluxoLimiteVisualizarExcluir);

            #endregion

            #region Fluxo de Liberação Limite de Credito

            var fluxoLiberacaoLimiteVisualizar = new Permissao()
            {
                Nome = "FluxoLiberacaoLimite_Acesso",
                Descricao = "Acesso Fluxo de Liberação de Limite de Crédito",
                Processo = "Parâmetros - Fluxo Liberação Limite de Crédito",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoLimite_Acesso"))) context.Permissao.AddOrUpdate(fluxoLiberacaoLimiteVisualizar);


            var fluxoLiberacaoLimiteVisualizarInserir = new Permissao()
            {
                Nome = "FluxoLiberacaoLimite_Inserir",
                Descricao = "Inserir Fluxo de Libeacao de Limite de Crédito",
                Processo = "Parâmetros - Fluxo Liberação Limite de Crédito",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoLimite_Inserir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoLimiteVisualizarInserir);

            var fluxoLiberacaoLimiteVisualizarExcluir = new Permissao()
            {
                Nome = "FluxoLiberacaoLimite_Excluir",
                Descricao = "Excluir um Fluxo de Limite de Crédito",
                Processo = "Parâmetros - Fluxo Liberação Limite de Crédito",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoLimite_Excluir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoLimiteVisualizarExcluir);

            #endregion

            #region Ordens de Vendas

            var ordem = new Permissao()
            {
                Nome = "OrdemVenda_Acesso",
                Descricao = "Acesso Ordens de Vendas",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OrdemVenda_Acesso"))) context.Permissao.AddOrUpdate(ordem);

            var ordemBloqueio = new Permissao()
            {
                Nome = "OrdemVenda_BloqueioManual",
                Descricao = "Bloqueio Manual de Ordens de Vendas",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OrdemVenda_BloqueioManual"))) context.Permissao.AddOrUpdate(ordemBloqueio);

            var ordemLiberacao = new Permissao()
            {
                Nome = "OrdemVenda_LiberacaoManual",
                Descricao = "Liberação Manual de Ordens de Vendas",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OrdemVenda_LiberacaoManual"))) context.Permissao.AddOrUpdate(ordemLiberacao);

            var ordemAlteraStatusFluxo = new Permissao()
            {
                Nome = "OrdemVenda_AlteraStatusFluxo",
                Descricao = "Altera o Status do Fluxo",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OrdemVenda_AlteraStatusFluxo"))) context.Permissao.AddOrUpdate(ordemAlteraStatusFluxo);

            var ordemAprivacaoFluxo = new Permissao()
            {
                Nome = "OrdemVenda_AprovacaoFluxo",
                Descricao = "Aprovar Remessa de uma ordem",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OrdemVenda_AprovacaoFluxo"))) context.Permissao.AddOrUpdate(ordemAprivacaoFluxo);


            var ordemBloqueioFluxo = new Permissao()
            {
                Nome = "OrdemVenda_BloqueioFluxo",
                Descricao = "Bloquear Remessa de uma ordem",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("OrdemVenda_BloqueioFluxo"))) context.Permissao.AddOrUpdate(ordemBloqueioFluxo);

            #endregion

            #region Divisao Remessa

            var divisao = new Permissao()
            {
                Nome = "AcompanhamentoOrdem_CockPit",
                Descricao = "Acompanhamento fluxo divisões de remessa",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AcompanhamentoOrdem_CockPit"))) context.Permissao.AddOrUpdate(divisao);

            var divisaoAcesso = new Permissao()
            {
                Nome = "DivisaoRemessa_Acesso",
                Descricao = "Acesso de divisões de remessa Detalhes",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("DivisaoRemessa_Acesso"))) context.Permissao.AddOrUpdate(divisaoAcesso);

            var divisaoSolicitacaoLiberacao = new Permissao()
            {
                Nome = "DivisaoRemessa_SolicitacaoLiberacao",
                Descricao = "Solicitação de liberação de remessa",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("DivisaoRemessa_SolicitacaoLiberacao"))) context.Permissao.AddOrUpdate(divisaoSolicitacaoLiberacao);

            var divisaoLog = new Permissao()
            {
                Nome = "DivisaoRemessa_Log",
                Descricao = "Visualiza Logs da liberação de remessa",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("DivisaoRemessa_Log"))) context.Permissao.AddOrUpdate(divisaoLog);
            var divisaoLiberar = new Permissao()
            {
                Nome = "DivisaoRemessa_Liberar",
                Descricao = "Liberação de remessa Manual",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("DivisaoRemessa_Liberar"))) context.Permissao.AddOrUpdate(divisaoLiberar);

            var divisaoBloquear = new Permissao()
            {
                Nome = "DivisaoRemessa_Bloquear",
                Descricao = "Liberação de remessa Manual",
                Processo = "Ordens de Vendas",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("DivisaoRemessa_Bloquear"))) context.Permissao.AddOrUpdate(divisaoBloquear);

            #endregion

            #region Estrutura Comercial - Diretoria

            var diretoriasacesso = new Permissao()
            {
                Nome = "EstruturaComercialDiretoria_Acesso",
                Descricao = "Acesso ao Cadastro de Diretorias",
                Processo = "Parâmetros - Diretoria",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("EstruturaComercialDiretoria_Acesso"))) context.Permissao.AddOrUpdate(diretoriasacesso);


            var diretoriaseditar = new Permissao()
            {
                Nome = "EstruturaComercialDiretoria_Editar",
                Descricao = "Editar Cadastro de Diretorias",
                Processo = "Parâmetros - Diretoria",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("EstruturaComercialDiretoria_Editar"))) context.Permissao.AddOrUpdate(diretoriaseditar);

            var diretoriasinsert = new Permissao()
            {
                Nome = "EstruturaComercialDiretoria_Inserir",
                Descricao = "Inserir Cadastro de Diretorias",
                Processo = "Parâmetros - Diretoria",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("EstruturaComercialDiretoria_Inserir"))) context.Permissao.AddOrUpdate(diretoriasinsert);

            #endregion

            #region Tipo de endividamento

            var tipoendividamentoacesso = new Permissao()
            {
                Nome = "TipoEndividamento_Acesso",
                Descricao = "Acesso ao Cadastro de Tipo de Endividamento",
                Processo = "Parâmetros - Tipo de Endividamento",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoEndividamento_Acesso"))) context.Permissao.AddOrUpdate(tipoendividamentoacesso);


            var tipoendividamentoeditar = new Permissao()
            {
                Nome = "TipoEndividamento_Editar",
                Descricao = "Editar Cadastro de Tipo de Endividamento",
                Processo = "Parâmetros - Tipo de Endividamento",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoEndividamento_Editar"))) context.Permissao.AddOrUpdate(tipoendividamentoeditar);

            var tipoendividamentoinserir = new Permissao()
            {
                Nome = "TipoEndividamento_Inserir",
                Descricao = "Inserir Cadastro de Tipo de Endividamento",
                Processo = "Parâmetros - Tipo de Endividamento",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoEndividamento_Inserir"))) context.Permissao.AddOrUpdate(tipoendividamentoinserir);

            #endregion

            #region Historico Conta Cliente
            var historicocontacliente = new Permissao()
            {
                Nome = "ContaCliente_Historico",
                Descricao = "Visualiza Historico da Conta Cliente",
                Processo = "Histórico do Cliente",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ContaCliente_Historico"))) context.Permissao.AddOrUpdate(historicocontacliente);
            #endregion

            #region Tipo Relação Grupo Economico
            var tiporelacao01 = new Permissao()
            {
                Nome = "TipoRelacaoGrupoEconomico_Visualizar",
                Descricao = "Visualiza Tipo de Relação de um Grupo Econômico",
                Processo = "Parâmetros - Tipo de Relação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoRelacaoGrupoEconomico_Visualizar"))) context.Permissao.AddOrUpdate(tiporelacao01);

            var tiporelacao02 = new Permissao()
            {
                Nome = "TipoRelacaoGrupoEconomico_Editar",
                Descricao = "Edita um Tipo de Relação de um Grupo Econômico",
                Processo = "Parâmetros - Tipo de Relação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoRelacaoGrupoEconomico_Editar"))) context.Permissao.AddOrUpdate(tiporelacao02);

            var tiporelacao03 = new Permissao()
            {
                Nome = "TipoRelacaoGrupoEconomico_Inserir",
                Descricao = "Insere um Tipo de Relação de um Grupo Econômico",
                Processo = "Parâmetros - Tipo de Relação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("TipoRelacaoGrupoEconomico_Inserir"))) context.Permissao.AddOrUpdate(tiporelacao03);
            #endregion

            #region Classificação Grupo Economico
            var tipoclassificacao = new Permissao()
            {
                Nome = "ClassificacaoGrupoEconomico_Visualizar",
                Descricao = "Visualiza a Classificação de Grupo Econômico",
                Processo = "Parâmetros - Classificações de Grupo Econômico",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ClassificacaoGrupoEconomico_Visualizar"))) context.Permissao.AddOrUpdate(tipoclassificacao);
            #endregion

            #region Fluxo de Grupo Economico

            var fluxogrupoEconomico = new Permissao()
            {
                Nome = "FluxoGrupoEconomico_Inserir",
                Descricao = "Insere o Fluxo de Grupo Economico",
                Processo = "Parâmetros - Fluxo de Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoGrupoEconomico_Inserir"))) context.Permissao.AddOrUpdate(fluxogrupoEconomico);

            var fluxogrupoEconomico1 = new Permissao()
            {
                Nome = "FluxoGrupoEconomico_Acesso",
                Descricao = "Visualiza o Fluxo de Grupo Economico",
                Processo = "Parâmetros - Fluxo de Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoGrupoEconomico_Acesso"))) context.Permissao.AddOrUpdate(fluxogrupoEconomico1);

            var fluxogrupoEconomico2 = new Permissao()
            {
                Nome = "FluxoGrupoEconomico_Editar",
                Descricao = "Edita o Fluxo de Grupo Economico",
                Processo = "Parâmetros - Fluxo de Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoGrupoEconomico_Editar"))) context.Permissao.AddOrUpdate(fluxogrupoEconomico2);

            var fluxogrupoEconomico3 = new Permissao()
            {
                Nome = "FluxoGrupoEconomico_Excluir",
                Descricao = "Excluir Fluxo de Grupo Economico",
                Processo = "Parâmetros - Fluxo de Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoGrupoEconomico_Excluir"))) context.Permissao.AddOrUpdate(fluxogrupoEconomico3);

            #endregion

            #region Grupo Economico

            var grupoEconomico = new Permissao()
            {
                Nome = "GrupoEconomico_Inserir",
                Descricao = "Insere Grupo Economico",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomico_Inserir"))) context.Permissao.AddOrUpdate(grupoEconomico);

            var grupoEconomico1 = new Permissao()
            {
                Nome = "GrupoEconomico_Acesso",
                Descricao = "Visualiza Grupo Economico",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomico_Acesso"))) context.Permissao.AddOrUpdate(grupoEconomico1);

            var grupoEconomico2 = new Permissao()
            {
                Nome = "GrupoEconomico_Editar",
                Descricao = "Editar Grupo Economico",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomico_Editar"))) context.Permissao.AddOrUpdate(grupoEconomico2);

            var grupoEconomico3 = new Permissao()
            {
                Nome = "GrupoEconomico_Excluir",
                Descricao = "Excluir Grupo Economico",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomico_Excluir"))) context.Permissao.AddOrUpdate(grupoEconomico3);

            #endregion

            #region Grupo Economico Membro

            var grupoEconomicoMembro = new Permissao()
            {
                Nome = "GrupoEconomicoMembros_Inserir",
                Descricao = "Insere Grupo Economico Membro",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomicoMembros_Inserir"))) context.Permissao.AddOrUpdate(grupoEconomicoMembro);

            var grupoEconomicoMembro1 = new Permissao()
            {
                Nome = "GrupoEconomicoMembros_Acesso",
                Descricao = "Visualiza Grupo Economico Membro",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomicoMembros_Acesso"))) context.Permissao.AddOrUpdate(grupoEconomicoMembro1);

            var grupoEconomicoMembro2 = new Permissao()
            {
                Nome = "GrupoEconomicoMembros_Editar",
                Descricao = "Edita Grupo Economico Membro",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomicoMembros_Editar"))) context.Permissao.AddOrUpdate(grupoEconomicoMembro2);

            var grupoEconomicoMembro3 = new Permissao()
            {
                Nome = "GrupoEconomicoMembros_Excluir",
                Descricao = "Excluir Grupo Economico Membro",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("GrupoEconomicoMembros_Excluir"))) context.Permissao.AddOrUpdate(grupoEconomicoMembro3);

            #endregion

            #region Estrutura de Perfil de Usuarios

            var estruturaPerfil = new Permissao()
            {
                Nome = "EstruturaPerfilUsuario_Acesso",
                Descricao = "Acesso na Estrutura de Perfil de Usuarios",
                Processo = "Parâmetros - Usuário x Perfil",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("EstruturaPerfilUsuario_Acesso"))) context.Permissao.AddOrUpdate(estruturaPerfil);

            var estruturaPerfil1 = new Permissao()
            {
                Nome = "EstruturaPerfilUsuario_Inserir",
                Descricao = "Inserir Estrutura de Perfil de Usuarios",
                Processo = "Parâmetros - Usuário x Perfil",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("EstruturaPerfilUsuario_Inserir"))) context.Permissao.AddOrUpdate(estruturaPerfil1);

            var estruturaPerfil2 = new Permissao()
            {
                Nome = "EstruturaPerfilUsuario_Editar",
                Descricao = "Editar Estrutura de Perfil de Usuarios",
                Processo = "Parâmetros - Usuário x Perfil",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("EstruturaPerfilUsuario_Editar"))) context.Permissao.AddOrUpdate(estruturaPerfil2);

            #endregion

            #region Cookpit
            var cookpitgrupoeconomico = new Permissao()
            {
                Nome = "Cookpit_GrupoEconomico",
                Descricao = "Visualiza Cookpit Grupo Econômico",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Cookpit_GrupoEconomico"))) context.Permissao.AddOrUpdate(cookpitgrupoeconomico);
            #endregion

            #region Fluxo de Grupos Economicos

            var fluxoGrupoEconomico = new Permissao()
            {
                Nome = "fluxoGrupoEconomico_AprovaReprovar",
                Descricao = "Aprovar ou Reprovar Fluxo Grupo Economico",
                Processo = "Grupos Econômicos",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("fluxoGrupoEconomico_AprovaReprovar"))) context.Permissao.AddOrUpdate(fluxoGrupoEconomico);

            #endregion

            #region LimiteCredito

            var fixarLimiteCredito = new Permissao()
            {
                Nome = "LimiteCredito_FixarLimite",
                Descricao = "Fixar Limite de Crédito",
                Processo = "Limite de Crédito",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("LimiteCredito_FixarLimite"))) context.Permissao.AddOrUpdate(fixarLimiteCredito);

            #endregion
        
            #region Fluxo OrdemVenda
            
            var fluxoLiberacaoOrdemVisualizar = new Permissao()
            {
                Nome = "FluxoLiberacaoOrdemVenda_Visualizar",
                Descricao = "Acesso Fluxo de Liberação de Ordem de Venda",
                Processo = "Parâmetros - Fluxo Liberação Ordem de Venda",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoOrdemVenda_Visualizar"))) context.Permissao.AddOrUpdate(fluxoLiberacaoOrdemVisualizar);

            var fluxoLiberacaoOrdemEditar = new Permissao()
            {
                Nome = "FluxoLiberacaoOrdemVenda_Editar",
                Descricao = "Editar Fluxo de Liberação de Ordem de Venda",
                Processo = "Parâmetros - Fluxo Liberação Ordem de Venda",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoOrdemVenda_Editar"))) context.Permissao.AddOrUpdate(fluxoLiberacaoOrdemEditar);


            var fluxoLiberacaoOrdemInserir = new Permissao()
            {
                Nome = "FluxoLiberacaoOrdemVenda_Inserir",
                Descricao = "Inserir Fluxo de Libeacao de Ordem de Venda",
                Processo = "Parâmetros - Fluxo Liberação Ordem de Venda",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoOrdemVenda_Inserir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoOrdemInserir);

            var fluxoLiberacaoOrdemExcluir = new Permissao()
            {
                Nome = "FluxoLiberacaoOrdemVenda_Excluir",
                Descricao = "Excluir um Fluxo de Ordem de Venda",
                Processo = "Parâmetros - Fluxo Liberação Ordem de Venda",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoOrdemVenda_Excluir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoOrdemExcluir);
            #endregion

            #region Fluxo Abono

            var fluxoLiberacaoAbonoVisualizar = new Permissao()
            {
                Nome = "FluxoLiberacaoAbono_Visualizar",
                Descricao = "Acesso Fluxo de Liberação de Abono",
                Processo = "Parâmetros - Fluxo Liberação Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoAbono_Visualizar"))) context.Permissao.AddOrUpdate(fluxoLiberacaoAbonoVisualizar);

            var fluxoLiberacaoAbonoEditar = new Permissao()
            {
                Nome = "FluxoLiberacaoAbono_Editar",
                Descricao = "Editar Fluxo de Liberação de Abono",
                Processo = "Parâmetros - Fluxo Liberação Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoAbono_Editar"))) context.Permissao.AddOrUpdate(fluxoLiberacaoAbonoEditar);


            var fluxoLiberacaoAbonoInserir = new Permissao()
            {
                Nome = "FluxoLiberacaoAbono_Inserir",
                Descricao = "Inserir Fluxo de Liberação de Abono",
                Processo = "Parâmetros - Fluxo Liberação Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoAbono_Inserir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoAbonoInserir);

            var fluxoLiberacaoAbonoExcluir = new Permissao()
            {
                Nome = "FluxoLiberacaoAbono_Excluir",
                Descricao = "Excluir um Fluxo de Abono",
                Processo = "Parâmetros - Fluxo Liberação Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoAbono_Excluir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoAbonoExcluir);
            #endregion

            #region Abono

            var AbonoAuto = new Permissao()
            {
                Nome = "Abono_Automatico",
                Descricao = "Abonar Automáticamente",
                Processo = "Proposta de Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Abono_Automatico"))) context.Permissao.AddOrUpdate(AbonoAuto);
            var AbonoProposta = new Permissao()
            {
                Nome = "Abono_Inserir",
                Descricao = "Criar proposta de Abono",
                Processo = "Proposta de Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Abono_Inserir"))) context.Permissao.AddOrUpdate(AbonoProposta);

            var AbonoPropostaCancelar = new Permissao()
            {
                Nome = "Abono_Cancelar",
                Descricao = "Cancelar proposta de Abono",
                Processo = "Proposta de Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Abono_Cancelar"))) context.Permissao.AddOrUpdate(AbonoPropostaCancelar);


            var AbonoPropostaAprovador= new Permissao()
            {
                Nome = "Abono_Aprovador",
                Descricao = "Aprovar ou Reprovar uma proposta de Abono no comite",
                Processo = "Proposta de Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Abono_Aprovador"))) context.Permissao.AddOrUpdate(AbonoPropostaAprovador);

            var AbonoPropostaCobranca = new Permissao()
            {
                Nome = "Abono_Cobranca",
                Descricao = "Envia proposta de Abono para cobrança",
                Processo = "Proposta de Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Abono_Cobranca"))) context.Permissao.AddOrUpdate(AbonoPropostaCobranca);

            var AbonoPropostaCobrancaComite = new Permissao()
            {
                Nome = "Abono_CobrancaComite",
                Descricao = "Envia proposta de Abono para comite",
                Processo = "Proposta de Abono",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Abono_CobrancaComite"))) context.Permissao.AddOrUpdate(AbonoPropostaCobrancaComite);

            #endregion

            #region Prorrogacao

            var ProrrogacaoProposta = new Permissao()
            {
                Nome = "Prorrogacao_Inserir",
                Descricao = "Criar proposta",
                Processo = "Proposta de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Prorrogacao_Inserir"))) context.Permissao.AddOrUpdate(ProrrogacaoProposta);

            var ProrrogacaoCobrancaEditar = new Permissao()
            {
                Nome = "Prorrogacao_Editar",
                Descricao = "Editar uma proposta",
                Processo = "Proposta de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Prorrogacao_Editar"))) context.Permissao.AddOrUpdate(ProrrogacaoCobrancaEditar);

            var ProrrogacaoCobranca = new Permissao()
            {
                Nome = "Prorrogacao_Cobranca",
                Descricao = "Envia proposta para cobrança",
                Processo = "Proposta de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Prorrogacao_Cobranca"))) context.Permissao.AddOrUpdate(ProrrogacaoCobranca);

            var ProrrogacaoComite= new Permissao()
            {
                Nome = "Prorrogacao_CobrancaComite",
                Descricao = "Envia proposta para comite",
                Processo = "Proposta de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Prorrogacao_CobrancaComite"))) context.Permissao.AddOrUpdate(ProrrogacaoComite);

            var ProrrogacaoAprovaReprovaComite = new Permissao()
            {
                Nome = "Prorrogacao_Comite",
                Descricao = "Aprovar ou Reprovar uma proposta no comite",
                Processo = "Proposta de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Prorrogacao_Comite"))) context.Permissao.AddOrUpdate(ProrrogacaoAprovaReprovaComite);

            var ProrrogacaoCancelar = new Permissao()
            {
                Nome = "Prorrogacao_Cancelar",
                Descricao = "Cancelar proposta de Prorrogação",
                Processo = "Proposta de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Prorrogacao_Cancelar"))) context.Permissao.AddOrUpdate(ProrrogacaoCancelar);

            var ProrrogacaoEfetivar = new Permissao()
            {
                Nome = "Prorrogacao_Efetivar",
                Descricao = "Efetivar proposta de Prorrogação",
                Processo = "Proposta de Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Prorrogacao_Efetivar"))) context.Permissao.AddOrUpdate(ProrrogacaoEfetivar);

            #endregion

            #region Fluxo Prorrogacao

            var fluxoLiberacaoProrrogacaoVisualizar = new Permissao()
            {
                Nome = "FluxoLiberacaoProrrogacao_Visualizar",
                Descricao = "Acesso Fluxo de Liberação de Prorrogação",
                Processo = "Parâmetros - Fluxo Liberação Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoProrrogacao_Visualizar"))) context.Permissao.AddOrUpdate(fluxoLiberacaoProrrogacaoVisualizar);

            var fluxoLiberacaoProrrogacaoEditar = new Permissao()
            {
                Nome = "FluxoLiberacaoProrrogacao_Editar",
                Descricao = "Editar Fluxo de Liberação de Prorrogação",
                Processo = "Parâmetros - Fluxo Liberação Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoProrrogacao_Editar"))) context.Permissao.AddOrUpdate(fluxoLiberacaoProrrogacaoEditar);


            var fluxoLiberacaoProrrogacaoInserir = new Permissao()
            {
                Nome = "FluxoLiberacaoProrrogacao_Inserir",
                Descricao = "Inserir Fluxo de Liberação de Prorrogação",
                Processo = "Parâmetros - Fluxo Liberação Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoProrrogacao_Inserir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoProrrogacaoInserir);

            var fluxoLiberacaoProrrogacaoExcluir = new Permissao()
            {
                Nome = "FluxoLiberacaoProrrogacao_Excluir",
                Descricao = "Excluir um Fluxo de Prorrogação",
                Processo = "Parâmetros - Fluxo Liberação Prorrogação",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoLiberacaoProrrogacao_Excluir"))) context.Permissao.AddOrUpdate(fluxoLiberacaoProrrogacaoExcluir);
            #endregion

            #region Garantias
            
            var garantiaVisualizar = new Permissao()
            {
                Nome = "Garantias_Visualizar",
                Descricao = "Visualizar Garantias do Cliente",
                Processo = "Conta Cliente - Garantias",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Garantias_Visualizar"))) context.Permissao.AddOrUpdate(garantiaVisualizar);

            var garantiaEditar = new Permissao()
            {
                Nome = "Garantias_Editar",
                Descricao = "Editar Garantias do Cliente",
                Processo = "Conta Cliente - Garantias",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Garantias_Editar"))) context.Permissao.AddOrUpdate(garantiaEditar);

            var garantiaInserir = new Permissao()
            {
                Nome = "Garantias_Inserir",
                Descricao = "Inserir Garantias do Cliente",
                Processo = "Conta Cliente - Garantias",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("Garantias_Inserir"))) context.Permissao.AddOrUpdate(garantiaInserir);

            #endregion

            #region Alcada Comercial

            var alcadaComercialVisualizar = new Permissao()
            {
                Nome = "AlcadaComl_Visualizar",
                Descricao = "Visualizar Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_Visualizar"))) context.Permissao.AddOrUpdate(alcadaComercialVisualizar);

            var alcadaComercialEditar = new Permissao()
            {
                Nome = "AlcadaComl_Editar",
                Descricao = "Editar Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_Editar"))) context.Permissao.AddOrUpdate(alcadaComercialEditar);

            var alcadaComercialEnviaCTC = new Permissao()
            {
                Nome = "AlcadaComl_EnviaCTC",
                Descricao = "Enviar ao CTC uma Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_EnviaCTC"))) context.Permissao.AddOrUpdate(alcadaComercialEnviaCTC);
            //var alcadaComercialEnviaCredito = new Permissao()
            //{
            //    Nome = "AlcadaComl_EnviaCredito",
            //    Descricao = "Enviar ao Crédito uma Alçada Comercial",
            //    Processo = "Alçada Comercial",
            //    Ativo = true,
            //};
            //if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_EnviaCredito"))) context.Permissao.AddOrUpdate(alcadaComercialEnviaCredito);

            var alcadaComercialCriar = new Permissao()
            {
                Nome = "AlcadaComl_Criar",
                Descricao = "Criar Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_Criar"))) context.Permissao.AddOrUpdate(alcadaComercialCriar);

            var alcadaComercialAprovar = new Permissao()
            {
                Nome = "AlcadaComl_Aprovar",
                Descricao = "Aprovar Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_Aprovar"))) context.Permissao.AddOrUpdate(alcadaComercialAprovar);

            var alcadaComercialEncerrar = new Permissao()
            {
                Nome = "AlcadaComl_Encerrar",
                Descricao = "Encerrar Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_Encerrar"))) context.Permissao.AddOrUpdate(alcadaComercialEncerrar);

            var alcadaComercialRejeitar = new Permissao()
            {
                Nome = "AlcadaComl_Rejeitar",
                Descricao = "Rejeitar Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_Rejeitar"))) context.Permissao.AddOrUpdate(alcadaComercialRejeitar);

            var alcadaComercialFixar = new Permissao()
            {
                Nome = "AlcadaComl_Fixar",
                Descricao = "Aprovar Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_Fixar"))) context.Permissao.AddOrUpdate(alcadaComercialFixar);

            var alcadaComercialSolAprovacao = new Permissao()
            {
                Nome = "AlcadaComl_SolicitarAprovacao",
                Descricao = "Solicitar Aprovação Alçada Comercial",
                Processo = "Alçada Comercial",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("AlcadaComl_SolicitarAprovacao"))) context.Permissao.AddOrUpdate(alcadaComercialSolAprovacao);

            #endregion

            #region Fluxo de Renovação Vigência LC

            var fluxorenovacaoVigenciaLC = new Permissao()
            {
                Nome = "FluxoRenovacaoVigenciaLC_Acesso",
                Descricao = "Visualizar o Fluxo de Renovação de Vigência de LC",
                Processo = "Parâmetros - Fluxo de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoRenovacaoVigenciaLC_Acesso"))) context.Permissao.AddOrUpdate(fluxorenovacaoVigenciaLC);

            var fluxorenovacaoVigenciaLC1 = new Permissao()
            {
                Nome = "FluxoRenovacaoVigenciaLC_Editar",
                Descricao = "Editar o Fluxo de Renovação de Vigência de LC",
                Processo = "Parâmetros - Fluxo de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoRenovacaoVigenciaLC_Editar"))) context.Permissao.AddOrUpdate(fluxorenovacaoVigenciaLC1);

            var fluxorenovacaoVigenciaLC2 = new Permissao()
            {
                Nome = "FluxoRenovacaoVigenciaLC_Excluir",
                Descricao = "Excluir o Fluxo de Renovação de Vigência de LC",
                Processo = "Parâmetros - Fluxo de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoRenovacaoVigenciaLC_Excluir"))) context.Permissao.AddOrUpdate(fluxorenovacaoVigenciaLC2);

            var fluxorenovacaoVigenciaLC3 = new Permissao()
            {
                Nome = "FluxoRenovacaoVigenciaLC_Inserir",
                Descricao = "Inserir o Fluxo de Renovação de Vigência de LC",
                Processo = "Parâmetros - Fluxo de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("FluxoRenovacaoVigenciaLC_Inserir"))) context.Permissao.AddOrUpdate(fluxorenovacaoVigenciaLC3);

            #endregion

            #region Proposta de Renovação Vigência LC

            var propostarenovacaoVigenciaLC = new Permissao()
            {
                Nome = "PropostaRenovacaoLimite_Acesso",
                Descricao = "Acessar Proposta de Renovação de Vigência",
                Processo = "Proposta de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PropostaRenovacaoLimite_Acesso"))) context.Permissao.AddOrUpdate(propostarenovacaoVigenciaLC);

            var propostarenovacaoVigenciaLC1 = new Permissao()
            {
                Nome = "PropostaRenovacaoLimite_Inserir",
                Descricao = "Criar Proposta de Renovação de Vigência",
                Processo = "Proposta de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PropostaRenovacaoLimite_Inserir"))) context.Permissao.AddOrUpdate(propostarenovacaoVigenciaLC1);

            var propostarenovacaoVigenciaLC2 = new Permissao()
            {
                Nome = "PropostaRenovacaoLimite_Editar",
                Descricao = "Editar Proposta de Renovação de Vigência",
                Processo = "Proposta de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PropostaRenovacaoLimite_Editar"))) context.Permissao.AddOrUpdate(propostarenovacaoVigenciaLC2);

            var propostarenovacaoVigenciaLC3 = new Permissao()
            {
                Nome = "PropostaRenovacaoLimite_Aprovar",
                Descricao = "Aprovar Proposta de Renovação de Vigência",
                Processo = "Proposta de Renovação de Vigência de LC",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("PropostaRenovacaoLimite_Aprovar"))) context.Permissao.AddOrUpdate(propostarenovacaoVigenciaLC3);

            #endregion

            #region Edicao de Limite de Crédito

            var edicaoLC = new Permissao()
            {
                Nome = "EditarLC",
                Descricao = "Editar Limite de Crédito",
                Processo = "Conta Cliente - Financeiro",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("EditarLC"))) context.Permissao.AddOrUpdate(edicaoLC);

            #endregion

            #region Download de Logs Arquivados

            var logArquivamentoAcessar = new Permissao()
            {
                Nome = "logArquivamento_Acesso",
                Descricao = "Acesso Logs Arquivados",
                Processo = "Logs Gerais",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("logArquivamento_Acesso"))) context.Permissao.AddOrUpdate(logArquivamentoAcessar);

            #endregion

            #region Resumo de Análises

            var resumoAnalises_Visualizar = new Permissao()
            {
                Nome = "ResumoAnalises_Visualizar",
                Descricao = "Visualizar Aba Resumo de Análises",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_Visualizar"))) context.Permissao.AddOrUpdate(resumoAnalises_Visualizar);

            var resumoAnalises_LeadtimePreAnalise = new Permissao()
            {
                Nome = "ResumoAnalises_LeadtimePreAnalise",
                Descricao = "Visualizar Leadtime Médio Geral de Pré-Análise",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_LeadtimePreAnalise"))) context.Permissao.AddOrUpdate(resumoAnalises_LeadtimePreAnalise);

            var resumoAnalises_LeadtimeAnalise = new Permissao()
            {
                Nome = "ResumoAnalises_LeadtimeAnalise",
                Descricao = "Visualizar Leadtime Médio Geral de Análise",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_LeadtimeAnalise"))) context.Permissao.AddOrUpdate(resumoAnalises_LeadtimeAnalise);
            
            var resumoAnalises_PropostasEmPreAnalise = new Permissao()
            {
                Nome = "ResumoAnalises_PropostasEmPreAnalise",
                Descricao = "Visualizar Quantidade de Propostas em Pré-Análise",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_PropostasEmPreAnalise"))) context.Permissao.AddOrUpdate(resumoAnalises_PropostasEmPreAnalise);
                        
            var resumoAnalises_PropostasEnviadasPreAnalise = new Permissao()
            {
                Nome = "ResumoAnalises_PropostasEnviadasPreAnalise",
                Descricao = "Visualizar Quantidade de Propostas Enviadas para Pré-Análise",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_PropostasEnviadasPreAnalise"))) context.Permissao.AddOrUpdate(resumoAnalises_PropostasEnviadasPreAnalise);
                        
            var resumoAnalises_PropostasEmAnalise = new Permissao()
            {
                Nome = "ResumoAnalises_PropostasEmAnalise",
                Descricao = "Visualizar Quantidade de Propostas em Análise",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_PropostasEmAnalise"))) context.Permissao.AddOrUpdate(resumoAnalises_PropostasEmAnalise);
            
            var resumoAnalises_PropostasEnviadasAnalise = new Permissao()
            {
                Nome = "ResumoAnalises_PropostasEnviadasAnalise",
                Descricao = "Visualizar Quantidade de Propostas Enviadas para Análise",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_PropostasEnviadasAnalise"))) context.Permissao.AddOrUpdate(resumoAnalises_PropostasEnviadasAnalise);
            
            var resumoAnalises_PropostasAlcadaAnalise = new Permissao()
            {
                Nome = "ResumoAnalises_PropostasAlcadaAnalise",
                Descricao = "Visualizar Quantidade de Propostas por Alçada de Análise",
                Processo = "Resumo de Análises",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAnalises_PropostasAlcadaAnalise"))) context.Permissao.AddOrUpdate(resumoAnalises_PropostasAlcadaAnalise);

            #endregion

            #region Resumo de Aprovações

            var resumoAprovacoes_Visualizar = new Permissao()
            {
                Nome = "ResumoAprovacoes_Visualizar",
                Descricao = "Visualizar Aba Resumo de Aprovações",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_Visualizar"))) context.Permissao.AddOrUpdate(resumoAprovacoes_Visualizar);

            var resumoAprovacoes_LeadtimeComite = new Permissao()
            {
                Nome = "ResumoAprovacoes_LeadtimeComite",
                Descricao = "Visualizar Leadtime Médio Geral em Comitê",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_LeadtimeComite"))) context.Permissao.AddOrUpdate(resumoAprovacoes_LeadtimeComite);

            var resumoAprovacoes_PropostasAguardandoAcaoAnalista = new Permissao()
            {
                Nome = "ResumoAprovacoes_PropostasAguardandoAcaoAnalista",
                Descricao = "Visualizar Quantidade de Propostas Aguardando Ação do Analista",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_PropostasAguardandoAcaoAnalista"))) context.Permissao.AddOrUpdate(resumoAprovacoes_PropostasAguardandoAcaoAnalista);

            var resumoAprovacoes_PropostasAprovadas = new Permissao()
            {
                Nome = "ResumoAprovacoes_PropostasAprovadas",
                Descricao = "Visualizar Quantidade de Propostas Aprovadas",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_PropostasAprovadas"))) context.Permissao.AddOrUpdate(resumoAprovacoes_PropostasAprovadas);

            var resumoAprovacoes_PropostasAprovadasPendencia = new Permissao()
            {
                Nome = "ResumoAprovacoes_PropostasAprovadasPendencia",
                Descricao = "Visualizar Quantidade de Propostas Aprovadas Com Pendência",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_PropostasAprovadasPendencia"))) context.Permissao.AddOrUpdate(resumoAprovacoes_PropostasAprovadasPendencia);

            var resumoAprovacoes_PropostasAprovadasPendenciaGarant = new Permissao()
            {
                Nome = "ResumoAprovacoes_PropostasAprovadasPendenciaGarant",
                Descricao = "Visualizar Quantidade de Propostas Aprovadas Com Pendência de Garantia",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_PropostasAprovadasPendenciaGarant"))) context.Permissao.AddOrUpdate(resumoAprovacoes_PropostasAprovadasPendenciaGarant);

            var resumoAprovacoes_PropostasEmAprovacao = new Permissao()
            {
                Nome = "ResumoAprovacoes_PropostasEmAprovacao",
                Descricao = "Visualizar Quantidade de Propostas em Aprovação",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_PropostasEmAprovacao"))) context.Permissao.AddOrUpdate(resumoAprovacoes_PropostasEmAprovacao);

            var resumoAprovacoes_PropostasAlcadaAprovacao = new Permissao()
            {
                Nome = "ResumoAprovacoes_PropostasAlcadaAprovacao",
                Descricao = "Visualizar Quantidade de Propostas por Alçada de Aprovação",
                Processo = "Resumo de Aprovações",
                Ativo = true,
            };
            if (!context.Permissao.Any(c => c.Nome.Equals("ResumoAprovacoes_PropostasAlcadaAprovacao"))) context.Permissao.AddOrUpdate(resumoAprovacoes_PropostasAlcadaAprovacao);

            #endregion

            #endregion

            #region Usuarios

            if (!context.Usuarios.Any(c => c.Login.Equals("Sistema")))
            {
                var usuarioReplicacao = new Usuario()
                {
                    ID = Guid.NewGuid(),
                    Nome = "Sistema",
                    Login = "sistema",
                    Email = "suporte@yara.com",
                    Ativo = true,
                    TipoAcesso = TipoAcesso.AD,
                    DataCriacao = DateTime.Now
                };

                context.Usuarios.AddOrUpdate(usuarioReplicacao);
            }
            #endregion

            #region "Processos"
            if (!context.Grupo.Any(c => c.Nome.Equals("Analise")))
            {
                context.Grupo.Add(new Grupo()
                {
                    ID = Guid.NewGuid(),
                    Nome = "Analise",
                    Ativo = true,
                    IsProcesso = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.Empty,


                });
            }

            if (!context.Grupo.Any(c => c.Nome.Equals("Prorrogação")))
            {
                context.Grupo.Add(new Grupo()
                {
                    ID = Guid.NewGuid(),
                    Nome = "Prorrogação",
                    Ativo = true,
                    IsProcesso = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.Empty,


                });
            }

            if (!context.Grupo.Any(c => c.Nome.Equals("Alçada comercial")))
            {
                context.Grupo.Add(new Grupo()
                {
                    ID = Guid.NewGuid(),
                    Nome = "Alçada comercial",
                    Ativo = true,
                    IsProcesso = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.Empty,


                });
            }

            if (!context.Grupo.Any(c => c.Nome.Equals("Abono")))
            {
                context.Grupo.Add(new Grupo()
                {
                    ID = Guid.NewGuid(),
                    Nome = "Abono",
                    Ativo = true,
                    IsProcesso = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.Empty,


                });
            }

            if (!context.Grupo.Any(c => c.Nome.Equals("Controle de Cobrança")))
            {
                context.Grupo.Add(new Grupo()
                {
                    ID = Guid.NewGuid(),
                    Nome = "Controle de Cobrança",
                    Ativo = true,
                    IsProcesso = true,
                    DataCriacao = DateTime.Now,
                    UsuarioIDCriacao = Guid.Empty,


                });
            }
            #endregion

            #region TipoCliente


            if (!context.TipoCliente.Any(c => c.Nome.Equals("ND")))
            {

                context.TipoCliente.AddOrUpdate(new TipoCliente()
                {
                    ID = Guid.Empty,
                    Nome = "Não Definido",
                    UsuarioIDCriacao = Guid.Empty,
                    Ativo = true,
                    DataCriacao = DateTime.Now
                });
            }

            #endregion

            #region Regiões
            if (!context.Regioes.Any(c => c.Nome.Equals("Sul"))) context.Regioes.AddOrUpdate(new Regiao(Guid.NewGuid(), "Sul"));
            if (!context.Regioes.Any(c => c.Nome.Equals("Nordeste"))) context.Regioes.AddOrUpdate(new Regiao(Guid.NewGuid(), "Nordeste"));
            if (!context.Regioes.Any(c => c.Nome.Equals("Sudeste"))) context.Regioes.AddOrUpdate(new Regiao(Guid.NewGuid(), "Sudeste"));
            if (!context.Regioes.Any(c => c.Nome.Equals("Norte"))) context.Regioes.AddOrUpdate(new Regiao(Guid.NewGuid(), "Norte"));
            if (!context.Regioes.Any(c => c.Nome.Equals("Centro-Oeste"))) context.Regioes.AddOrUpdate(new Regiao(Guid.NewGuid(), "Centro Oeste"));
            #endregion

            #region Status Ordem de Vendas

            var statusOrdem = new StatusOrdemVendas()
            {
                ID = Guid.NewGuid(),
                Status = "OB",
                Descricao = "Ordem de Venda Bloqueada",
                UsuarioIDCriacao = Guid.Empty,
                DataCriacao = DateTime.Now
            };
            if (!context.StatusOrdemVendas.Any(c => c.Status.Equals("OB"))) context.StatusOrdemVendas.AddOrUpdate(statusOrdem);

            var statusOrdem1 = new StatusOrdemVendas()
            {
                ID = Guid.NewGuid(),
                Status = "BM",
                Descricao = "Bloqueio Manual",
                UsuarioIDCriacao = Guid.Empty,
                DataCriacao = DateTime.Now
            };
            if (!context.StatusOrdemVendas.Any(c => c.Status.Equals("BM"))) context.StatusOrdemVendas.AddOrUpdate(statusOrdem1);

            var statusOrdem2 = new StatusOrdemVendas()
            {
                ID = Guid.NewGuid(),
                Status = "LM",
                Descricao = "Liberaçao Manual",
                UsuarioIDCriacao = Guid.Empty,
                DataCriacao = DateTime.Now
            };
            if (!context.StatusOrdemVendas.Any(c => c.Status.Equals("LM"))) context.StatusOrdemVendas.AddOrUpdate(statusOrdem2);

            var statusOrdem3 = new StatusOrdemVendas()
            {
                ID = Guid.NewGuid(),
                Status = "EA",
                Descricao = "Em Analise",
                UsuarioIDCriacao = Guid.Empty,
                DataCriacao = DateTime.Now
            };
            if (!context.StatusOrdemVendas.Any(c => c.Status.Equals("EA"))) context.StatusOrdemVendas.AddOrUpdate(statusOrdem3);

            var statusOrdem4 = new StatusOrdemVendas()
            {
                ID = Guid.NewGuid(),
                Status = "OL",
                Descricao = "Ordem de Venda Liberada",
                UsuarioIDCriacao = Guid.Empty,
                DataCriacao = DateTime.Now
            };
            if (!context.StatusOrdemVendas.Any(c => c.Status.Equals("OL"))) context.StatusOrdemVendas.AddOrUpdate(statusOrdem4);

            var statusOrdem5 = new StatusOrdemVendas()
            {
                ID = Guid.NewGuid(),
                Status = "OP",
                Descricao = "Ordem de Venda Pendente",
                UsuarioIDCriacao = Guid.Empty,
                DataCriacao = DateTime.Now
            };
            if (!context.StatusOrdemVendas.Any(c => c.Status.Equals("OP"))) context.StatusOrdemVendas.AddOrUpdate(statusOrdem5);


            #endregion

            #region Proposta Status

            var aprovado = new PropostaLCStatus()
            {
                ID = "AA",
                Nome = "Aprovado",
                Ativo = true,
                Ordem = 0
            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("AA"))) context.PropostaLCStatus.Add(aprovado);

            var aprovadoPendente = new PropostaLCStatus()
            {
                ID = "AP",
                Nome = "Aprovado por Outras Pendencia",
                Ativo = true,
                Ordem = 0
            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("AP"))) context.PropostaLCStatus.Add(aprovadoPendente);

            var aprovadoGarantia = new PropostaLCStatus()
            {
                ID = "AG",
                Nome = "Aprovado com Pendencia de Garantia",
                Ativo = true,
                Ordem = 0
            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("AG"))) context.PropostaLCStatus.Add(aprovadoGarantia);

            var criacao = new PropostaLCStatus()
            {
                ID = "XC",
                Nome = "Em Criação",
                Ativo = true,
                Ordem = 1
            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("XC"))) context.PropostaLCStatus.Add(criacao);
            
            var comCTC = new PropostaLCStatus()
            {
                ID = "CA",
                Nome = "Aguardando Parecer do CTC",
                Ativo = true,
                Ordem = 3

            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("CA"))) context.PropostaLCStatus.Add(comCTC);

            var enviadopreanalise = new PropostaLCStatus()
            {
                ID = "FA",
                Nome = "Enviado para Pré-Análise",
                Ativo = true,
                Ordem = 4

            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("FA"))) context.PropostaLCStatus.Add(enviadopreanalise);

            var empreanalise = new PropostaLCStatus()
            {
                ID = "FP",
                Nome = "Em Pré-Análise",
                Ativo = true,
                Ordem = 5

            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("FP"))) context.PropostaLCStatus.Add(empreanalise);

            var enviadoanalise = new PropostaLCStatus()
            {
                ID = "FE",
                Nome = "Enviado para Análise",
                Ativo = true,
                Ordem = 6

            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("FE"))) context.PropostaLCStatus.Add(enviadoanalise);
            
            var emanalise = new PropostaLCStatus()
            {
                ID = "FF",
                Nome = "Em Análise",
                Ativo = true,
                Ordem = 7

            };
            if (!context.PropostaLCStatus.Any(c => c.ID.Equals("FF"))) context.PropostaLCStatus.Add(emanalise);


            #endregion

            #region Empresa

            const string empresa = "Y";
            var yara = new Empresas()
            {
                ID = empresa,
                Nome = "Yara"
            };

            if (!context.Empresas.Any(c => c.ID.Equals("Y"))) context.Empresas.AddOrUpdate(yara);

            var galvani = new Empresas()
            {
                ID = "G",
                Nome = "Galvani"
            };

            if (!context.Empresas.Any(c => c.ID.Equals("G"))) context.Empresas.AddOrUpdate(galvani);

            var ambasEmpresas = new Empresas()
            {
                ID = "A",
                Nome = "Ambas"
            };

            if (!context.Empresas.Any(c => c.ID.Equals("A"))) context.Empresas.AddOrUpdate(ambasEmpresas);


            #endregion

            #region Classificação Grupo Economico

            var class01 = new ClassificacaoGrupoEconomico()
            {
                ID = 1,
                Nome = "LC Compartilhado"
            };

            if (!context.ClassificacaoGrupoEconomico.Any(c => c.Nome.Equals("LC Compartilhado"))) context.ClassificacaoGrupoEconomico.Add(class01);

            var class02 = new ClassificacaoGrupoEconomico()
            {
                ID = 2,
                Nome = "LC Individual"
            };

            if (!context.ClassificacaoGrupoEconomico.Any(c => c.Nome.Equals("LC Individual"))) context.ClassificacaoGrupoEconomico.Add(class02);

            //if (!context.ClassificacaoGrupoEconomico.Any(c => c.Nome.Equals("LC Compartilhado"))) context.ClassificacaoGrupoEconomico.Add(class01);

            //var class03 = new ClassificacaoGrupoEconomico()
            //{
            //    ID = 3,
            //    Nome = "Relações"
            //};

            //if (!context.ClassificacaoGrupoEconomico.Any(c => c.Nome.Equals("Relações"))) context.ClassificacaoGrupoEconomico.Add(class03);

            #endregion

            #region StatusGrupoEconomicoFluxo
            var statusgrupo01 = new StatusGrupoEconomicoFluxo()
            {
                ID = "PE",
                Nome = "Pendente para Exclusão",
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.StatusGrupoEconomicoFluxo.Any(c => c.ID.Equals("PE"))) context.StatusGrupoEconomicoFluxo.Add(statusgrupo01);

            var statusgrupo02 = new StatusGrupoEconomicoFluxo()
            {
                ID = "PI",
                Nome = "Pendente para Inclusão",
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.StatusGrupoEconomicoFluxo.Any(c => c.ID.Equals("PI"))) context.StatusGrupoEconomicoFluxo.Add(statusgrupo02);


            var statusgrupo03 = new StatusGrupoEconomicoFluxo()
            {
                ID = "AP",
                Nome = "Aprovado",
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.StatusGrupoEconomicoFluxo.Any(c => c.ID.Equals("AP"))) context.StatusGrupoEconomicoFluxo.Add(statusgrupo03);

            var statusgrupo04 = new StatusGrupoEconomicoFluxo()
            {
                ID = "RE",
                Nome = "Reprovado",
                DataCriacao = DateTime.Now,
                UsuarioIDCriacao = Guid.Empty
            };
            if (!context.StatusGrupoEconomicoFluxo.Any(c => c.ID.Equals("RE"))) context.StatusGrupoEconomicoFluxo.Add(statusgrupo04);



            #endregion

            #region StatusComite

            var EmAprovacao = new PropostaLCStatusComite()
            {
                ID = "AA",
                Ativo = true,
                Nome = "Aguardando aprovação"
            };
            if (!context.PropostaLCStatusComite.Any(c => c.ID.Equals("AA"))) context.PropostaLCStatusComite.AddOrUpdate(EmAprovacao);


            var Aprovado = new PropostaLCStatusComite()
            {
                ID = "AP",
                Ativo = true,
                Nome = "Aprovado"
            };
            if (!context.PropostaLCStatusComite.Any(c => c.ID.Equals("AP"))) context.PropostaLCStatusComite.AddOrUpdate(Aprovado);

            var Reprovado = new PropostaLCStatusComite()
            {
                ID = "RE",
                Ativo = true,
                Nome = "Reprovado"
            };
            if (!context.PropostaLCStatusComite.Any(c => c.ID.Equals("RE"))) context.PropostaLCStatusComite.AddOrUpdate(Reprovado);

            var ReprovadoEFinaliza = new PropostaLCStatusComite()
            {
                ID = "RF",
                Ativo = true,
                Nome = "Reprova e finaliza"
            };
            if (!context.PropostaLCStatusComite.Any(c => c.ID.Equals("RF"))) context.PropostaLCStatusComite.AddOrUpdate(ReprovadoEFinaliza);
            var Pendente = new PropostaLCStatusComite()
            {
                ID = "PE",
                Ativo = true,
                Nome = "Reprova e finaliza"
            };
            if (!context.PropostaLCStatusComite.Any(c => c.ID.Equals("PE"))) context.PropostaLCStatusComite.AddOrUpdate(Pendente);

            #endregion

            // TODO: Refazer parametros...

            #region Parametros Sistema

            var serasadias = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Chave = "serasa",
                Tipo = "dias",
                Grupo = "Serasa - Consulta válida em dias",
                Valor = "10"
            };
            if (!context.ParametroSistemas.Any(c => c.Chave.Equals("serasa") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(serasadias);

            var serasadiasg = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Chave = "serasa",
                Tipo = "dias",
                Grupo = "Serasa - Consulta válida em dias",
                Valor = "10"
            };
            if (!context.ParametroSistemas.Any(c => c.Chave.Equals("serasa") && c.EmpresasID == "G")) context.ParametroSistemas.Add(serasadiasg);

            // Controle de Cobrança - Dias
            var cobrancaDiasP = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "cobranca",
                Chave = "cdias",
                Grupo = "Controle de Cobrança",
                Valor = "10"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("cobranca") && c.Chave.Equals("cdias") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(cobrancaDiasP);

            var cobrancaDiasPG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "cobranca",
                Chave = "cdias",
                Grupo = "Controle de Cobrança",
                Valor = "10"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("cobranca") && c.Chave.Equals("cdias") && c.EmpresasID == "G")) context.ParametroSistemas.Add(cobrancaDiasPG);

            // Controle de Cobrança - Tipos de Titulos
            var cobrancaTiposP = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "cobranca",
                Chave = "ctipos",
                Grupo = "Controle de Cobrança",
                Valor = "RD"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("cobranca") && c.Chave.Equals("ctipos") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(cobrancaTiposP);

            var cobrancaTiposPG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "cobranca",
                Chave = "ctipos",
                Grupo = "Controle de Cobrança",
                Valor = "RD"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("cobranca") && c.Chave.Equals("ctipos") && c.EmpresasID == "G")) context.ParametroSistemas.Add(cobrancaTiposPG);

            // Controle de Cobrança - Prorogacao
            var prorrogDiasY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "prorrogacao",
                Chave = "diasmaximo",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "90"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("diasmaximo") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(prorrogDiasY);

            var prorrogDiasG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "prorrogacao",
                Chave = "diasmaximo",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "90"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("diasmaximo") && c.EmpresasID == "G")) context.ParametroSistemas.Add(prorrogDiasG);

            var prorrogJurBrlY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "prorrogacao",
                Chave = "jurospadraobrl",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "2.2"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("jurospadraobrl") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(prorrogJurBrlY);

            var prorrogJurBrlG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "prorrogacao",
                Chave = "jurospadraobrl",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "2.2"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("jurospadraobrl") && c.EmpresasID == "G")) context.ParametroSistemas.Add(prorrogJurBrlG);

            var prorrogJurUsdY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "prorrogacao",
                Chave = "jurospadraousd",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "2.5"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("jurospadraousd") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(prorrogJurUsdY);

            var prorrogJurUsdG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "prorrogacao",
                Chave = "jurospadraousd",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "2.5"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("jurospadraousd") && c.EmpresasID == "G")) context.ParametroSistemas.Add(prorrogJurUsdG);

            var prorrogJurMinY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "prorrogacao",
                Chave = "jurosmin",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "1"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("jurosmin") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(prorrogJurMinY);

            var prorrogJurMinG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "prorrogacao",
                Chave = "jurosmin",
                Grupo = "Controle de Cobrança - Prorrogação",
                Valor = "1"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("prorrogacao") && c.Chave.Equals("jurosmin") && c.EmpresasID == "G")) context.ParametroSistemas.Add(prorrogJurMinG);

            var abonoContaY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "abono",
                Chave = "contasap",
                Grupo = "Controle de Cobrança - Abono",
                Valor = "8031002"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("abono") && c.Chave.Equals("contasap") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(abonoContaY);

            var abonoContaG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "abono",
                Chave = "contasap",
                Grupo = "Controle de Cobrança - Abono",
                Valor = "8031002"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("abono") && c.Chave.Equals("contasap") && c.EmpresasID == "G")) context.ParametroSistemas.Add(abonoContaG);

            //Proposta de Alçada Comercial
            var tituloAbertoY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "alcada",
                Chave = "tituloAbertoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "8"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("tituloAbertoAlcada") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(tituloAbertoY);

            var tituloAbertoG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "alcada",
                Chave = "tituloAbertoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "8"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("tituloAbertoAlcada") && c.EmpresasID == "G")) context.ParametroSistemas.Add(tituloAbertoG);
            
            var historicoPrazoY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "alcada",
                Chave = "historicoPrazoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "15"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("historicoPrazoAlcada") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(historicoPrazoY);

            var historicoPrazoG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "alcada",
                Chave = "historicoPrazoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "15"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("historicoPrazoAlcada") && c.EmpresasID == "G")) context.ParametroSistemas.Add(historicoPrazoG);

            var historicoPesoY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "alcada",
                Chave = "historicoPesoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "5"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("historicoPesoAlcada") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(historicoPesoY);

            var historicoPesoG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "alcada",
                Chave = "historicoPesoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "5"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("historicoPesoAlcada") && c.EmpresasID == "G")) context.ParametroSistemas.Add(historicoPesoG);

            var conceitoAlcadaY = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "Y",
                Tipo = "alcada",
                Chave = "conceitoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "J"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("conceitoAlcada") && c.EmpresasID == "Y")) context.ParametroSistemas.Add(conceitoAlcadaY);

            var conceitoAlcadaG = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = "G",
                Tipo = "alcada",
                Chave = "conceitoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = "J"
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("conceitoAlcada") && c.EmpresasID == "G")) context.ParametroSistemas.Add(conceitoAlcadaG);

            var gruposExportacao = new Domain.Entities.ParametroSistema()
            {
                ID = Guid.NewGuid(),
                Ativo = true,
                UsuarioIDCriacao = new Guid("00000000-0000-0000-0000-000000000000"),
                DataCriacao = DateTime.Now,
                EmpresasID = null,
                Tipo = "alcada",
                Chave = "conceitoAlcada",
                Grupo = "Proposta Alçada Comercial - Crédito",
                Valor = ""
            };
            if (!context.ParametroSistemas.Any(c => c.Tipo.Equals("alcada") && c.Chave.Equals("conceitoAlcada") && c.EmpresasID == "G")) context.ParametroSistemas.Add(conceitoAlcadaG);

            #endregion

            context.SaveChanges();
        }
    }
}
