using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class LogFluxoAutomaticoValidator : AbstractValidator<BuscaLogFluxoAutomaticoDto>
    {
        public LogFluxoAutomaticoValidator()
        {
            //RuleFor(c => c.DataInicial)
            //    .NotEmpty()
            //    .WithMessage(Resources.Resources.DateBegin);

            //RuleFor(c => c.DataFinal)
            //    .NotEmpty().WithMessage(Resources.Resources.DateEnd)
            //    .GreaterThan(c => c.DataInicial.Value).WithMessage(Resources.Resources.DateBeginGreaterThanDateEnd)
            //    .When(c => c.DataInicial.HasValue);
        }
    }
}