namespace Yara.Data.Tests
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mudancas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnexoArquivo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        AnexoID = c.Guid(nullable: false),
                        Arquivo = c.Binary(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ExtensaoArquivo = c.String(nullable: false, maxLength: 6, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Anexo", t => t.AnexoID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.AnexoID);
            
            CreateTable(
                "dbo.Anexo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Obrigatorio = c.Boolean(nullable: false),
                        LayoutsProposta = c.String(maxLength: 255, unicode: false),
                        CategoriaDocumento = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLC",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NumeroInternoProposta = c.Int(nullable: false),
                        EmpresaID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        ContaClienteID = c.Guid(nullable: false),
                        TipoClienteID = c.Guid(),
                        ExperienciaID = c.Guid(),
                        EstadoCivil = c.String(maxLength: 50, unicode: false),
                        CPFConjugue = c.String(maxLength: 14, unicode: false),
                        NomeConjugue = c.String(maxLength: 128, unicode: false),
                        RegimeCasamento = c.String(maxLength: 50, unicode: false),
                        PossuiGerenteTecnico = c.Boolean(),
                        PossuiMaquinarioProprio = c.Boolean(),
                        PossuiArmazem = c.Boolean(),
                        ToneladasArmazem = c.Decimal(precision: 18, scale: 2),
                        PossuiAreaIrrigada = c.Boolean(),
                        AreaIrrigadaID = c.Guid(),
                        PossuiContratoProximaSafra = c.Boolean(),
                        Trading = c.String(maxLength: 128, unicode: false),
                        QtdeSacas = c.Decimal(precision: 18, scale: 2),
                        PrecoSaca = c.Decimal(precision: 18, scale: 2),
                        ClienteGarantia = c.Boolean(),
                        PossuiCriacaoDeAnimais = c.Boolean(),
                        PossuiOutrasReceitas = c.Boolean(),
                        PrincipaisFornecedores = c.String(maxLength: 256, unicode: false),
                        PrincipaisClientes = c.String(maxLength: 256, unicode: false),
                        ComentarioMercado = c.String(maxLength: 256, unicode: false),
                        NecessidadeAnualFertilizantes = c.Decimal(precision: 18, scale: 2),
                        PrecoMedioFt = c.Decimal(precision: 18, scale: 2),
                        NecessidadeAnualFoliar = c.Decimal(precision: 18, scale: 2),
                        PrecoMedioLt = c.Decimal(precision: 18, scale: 2),
                        NumeroComprasAno = c.Int(),
                        ResultadoUltimaSafra = c.Decimal(precision: 18, scale: 2),
                        NumeroClienteCooperados = c.Int(),
                        PrazoMedioVendas = c.Int(),
                        IdadeMediaCanavialID = c.Guid(),
                        TotalProducaoAlcool = c.Decimal(precision: 18, scale: 2),
                        TotalProducaoAcucar = c.Decimal(precision: 18, scale: 2),
                        VolumeMoagemPropria = c.Decimal(precision: 18, scale: 2),
                        CustoMedioProducao = c.Decimal(precision: 18, scale: 2),
                        CapacidadeMoagem = c.Decimal(precision: 18, scale: 2),
                        TotalMWAno = c.Decimal(precision: 18, scale: 2),
                        CustoCarregamentoTransporte = c.Decimal(precision: 18, scale: 2),
                        RestricaoSERASA = c.String(maxLength: 20, unicode: false),
                        RestricaoTJ = c.String(maxLength: 20, unicode: false),
                        RestricaoIBAMA = c.String(maxLength: 20, unicode: false),
                        RestricaoTrabalhoEscravo = c.String(maxLength: 20, unicode: false),
                        LCProposto = c.Decimal(precision: 18, scale: 2),
                        SharePretentido = c.Decimal(precision: 18, scale: 2),
                        PrazoEmDias = c.Int(),
                        FonteRecursosCarteira = c.String(maxLength: 128, unicode: false),
                        ParecerRepresentante = c.String(unicode: false, storeType: "text"),
                        ParecerCTC = c.String(unicode: false, storeType: "text"),
                        ComentarioPatrimonio = c.String(unicode: false, storeType: "text"),
                        PropostaLCStatusID = c.String(maxLength: 2, unicode: false),
                        ResponsavelID = c.Guid(),
                        CodigoSap = c.String(maxLength: 10, unicode: false),
                        PropostaLCDemonstrativoID = c.Guid(),
                        SolicitanteSerasaID = c.Guid(),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AreaIrrigada", t => t.AreaIrrigadaID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.TipoCliente", t => t.TipoClienteID)
                .ForeignKey("dbo.Experiencia", t => t.ExperienciaID)
                .ForeignKey("dbo.IdadeMediaCanavial", t => t.IdadeMediaCanavialID)
                .ForeignKey("dbo.PropostaLCStatus", t => t.PropostaLCStatusID)
                .ForeignKey("dbo.Usuario", t => t.ResponsavelID)
                .ForeignKey("dbo.SolicitanteSerasa", t => t.SolicitanteSerasaID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.TipoClienteID)
                .Index(t => t.ExperienciaID)
                .Index(t => t.AreaIrrigadaID)
                .Index(t => t.IdadeMediaCanavialID)
                .Index(t => t.PropostaLCStatusID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.SolicitanteSerasaID);
            
            CreateTable(
                "dbo.AreaIrrigada",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCBemRural",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        GarantiaID = c.Guid(),
                        IR = c.String(maxLength: 50, unicode: false),
                        CidadeID = c.Guid(),
                        AreaTotalHa = c.Decimal(precision: 18, scale: 2),
                        Benfeitorias = c.Decimal(precision: 18, scale: 2),
                        Onus = c.Decimal(precision: 18, scale: 2),
                        PropostaLCGarantia_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCGarantia", t => t.PropostaLCGarantia_ID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CidadeID)
                .Index(t => t.PropostaLCGarantia_ID);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        EstadoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .Index(t => t.EstadoID);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Sigla = c.String(maxLength: 120, unicode: false),
                        RegiaoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Regiao", t => t.RegiaoID)
                .Index(t => t.RegiaoID);
            
            CreateTable(
                "dbo.Regiao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 240, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCPrecoPorRegiao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        CidadeID = c.Guid(),
                        ValorHaCultivavel = c.Decimal(precision: 18, scale: 2),
                        ValorHaNaoCultivavel = c.Decimal(precision: 18, scale: 2),
                        ModuloRural = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CidadeID);
            
            CreateTable(
                "dbo.PropostaLCGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        TipoGarantiaID = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        GarantiaRecebida = c.Boolean(nullable: false),
                        Documento = c.String(maxLength: 20, unicode: false),
                        Nome = c.String(maxLength: 128, unicode: false),
                        Comentario = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoGarantia", t => t.TipoGarantiaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoGarantiaID);
            
            CreateTable(
                "dbo.TipoGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCBemUrbano",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        GarantiaID = c.Guid(),
                        IR = c.String(maxLength: 50, unicode: false),
                        Descricao = c.String(maxLength: 128, unicode: false),
                        AreaTotal = c.Decimal(precision: 18, scale: 2),
                        ValorComBenfeitorias = c.Decimal(precision: 18, scale: 2),
                        Onus = c.Decimal(precision: 18, scale: 2),
                        ValorAvaliado = c.Decimal(precision: 18, scale: 2),
                        PropostaLCGarantia_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCGarantia", t => t.PropostaLCGarantia_ID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.PropostaLCGarantia_ID);
            
            CreateTable(
                "dbo.ContaCliente",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Documento = c.String(nullable: false, maxLength: 24, unicode: false),
                        CodigoPrincipal = c.String(maxLength: 10, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Apelido = c.String(maxLength: 120, unicode: false),
                        SegmentoID = c.Guid(),
                        TipoClienteID = c.Guid(),
                        DataNascimentoFundacao = c.DateTime(),
                        Contato = c.String(maxLength: 120, unicode: false),
                        CEP = c.String(maxLength: 120, unicode: false),
                        Endereco = c.String(maxLength: 120, unicode: false),
                        Numero = c.String(maxLength: 64, unicode: false),
                        Complemento = c.String(maxLength: 64, unicode: false),
                        Bairro = c.String(maxLength: 120, unicode: false),
                        Cidade = c.String(maxLength: 120, unicode: false),
                        UF = c.String(maxLength: 120, unicode: false),
                        Pais = c.String(maxLength: 120, unicode: false),
                        Telefone = c.String(maxLength: 120, unicode: false),
                        Email = c.String(maxLength: 120, unicode: false),
                        BloqueioManual = c.Boolean(),
                        LiberacaoManual = c.Boolean(),
                        AdiantamentoLC = c.Boolean(),
                        RestricaoSerasa = c.Boolean(),
                        PendenciaSerasa = c.Int(nullable: false),
                        SolicitanteSerasaID = c.Guid(nullable: false),
                        TOP10 = c.Boolean(),
                        ClientePremium = c.Boolean(),
                        Simplificado = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Segmento", t => t.SegmentoID)
                .ForeignKey("dbo.TipoCliente", t => t.TipoClienteID)
                .Index(t => t.SegmentoID)
                .Index(t => t.TipoClienteID);
            
            CreateTable(
                "dbo.ContaCliente_EstruturaComercial",
                c => new
                    {
                        ContaClienteId = c.Guid(nullable: false),
                        EstruturaComercialId = c.String(nullable: false, maxLength: 10, unicode: false),
                        EmpresasId = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteId, t.EstruturaComercialId, t.EmpresasId })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteId)
                .ForeignKey("dbo.Empresa", t => t.EmpresasId)
                .ForeignKey("dbo.EstruturaComercial", t => t.EstruturaComercialId)
                .Index(t => t.ContaClienteId)
                .Index(t => t.EstruturaComercialId)
                .Index(t => t.EmpresasId);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EstruturaComercial",
                c => new
                    {
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        EstruturaComercialPapelID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Superior_CodigoSap = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.CodigoSap)
                .ForeignKey("dbo.EstruturaComercialPapel", t => t.EstruturaComercialPapelID)
                .ForeignKey("dbo.EstruturaComercial", t => t.Superior_CodigoSap)
                .Index(t => t.EstruturaComercialPapelID)
                .Index(t => t.Superior_CodigoSap);
            
            CreateTable(
                "dbo.EstruturaComercialPapel",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Papel = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Login = c.String(nullable: false, maxLength: 120, unicode: false),
                        TipoAcesso = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        EmpresaLogada = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        EmpresasID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .Index(t => t.EmpresasID);
            
            CreateTable(
                "dbo.Grupo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        IsProcesso = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Representante",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContaCliente_Representante",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        RepresentanteID = c.Guid(nullable: false),
                        EmpresasID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        CodigoSapCTC = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.RepresentanteID, t.EmpresasID })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .ForeignKey("dbo.Representante", t => t.RepresentanteID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.RepresentanteID)
                .Index(t => t.EmpresasID);
            
            CreateTable(
                "dbo.ContaClienteFinanceiro",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        EmpresasID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        LC = c.Decimal(precision: 18, scale: 2),
                        LCAdicional = c.Decimal(precision: 18, scale: 2),
                        Exposicao = c.Decimal(precision: 18, scale: 2),
                        Vigencia = c.DateTime(),
                        VigenciaFim = c.DateTime(),
                        VigenciaAdicional = c.DateTime(),
                        VigenciaAdicionalFim = c.DateTime(),
                        Rating = c.String(maxLength: 120, unicode: false),
                        ConceitoCobrancaID = c.Guid(),
                        Recebiveis = c.Decimal(precision: 18, scale: 2),
                        OperacaoFinanciamento = c.Decimal(precision: 18, scale: 2),
                        Conceito = c.Boolean(nullable: false),
                        DescricaoConceito = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.EmpresasID })
                .ForeignKey("dbo.ConceitoCobranca", t => t.ConceitoCobrancaID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.EmpresasID)
                .Index(t => t.ConceitoCobrancaID);
            
            CreateTable(
                "dbo.ConceitoCobranca",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 2, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Nome, unique: true, name: "Index");
            
            CreateTable(
                "dbo.Segmento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipoCliente",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        LayoutProposta = c.Int(nullable: false),
                        TipoSerasa = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCPecuaria",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        TipoPecuariaID = c.Guid(),
                        AnoPecuaria = c.Int(),
                        Quantidade = c.Decimal(precision: 18, scale: 2),
                        Preco = c.Decimal(precision: 18, scale: 2),
                        Despesa = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoPecuaria", t => t.TipoPecuariaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoPecuariaID);
            
            CreateTable(
                "dbo.TipoPecuaria",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 120, unicode: false),
                        UnidadeMedida = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCCultura",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        CulturaID = c.Guid(),
                        CidadeID = c.Guid(),
                        Area = c.Decimal(precision: 18, scale: 2),
                        Arrendamento = c.Decimal(precision: 18, scale: 2),
                        ProdutividadeMedia = c.Decimal(precision: 18, scale: 2),
                        Preco = c.Decimal(precision: 18, scale: 2),
                        CustoHa = c.Decimal(precision: 18, scale: 2),
                        ConsumoFertilizante = c.Decimal(precision: 18, scale: 2),
                        PrecoMedioFertilizante = c.Decimal(precision: 18, scale: 2),
                        MesPlantio = c.Int(),
                        MesColheita = c.Int(),
                        Quebra = c.Decimal(precision: 18, scale: 2),
                        CustoArrendamentoSacaHa = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .ForeignKey("dbo.Cultura", t => t.CulturaID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CulturaID)
                .Index(t => t.CidadeID);
            
            CreateTable(
                "dbo.Cultura",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        UnidadeMedida = c.String(nullable: false, maxLength: 10, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCMercado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        CulturaID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cultura", t => t.CulturaID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.CulturaID);
            
            CreateTable(
                "dbo.Experiencia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IdadeMediaCanavial",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCMaquinaEquipamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        GarantiaID = c.Guid(),
                        Descricao = c.String(maxLength: 128, unicode: false),
                        Ano = c.Int(),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        PropostaLCGarantia_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCGarantia", t => t.PropostaLCGarantia_ID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.PropostaLCGarantia_ID);
            
            CreateTable(
                "dbo.PropostaLCNecessidadeProduto",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        ProdutoServicoID = c.Guid(),
                        Quantidade = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProdutoServico", t => t.ProdutoServicoID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.ProdutoServicoID);
            
            CreateTable(
                "dbo.ProdutoServico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCOutraReceita",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        ReceitaID = c.Guid(),
                        AnoOutrasReceitas = c.Int(),
                        ReceitaPrevista = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.Receita", t => t.ReceitaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.ReceitaID);
            
            CreateTable(
                "dbo.Receita",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCParceriaAgricola",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        Documento = c.String(maxLength: 20, unicode: false),
                        InscricaoEstadual = c.String(maxLength: 40, unicode: false),
                        Nome = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID);
            
            CreateTable(
                "dbo.PropostaLCStatus",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 2, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Ordem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCReferencia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        TipoReferencia = c.String(maxLength: 20, unicode: false),
                        TipoEmpresaID = c.Guid(),
                        NomeEmpresa = c.String(maxLength: 128, unicode: false),
                        NomeBanco = c.String(maxLength: 128, unicode: false),
                        Municipio = c.String(maxLength: 128, unicode: false),
                        Telefone = c.String(maxLength: 64, unicode: false),
                        NomeContato = c.String(maxLength: 128, unicode: false),
                        Desde = c.String(maxLength: 20, unicode: false),
                        LCAtual = c.Decimal(precision: 18, scale: 2),
                        Vigencia = c.DateTime(),
                        Garantias = c.String(maxLength: 128, unicode: false),
                        Comentarios = c.String(maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoEmpresa", t => t.TipoEmpresaID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoEmpresaID);
            
            CreateTable(
                "dbo.TipoEmpresa",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SolicitanteSerasa",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TipoClienteID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        TipoSerasa = c.Int(nullable: false),
                        Json = c.String(unicode: false, storeType: "text"),
                        Total = c.Decimal(precision: 18, scale: 2),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.TipoCliente", t => t.TipoClienteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.TipoClienteID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.PropostaLCTipoEndividamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        TipoEndividamentoID = c.Guid(),
                        CurtoPrazo = c.Decimal(precision: 18, scale: 2),
                        LongoPrazo = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoEndividamento", t => t.TipoEndividamentoID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoEndividamentoID);
            
            CreateTable(
                "dbo.TipoEndividamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BloqueioLiberacaoCarteira",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProcessamentoCarteiraID = c.Guid(nullable: false),
                        Divisao = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                        Numero = c.String(maxLength: 120, unicode: false),
                        EnviadoSAP = c.Boolean(nullable: false),
                        Bloqueada = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProcessamentoCarteira", t => t.ProcessamentoCarteiraID)
                .Index(t => t.ProcessamentoCarteiraID);
            
            CreateTable(
                "dbo.ProcessamentoCarteira",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EmpresaID = c.String(nullable: false, maxLength: 1, unicode: false),
                        Cliente = c.String(nullable: false, maxLength: 10, unicode: false),
                        DataHora = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Motivo = c.String(unicode: false),
                        Detalhes = c.String(unicode: false),
                        OrdemVenda = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        SolicitanteFluxoID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SolicitanteFluxo", t => t.SolicitanteFluxoID)
                .Index(t => t.SolicitanteFluxoID);
            
            CreateTable(
                "dbo.SolicitanteFluxo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClassificacaoGrupoEconomico",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContaClienteCodigo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        CodigoPrincipal = c.Boolean(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 10, unicode: false),
                        Documento = c.String(nullable: false, maxLength: 24, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .Index(t => t.ContaClienteID);
            
            CreateTable(
                "dbo.ContaClienteComentario",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 255, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.ContaClienteTelefone",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 24, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .Index(t => t.ContaClienteID);
            
            CreateTable(
                "dbo.CulturaEstado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EstadoID = c.Guid(nullable: false),
                        CulturaID = c.Guid(nullable: false),
                        ProdutividadeMedia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcentagemFertilizanteCusto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MediaFertilizante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cultura", t => t.CulturaID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .Index(t => t.EstadoID)
                .Index(t => t.CulturaID);
            
            CreateTable(
                "dbo.CustoHaRegiao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CidadeID = c.Guid(nullable: false),
                        ValorHaCultivavel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorHaNaoCultivavel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModuloRural = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .Index(t => t.CidadeID);
            
            CreateTable(
                "dbo.DivisaoRemessa",
                c => new
                    {
                        Divisao = c.Int(nullable: false),
                        ItemOrdemVendaItem = c.Int(nullable: false),
                        OrdemVendaNumero = c.String(nullable: false, maxLength: 10, unicode: false),
                        QtdPedida = c.Decimal(nullable: false, precision: 15, scale: 3),
                        QtdEntregue = c.Decimal(nullable: false, precision: 15, scale: 3),
                        UnidadeMedida = c.String(maxLength: 3, unicode: false),
                        DataRemessa = c.DateTime(),
                        DataOrganizacao = c.DateTime(),
                        DataPreparacao = c.DateTime(),
                        DataCarregamento = c.DateTime(),
                        DataSaida = c.DateTime(),
                        Status = c.String(maxLength: 2, unicode: false),
                        Motivo = c.String(maxLength: 3, unicode: false),
                        Bloqueio = c.String(maxLength: 2, unicode: false),
                        BloqueioPortal = c.String(maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => new { t.Divisao, t.ItemOrdemVendaItem, t.OrdemVendaNumero })
                .ForeignKey("dbo.OrdemVenda", t => t.OrdemVendaNumero)
                .ForeignKey("dbo.ItemOrdemVenda", t => new { t.ItemOrdemVendaItem, t.OrdemVendaNumero })
                .Index(t => new { t.ItemOrdemVendaItem, t.OrdemVendaNumero })
                .Index(t => t.OrdemVendaNumero);
            
            CreateTable(
                "dbo.ItemOrdemVenda",
                c => new
                    {
                        Item = c.Int(nullable: false),
                        OrdemVendaNumero = c.String(nullable: false, maxLength: 10, unicode: false),
                        Material = c.String(nullable: false, maxLength: 18, unicode: false),
                        Centro = c.String(maxLength: 4, unicode: false),
                        Deposito = c.String(maxLength: 4, unicode: false),
                        CondPagto = c.String(maxLength: 4, fixedLength: true, unicode: false),
                        CondFrete = c.String(maxLength: 3, fixedLength: true, unicode: false),
                        Moeda = c.String(maxLength: 4, unicode: false),
                        TaxaCambio = c.Decimal(precision: 9, scale: 5),
                        DataEfetivaFixa = c.DateTime(),
                        MotivoRecusa = c.String(maxLength: 2, unicode: false),
                        QtdPedida = c.Decimal(nullable: false, precision: 15, scale: 3),
                        QtdEntregue = c.Decimal(nullable: false, precision: 15, scale: 3),
                        UnidadeMedida = c.String(maxLength: 3, unicode: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 13, scale: 2),
                        PagaRetira = c.Boolean(nullable: false),
                        DescricaoMaterial = c.String(maxLength: 100, unicode: false),
                        OutrosBloqueios = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => new { t.Item, t.OrdemVendaNumero })
                .ForeignKey("dbo.OrdemVenda", t => t.OrdemVendaNumero)
                .Index(t => t.OrdemVendaNumero);
            
            CreateTable(
                "dbo.OrdemVenda",
                c => new
                    {
                        Numero = c.String(nullable: false, maxLength: 10, unicode: false),
                        Tipo = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        Canal = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        Setor = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        OrgVendas = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        CodigoCtc = c.String(maxLength: 3, fixedLength: true, unicode: false),
                        CodigoGc = c.String(maxLength: 4, fixedLength: true, unicode: false),
                        Emissor = c.String(nullable: false, maxLength: 10, unicode: false),
                        Pagador = c.String(maxLength: 10, unicode: false),
                        Representante = c.String(maxLength: 10, unicode: false),
                        CondPagto = c.String(maxLength: 4, fixedLength: true, unicode: false),
                        CondFrete = c.String(maxLength: 3, fixedLength: true, unicode: false),
                        PedidoCliente = c.String(maxLength: 20, unicode: false),
                        Moeda = c.String(maxLength: 5, unicode: false),
                        TaxaCambio = c.Decimal(precision: 9, scale: 5),
                        Cultura = c.String(maxLength: 3, unicode: false),
                        BloqueioRemessa = c.String(maxLength: 2, unicode: false),
                        BloqueioFaturamento = c.String(maxLength: 2, unicode: false),
                        DataEfetivaFixa = c.DateTime(),
                        DataEmissao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                        UltimaAtualizacao = c.DateTime(nullable: false),
                        UsuarioIdCriacao = c.Guid(nullable: false),
                        UsuarioIdAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        DataValidade = c.DateTime(),
                    })
                .PrimaryKey(t => t.Numero);
            
            CreateTable(
                "dbo.EstruturaPerfilUsuario",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        PerfilId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EstruturaComercial", t => t.CodigoSap)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.CodigoSap)
                .Index(t => t.PerfilId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Ordem = c.Int(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Feriado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DataFeriado = c.DateTime(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ferias",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        FeriasInicio = c.DateTime(nullable: false),
                        FeriasFim = c.DateTime(nullable: false),
                        SubstitutoID = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.SubstitutoID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID)
                .Index(t => t.SubstitutoID);
            
            CreateTable(
                "dbo.FluxoGrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        PerfilId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        ClassificacaoGrupoEconomicoId = c.Int(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassificacaoGrupoEconomico", t => t.ClassificacaoGrupoEconomicoId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.PerfilId)
                .Index(t => t.ClassificacaoGrupoEconomicoId);
            
            CreateTable(
                "dbo.FluxoLiberacaoLimiteCredito",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SegmentoID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrimeiroPerfilID = c.Guid(nullable: false),
                        SegundoPerfilID = c.Guid(),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PrimeiroPerfilID)
                .ForeignKey("dbo.Segmento", t => t.SegmentoID)
                .ForeignKey("dbo.Perfil", t => t.SegundoPerfilID)
                .Index(t => t.SegmentoID)
                .Index(t => t.PrimeiroPerfilID)
                .Index(t => t.SegundoPerfilID);
            
            CreateTable(
                "dbo.FluxoLiberacaoManual",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        SegmentoID = c.Guid(),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Usuario = c.Guid(),
                        Grupo = c.Guid(),
                        Estrutura = c.String(maxLength: 120, unicode: false),
                        Aprovador = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Segmento", t => t.SegmentoID)
                .Index(t => t.SegmentoID);
            
            CreateTable(
                "dbo.GrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CodigoGrupo = c.String(nullable: false, maxLength: 10, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Descricao = c.String(nullable: false, maxLength: 120, unicode: false),
                        TipoRelacaoGrupoEconomicoID = c.Guid(nullable: false),
                        StatusGrupoEconomicoFluxoID = c.String(nullable: false, maxLength: 3, unicode: false),
                        ClassificacaoGrupoEconomicoID = c.Int(nullable: false),
                        EmpresasID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassificacaoGrupoEconomico", t => t.ClassificacaoGrupoEconomicoID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .ForeignKey("dbo.StatusGrupoEconomicoFluxo", t => t.StatusGrupoEconomicoFluxoID)
                .ForeignKey("dbo.TipoRelacaoGrupoEconomico", t => t.TipoRelacaoGrupoEconomicoID)
                .Index(t => t.TipoRelacaoGrupoEconomicoID)
                .Index(t => t.StatusGrupoEconomicoFluxoID)
                .Index(t => t.ClassificacaoGrupoEconomicoID)
                .Index(t => t.EmpresasID);
            
            CreateTable(
                "dbo.StatusGrupoEconomicoFluxo",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 3, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipoRelacaoGrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        ClassificacaoGrupoEconomicoID = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassificacaoGrupoEconomico", t => t.ClassificacaoGrupoEconomicoID)
                .Index(t => t.ClassificacaoGrupoEconomicoID);
            
            CreateTable(
                "dbo.GrupoEconomicoMembro",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        GrupoEconomicoID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        StatusGrupoEconomicoFluxoID = c.String(nullable: false, maxLength: 3, unicode: false),
                        MembroPrincipal = c.Boolean(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.GrupoEconomicoID })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.GrupoEconomico", t => t.GrupoEconomicoID)
                .ForeignKey("dbo.StatusGrupoEconomicoFluxo", t => t.StatusGrupoEconomicoFluxoID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.GrupoEconomicoID)
                .Index(t => t.StatusGrupoEconomicoFluxoID);
            
            CreateTable(
                "dbo.HistoricoContaCliente",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        EmpresasID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Ano = c.Int(nullable: false),
                        Montante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantePrazo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontanteAVista = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiasAtraso = c.Int(nullable: false),
                        PesoAtraso = c.Single(nullable: false),
                        PRDias = c.Int(nullable: false),
                        PRPeso = c.Single(nullable: false),
                        REPRDias = c.Int(nullable: false),
                        REPRPeso = c.Single(nullable: false),
                        Pefin = c.Boolean(nullable: false),
                        OpFinanciamento = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.EmpresasID);
            
            CreateTable(
                "dbo.LiberacaoGrupoEconomicoFluxo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SolicitanteGrupoEconomicoID = c.Guid(nullable: false),
                        FluxoGrupoEconomicoID = c.Guid(nullable: false),
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        UsuarioID = c.Guid(),
                        StatusGrupoEconomicoFluxoID = c.String(nullable: false, maxLength: 3, unicode: false),
                        GrupoEconomicoID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EstruturaComercial", t => t.CodigoSap)
                .ForeignKey("dbo.FluxoGrupoEconomico", t => t.FluxoGrupoEconomicoID)
                .ForeignKey("dbo.GrupoEconomico", t => t.GrupoEconomicoID)
                .ForeignKey("dbo.SolicitanteGrupoEconomico", t => t.SolicitanteGrupoEconomicoID)
                .ForeignKey("dbo.StatusGrupoEconomicoFluxo", t => t.StatusGrupoEconomicoFluxoID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.SolicitanteGrupoEconomicoID)
                .Index(t => t.FluxoGrupoEconomicoID)
                .Index(t => t.CodigoSap)
                .Index(t => t.UsuarioID)
                .Index(t => t.StatusGrupoEconomicoFluxoID)
                .Index(t => t.GrupoEconomicoID);
            
            CreateTable(
                "dbo.SolicitanteGrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LogDivisaoRemessaLiberacao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProcessamentoCarteiraID = c.Guid(nullable: false),
                        OrdemDivisao = c.Int(nullable: false),
                        OrdemVendaNumero = c.String(nullable: false, maxLength: 10, unicode: false),
                        OrdemVendaItem = c.Int(nullable: false),
                        Restricao = c.String(unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProcessamentoCarteira", t => t.ProcessamentoCarteiraID)
                .Index(t => t.ProcessamentoCarteiraID);
            
            CreateTable(
                "dbo.LogLevel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        LogLevelID = c.Int(nullable: false),
                        Usuario = c.String(maxLength: 120, unicode: false),
                        UsuarioID = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 255, unicode: false),
                        Pagina = c.String(maxLength: 120, unicode: false),
                        Navegador = c.String(maxLength: 255, unicode: false),
                        IP = c.String(maxLength: 120, unicode: false),
                        IDTransacao = c.Guid(),
                        Idioma = c.String(maxLength: 120, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LogLevel", t => t.LogLevelID)
                .Index(t => t.LogLevelID);
            
            CreateTable(
                "dbo.ValorSaca",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrdemVendaFluxo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SolicitanteFluxoID = c.Guid(nullable: false),
                        FluxoLiberacaoManualID = c.Guid(nullable: false),
                        Divisao = c.Int(nullable: false),
                        ItemOrdemVenda = c.Int(nullable: false),
                        OrdemVendaNumero = c.String(nullable: false, maxLength: 120, unicode: false),
                        UsuarioID = c.Guid(),
                        Status = c.String(nullable: false, maxLength: 2, unicode: false),
                        EmpresaId = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FluxoLiberacaoManual", t => t.FluxoLiberacaoManualID)
                .ForeignKey("dbo.SolicitanteFluxo", t => t.SolicitanteFluxoID)
                .Index(t => t.SolicitanteFluxoID)
                .Index(t => t.FluxoLiberacaoManualID);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NomePtbr = c.String(nullable: false, maxLength: 120, unicode: false),
                        NomeEnus = c.String(nullable: false, maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ParametroSistema",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Grupo = c.String(nullable: false, maxLength: 120, unicode: false),
                        Tipo = c.String(nullable: false, maxLength: 120, unicode: false),
                        Chave = c.String(maxLength: 120, unicode: false),
                        Valor = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Ordem = c.Int(),
                        EmpresasID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .Index(t => t.EmpresasID);
            
            CreateTable(
                "dbo.PendenciaSerasa",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SolicitanteSerasaID = c.Guid(nullable: false),
                        Data = c.DateTime(),
                        Modalidade = c.String(maxLength: 120, unicode: false),
                        Quantidade = c.Int(),
                        Valor = c.Decimal(precision: 18, scale: 2),
                        Empresa = c.String(maxLength: 120, unicode: false),
                        CNPJ = c.String(maxLength: 120, unicode: false),
                        Falencia = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SolicitanteSerasa", t => t.SolicitanteSerasaID)
                .Index(t => t.SolicitanteSerasaID);
            
            CreateTable(
                "dbo.PorcentagemQuebra",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Porcentagem = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProdutividadeMedia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        RegiaoID = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Regiao", t => t.RegiaoID)
                .Index(t => t.RegiaoID);
            
            CreateTable(
                "dbo.PropostaLCDemonstrativo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NomeArquivo = c.String(maxLength: 255, unicode: false),
                        Html = c.String(unicode: false, storeType: "text"),
                        Tipo = c.String(maxLength: 2, unicode: false),
                        DataUpload = c.DateTime(nullable: false),
                        Conteudo = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaLCHistorico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        PropostaLCStatusID = c.String(nullable: false, maxLength: 2, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCStatus", t => t.PropostaLCStatusID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.PropostaLCStatusID);
            
            CreateTable(
                "dbo.StatusOrdemVendas",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Status = c.String(maxLength: 120, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Titulo",
                c => new
                    {
                        NumeroDocumento = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        Linha = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        AnoExercicio = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        Empresa = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        TipoDocumento = c.String(maxLength: 2, unicode: false),
                        DataEmissaoDocumento = c.DateTime(nullable: false),
                        CodigoRazao = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        CodigoCliente = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        OrdemVendaNumero = c.String(maxLength: 10, unicode: false),
                        OrdemVendaItem = c.Int(nullable: false),
                        NotaFiscal = c.String(maxLength: 20, unicode: false),
                        ValorInterno = c.Decimal(nullable: false, precision: 13, scale: 2),
                        ValorDocumento = c.Decimal(nullable: false, precision: 13, scale: 2),
                        DataVencimento = c.DateTime(nullable: false),
                        DataOriginal = c.DateTime(),
                        TextoDocumento = c.String(maxLength: 50, unicode: false),
                        InstrumentoPagamento = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        NumeroDocumentoCompensacao = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        MoedaInterna = c.String(maxLength: 5, unicode: false),
                        MoedaDocumento = c.String(maxLength: 5, unicode: false),
                        CreditoDebito = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        CobrancaAutomatica = c.String(nullable: false, maxLength: 5, unicode: false),
                        Aberto = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.NumeroDocumento, t.Linha, t.AnoExercicio });
            
            CreateTable(
                "dbo.Usuario_EstruturaComercial",
                c => new
                    {
                        UsuarioID = c.Guid(nullable: false),
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.CodigoSap })
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .ForeignKey("dbo.EstruturaComercial", t => t.CodigoSap)
                .Index(t => t.UsuarioID)
                .Index(t => t.CodigoSap);
            
            CreateTable(
                "dbo.Grupo_Permissao",
                c => new
                    {
                        GrupoID = c.Guid(nullable: false),
                        PermissaoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrupoID, t.PermissaoID })
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .ForeignKey("dbo.Permissao", t => t.PermissaoID)
                .Index(t => t.GrupoID)
                .Index(t => t.PermissaoID);
            
            CreateTable(
                "dbo.Usuario_Grupo",
                c => new
                    {
                        UsuarioID = c.Guid(nullable: false),
                        GrupoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.GrupoID })
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .Index(t => t.UsuarioID)
                .Index(t => t.GrupoID);
            
            CreateTable(
                "dbo.Usuario_Representante",
                c => new
                    {
                        UsuarioID = c.Guid(nullable: false),
                        RepresentanteID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.RepresentanteID })
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .ForeignKey("dbo.Representante", t => t.RepresentanteID)
                .Index(t => t.UsuarioID)
                .Index(t => t.RepresentanteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCHistorico", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCHistorico", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropForeignKey("dbo.PropostaLCHistorico", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.ProdutividadeMedia", "RegiaoID", "dbo.Regiao");
            DropForeignKey("dbo.PendenciaSerasa", "SolicitanteSerasaID", "dbo.SolicitanteSerasa");
            DropForeignKey("dbo.ParametroSistema", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.OrdemVendaFluxo", "SolicitanteFluxoID", "dbo.SolicitanteFluxo");
            DropForeignKey("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID", "dbo.FluxoLiberacaoManual");
            DropForeignKey("dbo.Log", "LogLevelID", "dbo.LogLevel");
            DropForeignKey("dbo.LogDivisaoRemessaLiberacao", "ProcessamentoCarteiraID", "dbo.ProcessamentoCarteira");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "StatusGrupoEconomicoFluxoID", "dbo.StatusGrupoEconomicoFluxo");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "SolicitanteGrupoEconomicoID", "dbo.SolicitanteGrupoEconomico");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "GrupoEconomicoID", "dbo.GrupoEconomico");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "FluxoGrupoEconomicoID", "dbo.FluxoGrupoEconomico");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "CodigoSap", "dbo.EstruturaComercial");
            DropForeignKey("dbo.HistoricoContaCliente", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.HistoricoContaCliente", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.GrupoEconomicoMembro", "StatusGrupoEconomicoFluxoID", "dbo.StatusGrupoEconomicoFluxo");
            DropForeignKey("dbo.GrupoEconomicoMembro", "GrupoEconomicoID", "dbo.GrupoEconomico");
            DropForeignKey("dbo.GrupoEconomicoMembro", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.GrupoEconomico", "TipoRelacaoGrupoEconomicoID", "dbo.TipoRelacaoGrupoEconomico");
            DropForeignKey("dbo.TipoRelacaoGrupoEconomico", "ClassificacaoGrupoEconomicoID", "dbo.ClassificacaoGrupoEconomico");
            DropForeignKey("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID", "dbo.StatusGrupoEconomicoFluxo");
            DropForeignKey("dbo.GrupoEconomico", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.GrupoEconomico", "ClassificacaoGrupoEconomicoID", "dbo.ClassificacaoGrupoEconomico");
            DropForeignKey("dbo.FluxoLiberacaoManual", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.FluxoLiberacaoLimiteCredito", "SegundoPerfilID", "dbo.Perfil");
            DropForeignKey("dbo.FluxoLiberacaoLimiteCredito", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.FluxoLiberacaoLimiteCredito", "PrimeiroPerfilID", "dbo.Perfil");
            DropForeignKey("dbo.FluxoGrupoEconomico", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.FluxoGrupoEconomico", "ClassificacaoGrupoEconomicoId", "dbo.ClassificacaoGrupoEconomico");
            DropForeignKey("dbo.Ferias", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Ferias", "SubstitutoID", "dbo.Usuario");
            DropForeignKey("dbo.EstruturaPerfilUsuario", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.EstruturaPerfilUsuario", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.EstruturaPerfilUsuario", "CodigoSap", "dbo.EstruturaComercial");
            DropForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" }, "dbo.ItemOrdemVenda");
            DropForeignKey("dbo.ItemOrdemVenda", "OrdemVendaNumero", "dbo.OrdemVenda");
            DropForeignKey("dbo.DivisaoRemessa", "OrdemVendaNumero", "dbo.OrdemVenda");
            DropForeignKey("dbo.CustoHaRegiao", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.CulturaEstado", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.CulturaEstado", "CulturaID", "dbo.Cultura");
            DropForeignKey("dbo.ContaClienteTelefone", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaClienteComentario", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.ContaClienteComentario", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaClienteCodigo", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.BloqueioLiberacaoCarteira", "ProcessamentoCarteiraID", "dbo.ProcessamentoCarteira");
            DropForeignKey("dbo.ProcessamentoCarteira", "SolicitanteFluxoID", "dbo.SolicitanteFluxo");
            DropForeignKey("dbo.AnexoArquivo", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCTipoEndividamento", "TipoEndividamentoID", "dbo.TipoEndividamento");
            DropForeignKey("dbo.PropostaLCTipoEndividamento", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLC", "SolicitanteSerasaID", "dbo.SolicitanteSerasa");
            DropForeignKey("dbo.SolicitanteSerasa", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.SolicitanteSerasa", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.SolicitanteSerasa", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.PropostaLC", "ResponsavelID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCReferencia", "TipoEmpresaID", "dbo.TipoEmpresa");
            DropForeignKey("dbo.PropostaLCReferencia", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropForeignKey("dbo.PropostaLCParceriaAgricola", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCOutraReceita", "ReceitaID", "dbo.Receita");
            DropForeignKey("dbo.PropostaLCOutraReceita", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCNecessidadeProduto", "ProdutoServicoID", "dbo.ProdutoServico");
            DropForeignKey("dbo.PropostaLCMaquinaEquipamento", "PropostaLCGarantia_ID", "dbo.PropostaLCGarantia");
            DropForeignKey("dbo.PropostaLCMaquinaEquipamento", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLC", "IdadeMediaCanavialID", "dbo.IdadeMediaCanavial");
            DropForeignKey("dbo.PropostaLC", "ExperienciaID", "dbo.Experiencia");
            DropForeignKey("dbo.PropostaLCCultura", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCMercado", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCMercado", "CulturaID", "dbo.Cultura");
            DropForeignKey("dbo.PropostaLCCultura", "CulturaID", "dbo.Cultura");
            DropForeignKey("dbo.PropostaLCCultura", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.PropostaLCPecuaria", "TipoPecuariaID", "dbo.TipoPecuaria");
            DropForeignKey("dbo.PropostaLCPecuaria", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.ContaCliente", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.PropostaLC", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.ContaCliente", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.PropostaLC", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaClienteFinanceiro", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.ContaClienteFinanceiro", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaClienteFinanceiro", "ConceitoCobrancaID", "dbo.ConceitoCobranca");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "EstruturaComercialId", "dbo.EstruturaComercial");
            DropForeignKey("dbo.Usuario_Representante", "RepresentanteID", "dbo.Representante");
            DropForeignKey("dbo.Usuario_Representante", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.ContaCliente_Representante", "RepresentanteID", "dbo.Representante");
            DropForeignKey("dbo.ContaCliente_Representante", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.ContaCliente_Representante", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.Usuario_Grupo", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.Usuario_Grupo", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Grupo_Permissao", "PermissaoID", "dbo.Permissao");
            DropForeignKey("dbo.Grupo_Permissao", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.Usuario_EstruturaComercial", "CodigoSap", "dbo.EstruturaComercial");
            DropForeignKey("dbo.Usuario_EstruturaComercial", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.EstruturaComercial", "Superior_CodigoSap", "dbo.EstruturaComercial");
            DropForeignKey("dbo.EstruturaComercial", "EstruturaComercialPapelID", "dbo.EstruturaComercialPapel");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "EmpresasId", "dbo.Empresa");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "ContaClienteId", "dbo.ContaCliente");
            DropForeignKey("dbo.PropostaLCBemUrbano", "PropostaLCGarantia_ID", "dbo.PropostaLCGarantia");
            DropForeignKey("dbo.PropostaLCBemUrbano", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCBemRural", "PropostaLCGarantia_ID", "dbo.PropostaLCGarantia");
            DropForeignKey("dbo.PropostaLCGarantia", "TipoGarantiaID", "dbo.TipoGarantia");
            DropForeignKey("dbo.PropostaLCGarantia", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCBemRural", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCPrecoPorRegiao", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCPrecoPorRegiao", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.PropostaLCBemRural", "CidadeID", "dbo.Cidade");
            DropForeignKey("dbo.Estado", "RegiaoID", "dbo.Regiao");
            DropForeignKey("dbo.Cidade", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.PropostaLC", "AreaIrrigadaID", "dbo.AreaIrrigada");
            DropForeignKey("dbo.AnexoArquivo", "AnexoID", "dbo.Anexo");
            DropIndex("dbo.Usuario_Representante", new[] { "RepresentanteID" });
            DropIndex("dbo.Usuario_Representante", new[] { "UsuarioID" });
            DropIndex("dbo.Usuario_Grupo", new[] { "GrupoID" });
            DropIndex("dbo.Usuario_Grupo", new[] { "UsuarioID" });
            DropIndex("dbo.Grupo_Permissao", new[] { "PermissaoID" });
            DropIndex("dbo.Grupo_Permissao", new[] { "GrupoID" });
            DropIndex("dbo.Usuario_EstruturaComercial", new[] { "CodigoSap" });
            DropIndex("dbo.Usuario_EstruturaComercial", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaLCHistorico", new[] { "PropostaLCStatusID" });
            DropIndex("dbo.PropostaLCHistorico", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCHistorico", new[] { "UsuarioID" });
            DropIndex("dbo.ProdutividadeMedia", new[] { "RegiaoID" });
            DropIndex("dbo.PendenciaSerasa", new[] { "SolicitanteSerasaID" });
            DropIndex("dbo.ParametroSistema", new[] { "EmpresasID" });
            DropIndex("dbo.OrdemVendaFluxo", new[] { "FluxoLiberacaoManualID" });
            DropIndex("dbo.OrdemVendaFluxo", new[] { "SolicitanteFluxoID" });
            DropIndex("dbo.Log", new[] { "LogLevelID" });
            DropIndex("dbo.LogDivisaoRemessaLiberacao", new[] { "ProcessamentoCarteiraID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "GrupoEconomicoID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "StatusGrupoEconomicoFluxoID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "UsuarioID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "CodigoSap" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "FluxoGrupoEconomicoID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "SolicitanteGrupoEconomicoID" });
            DropIndex("dbo.HistoricoContaCliente", new[] { "EmpresasID" });
            DropIndex("dbo.HistoricoContaCliente", new[] { "ContaClienteID" });
            DropIndex("dbo.GrupoEconomicoMembro", new[] { "StatusGrupoEconomicoFluxoID" });
            DropIndex("dbo.GrupoEconomicoMembro", new[] { "GrupoEconomicoID" });
            DropIndex("dbo.GrupoEconomicoMembro", new[] { "ContaClienteID" });
            DropIndex("dbo.TipoRelacaoGrupoEconomico", new[] { "ClassificacaoGrupoEconomicoID" });
            DropIndex("dbo.GrupoEconomico", new[] { "EmpresasID" });
            DropIndex("dbo.GrupoEconomico", new[] { "ClassificacaoGrupoEconomicoID" });
            DropIndex("dbo.GrupoEconomico", new[] { "StatusGrupoEconomicoFluxoID" });
            DropIndex("dbo.GrupoEconomico", new[] { "TipoRelacaoGrupoEconomicoID" });
            DropIndex("dbo.FluxoLiberacaoManual", new[] { "SegmentoID" });
            DropIndex("dbo.FluxoLiberacaoLimiteCredito", new[] { "SegundoPerfilID" });
            DropIndex("dbo.FluxoLiberacaoLimiteCredito", new[] { "PrimeiroPerfilID" });
            DropIndex("dbo.FluxoLiberacaoLimiteCredito", new[] { "SegmentoID" });
            DropIndex("dbo.FluxoGrupoEconomico", new[] { "ClassificacaoGrupoEconomicoId" });
            DropIndex("dbo.FluxoGrupoEconomico", new[] { "PerfilId" });
            DropIndex("dbo.Ferias", new[] { "SubstitutoID" });
            DropIndex("dbo.Ferias", new[] { "UsuarioID" });
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "UsuarioId" });
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "PerfilId" });
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "CodigoSap" });
            DropIndex("dbo.ItemOrdemVenda", new[] { "OrdemVendaNumero" });
            DropIndex("dbo.DivisaoRemessa", new[] { "OrdemVendaNumero" });
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" });
            DropIndex("dbo.CustoHaRegiao", new[] { "CidadeID" });
            DropIndex("dbo.CulturaEstado", new[] { "CulturaID" });
            DropIndex("dbo.CulturaEstado", new[] { "EstadoID" });
            DropIndex("dbo.ContaClienteTelefone", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaClienteComentario", new[] { "UsuarioID" });
            DropIndex("dbo.ContaClienteComentario", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaClienteCodigo", new[] { "ContaClienteID" });
            DropIndex("dbo.ProcessamentoCarteira", new[] { "SolicitanteFluxoID" });
            DropIndex("dbo.BloqueioLiberacaoCarteira", new[] { "ProcessamentoCarteiraID" });
            DropIndex("dbo.PropostaLCTipoEndividamento", new[] { "TipoEndividamentoID" });
            DropIndex("dbo.PropostaLCTipoEndividamento", new[] { "PropostaLCID" });
            DropIndex("dbo.SolicitanteSerasa", new[] { "UsuarioID" });
            DropIndex("dbo.SolicitanteSerasa", new[] { "ContaClienteID" });
            DropIndex("dbo.SolicitanteSerasa", new[] { "TipoClienteID" });
            DropIndex("dbo.PropostaLCReferencia", new[] { "TipoEmpresaID" });
            DropIndex("dbo.PropostaLCReferencia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCParceriaAgricola", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCOutraReceita", new[] { "ReceitaID" });
            DropIndex("dbo.PropostaLCOutraReceita", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCNecessidadeProduto", new[] { "ProdutoServicoID" });
            DropIndex("dbo.PropostaLCNecessidadeProduto", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCMaquinaEquipamento", new[] { "PropostaLCGarantia_ID" });
            DropIndex("dbo.PropostaLCMaquinaEquipamento", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCMercado", new[] { "CulturaID" });
            DropIndex("dbo.PropostaLCMercado", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "CidadeID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "CulturaID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPecuaria", new[] { "TipoPecuariaID" });
            DropIndex("dbo.PropostaLCPecuaria", new[] { "PropostaLCID" });
            DropIndex("dbo.ConceitoCobranca", "Index");
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ConceitoCobrancaID" });
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "EmpresasID" });
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaCliente_Representante", new[] { "EmpresasID" });
            DropIndex("dbo.ContaCliente_Representante", new[] { "RepresentanteID" });
            DropIndex("dbo.ContaCliente_Representante", new[] { "ContaClienteID" });
            DropIndex("dbo.Usuario", new[] { "EmpresasID" });
            DropIndex("dbo.EstruturaComercial", new[] { "Superior_CodigoSap" });
            DropIndex("dbo.EstruturaComercial", new[] { "EstruturaComercialPapelID" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "EmpresasId" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "EstruturaComercialId" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "ContaClienteId" });
            DropIndex("dbo.ContaCliente", new[] { "TipoClienteID" });
            DropIndex("dbo.ContaCliente", new[] { "SegmentoID" });
            DropIndex("dbo.PropostaLCBemUrbano", new[] { "PropostaLCGarantia_ID" });
            DropIndex("dbo.PropostaLCBemUrbano", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCGarantia", new[] { "TipoGarantiaID" });
            DropIndex("dbo.PropostaLCGarantia", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPrecoPorRegiao", new[] { "CidadeID" });
            DropIndex("dbo.PropostaLCPrecoPorRegiao", new[] { "PropostaLCID" });
            DropIndex("dbo.Estado", new[] { "RegiaoID" });
            DropIndex("dbo.Cidade", new[] { "EstadoID" });
            DropIndex("dbo.PropostaLCBemRural", new[] { "PropostaLCGarantia_ID" });
            DropIndex("dbo.PropostaLCBemRural", new[] { "CidadeID" });
            DropIndex("dbo.PropostaLCBemRural", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLC", new[] { "SolicitanteSerasaID" });
            DropIndex("dbo.PropostaLC", new[] { "ResponsavelID" });
            DropIndex("dbo.PropostaLC", new[] { "PropostaLCStatusID" });
            DropIndex("dbo.PropostaLC", new[] { "IdadeMediaCanavialID" });
            DropIndex("dbo.PropostaLC", new[] { "AreaIrrigadaID" });
            DropIndex("dbo.PropostaLC", new[] { "ExperienciaID" });
            DropIndex("dbo.PropostaLC", new[] { "TipoClienteID" });
            DropIndex("dbo.PropostaLC", new[] { "ContaClienteID" });
            DropIndex("dbo.AnexoArquivo", new[] { "AnexoID" });
            DropIndex("dbo.AnexoArquivo", new[] { "PropostaLCID" });
            DropTable("dbo.Usuario_Representante");
            DropTable("dbo.Usuario_Grupo");
            DropTable("dbo.Grupo_Permissao");
            DropTable("dbo.Usuario_EstruturaComercial");
            DropTable("dbo.Titulo");
            DropTable("dbo.StatusOrdemVendas");
            DropTable("dbo.PropostaLCHistorico");
            DropTable("dbo.PropostaLCDemonstrativo");
            DropTable("dbo.ProdutividadeMedia");
            DropTable("dbo.PorcentagemQuebra");
            DropTable("dbo.PendenciaSerasa");
            DropTable("dbo.ParametroSistema");
            DropTable("dbo.Pais");
            DropTable("dbo.OrdemVendaFluxo");
            DropTable("dbo.ValorSaca");
            DropTable("dbo.Log");
            DropTable("dbo.LogLevel");
            DropTable("dbo.LogDivisaoRemessaLiberacao");
            DropTable("dbo.SolicitanteGrupoEconomico");
            DropTable("dbo.LiberacaoGrupoEconomicoFluxo");
            DropTable("dbo.HistoricoContaCliente");
            DropTable("dbo.GrupoEconomicoMembro");
            DropTable("dbo.TipoRelacaoGrupoEconomico");
            DropTable("dbo.StatusGrupoEconomicoFluxo");
            DropTable("dbo.GrupoEconomico");
            DropTable("dbo.FluxoLiberacaoManual");
            DropTable("dbo.FluxoLiberacaoLimiteCredito");
            DropTable("dbo.FluxoGrupoEconomico");
            DropTable("dbo.Ferias");
            DropTable("dbo.Feriado");
            DropTable("dbo.Perfil");
            DropTable("dbo.EstruturaPerfilUsuario");
            DropTable("dbo.OrdemVenda");
            DropTable("dbo.ItemOrdemVenda");
            DropTable("dbo.DivisaoRemessa");
            DropTable("dbo.CustoHaRegiao");
            DropTable("dbo.CulturaEstado");
            DropTable("dbo.ContaClienteTelefone");
            DropTable("dbo.ContaClienteComentario");
            DropTable("dbo.ContaClienteCodigo");
            DropTable("dbo.ClassificacaoGrupoEconomico");
            DropTable("dbo.SolicitanteFluxo");
            DropTable("dbo.ProcessamentoCarteira");
            DropTable("dbo.BloqueioLiberacaoCarteira");
            DropTable("dbo.TipoEndividamento");
            DropTable("dbo.PropostaLCTipoEndividamento");
            DropTable("dbo.SolicitanteSerasa");
            DropTable("dbo.TipoEmpresa");
            DropTable("dbo.PropostaLCReferencia");
            DropTable("dbo.PropostaLCStatus");
            DropTable("dbo.PropostaLCParceriaAgricola");
            DropTable("dbo.Receita");
            DropTable("dbo.PropostaLCOutraReceita");
            DropTable("dbo.ProdutoServico");
            DropTable("dbo.PropostaLCNecessidadeProduto");
            DropTable("dbo.PropostaLCMaquinaEquipamento");
            DropTable("dbo.IdadeMediaCanavial");
            DropTable("dbo.Experiencia");
            DropTable("dbo.PropostaLCMercado");
            DropTable("dbo.Cultura");
            DropTable("dbo.PropostaLCCultura");
            DropTable("dbo.TipoPecuaria");
            DropTable("dbo.PropostaLCPecuaria");
            DropTable("dbo.TipoCliente");
            DropTable("dbo.Segmento");
            DropTable("dbo.ConceitoCobranca");
            DropTable("dbo.ContaClienteFinanceiro");
            DropTable("dbo.ContaCliente_Representante");
            DropTable("dbo.Representante");
            DropTable("dbo.Permissao");
            DropTable("dbo.Grupo");
            DropTable("dbo.Usuario");
            DropTable("dbo.EstruturaComercialPapel");
            DropTable("dbo.EstruturaComercial");
            DropTable("dbo.Empresa");
            DropTable("dbo.ContaCliente_EstruturaComercial");
            DropTable("dbo.ContaCliente");
            DropTable("dbo.PropostaLCBemUrbano");
            DropTable("dbo.TipoGarantia");
            DropTable("dbo.PropostaLCGarantia");
            DropTable("dbo.PropostaLCPrecoPorRegiao");
            DropTable("dbo.Regiao");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.PropostaLCBemRural");
            DropTable("dbo.AreaIrrigada");
            DropTable("dbo.PropostaLC");
            DropTable("dbo.Anexo");
            DropTable("dbo.AnexoArquivo");
        }
    }
}
