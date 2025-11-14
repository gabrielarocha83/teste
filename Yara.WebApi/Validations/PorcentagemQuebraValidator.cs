using FluentValidation;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class PorcentagemQuebraValidator : AbstractValidator<PorcentagemQuebraDto>
    {
        public PorcentagemQuebraValidator()
        {
            RuleFor(c => c.Porcentagem).NotNull().NotEmpty().WithMessage(Resources.Resources.porcentagemquebraPorcentagem);
            RuleFor(c => c.Porcentagem).GreaterThan(0).LessThanOrEqualTo(100).WithMessage(Resources.Resources.porcentagemquebraPorcentagemMaiorqueZeroeMenorouIguala100);
            //RuleFor(c => c.DataCriacao).NotNull().NotEmpty().WithMessage(Resources.Resources.dateRequired);
        }
    }
}