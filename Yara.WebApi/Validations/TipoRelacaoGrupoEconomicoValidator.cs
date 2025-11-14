using FluentValidation;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

namespace Yara.WebApi.Validations
{
    public class TipoRelacaoGrupoEconomicoValidator : AbstractValidator<TipoRelacaoGrupoEconomicoDto>
    {
        public TipoRelacaoGrupoEconomicoValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldNamePattern);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            RuleFor(c => c.ClassificacaoGrupoEconomicoID).NotNull().WithMessage(Resources.Resources.typeclassification);
        }
    }
}