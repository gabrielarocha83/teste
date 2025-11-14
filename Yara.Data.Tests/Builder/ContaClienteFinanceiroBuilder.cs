using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ContaClienteFinanceiroBuilder
    {
        private Guid contaclienteID = new Guid();
        private bool clientepremium = true;
        private Guid conceitocobrancaID = Guid.NewGuid();
        private DateTime vigencia = DateTime.Now;
        private decimal exposicao = 0;
        private decimal lc = 0;
        private decimal lcdisponivel = 0;
        private decimal recebiveis = 0;
        private decimal operacaofinanciamento = 0;
        private string rating = string.Empty;
        private Guid _usuarioIDcriacao = Guid.NewGuid();
        private Guid? _usuarioIDalteracao = Guid.NewGuid();
        private DateTime _datacriacao = DateTime.Now;
        private DateTime? _dataalteracao = DateTime.Now;

        public ContaClienteFinanceiro Build()
        {
            var conta = new ContaClienteFinanceiro
            {
                ContaClienteID = contaclienteID,
                ConceitoCobrancaID = conceitocobrancaID,
                Vigencia = vigencia,
                Exposicao = exposicao,
                LC = lc,
                Recebiveis = recebiveis,
                Rating = rating,
                OperacaoFinanciamento = operacaofinanciamento,
                UsuarioIDAlteracao = _usuarioIDalteracao,
                DataCriacao = _datacriacao,
                DataAlteracao = _dataalteracao,
                UsuarioIDCriacao = _usuarioIDcriacao
            };


            return conta;
        }

        public ContaClienteFinanceiroBuilder WithContaClienteID(Guid id)
        {
            contaclienteID = id;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithConceitoCobrancaID(Guid id)
        {
            conceitocobrancaID = id;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithClientePremium(bool premium)
        {
            clientepremium = premium;
            return this;
        }

        public ContaClienteFinanceiroBuilder WitVigencia(DateTime Vigencia)
        {
            vigencia = Vigencia;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithExposicao(decimal Exposicao)
        {
            exposicao = Exposicao;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithLC(decimal LC)
        {
            lc = LC;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithLCDisponivel(decimal LCDisponivel)
        {
            lcdisponivel = LCDisponivel;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithRecebiveis(decimal Recebiveis)
        {
            recebiveis = Recebiveis;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithRating(string Rating)
        {
            rating = Rating;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithOperacaoFinanciamento(decimal OperacaoFinanciamento)
        {
            operacaofinanciamento = OperacaoFinanciamento;
            return this;
        }


        public ContaClienteFinanceiroBuilder WithDataCriacao(DateTime datacriacao)
        {
            _datacriacao = datacriacao;
            return this;
        }


        public ContaClienteFinanceiroBuilder WithUsuarioIDCriacao(Guid usuarioIDCriacao)
        {
            _usuarioIDcriacao = usuarioIDCriacao;
            return this;
        }

        public ContaClienteFinanceiroBuilder WithUsuarioIDAlteracao(Guid usuarioIDAlteracao)
        {
            _usuarioIDalteracao = usuarioIDAlteracao;
            return this;
        }

        public ContaClienteFinanceiroBuilder WitDataAlteracao(DateTime dataAlteracao)
        {
            _dataalteracao = dataAlteracao;
            return this;
        }

        public static implicit operator ContaClienteFinanceiro(ContaClienteFinanceiroBuilder instance)
        {
            return instance.Build();
        }
    }
}