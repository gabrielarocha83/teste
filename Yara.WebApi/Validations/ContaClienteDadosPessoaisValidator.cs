using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class ContaClienteDadosPessoaisValidator : AbstractValidator<ContaClienteAlteracaoDadosPessoaisDto>
    {
        public ContaClienteDadosPessoaisValidator()
        {
            RuleFor(c => c.Endereco).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclienteEndereco);
            RuleFor(c => c.Endereco).MaximumLength(120).WithMessage(Resources.Resources.contaclienteNome120);

            RuleFor(c => c.Cidade).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclienteCidade);
            RuleFor(c => c.Cidade).MaximumLength(120).WithMessage(Resources.Resources.contaclienteCidade120);

            RuleFor(c => c.CEP).NotNull().NotEmpty().When(c => c.Pais == "BR").WithMessage(Resources.Resources.contaclienteCEP);
            RuleFor(c => c.CEP).MaximumLength(120).When(c => c.Pais == "BR").WithMessage(Resources.Resources.contaclienteCEP120);
            
            RuleFor(c => c.UF).MaximumLength(120).When(c => c.Pais == "BR").WithMessage(Resources.Resources.contaclienteUF120);
            RuleFor(c => c.UF).NotNull().NotEmpty().When(c => c.Pais == "BR").WithMessage(Resources.Resources.contaclineteUF);

            RuleFor(c => c.TipoClienteID).NotNull().NotEmpty().WithMessage(Resources.Resources.contaclienteTipoClienteID);

            RuleFor(c => c.SegmentoID).NotNull().NotEmpty().WithMessage(Resources.Resources.contaClienteSegmentId);
        }
    }
}