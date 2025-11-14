using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class LiberacaoOrdemVendaFluxoValidator : AbstractValidator<LiberacaoOrdemVendaFluxoDto>
    {
        public LiberacaoOrdemVendaFluxoValidator()
        {
            RuleFor(c => c.Comentario).MaximumLength(500).WithMessage("Este comentário excedeu o máximo de caracteres permitidos. (max. 500 caracteres).");
        }
    }
}